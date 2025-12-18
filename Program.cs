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

        private static string[] _args;

        private static void Main(string[] args)
        {
            _args = args;
            var action = "help";
            action = GetArg(0);
             
            switch(action)
            {
                case "--help":
                case "help":
                case "-h":
                    RunHelp();
                    break;
                case "--cqrs":
                case "cqrs":
                case "-c":
                    RunCQRS();
                    break;
                case "--rest":
                case "--res":
                case "rest":
                case "res":
                case "-r":
                    RunREST();
                    break;
            }

        }

        private static void RunREST()
        {
            if (_args.Length != 9)
            {
                Say("Invalid amount of parameters for cqrs. run -h for help");
                return;
            }
            var className = GetArg(1);
            var classNamePlural = GetArg(2);
            var primaryKeyDataType = GetArg(3);
            var cqrsInstruction = GetArg(4);
            var moduleName = GetArg(5);
            var crudCase = GetArg(6);
            var output = GetArg(7);
            var workspace = GetArg(8);
             
            string basepath2 = "output\\" + _args[5] + "." + _args[1] + ".res.txt";
            string formats2 = "Formats\\";
            Codular code2 = new Codular();
            code2.SetArgs(_args);
            string gend2 = code2.Generate(basepath2, formats2);
            Console.WriteLine("Done");
            File.AppendAllText("_cmd.history", DateTime.Now.ToString() + Environment.NewLine + "nboni.CodeGen.exe " + string.Join(" ", _args) + Environment.NewLine);
            if (output == "core")
            {
                CodeUpdater cu2 = new CodeUpdater();
                cu2.SetArgs(_args);
                cu2.Generate(gend2, formats2 + "paths.json");
            }
            if (output == "ui")
            {
                CodeUpdater cu3 = new CodeUpdater();
                cu3.SetArgs(_args);
                cu3.GenerateUI(gend2, formats2 + "paths_ui.json");
            }
        }

        private static void RunCQRS()
        {
            
            if(_args.Length != 8)
            {
                Say("Invalid amount of parameters for REST. run -h for help");
                return;
            }
            var className = GetArg(1);
            var classNamePlural = GetArg(2);
            var primaryKeyDataType = GetArg(3);
            var cqrsInstruction = GetArg(4);
            var moduleName = GetArg(5);
            var crudCase = GetArg(6);
            var output = GetArg(7);
            //var workspace = GetArg(8);

 
    
            string basepath = "output\\" + moduleName + "." + className + ".cqrs.txt";
            string formats = "Formats\\";
            Codular code = new Codular();
            code.SetCQRSArgs(_args);
            string gend = code.GenerateCQRS(basepath, formats);
            Console.WriteLine("Done");
            File.AppendAllText("_cqrs.history", DateTime.Now.ToString() + Environment.NewLine + "nboni.CodeGen.exe " + string.Join(" ", _args) + Environment.NewLine);
            if (genfile == "core")
            {
                CodeUpdater cu = new CodeUpdater();
                cu.SetArgsCQRS(_args);
                cu.Generate(gend, formats + "paths_cqrs.json");
            }
        }

        private static void Say(string v = "")
        {
            Console.WriteLine(v);
        }

        private static void RunHelp()
        {
            Say("Help Topics for Godegen");
            Say("-----------------------");
            Say();
            Say("Commands:");
            Say("Help: -h, --help, help");
            Say("Rest Command: -r, --rest, --res, rest, res");
            Say("CQRS: -c, --cqrs, cqrs");
            Say();
            Say();
            Say("REST parameters:");
            Say("----------------");
            Say("class Name:\tName of the Entity - User");
            Say("class Name Plural:\tName of the Entity in Plural form - Users");


        }

        private static string GetArg(int v)
        { 
            if(_args == null) return string.Empty;
            if(_args.Length > 9) return string.Empty; 
            return _args[v]; 
        }
    }
}
