using nboni.CodeGen.Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using static System.Net.Mime.MediaTypeNames;

namespace nboni.CodeGen
{
    internal class CodeUpdater
    {
        public CodeUpdater()
        {
        }

        private CodePaths cps { get; set; }
        private string Module { get; set; }
        private string Class1 { get; set; }
        private string Class2 { get; set; } 
        private string Area { get; set; }
        private string KeyId { get; set; }


        private string cqrsname { get; set; }
        private string cqrscmd { get; set; }

        private string cqrsmethod;

        private List<string> codelines { get; set; }

        internal void Generate(string gend, string pathsfile)
        {
            //I need to iterate over tje settings
            var config = File.ReadAllText(pathsfile);
            cps  = JsonConvert.DeserializeObject<CodePaths>(config);
             
            codelines = gend.Split(new string[] { "\r\n" }, StringSplitOptions.RemoveEmptyEntries).ToList();

            foreach (var snippet in cps.snippets)
            {
                switch (snippet.action)
                {
                    case "create": 
                        SnippetCreate(snippet);
                        break;
                    case "createsafe":
                        SnippetCreateSafe(snippet);
                        break;
                    case "update":
                        SnippetUpdate(snippet);
                        break;
                }
            }
        }


        internal void GenerateUI(string gend, string pathsfile)
        {
            //I need to iterate over tje settings
            var config = File.ReadAllText(pathsfile);
            cps = JsonConvert.DeserializeObject<CodePaths>(config);

            codelines = gend.Split(new string[] { "\r\n" }, StringSplitOptions.RemoveEmptyEntries).ToList();

            foreach (var snippet in cps.snippets)
            {
                switch (snippet.action)
                {
                    case "create":
                        SnippetCreate(snippet);
                        break;
                    case "createsafe":
                        SnippetCreateSafe(snippet);
                        break;
                    case "update":
                        SnippetUpdate(snippet);
                        break;
                }
            }
        }

        /// <summary>
        /// update an existing code file
        /// </summary>
        /// <param name="snippet"></param>
        private void SnippetUpdate(CodeSnippet snippet)
        {
            var snip = SearchSnippet(snippet.code);
            if (string.IsNullOrEmpty(snip)) return;
            var dir = cps.basepath + snippet.folder;
            dir = dir.Replace("%Z1%", Area);
            dir = dir.Replace("%Z%", Module);
            dir = dir.Replace("%T%", KeyId);
            dir = dir.Replace("%N%", Class2);
            dir = dir.Replace("%H%", Class1);
            dir = dir.Replace("%CN%", cqrsname);
            dir = dir.Replace("%CC%", cqrscmd);
            dir = dir.Replace("%CM%", cqrsmethod);

            var fle = snippet.file.Replace("%H%", Class1);
            fle = fle.Replace("%N%", Class2);
            fle = fle.Replace("%Z%", Module);
            fle = fle.Replace("%T%", KeyId);
            fle = fle.Replace("%Z1%", Area);
            fle = fle.Replace("%CN%", cqrsname);
            fle = fle.Replace("%CC%", cqrscmd);
            fle = fle.Replace("%CM%", cqrsmethod);
            //search for directoryModule
            if (!Directory.Exists(dir))
            {
                Directory.CreateDirectory(dir);
            }
            if(!File.Exists(dir + fle))
            {
                return;
            }
            //if snippet exist return
            var codefile = File.ReadAllText(dir + fle);
            if (codefile.Contains(snip)) return;
            codefile = codefile.Replace("///" + snippet.code, snip + "///" + snippet.code);
            codefile = codefile.Replace("//#" + snippet.code, snip + "//#" + snippet.code);
            //write file back
            File.WriteAllText(dir + fle, codefile);
        }

