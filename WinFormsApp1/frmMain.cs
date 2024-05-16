using Microsoft.Web.WebView2.Core;
using System;
using System.Diagnostics;
using System.IO;
using System.Text.Json;
using System.Text.Json.Nodes;
using System.Text.Json.Serialization;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using System.Xml;
using JsonSerializer = System.Text.Json.JsonSerializer;

namespace EasyREST
{
    public partial class frmMain : Form
    {
        private LinkedList<class_rest> lstRESTitems = new();

        private list_items lstItems = new();

        //
        // FORM INITIALISATION
        //
        public frmMain()
        {
            InitializeComponent();
            this.webView2.Left = 0;
            webView2.Top = menuStrip1.Height;
            this.webView2.Width = this.ClientSize.Width - 1;
            this.webView2.Height = this.ClientSize.Height - menuStrip1.Height - 1;
            this.StartPosition = FormStartPosition.CenterScreen;
        }

        private async void Form1_Load(object sender, EventArgs e)
        {
            webView2.CoreWebView2InitializationCompleted += WebView_CoreWebView2InitializationCompleted!;

            Helpers.DebugMessage("before InitializeAsync");
            await InitializeAsync();
            Helpers.DebugMessage("after InitializeAsync");

            if ((webView2 == null) || (webView2.CoreWebView2 == null))
            {
                Helpers.DebugMessage("not ready");
            }

#if !DEBUG
                webView2.CoreWebView2.Settings.AreBrowserAcceleratorKeysEnabled = false;
#endif

            // Create a virtual host so HTML file can load referenced script files etc.
            webView2!.CoreWebView2!.SetVirtualHostNameToFolderMapping("appassets", "assets", CoreWebView2HostResourceAccessKind.DenyCors);

            webView2.Source = new Uri("https://appassets/view.html");
            Helpers.DebugMessage("after NavigateToString");
        }

        private void WebView_CoreWebView2InitializationCompleted(object sender, Microsoft.Web.WebView2.Core.CoreWebView2InitializationCompletedEventArgs e)
        {
            Helpers.DebugMessage("WebView_CoreWebView2InitializationCompleted");
        }

        private async Task InitializeAsync()
        {
            Helpers.DebugMessage("InitializeAsync");
            await webView2.EnsureCoreWebView2Async(null);
            webView2.CoreWebView2.WebMessageReceived += MessageReceived!;
            Helpers.DebugMessage("WebView2 Runtime version: " + webView2.CoreWebView2.Environment.BrowserVersionString);
        }


