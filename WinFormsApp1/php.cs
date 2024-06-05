using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection.Metadata;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;
using static System.Windows.Forms.LinkLabel;

namespace EasyREST
{
    internal class php
    {
        private list_items _nodes = new list_items();
        private string _base = "X:\\\\rest2\\";

        public void ExportCode(list_items lstItems)
        {
            this._nodes = lstItems;
            copyAssets();
            this.exportNodes();
            this.exportNodeClasses();
        }

        private void exportNodes()
        {
            // Set a variable to the Documents path.
            // string docPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            string docPath = _base + "v1\\model\\paths";
            using (StreamWriter outputFile = new StreamWriter(Path.Combine(docPath, "nodes.php")))
            {
                outputFile.WriteLine("<?php");
                outputFile.WriteLine("$this->_nodes = [");

                this.writeProperties("    ", (object)this._nodes, outputFile);
                outputFile.WriteLine("    'Items' => [");
                foreach (class_rest item in this._nodes.Items)
                {
                    outputFile.WriteLine("        [");
                    this.writeProperties("            ", item, outputFile);
                    outputFile.WriteLine("        ],");
                }
                outputFile.WriteLine("    ],");
                outputFile.WriteLine("];");
            }

        }

        private void writeProperties(string indent, object obj, StreamWriter sw)
        {
            foreach (var prop in obj.GetType().GetProperties())
            {
                switch (prop.PropertyType.Name)
                {
                    case "Int32":
                        sw.WriteLine("{0}'{1}' => {2}, ", indent, prop.Name, prop.GetValue(obj, null));
                        break;
                    case "String":
                        sw.WriteLine("{0}'{1}' => '{2}', ", indent, prop.Name, prop.GetValue(obj, null));
                        break;
                    case "Boolean":
                        sw.WriteLine("{0}'{1}' => {2}, ", indent, prop.Name, prop.GetValue(obj, null));
                        break;
                    case "Dictionary`2":
                        sw.WriteLine("{0}'{1}' => [", indent, prop.Name);
                        var dict = prop.GetValue(obj, null) as Dictionary<string, string>;
                        if (dict != null)
                        {
                            foreach (var key in dict)
                            {
                                sw.WriteLine("    {0}'{1}' => '{2}',", indent, key.Key, key.Value);
                            }
                        }
                        sw.WriteLine("{0}],", indent);
                        break;
                    default:
                        var type = prop.PropertyType.Name;
                        break;

                }

            }
        }

        private void exportNodeClasses()
        {
            foreach(class_rest node in this._nodes.Items)
            {
                string classname = node.Noun == "/" ? $"root_{node.ID}" :$"{node.Noun}_{node.ID}";
                string filename = classname + ".php";

                // Get node path
                string nodepath = node.Noun;
                class_rest tmpnode = node;
                while(tmpnode != null)
                {
                    tmpnode = _nodes.Items.Find(obj => obj.ID == tmpnode.PreviousNodeID)!;
                    if(tmpnode != null && tmpnode.Noun != "/")
                    {
                        nodepath = $"{tmpnode.Noun}/{nodepath}";
                    }
                    
                }

                // Load template and fill in blanks
                string classtemplate = File.ReadAllText("assets/php/build/v1/model/nodeclass.php");
                classtemplate = classtemplate.Replace("{endpoint}", nodepath);
                classtemplate = classtemplate.Replace("{noun}", node.Noun);
                classtemplate = classtemplate.Replace("{nodeclass}", classname);

                // Fill in method functions
                classtemplate = FillMethods(node, classtemplate, nodepath);

                // Save the file
                string docPath = _base + $"v1\\model\\paths\\{filename}";
                File.WriteAllText(docPath, classtemplate);
            }
        }

        private void copyAssets()
        {
            string source = "assets\\php\\copy";
            string dest = this._base;
            DirectoryCopy(source, dest, true);
        }

