using EasyREST;
using System.IO;

namespace RestTests
{
    [TestClass]
    public class RestClassTests
    {
        [TestMethod]
        public void CreateClass()
        {
            // Create a new instamce of the class
            list_items li = new list_items();
            Assert.IsNotNull(li);
            // Check there are no nodes
            Assert.AreEqual(0, li.Count);
            // Add the root node
            li.AddRoot();
            // Should now be just 1 node
            Assert.AreEqual(1, li.Count);
        }

        // Exception tests
        private bool AddPathException(string path, string exceptionMessage)
        {
            try
            {
                list_items li = new list_items();
                li.Add(path);
                // Test fails if it makes it this far
                Assert.Fail("Expected exception was not thrown.");
                return false;
            }
            catch (ArgumentException ex) // <-- Expected Exception class
            {
                Assert.AreEqual(ex.Message, exceptionMessage);
            }
            catch (Exception ex) // All the other exceptions
            {
                Assert.Fail("Thrown exception was of the wrong type");
                return false;
            }
            return true;
        }

        [TestMethod]
        public void TestAddPathExceptions()
        {
            AddPathException("", "No path given.");
            AddPathException(null, "No path given.");
            AddPathException("///", "No path given.");
            AddPathException("a/b//c/d", "Empty node found in path.");
            AddPathException("a/b /c/ /d", "Only characters A-Z, a-z, 0-9 and / allowed.");
        }
        
        [TestMethod]
        public void TestAddNodes()
        {
            list_items li = new list_items();
            li.AddRoot();
            li.Add("a/b/c/d");
            // There should now be 5 nodes including root
            Assert.AreEqual(5, li.Count);
        }

        [TestMethod] 
        public void TestMergeNodes()
        {
            TestAddNodes();
            list_items li = new list_items();
            li.AddRoot();
            li.Add("a/b/c/d");
            li.Add("a/b/x/y");
            // Should now be 7 nodes, including root, as path is merged.
            Assert.AreEqual(7, li.Count);
        }

    }
}