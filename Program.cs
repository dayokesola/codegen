using System;
using System.IO;

namespace nboni.CodeGen
{
    class Program
    {
        public static string mycase;
        public static string module; 
        public static string genfile;
        public static string workspace;
        static void Main(string[] args)
        {
            //var v0 = "User";
            //var v1 = "Users";
            //var v2 = "long";
            //var v3 = "UserName:string; FirstName: string; LastName: string; Subject: string;StatusId";
            //var v4 = "module";
            //var v5 = "case: upper or lower";
            //var v6 = "code gen type: core or ui";
            //var v7 = "ui controller ";
             
            try { var v0 = args[0]; } catch{ throw new Exception("First Parameter is Class Name"); } 
            try { var v1 = args[1]; } catch { throw new Exception("Second Parameter is Class Name in Plural"); } 
            try { var v2 = args[2]; } catch { args[2] = "long"; } 
            try { var v3 = args[3]; } catch { args[3] = "name;code;info;statusid"; } 
            try { module = args[4]; } catch { throw new Exception("Param 5: Module name is required"); } 
            try { mycase = args[5]; } catch { mycase = "lower"; } 
            try { genfile = args[6]; } catch { genfile = "core"; } 
            try { workspace = args[7]; } catch { workspace = module; } 
            var basepath = "output\\" + args[4] + "." + args[0] + ".txt";
            var formats = "Formats\\";

            var code = new Codular();
            code.SetArgs(args); 
            var gend = code.Generate(basepath, formats);
            Console.WriteLine("Done");

            File.AppendAllText("_cmd.history", DateTime.Now.ToString() + Environment.NewLine + "nboni.CodeGen.exe " + string.Join(" ", args) + Environment.NewLine + Environment.NewLine);

            if(genfile == "core")
            {
                var cu = new CodeUpdater();
                cu.SetArgs(args);
                cu.Generate(gend, formats + "paths.json");
            } 

            if (genfile == "ui")
            {
                var cu = new CodeUpdater();
                cu.SetArgs(args); 
                cu.GenerateUI(gend, formats + "paths_ui.json");
            }
        }
    }

}
