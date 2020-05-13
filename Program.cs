using System;
using System.IO;

namespace nboni.CodeGen
{
    class Program
    {
        public static string mycase;
        static void Main(string[] args)
        {
            var basepath = "output\\" + args[0] + ".txt";
            var formats = "Formats\\";

            //var v0 = "User";
            //var v1 = "Users";
            //var v2 = "long";
            //var v3 = "UserName:string; FirstName: string; LastName: string; Subject: string;";
            var v4 = "";  //sub app
            try { v4 = args[4]; } catch {  }

            mycase = "upper";  //sub app
            try { mycase = args[5]; } catch { mycase = "upper"; }



            var code = new Codular(basepath, args[0], args[1], args[2], args[3], formats, v4);
            code.Generate();
            Console.WriteLine("Done");

            File.AppendAllText("_cmd.history", DateTime.Now.ToString() + Environment.NewLine + "nboni.CodeGen.exe " + string.Join(" ", args) + Environment.NewLine + Environment.NewLine);
        }
    }

}
