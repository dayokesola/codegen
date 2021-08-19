using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace nboni.CodeGen
{

    public static class Stringer
    {
        public static string DetailView(Dictionary<string, string> fields, string formats)
        {
            var temp1 = @"      <dt class='col-sm-3'>%K%</dt>
                                <dd class='col-sm-9'>{{ form.%K% }}</dd>";
            try
            {
                temp1 = File.ReadAllText(formats + "detailview.ini");
            }
            catch
            {

            }

            var txt = "";
            foreach (var field in fields)
            {
                txt += temp1.Replace("%K%", field.Key) + Environment.NewLine;
            }
            char[] trim = { ',' };
            return txt.Trim(trim);
        }

        public static string Document(Dictionary<string, string> fields)
        {
            var txt = "";
            foreach (var field in fields)
            {
                txt += "/// <param name='" + FirstToLower(field.Key) + "'></param>" + Environment.NewLine;
            }
            char[] trim = { ',' };
            return txt.Trim(trim);
        }

        public static string FactorMapper(Dictionary<string, string> fields)
        {
            var txt = "";
            foreach (var field in fields)
            {
                txt += field.Key + " = obj." + field.Key + "," + Environment.NewLine;
            }
            return txt;
        }

        public static string FirstToLower(string k)
        {
            if (string.IsNullOrEmpty(k))
            {
                return k;
            }
            if (k.Length == 1)
            {
                return k.ToLower();
            }

            return k.Substring(0, 1).ToLower() + k.Substring(1, k.Length - 1);
        }

        public static string getHeader(string field, string template)
        {
            var t = template.Replace("%k%", FirstToLower(field));
            t = t.Replace("%K%", field) + Environment.NewLine;
            return t;
        }

        public static string JObjectHelper(Dictionary<string, string> fields)
        {
            var txt = "";
            foreach (var field in fields)
            {
                txt += "jo.Add('" + FirstToLower(field.Key) + "', " + FirstToLower(field.Key) + ");" + Environment.NewLine;
            }
            char[] trim = { ',' };
            return txt.Trim(trim);
        }

        public static string JSModel(Dictionary<string, string> fields)
        {
            var txt = "Id: 0,";
            if(Program.mycase == "lower")
            {
                txt = txt.ToLower();
            }
            foreach (var field in fields)
            {
                txt += Environment.NewLine + field.Key + ": '',";
            }
            char[] trim = { ',' };
            return txt.Trim(trim);
        }

        public static string ModelVariables(Dictionary<string, string> fields)
        {
            var txt = "";
            foreach (var field in fields)
            {
                txt += "model." + field.Key + ",";
            }
            char[] trim = { ',' };
            return txt.Trim(trim);
        }

        public static string Paginator(Dictionary<string, string> fields)
        {
            var txt = "";
            foreach (var field in fields)
            {
                txt += FirstToLower(field.Key) + " = ViewBag." + FirstToLower(field.Key) + ", " + Environment.NewLine;
            }
            char[] trim = { ',' };
            return txt.Trim(trim);
        }

        public static string Params(Dictionary<string, string> fields)
        {
            var txt = "";
            foreach (var field in fields)
            {
                var def = "";
                var fv = field.Value;
                switch (field.Value)
                {
                    case "string":
                        def = "''";
                        break;
                    case "Guid":
                        def = "''";
                        fv = "string";
                        break;
                    case "int":
                    case "decimal":
                    case "short":
                    case "tinyint":
                    case "long":
                    case "double":
                        def = "0";
                        break;
                    case "bool":
                        fv = "bool?";
                        def = "null";
                        break;
                    case "DateTime":
                        def = "null";
                        fv = "DateTime?";
                        break;

                    default:
                        def = "null";
                        fv = field.Value + "?";
                        break;
                }
                txt += fv + " " + FirstToLower(field.Key) + " = " + def + ",";
            }
            char[] trim = { ',' };
            return txt.Trim(trim);
        }

        public static string Properties(Dictionary<string, string> fields, int mode = 1)
        {
            //mode 2 is model
            //add a string for obkect

            var txt = "";
            foreach (var field in fields)
            {
                txt += @"/// <summary>
            /// 
            /// </summary>";
                txt += Environment.NewLine + "public " + field.Value + " " + field.Key + " { get; set; }" + Environment.NewLine;

                if (mode == 2)
                {
                    if (field.Key.EndsWith("Id"))
                    {
                        var b = field.Key.Substring(0, field.Key.Length - 2) + "Name";

                        txt += @"/// <summary>
            /// 
            /// </summary>";
                        txt += Environment.NewLine + "public string " + b + " { get; set; }" + Environment.NewLine;
                    }


                    if (field.Key.EndsWith("id"))
                    {
                        var b = field.Key.Substring(0, field.Key.Length - 2) + "name";

                        txt += @"/// <summary>
            /// 
            /// </summary>";
                        txt += Environment.NewLine + "public string " + b + " { get; set; }" + Environment.NewLine;
                    }
                }
            }
            char[] trim = { ',' };
            return txt.Trim(trim);
        }

        public static string Queryable(Dictionary<string, string> fields)
        {
            var temp1 = @"if (!string.IsNullOrEmpty(%k%))
            {
                table = table.Where(x => x.%K% == %k%);
            }";
            var temp2 = @"if (%k% > 0)
            {
                table = table.Where(x => x.%K% == %k%);
            }";
            var temp3 = @"if (%k%.HasValue)
            {
                var %k%Val =  %k%.GetValueOrDefault();
                table = table.Where(x => x.%K% == %k%Val);
            }";


            var txt = "";
            foreach (var field in fields)
            {
                var temp = "";
                switch (field.Value)
                {
                    case "string":
                        temp = temp1;
                        break;
                    case "int":
                    case "long":
                    case "decimal":
                    case "double":
                        temp = temp2;
                        break;
                    case "bool":
                    case "DateTime":
                        temp = temp3;
                        break;
                    default:
                        temp = temp3;
                        break;
                }
                var t = temp.Replace("%k%", FirstToLower(field.Key));
                t = t.Replace("%K%", field.Key) + Environment.NewLine;

                txt += t;
            }
            char[] trim = { ',' };
            return txt.Trim(trim);
        }

        public static string QueryableView(Dictionary<string, string> fields)
        {
            var temp1 = @"if (!string.IsNullOrEmpty(%k%))
            {
                sql += $' and %K% = @{c} ';
                AddParam('%k%', %k%);
                c++;
            }";
            var temp2 = @"if (%k% > 0)
            {
                sql += $' and %K% = @{c} ';
                AddParam('%k%', %k%);
                c++;
            }";
            var temp3 = @"if (%k%.HasValue)
            {
                var %k%Val =  %k%.GetValueOrDefault();
                sql += $' and %K% = @{c} ';
                AddParam('%k%', %k%Val);
                c++;
            }";


            var txt = "";
            foreach (var field in fields)
            {
                var temp = "";
                switch (field.Value)
                {
                    case "string":
                        temp = temp1;
                        break;
                    case "int":
                    case "short":
                    case "long":
                    case "tinyint":
                    case "decimal":
                    case "double":
                        temp = temp2;
                        break;
                    case "bool":
                    case "DateTime":
                        temp = temp3;
                        break;
                    default:
                        temp = temp3;
                        break;
                }
                var t = temp.Replace("%k%", FirstToLower(field.Key));
                t = t.Replace("%K%", field.Key) + Environment.NewLine;

                txt += t;
            }
            char[] trim = { ',' };
            return txt.Trim(trim);
        }

        public static string QueryString(Dictionary<string, string> fields)
        {
            var txt = "";
            foreach (var field in fields)
            {
                txt += FirstToLower(field.Key) + " = " + FirstToLower(field.Key) + ", " + Environment.NewLine;
            }
            char[] trim = { ',' };
            return txt.Trim(trim);
        }

        public static string SaveFields(Dictionary<string, string> fields, string formats)
        {
            var temp1 = @"<div class='form-group col-sm-12 col-md-6 col-lg-3'>
                            @Html.LabelFor(model => model.%K%, htmlAttributes: new { @class = 'req control-label' })
                            @Html.EditorFor(model => model.%K%, new { htmlAttributes = new { @class = 'form-control' } })
                        </div> ";
            try
            {
                temp1 = File.ReadAllText(formats + "saveformitem.ini");
            }
            catch
            {
                temp1 = @"<div class='form-group col-sm-12 col-md-6 col-lg-3'>
                            @Html.LabelFor(model => model.%K%, htmlAttributes: new { @class = 'req control-label' })
                            @Html.EditorFor(model => model.%K%, new { htmlAttributes = new { @class = 'form-control' } })
                        </div> ";
            }

            var txt = "";
            foreach (var field in fields)
            {
                txt += temp1.Replace("%K%", field.Key) + Environment.NewLine;
            }
            char[] trim = { ',' };
            return txt.Trim(trim);
        }

        public static string SearchFields(Dictionary<string, string> fields, string formats)
        {
            var temp1 = @" 
                        <div class='form-group'>
                            <label>%K%</label>
                            <input type='text' name='%k%' id='%k%' value='@ViewBag.%k%' placeholder='%K%' class='form-control'>
                        </div>
                   ";
            try
            {
                temp1 = File.ReadAllText(formats + "indexformitem.ini");
            }
            catch
            {
                temp1 = @" 
                        <div class='form-group'>
                            <label>%K%</label>
                            <input type='text' name='%k%' id='%k%' value='@ViewBag.%k%' placeholder='%K%' class='form-control'>
                        </div>
                   ";
            }

            var txt = "";
            foreach (var field in fields)
            {
                var t = temp1.Replace("%k%", FirstToLower(field.Key));
                t = t.Replace("%K%", field.Key) + Environment.NewLine;
                txt += t;
            }
            char[] trim = { ',' };
            return txt.Trim(trim);
        }

        public static string TableColumns(Dictionary<string, string> fields)
        {
            var txt = "";
            foreach (var field in fields)
            { 

                if (Program.mycase == "lower")
                {
                    txt += Environment.NewLine + "{'data': '" + field.Key.ToLower() + "' },";
                }
                else
                {
                    txt += Environment.NewLine + "{'data': '" + field.Key + "' },";

                }

            }

            if (Program.mycase == "lower")
            {
                txt += Environment.NewLine + "{'data': 'recordstatustext' },";
                txt += Environment.NewLine + "{'data': 'createdattext' },";
                txt += Environment.NewLine + "{'data': 'updatedattext' },";
            }
            else
            {
                txt += Environment.NewLine + "{'data': 'RecordStatusText' },";
                txt += Environment.NewLine + "{'data': 'CreatedAtText' },";
                txt += Environment.NewLine + "{'data': 'UpdatedAtText' },";

            }

            char[] trim = { ',' };
            return txt.Trim(trim);
        }

        public static string TableHeader(Dictionary<string, string> fields)
        {
            var temp1 = @" <th>%K%</th> ";

            var txt = "";
            foreach (var field in fields)
            {
                txt += getHeader(field.Key, temp1);
            }
            txt += getHeader("Status", temp1);
            txt += getHeader("Created", temp1);
            txt += getHeader("Updated", temp1);


            char[] trim = { ',' };
            return txt.Trim(trim);
        }

        public static string TableParams(Dictionary<string, string> _fields, string idType, string db)
        {
            var txt = "";

            var _id = "Id";

            if (Program.mycase == "lower")
            {
                _id = "id";
            } 

            var fields = new Dictionary<string, string>
            {
                { _id, idType }
            };

            foreach (var _f in _fields)
            {
                fields.Add(_f.Key, _f.Value);
            }

            if (db == "SQLSERVER")
            {
                foreach (var field in fields)
                {
                    var def = "";
                    var fv = field.Value;
                    switch (field.Value)
                    {
                        case "string":
                            def = "[nvarchar](64) NULL";
                            break;
                        case "Guid":
                            def = "uuid";
                            break;
                        case "short":
                            def = "smallint";
                            break;
                        case "int":
                            def = "int";
                            break;
                        case "int?":
                            def = "int NULL";
                            break;
                        case "decimal":
                            def = "decimal(18,2) NOT NULL";
                            break;
                        case "long":
                            def = "bigint";
                            break;
                        case "long?":
                            def = "bigint NULL";
                            break;
                        case "double":
                            def = "float NOT NULL";
                            break;
                        case "bool":
                            def = "bit NOT NULL";
                            break;
                        case "DateTime":
                            def = "datetime NOT NULL";
                            break;
                        case "DateTime?":
                            def = "datetime NULL";
                            break;

                        default:
                            def = field.Value;
                            break;
                    }

                    if (field.Key.ToLower() == "id")
                    {
                        if (field.Value.ToLower() == "int" || field.Value.ToLower() == "long" || field.Value.ToLower() == "short")
                        {

                            def += " NOT NULL IDENTITY(1,1)";

                        }
                    }

                    txt += "'" + field.Key.ToLower() + "' " + def + "," + Environment.NewLine;
                }
            }

            if (db == "MYSQL")
            {
                foreach (var field in fields)
                {
                    var def = "";
                    var fv = field.Value;
                    switch (field.Value)
                    {
                        case "string":
                            def = "varchar(64) NULL";
                            break;
                        case "Guid":
                            def = "uuid";
                            break;
                        case "short":
                            def = "smallint";
                            break;
                        case "int":
                            def = "int";
                            break;
                        case "int?":
                            def = "int NULL";
                            break;
                        case "decimal":
                            def = "decimal(18,2) NOT NULL";
                            break;
                        case "long":
                            def = "bigint";
                            break;
                        case "long?":
                            def = "bigint NULL";
                            break;
                        case "double":
                            def = "float NOT NULL";
                            break;
                        case "bool":
                            def = "bit NOT NULL";
                            break;
                        case "DateTime":
                            def = "datetime NOT NULL";
                            break;
                        case "DateTime?":
                            def = "datetime NULL";
                            break;

                        default:
                            def = field.Value;
                            break;
                    }

                    if (field.Key.ToLower() == "id")
                    {
                        if (field.Value.ToLower() == "int" || field.Value.ToLower() == "long" || field.Value.ToLower() == "short")
                        {

                            def += " NOT NULL AUTO_INCREMENT";

                        }
                    }

                    txt += "" + field.Key.ToLower() + " " + def + "," + Environment.NewLine;
                }
            }

            if (db == "POSTGRES")
            {
                foreach (var field in fields)
                {
                    var def = "";
                    var fv = field.Value;
                    switch (field.Value)
                    {
                        case "string":
                            def = "character varying(128)";
                            break;
                        case "Guid":
                            def = "uuid";
                            break;
                        case "short":
                            def = "smallint";
                            break;
                        case "int":
                            def = "integer";
                            break;
                        case "int?":
                            def = "integerv NULL";
                            break;
                        case "decimal":
                            def = "decimal(18,2) NOT NULL";
                            break;
                        case "long":
                            def = "bigint";
                            break;
                        case "long?":
                            def = "bigint NULL";
                            break;
                        case "double":
                            def = "double precision";
                            break;
                        case "bool":
                            def = "boolean";
                            break;
                        case "bool?":
                            def = "boolean NULL";
                            break;
                        case "DateTime":
                            def = "timestamp without time zone";
                            break;
                        case "DateTime?":
                            def = "timestamp without time zone NULL";
                            break;

                        default:
                            def = field.Value;
                            break;
                    }

                    if (field.Key.ToLower() == "id")
                    {
                        if (field.Value.ToLower() == "int")
                        {

                            def = "serial NOT NULL";

                        }

                        if (field.Value.ToLower() == "long")
                        {

                            def = "bigserial NOT NULL";

                        }

                        if (field.Value.ToLower() == "short")
                        {

                            def = "smallserial NOT NULL";

                        }
                    }

                    txt += "" + field.Key.ToLower() + " " + def + "," + Environment.NewLine;
                }
            }


            return txt;
        }

        public static string TableRow(Dictionary<string, string> fields)
        {
            var temp1 = @"  <td data-label='%K%'>
                                        @Html.DisplayFor(modelItem => item.%K%)
                                    </td >";

            var txt = "";
            foreach (var field in fields)
            {
                var t = "";

                if (Program.mycase == "lower")
                {
                    t = temp1.Replace("%k%",field.Key.ToLower());
                    t = t.Replace("%K%", field.Key.ToLower()) + Environment.NewLine;
                }
                else
                {
                    t = temp1.Replace("%k%", FirstToLower(field.Key));
                    t = t.Replace("%K%", field.Key) + Environment.NewLine;
                }



                txt += t;
            }
            char[] trim = { ',' };
            return txt.Trim(trim);
        }

        public static Dictionary<string, string> Transform(string v4)
        {
            //"FIELD:TYPE;"
            var fields = new Dictionary<string, string>();
            try
            {
                if (string.IsNullOrEmpty(v4))
                {
                    fields.Add("name", "string");
                    return fields;
                }
                char[] sep1 = { ';' };

                var bit1 = v4.Split(sep1, StringSplitOptions.RemoveEmptyEntries);
                if (bit1.Length <= 0) return fields;
                char[] sep2 = { ':' };

                foreach (var bit in bit1)
                {
                    var bit2 = bit.Split(sep2, StringSplitOptions.RemoveEmptyEntries);
                    if (bit2.Count() == 1)
                    {
                        fields.Add(bit2[0], "string");
                    }

                    if (bit2.Count() == 2)
                    {
                        fields.Add(bit2[0], bit2[1]);
                    }
                }
            }
            catch { }
            return fields;
        }
        public static string UpperVariables(Dictionary<string, string> fields)
        {
            var txt = "";
            foreach (var field in fields)
            {
                txt += field.Key + ",";
            }
            char[] trim = { ',' };
            return txt.Trim(trim);
        }

        public static string Variables(Dictionary<string, string> fields)
        {
            var txt = "";
            foreach (var field in fields)
            { 
                txt += FirstToLower(field.Key) + ",";
            }
            char[] trim = { ','};
            return txt.Trim(trim);
        }
        public static string Variables(Dictionary<string, string> fields, string dot)
        {
            var txt = "";
            foreach (var field in fields)
            {
                txt += dot + "." + field.Key + ",";
            }
            char[] trim = { ',' };
            return txt.Trim(trim).ToLower();
        }
        public static string ViewBag(Dictionary<string, string> fields)
        {
            var txt = "";
            foreach (var field in fields)
            {
                txt += "ViewBag." + FirstToLower(field.Key) + " = " + FirstToLower(field.Key) + ";" + Environment.NewLine;
            }
            char[] trim = { ',' };
            return txt.Trim(trim);
        }
    }

}