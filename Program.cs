using System;
using System.IO;

namespace nboni.CodeGen
{
    class Program
    {
        public static string mycase;
        static void Main(string[] args)
        {
           

            //var v0 = "User";
            //var v1 = "Users";
            //var v2 = "long";
            //var v3 = "UserName:string; FirstName: string; LastName: string; Subject: string;StatusId";
            var v4 = "";  //sub app
            try { v4 = args[4]; } catch {  }

            mycase = "upper";  //sub app
            try { mycase = args[5]; } catch { mycase = "upper"; }

            var genfile = "no";
            try { genfile = args[6]; } catch { genfile = "no"; }


            var workspace = v4;
            try { workspace = args[7]; } catch { workspace = v4; }

            var basepath = "output\\" + args[4] + "." + args[0] + ".txt";
            var formats = "Formats\\";

            var code = new Codular(basepath, args[0], args[1], args[2], args[3], formats, v4, workspace);
            var gend = code.Generate();
            Console.WriteLine("Done");

            File.AppendAllText("_cmd.history", DateTime.Now.ToString() + Environment.NewLine + "nboni.CodeGen.exe " + string.Join(" ", args) + Environment.NewLine + Environment.NewLine);

            if(genfile == "yes")
            {
                var cu = new CodeUpdater();
                cu.SetArgs(args);
                cu.Generate(gend, formats + "paths.json");
            }
        }
    }

}
