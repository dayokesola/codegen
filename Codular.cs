using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace nboni.CodeGen
{
    public class Codular
    {
        private string basepath;
        private string v1;
        private string v2;
        private string v3;
        private string v4;
        private string v5;
        private string formats;
        private StringBuilder code;
        private Dictionary<string, string> fields;

        public Codular(string basepath, string v1, string v2, string v3 = "long", string v4 = "", string _formats = "", string v5 = "")
        {
            code = new StringBuilder();
            this.basepath = basepath;
            this.v1 = v1;
            this.v2 = v2;
            this.v3 = v3;
            this.v4 = v4;
            this.v5 = v5;
            this.formats = _formats;
            fields = Stringer.Transform(v4);
            
        }

       
        internal void Generate()
        {
            GenerateEntity();
            GenerateModel();
            GenerateForm();
            GenerateAutoMapper();
            GenerateModelFactory();
            GenerateFactoryService();
            GenerateDbContext();
            GenerateEntityRepository();
            GenerateRepositoryService(true);
            GenerateService();
            GenerateServiceModule();
            GenerateAPIController();
            GenerateAPILibrary();
            GenerateAPILibraryRepository();
            GenerateMVCController();
            GenerateSaveView();
            GenerateIndexView();
            File.WriteAllText(basepath, code.ToString());
        }

        private void Paint(string txt, string title = "")
        {
            txt = txt.Replace("%H%", v1);
            txt = txt.Replace("%N%", v2);
            txt = txt.Replace("%T%", v3);
            txt = txt.Replace("%Z%", v5);
            txt = txt.Replace("%z%", v5.ToLower());
            txt = txt.Replace("%h%", v1.ToLower());
            txt = txt.Replace("%n%", v2.ToLower());
            txt = txt.Replace("%P%", Stringer.Params(fields));
            txt = txt.Replace("%V%", Stringer.Variables(fields));
            txt = txt.Replace("%W%", Stringer.Variables(fields, "x"));
            txt = txt.Replace("%WT%", Stringer.TableParams(fields, v3));
            txt = txt.Replace("%U%", Stringer.UpperVariables(fields));
            txt = txt.Replace("%M%", Stringer.ModelVariables(fields));
            txt = txt.Replace("%Q%", Stringer.QueryString(fields));
            txt = txt.Replace("%F%", Stringer.Queryable(fields));
            txt = txt.Replace("%F1%", Stringer.QueryableView(fields));
            txt = txt.Replace("%FG%", Stringer.FactorMapper(fields));
            txt = txt.Replace("%DV%", Stringer.DetailView(fields, formats));
            txt = txt.Replace("%C%", Stringer.Properties(fields));
            txt = txt.Replace("%JM%", Stringer.JSModel(fields));
            txt = txt.Replace("%TC%", Stringer.TableColumns(fields));
            txt = txt.Replace("%J%", Stringer.JObjectHelper(fields));
            txt = txt.Replace("%B%", Stringer.ViewBag(fields));
            txt = txt.Replace("%D%", Stringer.Document(fields));
            txt = txt.Replace("%G%", Stringer.Paginator(fields));
            txt = txt.Replace("%TR%", Stringer.TableHeader(fields));
            txt = txt.Replace("%TD%", Stringer.TableRow(fields));
            txt = txt.Replace("%SR%", Stringer.SearchFields(fields, formats));
            txt = txt.Replace("%S%", Stringer.SaveFields(fields, formats));
            txt = txt.Replace("'", "\"");
            txt = txt.Replace("`", "'");
            code.AppendLine(title);
            code.AppendLine(txt);
            code.AppendLine("");
            code.AppendLine("-------------------------------------------------");
            code.AppendLine("");
        }

        private void GenerateRepositoryService(bool context = true)
        { 
            var ct = "_context";
            if (!context) ct = "";
            var txt = File.ReadAllText(formats + "reposervice.ini");
            txt = txt.Replace("%CT%", ct); 
            Paint(txt, "REPOSITORY SERVICE");
        }

        private void GenerateEntityRepository()
        {
            var txt = File.ReadAllText(formats + "entityrepo.ini");
            Paint(txt, "ENTITY REPOSITORY");
        }

        private void GenerateDbContext()
        {
            var txt = File.ReadAllText(formats + "dbcontext.ini");
            Paint(txt, "DB CONTEXT");
        }

        private void GenerateFactoryService()
        {
            code.AppendLine(""); 
            var txt = File.ReadAllText(formats + "factoryservice.ini"); 
            Paint(txt, "FACTORY SERVICE"); 
        }

        private void GenerateModelFactory()
        { 
            var txt = File.ReadAllText(formats + "modelfactory.ini"); 
            Paint(txt, "MODEL FACTORY");
        }

        private void GenerateAutoMapper()
        {  
            var txt = File.ReadAllText(formats + "automapper.ini"); 
            Paint(txt, "AUTOMAPPER CONFIG");
        }

        private void GenerateEntity()
        { 
            var txt = File.ReadAllText(formats + "entity.ini"); 
            Paint(txt, "ENTITY");
        }

        private void GenerateForm()
        { 
            var txt = File.ReadAllText(formats + "form.ini"); 
            Paint(txt, "FORM");
        }

        private void GenerateModel()
        {  
            var txt = File.ReadAllText(formats + "model.ini"); 
            Paint(txt, "MODEL"); 
        }

        private void GenerateService()
        { 
            var txt = File.ReadAllText(formats + "service.ini"); 
            Paint(txt, "SERVICE");
        }

        private void GenerateServiceModule()
        { 
            var txt = File.ReadAllText(formats + "servicemodule.ini"); 
            Paint(txt, "SERVICE MODULE");
        }

        private void GenerateAPIController()
        {               
            var txt = File.ReadAllText(formats + "apicontroller.ini"); 
            Paint(txt, "API CONTROLLER");
        }
        
        private void GenerateAPILibrary()
        {
            var txt = File.ReadAllText(formats + "apilibrary.ini");  
            Paint(txt, "API Library Service");
        }
        
        private void GenerateAPILibraryRepository()
        {
            var txt = File.ReadAllText(formats + "apilibraryrepo.ini");   
            Paint(txt, "API Library MODULE");
        }
        
        private void GenerateMVCController()
        {
            var txt = File.ReadAllText(formats + "mvccontroller.ini");   
            Paint(txt, "MVC CONTROLLER");
        }
        
        private void GenerateSaveView()
        {
            var txt = File.ReadAllText(formats + "saveview.ini");  
            Paint(txt, "SAVE VIEW");
        }
        
        private void GenerateIndexView()
        {
            var txt = File.ReadAllText(formats + "indexview.ini");   
            Paint(txt, "INDEX VIEW");
        }
    }

}