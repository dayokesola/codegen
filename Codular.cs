using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Microsoft.SqlServer.Server;

namespace nboni.CodeGen
{
    public class Codular
    {
        private string basepath;

        private string classname;

        private string classnameplural;

        private string idtype;

        private string cqrsname;

        private string cqrscmd;

        private string cqrsmethod;

        private string fieldstext;

        private string mycase;

        private string genfile;

        private string formats;

        private string module;

        private string workspace;

        private StringBuilder code;

        private Dictionary<string, string> fields;

        internal void SetArgs(string[] args)
        {
            classname = args[1];
            classnameplural = args[2];
            idtype = args[3];
            fieldstext = args[4];
            module = args[5];
            mycase = args[6];
            genfile = args[7];
            workspace = args[8];
            fields = Stringer.Transform(fieldstext);
        }

        internal void SetCQRSArgs(string[] args)
        {
            classname = args[1];
            classnameplural = args[2];
            idtype = args[3];
            fieldstext = args[4];
            char[] sep2 = new char[2] { ':', ';' };
            string[] bits = fieldstext.Split(sep2, StringSplitOptions.RemoveEmptyEntries);
            cqrsname = bits[0];
            cqrscmd = bits[1];
            cqrsmethod = bits[2];
            module = args[5];
            mycase = args[6];
            genfile = args[7];
        }

        internal string Generate(string _basepath, string _formats)
        {
            code = new StringBuilder();
            basepath = _basepath;
            formats = _formats;
            GenerateEntity();
            GenerateModel();
            GenerateForm();
            GenerateAutoMapper();
            GenerateModelFactory();
            GenerateFactoryService();
            GenerateDbContext();
            GenerateEntityRepository();
            GenerateRepositoryService();
            GenerateService();
            GenerateServiceModule();
            GenerateAPIController();
            GenerateAPILibrary();
            GenerateAPILibraryRepository();
            GenerateMVCController();
            GenerateSaveView();
            GenerateIndexView();
            File.WriteAllText(basepath, code.ToString());
            return code.ToString();
        }

        private void Paint(string txt, string title = "")
        {
            txt = txt.Replace("%ID_QRY%", Stringer.ID_Query(idtype));
            txt = txt.Replace("%ID_PRM%", Stringer.ID_Param(idtype));
            txt = txt.Replace("%ID_FLT%", Stringer.ID_Filter(idtype));
            txt = txt.Replace("%ID_INIT%", Stringer.ID_Generate(idtype));
            txt = txt.Replace("%ID_AUTO%", Stringer.ID_Setup(idtype));
            txt = txt.Replace("%ID_DEF%", Stringer.ID_Default(idtype));
            txt = txt.Replace("%H%", classname);
            txt = txt.Replace("%N%", classnameplural);
            txt = txt.Replace("%T%", idtype);
            txt = txt.Replace("%Z%", module);
            txt = txt.Replace("%z%", module.ToLower());
            txt = txt.Replace("%h%", classname.ToLower());
            txt = txt.Replace("%n%", classnameplural.ToLower());
            txt = txt.Replace("%Y%", workspace);
            txt = txt.Replace("%Z1%", workspace);
            txt = txt.Replace("%P%", Stringer.Params(fields));
            txt = txt.Replace("%P3%", Stringer.Params(fields, 3));
            txt = txt.Replace("%V%", Stringer.Variables(fields));
            txt = txt.Replace("%V3%", Stringer.Variables3(fields));
            txt = txt.Replace("%W%", Stringer.Variables(fields, "x0"));
            txt = txt.Replace("%QJ%", Stringer.QueryJoins(fields));
            txt = txt.Replace("%WT1%", Stringer.TableParams(fields, idtype, "SQLSERVER"));
            txt = txt.Replace("%WT2%", Stringer.TableParams(fields, idtype, "MYSQL"));
            txt = txt.Replace("%WT3%", Stringer.TableParams(fields, idtype, "POSTGRES"));
            txt = txt.Replace("%U%", Stringer.UpperVariables(fields));
            txt = txt.Replace("%M%", Stringer.ModelVariables(fields));
            txt = txt.Replace("%Q%", Stringer.QueryString(fields));
            txt = txt.Replace("%F%", Stringer.Queryable(fields));
            txt = txt.Replace("%F1%", Stringer.QueryableView(fields, formats));
            txt = txt.Replace("%FG%", Stringer.FactorMapper(fields));
            txt = txt.Replace("%DV%", Stringer.DetailView(fields, formats));
            txt = txt.Replace("%C%", Stringer.Properties(fields));
            txt = txt.Replace("%CPOCO%", Stringer.PropertiesNpoco(fields));
            txt = txt.Replace("%CPOCO2%", Stringer.PropertiesNpoco(fields, 2));
            txt = txt.Replace("%C2%", Stringer.Properties(fields, 2));
            txt = txt.Replace("%C3%", Stringer.Properties(fields, 3));
            txt = txt.Replace("%JM%", Stringer.JSModel(fields, idtype));
            txt = txt.Replace("%TC%", Stringer.TableColumns(fields));
            txt = txt.Replace("%J%", Stringer.JObjectHelper(fields));
            txt = txt.Replace("%B%", Stringer.ViewBag(fields));
            txt = txt.Replace("%D%", Stringer.Document(fields));
            txt = txt.Replace("%G%", Stringer.Paginator(fields));
            txt = txt.Replace("%TR%", Stringer.TableHeader(fields));
            txt = txt.Replace("%TD%", Stringer.TableRow(fields));
            txt = txt.Replace("%MM1%", Stringer.MigrationMapper(fields));
            txt = txt.Replace("%MM2%", Stringer.MigrationMapper(fields));
            txt = txt.Replace("%SR%", Stringer.SearchFields(fields, formats));
            txt = txt.Replace("%S%", Stringer.SaveFields(fields, formats));
            txt = txt.Replace("%MC%", Stringer.MappingColumns(fields));
            txt = txt.Replace("%FDL%", Stringer.FieldDataList(fields));
            txt = txt.Replace("%FDC%", Stringer.FieldDataCode(fields));
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
            string ct = "_context";
            if (!context)
            {
                ct = "";
            }
            string txt = File.ReadAllText(formats + "reposervice.ini");
            txt = txt.Replace("%CT%", ct);
            Paint(txt, "REPOSITORY SERVICE");
        }

