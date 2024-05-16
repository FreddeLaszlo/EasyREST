using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Xml.Serialization;
using JsonSerializer = System.Text.Json.JsonSerializer;

namespace EasyREST
{
    /// <summary>
    /// Class to encapsulate a RESTful endpoint
    /// </summary>
    public class class_rest
    {

        public int ID { get; } = 0;
        public int PreviousNodeID { get; set; } = -1;
        //public bool IsSubRoot { get; set; } = false;
        /// <summary>
        /// Property <c>Noun</c> is the word used as the RESTful endpoint
        /// </summary>
        public string Noun { get; set; } = "";
        /// <summary>
        /// Property <c>Description</c> describes what the RESTful endpoint is for.
        /// </summary>
        public string Description { get; set; } = "";

        // GET items
        public bool UseGET { get; set; }  = true;
        public bool UseGETAuthorisation { get; set; } = false;
        public Dictionary<string, string> ExpectsGET { get; set; } = [];
        public Dictionary<string, string> ReturnsGET { get; set; } = [];
        public bool ReturnsArrayGET { get; set; } = false;
        public bool ReturnsCacheableGET { get; set; } = false;

        // POST items
        public bool UsePOST { get; set; } = false;
        public bool UsePOSTAuthorisation { get; set; } = true;
        public Dictionary<string, string> ExpectsPOST { get; set; } = [];
        public Dictionary<string, string> ReturnsPOST { get; set; } = [];
        public bool ReturnsArrayPOST { get; set; } = false;
        public bool ReturnsCacheablePOST { get; set; } = false;

        // PATCH items
        public bool UsePATCH { get; set; } = false;
        public bool UsePATCHAuthorisation { get; set; } = true;
        public Dictionary<string, string> ExpectsPATCH { get; set; } = [];
        public Dictionary<string, string> ReturnsPATCH { get; set; } = [];
        public bool ReturnsArrayPATCH { get; set; } = false;
        public bool ReturnsCacheablePATCH { get; set; } = false;

        // DELETE items
        public bool UseDELETE { get; set; }= false;
        public bool UseDELETEAuthorisation { get; set; } = true;
        public Dictionary<string, string> ExpectsDELETE { get; set; } = [];
        public Dictionary<string, string> ReturnsDELETE { get; set; } = [];
        public bool ReturnsArrayDELETE { get; set; } = false;
        public bool ReturnsCacheableDELETE { get; set; } = false;


        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="noun"></param>
        /// <param name="description"></param>
        /// <param name="useGET"></param>
        /// <param name="usePOST"></param>
        /// <param name="useDELETE"></param>
        /// <param name="usePATCH"></param>
        /// <param name="usePOSTAuthorisation"></param>
        /// <param name="useDELETEAuthorisation"></param>
        /// <param name="usePATCHAuthorisation"></param>
        public class_rest(string noun, string description, bool useGET, bool usePOST, bool useDELETE, bool usePATCH, bool usePOSTAuthorisation, bool useDELETEAuthorisation, bool usePATCHAuthorisation, bool useGETAuthorisation)
        {
            ID = IDs.GetNewID();
            Noun = noun;
            Description = description;
            UseGET = useGET;
            UsePOST = usePOST;
            UseDELETE = useDELETE;
            UsePATCH = usePATCH;
            UsePOSTAuthorisation = usePOSTAuthorisation;
            UseDELETEAuthorisation = useDELETEAuthorisation;
            UsePATCHAuthorisation = usePATCHAuthorisation;
            UseGETAuthorisation = useGETAuthorisation;
            //Position = position;
            //List = list;
        }
    
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="noun"></param>
        /// <param name="list"></param>
        /// <param name="position"></param>
        public class_rest(string noun)
        {
            ID = IDs.GetNewID();
            Noun = noun;
            //List = list;    
            //Position = position;
        }

        /// <summary>
        /// Constructor
        /// </summary>
        public class_rest()
        {
            ID = IDs.GetNewID();
        }
    }

    public class list_items
    {
        public int Count { get => items.Count;  }
        public string Title { get; set; } = "";
        public string Description { get; set; } = "";

        public int Port { get; set; } = 8080;

        private List<class_rest> items = [];

        public List<class_rest> Items {
            get
            {
                return items;
            }
            set  {
                if (value != null)
                {
                    items = value;
                    int maxID = items.Max((n) => n.ID);
                    IDs.SetStartID(maxID);
                } else
                {
                    items = [];
                }
            }
        }

        public void SetItems(List<class_rest> newItems)
        {
            Items = newItems;
            int maxID = Items.Max((n) => n.ID);
            IDs.SetStartID(maxID);
        }

        /// <summary>
        /// The last error message
        /// </summary>
        private string ErrorMessage = "";

        public string LastError() {  return ErrorMessage; } 

