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
            if (args[0] == "cqrs")
            {
                //var v0 = "cqrs";
                //var v1 = "User";
                //var v2 = "Users";
                //var v3 = "long";
                //var v4 = "CommandName:Command";
                //var v5 = "module";
                //var v6 = "case: upper or lower";
                //var v7 = "code gen type: core or ui";
                //var v8 = "ui area ";


                try { var v1 = args[1]; } catch { throw new Exception("First Parameter is Class Name"); }
                try { var v2 = args[2]; } catch { throw new Exception("Second Parameter is Class Name in Plural"); }

                try { var v3 = args[3]; } catch { args[3] = "long"; }
                try { var v4 = args[4]; } catch { args[4] = "CreateUser:Command"; }
                try { module = args[5]; } catch { throw new Exception("Param 5: Module name is required"); }
                try { mycase = args[6]; } catch { mycase = "lower"; args[5] = mycase; }
                try { genfile = args[7]; } catch { genfile = "core"; args[6] = genfile; }
                try { workspace = args[8]; } catch { workspace = module; args[7] = workspace; }
                var basepath = "output\\" + args[5] + "." + args[1] + ".cqrs.txt";
                var formats = "Formats\\";



                var code = new Codular();
                code.SetCQRSArgs(args); 
                var gend = code.GenerateCQRS(basepath, formats);
                Console.WriteLine("Done");
                File.AppendAllText("_cqrs.history", DateTime.Now.ToString() + Environment.NewLine + "nboni.CodeGen.exe " + string.Join(" ", args) + Environment.NewLine);

                if (genfile == "core")
                {
                    var cu = new CodeUpdater();
                    cu.SetArgsCQRS(args);
                    cu.Generate(gend, formats + "paths_cqrs.json");
                }
            }
            else if (args[0] == "res")
            {
                //var v0 = "res";
                //var v1 = "User";
                //var v2 = "Users";
                //var v3 = "long";
                //var v4 = "UserName:string; FirstName: string; LastName: string; Subject: string;StatusId";
                //var v5 = "module";
                //var v6 = "case: upper or lower";
                //var v7 = "code gen type: core or ui";
                //var v8 = "ui area ";


                try { var v1 = args[1]; } catch { throw new Exception("First Parameter is Class Name"); }
                try { var v2 = args[2]; } catch { throw new Exception("Second Parameter is Class Name in Plural"); }
                try { var v3 = args[3]; } catch { args[3] = "long"; }
                try { var v4 = args[4]; } catch { args[4] = "name;code;info;statusid"; }
                try { module = args[5]; } catch { throw new Exception("Param 5: Module name is required"); }
                try { mycase = args[6]; } catch { mycase = "lower"; args[5] = mycase; }
                try { genfile = args[7]; } catch { genfile = "core"; args[6] = genfile; }
                try { workspace = args[8]; } catch { workspace = module; args[7] = workspace; }
                var basepath = "output\\" + args[5] + "." + args[1] + ".res.txt";
                var formats = "Formats\\";

                var code = new Codular();
                code.SetArgs(args);
                var gend = code.Generate(basepath, formats);
                Console.WriteLine("Done");

                File.AppendAllText("_cmd.history", DateTime.Now.ToString() + Environment.NewLine + "nboni.CodeGen.exe " + string.Join(" ", args) + Environment.NewLine);

                if (genfile == "core")
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

}
