using System.Collections.Generic;

namespace nboni.CodeGen.Model
{
    public class CodePaths
    {
        public string basepath { get; set; }
        public List<CodeSnippet> snippets { get; set; }
    }

    public class CodeSnippet
    {
        public string code { get; set; }
        public string action { get; set; }
        public string folder { get; set; }
        public string file { get; set; }
    }
}