        /// <summary>
        /// Checks for empty nodes in path.
        /// </summary>
        /// <param name="path"></param>
        /// <returns>String with error message or "OK" if path is good.</returns>
        public bool CheckPath(string path)
        {
            ErrorMessage = "";
            if (string.IsNullOrEmpty(path))
            {
                ErrorMessage = "Empty path";
                return false;
            }
            
            string[] nodes = path.Split('/');
            if (nodes.Length == 0)
            {
                ErrorMessage = "No nodes found in path";
                return false;
            }
            for(int i = 0; i < nodes.Length; i++)
            {
                if (nodes[i].Length == 0)
                {
                    ErrorMessage = "Empty node found in path";
                    return false;
                }
            }
                return true;
        }

        /// <summary>
        /// Adds a path to the nodes list
        /// </summary>
        /// <param name="path"></param>
        /// <returns>Number of nodes added</returns>
        /// <exception cref="ArgumentException"></exception>
        public int Add(string path)
        {
            // ensure path is not null
            // remove first and last whitespaces and '/'
            path ??= string.Empty;
            path = path.Trim();
            path = path.Trim('/');

            // empty string error
            if (path.Length == 0) throw new ArgumentException("No path given.");

            // check for illegal characters
            Regex r = new Regex("^[a-zA-Z0-9/]*$");
            if (!r.IsMatch(path)) throw new ArgumentException("Only characters A-Z, a-z, 0-9 and / allowed.");
            
            // split path into nodes (nouns) and add to nodes list
            int numAdded = 0;
            int currentNodeID = -1;
            String[] nouns = path.Split('/');

            //Check for empty nodes 
            for(int i=0; i<nouns.Length; i++)
            {
                if (nouns[i].Length == 0) throw new ArgumentException("Empty node found in path.");
            }

            // Find the appropiate root node.
            // There should only be one (should auto add at creation of a new project)
            class_rest? item = Items.Find(obj => obj.PreviousNodeID < 0);
            if (item != null)
            {
                currentNodeID = item.ID;
            }
            else
            {
                throw new ArgumentException("No root node found.");
            }
            // Add the remaining path
            for(int i = 0; i < nouns.Length; i++)
            {
                item = Items.Find(obj => obj.Noun == nouns[i] && obj.PreviousNodeID == currentNodeID);
                // add it if it does not exist
                if (item == null)
                {
                    class_rest node = new(nouns[i]);
                    node.PreviousNodeID = currentNodeID;
                    currentNodeID = node.ID;
                    Items.Add(node);
                    numAdded++;                    
                } else
                {
                    currentNodeID = item.ID;
                }
            }
            return numAdded;
        }

        public bool UpdateItem(int ID, class_rest item)
        {
            if(item == null) return false;
            int index = Items.FindIndex((obj) => obj.ID == ID); 
            if (index < 0)  return false;
            Items[index].Noun = item.Noun;
            Items[index].Description = item.Description;
            return true;
        }

        /// <summary>
        /// Returns an item with id ID;
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        public class_rest Get(int ID)
        {
            return Items.Find((item) => item.ID == ID)!;
        }

        public void RemoveAll()
        {
            Items.Clear();
            this.Title = "";
            this.Description = "";
            this.Port = 8080;
            IDs.SetStartID(0);
        }

        public void AddRoot() {
            // Add the root node
            class_rest node = new("/")
            {
                PreviousNodeID = -1,
                Description = "Root node",
                UseGET = false
            };
            Items.Add(node);
        }

        public string Save(string filename)
        {
            if (filename != "")
            {
                try
                {
                    string jsonString = JsonSerializer.Serialize(this);
                    File.WriteAllText(filename, jsonString);
                } catch ( Exception e)
                {
                    return e.Message;
                } 
            }
            return "OK";
        }

        public string Load(string filename)
        {
            this.RemoveAll();
            if (filename != "")
            {
                try
                {
                    using (StreamReader r = new(filename))
                    {
                        string json = r.ReadToEnd();
                        list_items tmp = JsonSerializer.Deserialize<list_items>(json)!;
                        this.Title = tmp.Title;
                        this.Description = tmp.Description; 
                        this.Port = tmp.Port;
                        this.Items = tmp.Items;
                    }
                }
                catch (Exception e)
                {
                    return e.Message;
                }
            }
            return "OK";
        }

    }

    internal static class IDs
    {
        private static int newID = 0;

        /// <summary>
        /// Method <c>GetID</c> Generates a new ID
        /// </summary>
        /// <returns>integer</returns>
        public static int GetNewID()
        {
            newID++;
            return newID;
        }

        public static void SetStartID(int startID)
        {
            newID = startID;
        }

        /// <summary>
        /// Method <c>SetID</c> sets the current ID to increment from.
        /// </summary>
        /// <param name="ID"></param>
        public static void SetID(int ID)
        {
            newID = ID;
        }

    }


}