        private void DirectoryCopy(string sourceDirName, string destDirName, bool copySubDirs)
        {
            // Get the subdirectories for the specified directory.
            DirectoryInfo dir = new DirectoryInfo(sourceDirName);

            if (!dir.Exists)
            {
                throw new DirectoryNotFoundException(
                    "Source directory does not exist or could not be found: "
                    + sourceDirName);
            }

            DirectoryInfo[] dirs = dir.GetDirectories();
            // If the destination directory doesn't exist, create it.
            if (!Directory.Exists(destDirName))
            {
                Directory.CreateDirectory(destDirName);
            }

            // Get the files in the directory and copy them to the new location.
            FileInfo[] files = dir.GetFiles();
            foreach (FileInfo file in files)
            {
                string temppath = Path.Combine(destDirName, file.Name);
               file.CopyTo(temppath, true);
            }

            // If copying subdirectories, copy them and their contents to new location.
            if (copySubDirs)
            {
                foreach (DirectoryInfo subdir in dirs)
                {
                    string temppath = Path.Combine(destDirName, subdir.Name);
                    DirectoryCopy(subdir.FullName, temppath, copySubDirs);
                }
            }
        }

        private string FillMethods(class_rest node, string template, string endpoint)
        {
            string _case  = "";
            string _getFunc = "";
            string _postFunc = "";
            string _patchFunc = "";
            string _deleteFunc = "";
            
            if (node.UseGET == true)
            {
                _case += "case 'GET':\n" +
                    "                $this->processGET($node, $params);\n" +
                    "                break;";
                _getFunc = $"private function processGET($node, $params) {{\n" +
                    $"        $res = new Response(Response::httpResponseOK, true, \"Reached GET method for /{endpoint}\");\n" +
                    $"        $res->addMessage(\"The following parameters were recieved:\");\n" +
                    $"        foreach ($params as $key => $value) {{\n" +
                    $"            $res->addMessage(\"$key: $value\");\n" +
                    $"        }}\n" +
                    $"        $res->send();\n" +
                    $"    }}";
            }

            if (node.UsePOST == true)
            {
                _case += "case 'POST':\n" +
                    "                $this->processPOST($node, $params);\n" +
                    "                break;";
                _postFunc = $"private function processPOST($node, $params) {{\n" +
                    $"        $res = new Response(Response::httpResponseOK, true, \"Reached POST method for /{endpoint}\");\n" +
                    $"        $res->addMessage(\"The following parameters were recieved:\");\n" +
                    $"        foreach ($params as $key => $value) {{\n" +
                    $"            $res->addMessage(\"$key: $value\");\n" +
                    $"        }}\n" +
                    $"        $res->send();\n" +
                    $"    }}";
            }
            
            if (node.UsePATCH == true)
            {
                _case += "case 'PATCH':\n" +
                    "                $this->processPATCH($node, $params);\n" +
                    "                break;";
                _patchFunc = $"private function processPATCH($node, $params) {{\n" +
                    $"        $res = new Response(Response::httpResponseOK, true, \"Reached PATCH method for /{endpoint}\");\n" +
                    $"        $res->addMessage(\"The following parameters were recieved:\");\n" +
                    $"        foreach ($params as $key => $value) {{\n" +
                    $"            $res->addMessage(\"$key: $value\");\n" +
                    $"        }}\n" +
                    $"        $res->send();\n" +
                    $"    }}";
            }
            if (node.UseDELETE == true)
            
            {
                _case += "case 'DELETE':\n" +
                    "                $this->processDELETE($node, $params);\n" +
                    "                break;";
                _deleteFunc = $"private function processDELETE($node, $params) {{\n" +
                    $"        $res = new Response(Response::httpResponseOK, true, \"Reached DELETE method for /{endpoint}\");\n" +
                    $"        $res->addMessage(\"The following parameters were recieved:\");\n" +
                    $"        foreach ($params as $key => $value) {{\n" +
                    $"            $res->addMessage(\"$key: $value\");\n" +
                    $"        }}\n" +
                    $"        $res->send();\n" +
                    $"    }}";
            }

            template = template.Replace("{switch}", _case);
            template = template.Replace("{GET_FUNC}", _getFunc);
            template = template.Replace("{POST_FUNC}", _postFunc);
            template = template.Replace("{PATCH_FUNC}", _patchFunc);
            template = template.Replace("{DELETE_FUNC}", _deleteFunc);
            return template;
        }

    }
}