        /// <summary>
        /// Create a new code file
        /// </summary>
        /// <param name="snippet"></param>
        private void SnippetCreate(CodeSnippet snippet)
        {
            var snip = SearchSnippet(snippet.code);
            if (string.IsNullOrEmpty(snip)) return;

            var dir = cps.basepath + snippet.folder;
            dir = dir.Replace("%Z1%", Area);
            dir = dir.Replace("%Z%", Module);
            dir = dir.Replace("%T%", KeyId);
            dir = dir.Replace("%N%", Class2);
            dir = dir.Replace("%H%", Class1);
            dir = dir.Replace("%CN%", cqrsname);
            dir = dir.Replace("%CC%", cqrscmd);
            dir = dir.Replace("%CM%", cqrsmethod);

            var fle = snippet.file.Replace("%H%", Class1);
            fle = fle.Replace("%N%", Class2);
            fle = fle.Replace("%Z%", Module);
            fle = fle.Replace("%T%", KeyId);
            fle = fle.Replace("%Z1%", Area);
            fle = fle.Replace("%CN%", cqrsname);
            fle = fle.Replace("%CC%", cqrscmd);
            fle = fle.Replace("%CM%", cqrsmethod);

            if (!Directory.Exists(dir))
            {
                Directory.CreateDirectory(dir);
            } 

            if(File.Exists(dir + fle)){
                File.Delete(dir + fle);
            }
            File.WriteAllText(dir + fle, snip);
        }

        private void SnippetCreateSafe(CodeSnippet snippet)
        {
            var snip = SearchSnippet(snippet.code);
            if (string.IsNullOrEmpty(snip)) return;

            var dir = cps.basepath + snippet.folder;
            dir = dir.Replace("%Z1%", Area);
            dir = dir.Replace("%Z%", Module);
            dir = dir.Replace("%T%", KeyId);
            dir = dir.Replace("%N%", Class2);
            dir = dir.Replace("%H%", Class1);
            dir = dir.Replace("%CN%", cqrsname);
            dir = dir.Replace("%CC%", cqrscmd);
            dir = dir.Replace("%CM%", cqrsmethod);

            var fle = snippet.file.Replace("%H%", Class1);
            fle = fle.Replace("%N%", Class2);
            fle = fle.Replace("%Z%", Module);
            fle = fle.Replace("%T%", KeyId);
            fle = fle.Replace("%Z1%", Area);
            fle = fle.Replace("%CN%", cqrsname);
            fle = fle.Replace("%CC%", cqrscmd);
            fle = fle.Replace("%CM%", cqrsmethod);

            if (!Directory.Exists(dir))
            {
                Directory.CreateDirectory(dir);
            }

            if (File.Exists(dir + fle))
            {
                return;
            }
            File.WriteAllText(dir + fle, snip);
        }

        private string SearchSnippet(string code)
        {
            var found = false;
            var iLine = 0;
            var txt = "";
            foreach(var ln in codelines)
            {
                if (ln.StartsWith(code + ">>>"))
                {
                    found = true;
                } 
                iLine++; 
                if (found) break;
            }

            if (!found) return txt;

            //i found the ling
            for(int i = iLine; i < codelines.Count; i++)
            {
                if (codelines[i].StartsWith(">>>"))
                {
                    found = false;
                }

                if (found)
                {
                    txt += codelines[i] + Environment.NewLine;
                }
                else
                {
                    break;
                }
            }
            return txt;

        }

        internal void SetArgs(string[] args)
        {
            Module = args[5];
            Class1 = args[1];
            Class2 = args[2];
            Area = args[8];
            KeyId = args[3];
        }

        internal void SetArgsCQRS(string[] args)
        {
            Module = args[5];
            Class1 = args[1];
            Class2 = args[2];

            var fieldstext = args[4];
            char[] sep2 = { ':', ';'  }; 
            var bits = fieldstext.Split(sep2, StringSplitOptions.RemoveEmptyEntries);
            cqrsname = bits[0];
            cqrscmd = bits[1];
            cqrsmethod = bits[2];

            Area = "";
            KeyId = args[2];
        }
    }
}