        private void GenerateEntityRepository()
        {
            string txt = File.ReadAllText(formats + "entityrepo.ini");
            Paint(txt, "ENTITY REPOSITORY");
        }

        private void GenerateDbContext()
        {
            string txt = File.ReadAllText(formats + "dbcontext.ini");
            Paint(txt, "DB CONTEXT");
        }

        private void GenerateFactoryService()
        {
            code.AppendLine("");
            string txt = File.ReadAllText(formats + "factoryservice.ini");
            Paint(txt, "FACTORY SERVICE");
        }

        private void GenerateModelFactory()
        {
            string txt = File.ReadAllText(formats + "modelfactory.ini");
            Paint(txt, "MODEL FACTORY");
        }

        private void GenerateAutoMapper()
        {
            string txt = File.ReadAllText(formats + "automapper.ini");
            Paint(txt, "AUTOMAPPER CONFIG");
        }

        private void GenerateEntity()
        {
            string txt = File.ReadAllText(formats + "entity.ini");
            Paint(txt, "ENTITY");
        }

        private void GenerateForm()
        {
            string txt = File.ReadAllText(formats + "form.ini");
            Paint(txt, "FORM");
        }

        private void GenerateModel()
        {
            string txt = File.ReadAllText(formats + "model.ini");
            Paint(txt, "MODEL");
        }

        private void GenerateService()
        {
            string txt = File.ReadAllText(formats + "service.ini");
            Paint(txt, "SERVICE");
        }

        private void GenerateServiceModule()
        {
            string txt = File.ReadAllText(formats + "servicemodule.ini");
            Paint(txt, "SERVICE MODULE");
        }

        private void GenerateAPIController()
        {
            string txt = File.ReadAllText(formats + "apicontroller.ini");
            Paint(txt, "API CONTROLLER");
        }

        private void GenerateAPILibrary()
        {
            string txt = File.ReadAllText(formats + "apilibrary.ini");
            Paint(txt, "API Library Service");
        }

        private void GenerateAPILibraryRepository()
        {
            string txt = File.ReadAllText(formats + "apilibraryrepo.ini");
            Paint(txt, "API Library MODULE");
        }

        private void GenerateMVCController()
        {
            string txt = File.ReadAllText(formats + "mvccontroller.ini");
            Paint(txt, "MVC CONTROLLER");
        }

        private void GenerateSaveView()
        {
            string txt = File.ReadAllText(formats + "saveview.ini");
            Paint(txt, "SAVE VIEW");
        }

        private void GenerateIndexView()
        {
            string txt = File.ReadAllText(formats + "indexview.ini");
            Paint(txt, "INDEX VIEW");
        }

        private void GenerateCQRS()
        {
            string txt = File.ReadAllText(formats + "cqrs.ini");
            PaintCQRS(txt, "CQRS VIEW");
        }

        private void GenerateCQRSAPIController()
        {
            string txt = File.ReadAllText(formats + "apicontroller.ini");
            PaintCQRS(txt, "API COntroller");
        }

        internal string GenerateCQRS(string _basepath, string _formats)
        {
            code = new StringBuilder();
            basepath = _basepath;
            formats = _formats;
            GenerateCQRS();
            GenerateCQRSAPIController();
            File.WriteAllText(basepath, code.ToString());
            return code.ToString();
        }

        private void PaintCQRS(string txt, string title = "")
        {
            txt = txt.Replace("%H%", classname);
            txt = txt.Replace("%N%", classnameplural);
            txt = txt.Replace("%CN%", cqrsname);
            txt = txt.Replace("%CC%", cqrscmd);
            txt = txt.Replace("%CM%", cqrsmethod);
            txt = txt.Replace("%T%", idtype);
            txt = txt.Replace("%Z%", module);
            txt = txt.Replace("%z%", module.ToLower());
            txt = txt.Replace("%h%", classname.ToLower());
            txt = txt.Replace("%n%", classnameplural.ToLower());
            txt = txt.Replace("%cn%", cqrsname.ToLower());
            txt = txt.Replace("'", "\"");
            txt = txt.Replace("`", "'");
            code.AppendLine(title);
            code.AppendLine(txt);
            code.AppendLine("");
            code.AppendLine("-------------------------------------------------");
            code.AppendLine("");
        }
    }


}