using nboni.CodeGen.Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq; 

namespace nboni.CodeGen
{
    internal class CodeUpdater
    {
        private string cqrsmethod;

        private CodePaths cps { get; set; }

        private string Module { get; set; }

        private string Class1 { get; set; }

        private string Class2 { get; set; }

        private string Area { get; set; }

        private string KeyId { get; set; }

        private string cqrsname { get; set; }

        private string cqrscmd { get; set; }

        private List<string> codelines { get; set; }

        internal void Generate(string gend, string pathsfile)
        {
            string config = File.ReadAllText(pathsfile);
            cps = JsonConvert.DeserializeObject<CodePaths>(config);
            codelines = gend.Split(new string[1] { "\r\n" }, StringSplitOptions.RemoveEmptyEntries).ToList();
            foreach (CodeSnippet snippet in cps.snippets)
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
            string config = File.ReadAllText(pathsfile);
            cps = JsonConvert.DeserializeObject<CodePaths>(config);
            codelines = gend.Split(new string[1] { "\r\n" }, StringSplitOptions.RemoveEmptyEntries).ToList();
            foreach (CodeSnippet snippet in cps.snippets)
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

        private void SnippetUpdate(CodeSnippet snippet)
        {
            string snip = SearchSnippet(snippet.code);
            if (string.IsNullOrEmpty(snip))
            {
                return;
            }
            string dir = cps.basepath + snippet.folder;
            dir = dir.Replace("%Z1%", Area);
            dir = dir.Replace("%Z%", Module);
            dir = dir.Replace("%T%", KeyId);
            dir = dir.Replace("%N%", Class2);
            dir = dir.Replace("%H%", Class1);
            dir = dir.Replace("%CN%", cqrsname);
            dir = dir.Replace("%CC%", cqrscmd);
            dir = dir.Replace("%CM%", cqrsmethod);
            string fle = snippet.file.Replace("%H%", Class1);
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
                string codefile = File.ReadAllText(dir + fle);
                if (!codefile.Contains(snip))
                {
                    codefile = codefile.Replace("///" + snippet.code, snip + "///" + snippet.code);
                    codefile = codefile.Replace("//#" + snippet.code, snip + "//#" + snippet.code);
                    File.WriteAllText(dir + fle, codefile);
                }
            }
        }

        private void SnippetCreate(CodeSnippet snippet)
        {
            string snip = SearchSnippet(snippet.code);
            if (!string.IsNullOrEmpty(snip))
            {
                string dir = cps.basepath + snippet.folder;
                dir = dir.Replace("%Z1%", Area);
                dir = dir.Replace("%Z%", Module);
                dir = dir.Replace("%T%", KeyId);
                dir = dir.Replace("%N%", Class2);
                dir = dir.Replace("%H%", Class1);
                dir = dir.Replace("%CN%", cqrsname);
                dir = dir.Replace("%CC%", cqrscmd);
                dir = dir.Replace("%CM%", cqrsmethod);
                string fle = snippet.file.Replace("%H%", Class1);
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
                    File.Delete(dir + fle);
                }
                File.WriteAllText(dir + fle, snip);
            }
        }

        private void SnippetCreateSafe(CodeSnippet snippet)
        {
            string snip = SearchSnippet(snippet.code);
            if (!string.IsNullOrEmpty(snip))
            {
                string dir = cps.basepath + snippet.folder;
                dir = dir.Replace("%Z1%", Area);
                dir = dir.Replace("%Z%", Module);
                dir = dir.Replace("%T%", KeyId);
                dir = dir.Replace("%N%", Class2);
                dir = dir.Replace("%H%", Class1);
                dir = dir.Replace("%CN%", cqrsname);
                dir = dir.Replace("%CC%", cqrscmd);
                dir = dir.Replace("%CM%", cqrsmethod);
                string fle = snippet.file.Replace("%H%", Class1);
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
                if (!File.Exists(dir + fle))
                {
                    File.WriteAllText(dir + fle, snip);
                }
            }
        }

        private string SearchSnippet(string code)
        {
            bool found = false;
            int iLine = 0;
            string txt = "";
            foreach (string ln in codelines)
            {
                if (ln.StartsWith(code + ">>>"))
                {
                    found = true;
                }
                iLine++;
                if (found)
                {
                    break;
                }
            }
            if (!found)
            {
                return txt;
            }
            for (int i = iLine; i < codelines.Count; i++)
            {
                if (codelines[i].StartsWith(">>>"))
                {
                    found = false;
                }
                if (found)
                {
                    txt = txt + codelines[i] + Environment.NewLine;
                    continue;
                }
                break;
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
            string fieldstext = args[4];
            char[] sep2 = new char[2] { ':', ';' };
            string[] bits = fieldstext.Split(sep2, StringSplitOptions.RemoveEmptyEntries);
            cqrsname = bits[0];
            cqrscmd = bits[1];
            cqrsmethod = bits[2];
            Area = "";
            KeyId = args[2];
        }
    }
}