        void MessageReceived(object sender, CoreWebView2WebMessageReceivedEventArgs args)
        {
            try
            {
                JsonNode json = JsonNode.Parse(args.WebMessageAsJson)!;
                String msg = (string)json!["msg"]!;
                switch (msg)
                {
                    case "item_Clicked":
                        // Show a modal dialog after the current event handler is completed, to avoid potential reentrancy
                        // caused by running a nested message loop in the WebView2 event handler.
                        System.Threading.SynchronizationContext.Current!.Post((_) =>
                        {
                            string strID = (string)json!["id"]!;
                            int id = Int32.Parse(strID);
                            String info = $"item_Clicked: id = {id}";
                            Helpers.DebugMessage(info);
                            EditItem(id);
                        }, null);
                        break;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void Form1_Resize(object sender, EventArgs e)
        {
            webView2.Left = 0;
            webView2.Top = menuStrip1.Height;
            int width = this.ClientSize.Width < 700 ? 700 : this.ClientSize.Width - 1;
            int height = (this.ClientSize.Height - menuStrip1.Height) < 300 ? 300 : (this.ClientSize.Height - menuStrip1.Height);
            webView2.Width = width;
            webView2.Height = height;
        }

        private void webView2_NavigationCompleted(object sender, CoreWebView2NavigationCompletedEventArgs e)
        {
            lstItems = new list_items();
            lstItems.AddRoot();
            lblLoading.Visible = false;
            string json = JsonSerializer.Serialize(lstItems);
            webView2.CoreWebView2.PostWebMessageAsString("{\"command\":\"list\", \"params\":" + json + "}");
        }

        /// <summary>
        /// Add a new nodes 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AddPathToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Show add path dialog
            System.Threading.SynchronizationContext.Current!.Post((_) =>
            {
                Helpers.DebugMessage("Add path clicked");
                AddPath();
            }, null);
        }

        private void AddPath()
        {
            frmAddPath dlg = new frmAddPath();
            dlg.ShowDialog(this);
            if (dlg.DialogResult == DialogResult.OK)
            {
                try
                {
                    string path = dlg.GetPath();
                    int numAdded = lstItems.Add(path);
                    Helpers.DebugMessage("Added " + numAdded.ToString() + " nodes for '" + path + "'");
                    // Redraw the nodes
                    string json = JsonSerializer.Serialize(lstItems);
                    webView2.CoreWebView2.PostWebMessageAsString("{\"command\":\"list\", \"params\":" + json + "}");
                } catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            dlg.Dispose();
        }

        private void EditItem(int ID)
        {
            class_rest item = lstItems.Get(ID);
            frmEditItem dlg = new frmEditItem(item);
            // Show add path dialog
            dlg.ShowDialog();
            if (dlg.DialogResult == DialogResult.OK)
            {

                item.Noun = dlg.Noun;
                item.Description = dlg.Description;
                // GET items
                item.UseGET = dlg.UseGET;
                item.UseGETAuthorisation = dlg.AuthoriseGET;
                item.ExpectsGET = dlg.DictionaryGetExpected;
                item.ReturnsGET = dlg.DictionaryGetReturned;
                item.ReturnsArrayGET = dlg.ReturnsArrayGET;
                item.ReturnsCacheableGET = dlg.ReturnsCahceableGET;

                // POST items
                item.UsePOST = dlg.UsePOST;
                item.UsePOSTAuthorisation = dlg.AuthorisePOST;
                item.ExpectsPOST = dlg.DictionaryPostExpected;
                item.ReturnsPOST = dlg.DictionaryPostReturned;
                item.ReturnsArrayPOST = dlg.ReturnsArrayPOST;
                item.ReturnsCacheablePOST = dlg.ReturnsCahceablePOST;

                // PATCH items
                item.UsePATCH = dlg.UsePATCH;
                item.UsePATCHAuthorisation = dlg.AuthorisePATCH;
                item.ExpectsPATCH = dlg.DictionaryPatchExpected;
                item.ReturnsPATCH = dlg.DictionaryPatchReturned;
                item.ReturnsArrayPATCH = dlg.ReturnsArrayPOST;
                item.ReturnsCacheablePATCH = dlg.ReturnsCahceablePATCH;

                item.UseDELETE = dlg.UseDELETE;
                item.UseDELETEAuthorisation = dlg.AuthoriseDELETE;
                item.ExpectsDELETE = dlg.DictionaryDeleteExpected;
                item.ReturnsDELETE = dlg.DictionaryDeleteReturned;
                item.ReturnsArrayDELETE = dlg.ReturnsArrayDELETE;
                item.ReturnsCacheableDELETE = dlg.ReturnsCahceableDELETE;

                if (lstItems.UpdateItem(ID, item) == true)
                {
                    string json = JsonSerializer.Serialize(lstItems);
                    webView2.CoreWebView2.PostWebMessageAsString("{\"command\":\"list\", \"params\":" + json + "}");
                }
            }
            dlg.Dispose();
        }

        private void newProjectToolStripMenuItem_Click(object sender, EventArgs e)
        {
            lstItems.RemoveAll();
            lstItems.AddRoot();
            string json = JsonSerializer.Serialize(lstItems);
            webView2.CoreWebView2.PostWebMessageAsString("{\"command\":\"list\", \"params\":" + json + "}");
        }

        private void saveProjectToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFileDialog sd = new SaveFileDialog();
            sd.Filter = "JSON file|*.json";
            sd.Title = "Save a JSON File";
            sd.ShowDialog();
            if (sd.FileName != "")
            {
                // If the file name is not an empty string open it for saving.
                string result = lstItems.Save(sd.FileName);
                if (result != "OK")
                {
                    MessageBox.Show(result, "Failed to save file");
                }
            }

            sd.Dispose();
        }

        private void loadProjectToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog od = new OpenFileDialog();
            od.Filter = "JSON file|*.json";
            od.Title = "Load a JSON File";
            od.ShowDialog();
            if (od.FileName != "")
            {
                string result = lstItems.Load(od.FileName);
                if (result != "OK")
                {
                    MessageBox.Show(result, "Failed to load file");
                }
                else
                {
                    string json = JsonSerializer.Serialize(lstItems);
                    webView2.CoreWebView2.PostWebMessageAsString("{\"command\":\"list\", \"params\":" + json + "}");
                }
            }
            od.Dispose();
        }

        private void projectOptionsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmProjectOptions dlg = new frmProjectOptions();
            dlg.Title = lstItems.Title;
            dlg.Description = lstItems.Description;
            dlg.Port = lstItems.Port;
            // Show project options dialog
            dlg.ShowDialog();
            if (dlg.DialogResult == DialogResult.OK)
            {
                lstItems.Title = dlg.Title;
                lstItems.Description = dlg.Description;
                lstItems.Port = dlg.Port;
                string json = JsonSerializer.Serialize(lstItems);
                webView2.CoreWebView2.PostWebMessageAsString("{\"command\":\"list\", \"params\":" + json + "}");
            }
            dlg.Dispose();
        }
    }
}
 