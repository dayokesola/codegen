using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Microsoft.SqlServer.Server;
using System.Reflection;

namespace nboni.CodeGen
{

    public static class Stringer
    {
        public static string DetailView(Dictionary<string, string> fields, string formats)
        {
            var temp0 = @"      <dt class='col-sm-4'>%K%</dt>
                                <dd class='col-sm-8'>{{ form.%K% }}</dd>";
            var temp1 = @"";
            try
            {
                temp1 = File.ReadAllText(formats + "detailview.ini");
            }
            catch
            {
                temp1 = temp0;
            }

            var txt = "";
            foreach (var field in fields)
            {
                if (Program.mycase == "lower")
                {
                    txt += temp1.Replace("%K%", field.Key.ToLower()) + Environment.NewLine;
                }
                else
                {
                    txt += temp1.Replace("%K%", FirstToLower(field.Key)) + Environment.NewLine;
                }
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

        public static string FieldDataList(Dictionary<string, string> fields)
        {
            var txt = "";
            foreach (var field in fields)
            {
                txt += field.Key + ",";
            }
            char[] trim = { ',' };
            return txt.Trim(trim);
        }

        public static string FieldDataCode(Dictionary<string, string> fields)
        {
            var txt = ""; 
            foreach (var field in fields)
            { 
                switch (field.Value)
                {
                    case "string":
                    case "Guid":
                    case "double":
                    case "bool": 
                        txt += "0";
                        break; 
                    case "short":
                    case "int":
                    case "int?":
                    case "long":
                    case "long?":
                        txt += "1";  
                        break;
                    case "decimal":
                        txt += "2";
                        break;  
                    case "DateTime":
                    case "DateRangeFilter":
                    case "DateTime?":
                        txt += "3";
                        break;  
                    default:
                        txt += "0";
                        break;
                } 
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

        public static string JSModel(Dictionary<string, string> fields, string idtype)
        {
            var txt = "Id: 0,";
            
            if(idtype == "Guid")
            {
                txt = "Id: '00000000-0000-0000-0000-000000000000',";
            }
            
            if (Program.mycase == "lower")
            {
                txt = txt.ToLower();
            }
            foreach (var field in fields)
            {
                if (Program.mycase == "lower")
                {
                    switch (field.Value)
                    {
                        case "Guid":
                            txt += Environment.NewLine + field.Key.ToLower() + ": '00000000-0000-0000-0000-000000000000',";
                            break;  
                        case "int":
                        case "long":
                        case "int?":
                        case "long?":
                        case "short":
                        case "short?":
                            txt += Environment.NewLine + field.Key.ToLower() + ": 0,";
                            break;
                        case "DateRangeFilter":
                            txt += Environment.NewLine + field.Key.ToLower() + ": '',";
                            txt += Environment.NewLine + field.Key.ToLower() + "2: '',";
                            break;
                        default:
                            txt += Environment.NewLine + field.Key.ToLower() + ": '',";
                            break;
                    }

                }
                else
                {

                    switch (field.Value)
                    {
                        case "Guid":
                            txt += Environment.NewLine + FirstToLower(field.Key) + ": '00000000-0000-0000-0000-000000000000',";
                            break;
                        case "int":
                        case "long":
                        case "int?":
                        case "long?":
                        case "short":
                        case "short?":
                            txt += Environment.NewLine + FirstToLower(field.Key) + ": 0,";
                            break;
                        case "DateRangeFilter":
                            txt += Environment.NewLine + FirstToLower(field.Key) + ": '',";
                            txt += Environment.NewLine + FirstToLower(field.Key) + "2: '',";
                            break;
                        default:
                            txt += Environment.NewLine + FirstToLower(field.Key) + ": '',";
                            break;
                    } 
                }
            }
            char[] trim = { ',' };
            return txt.Trim(trim);
        }

        public static string MappingColumns(Dictionary<string, string> fields)
        {
            var txt = "";
            foreach (var field in fields)
            {
                txt += $"x.Column(y => y.{field.Key}).WithName('{field.Key.ToLower()}');" + Environment.NewLine;
            }
            return txt;
        }

        public static string MigrationMapper(Dictionary<string, string> fields)
        {
            var txt = "";

            //column names
            foreach (var field in fields)
            {
                txt += $"builder.Property(o => o.{field.Key}).HasColumnName('{field.Key.ToLower()}'); " + Environment.NewLine;

            }
            txt += Environment.NewLine;

            //string columns
            foreach (var field in fields)
            {
                switch (field.Value)
                {
                    case "string":
                        txt += "builder.Property(o => o." + field.Key + ").HasColumnType(\"varchar\").HasMaxLength(128);" + Environment.NewLine;
                        break;
                }
            }

            txt += Environment.NewLine;

            //index id columns
            foreach (var field in fields)
            {
                if (field.Key.ToLower() == "id")
                {
                    if (field.Value.ToLower() == "int" || field.Value.ToLower() == "long" || field.Value.ToLower() == "short")
                    {
                    }
                    else
                    {
                        txt += "builder.Property(o => o.id).HasDefaultValueSql(\"UUID()\"); " + Environment.NewLine;
                    }
                }
                else if (field.Key.ToLower().EndsWith("id"))
                {
                    txt += "builder.HasIndex(o => o." + field.Key + "); " + Environment.NewLine;
                }
                else { }
            }
            return txt;
        }
        public static string ModelVariables(Dictionary<string, string> fields)
        {
            var txt = "";
            foreach (var field in fields)
            {
                if (field.Value.ToLower() == "daterangefilter")
                {
                    txt += "model." + field.Key + ".ToDateRangeFilter(),";
                }
                else
                {
                    txt += "model." + field.Key + ",";
                }
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

        public static string Params(Dictionary<string, string> fields, int mode = 1)
        {
            var txt = "";
            foreach (var field in fields)
            {
                var def = "";
                var fv = field.Value;
                var nuf = "";
                switch (field.Value)
                {
                    case "string":
                        def = "''";
                        break;
                    case "Guid":
                        def = "null";
                        fv = "Guid?";
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
                    case "DateRangeFilter":
                        def = "null";
                        fv = "DateRangeFilter";
                        nuf = "DateTime?";
                        break;
                    default:
                        def = "null";
                        fv = field.Value + "?";
                        break;
                }

                if (field.Value == "DateRangeFilter" && mode == 3)
                {

                    txt += nuf + " " + FirstToLower(field.Key) + " = " + def + ",";
                    txt += nuf + " " + FirstToLower(field.Key) + "2 = " + def + ",";
                }
                else
                {
                    txt += fv + " " + FirstToLower(field.Key) + " = " + def + ",";
                }
            }
            char[] trim = { ',' };
            return txt.Trim(trim);
        }

        public static string Properties(Dictionary<string, string> fields, int mode = 1)
        {
            //mode 2 is model
            //add a string for obkect

            var txt = "";
            var defstring = " = string.Empty;";
            foreach (var field in fields)
            {
                var def = "";
                if (field.Value.ToLower() == "string") def = defstring;

                var datatype = field.Value;
                if (field.Value.ToLower() == "daterangefilter")
                {
                    datatype = "DateTime";
                }

                txt += @"
        /// <summary>
        /// 
        /// </summary>";
                txt += Environment.NewLine +  
                    "\t\tpublic " + datatype + " " + field.Key + " { get; set; }" + def + Environment.NewLine + Environment.NewLine;

                if (field.Value.ToLower() == "daterangefilter" && mode == 3)
                {
                    txt += @"
        /// <summary>
        /// 
        /// </summary>";
                    txt += Environment.NewLine + 
                        "\t\tpublic " + datatype + " " + field.Key + "2 { get; set; }" + def + Environment.NewLine + Environment.NewLine;
                }

                if (mode == 2)
                {
                    if (field.Key.EndsWith("Id"))
                    {
                        var b = field.Key.Substring(0, field.Key.Length - 2) + "Name";

                        txt += @"/// <summary>
            /// 
            /// </summary>";
                        txt += Environment.NewLine + "\t\tpublic string " + b + " { get; set; }" + Environment.NewLine + Environment.NewLine;
                    }


                    if (field.Key.EndsWith("id"))
                    {
                        var b = field.Key.Substring(0, field.Key.Length - 2) + "name";

                        txt += @"/// <summary>
            /// 
            /// </summary>";
                        txt += Environment.NewLine + "\t\tpublic string " + b + " { get; set; }" + Environment.NewLine + Environment.NewLine;
                    }
                }
            }
            char[] trim = { ',' };
            return txt.Trim(trim);
        }
        public static string PropertiesNpoco(Dictionary<string, string> fields, int mode = 1)
        {
            //mode 2 is model
            //add a string for obkect

            var txt = "";
            var defstring = " = string.Empty;";
            foreach (var field in fields)
            {
                var def = "";
                if (field.Value.ToLower() == "string") def = defstring;

                var datatype = field.Value;
                if (field.Value.ToLower() == "daterangefilter")
                {
                    datatype = "DateTime";
                }

                txt += @"
        /// <summary>
        /// 
        /// </summary>";
                txt += Environment.NewLine + "\t\t[Column('" + field.Key.ToLower() + "')]" + Environment.NewLine +
                    "\t\tpublic " + datatype + " " + field.Key + " { get; set; }" + def + Environment.NewLine + Environment.NewLine;

                if (field.Value.ToLower() == "daterangefilter" && mode == 3)
                {
                    txt += @"
        /// <summary>
        /// 
        /// </summary>";
                    txt += Environment.NewLine + "\t\t[Column('" + field.Key.ToLower() + "')]" + Environment.NewLine +
                        "\t\tpublic " + datatype + " " + field.Key + "2 { get; set; }" + def + Environment.NewLine + Environment.NewLine;
                }

                if (mode == 2)
                {
                    if (field.Key.EndsWith("Id"))
                    {
                        var b = field.Key.Substring(0, field.Key.Length - 2) + "Name";

                        txt += @"/// <summary>
            /// 
            /// </summary>";
                        txt += Environment.NewLine + "\t\t[Column('" + b.ToLower() + "')]" + Environment.NewLine +
                            "\t\tpublic string " + b + " { get; set; }" + Environment.NewLine + Environment.NewLine;
                    }


                    if (field.Key.EndsWith("id"))
                    {
                        var b = field.Key.Substring(0, field.Key.Length - 2) + "name";

                        txt += @"/// <summary>
            /// 
            /// </summary>";
                        txt += Environment.NewLine + "\t\t[Column('" + b.ToLower() + "')]" + Environment.NewLine +
                             "\t\tpublic string " + b + " { get; set; } = string.Empty;" + Environment.NewLine + Environment.NewLine;
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
            var temp4 = @"if (%k%.startdate.HasValue)
            {
                var %k%SdVal =  %k%.startdate.GetValueOrDefault();
                table = table.Where(x => x.%K% >= %k%SdVal);
            }

            if (%k%.enddate.HasValue)
            {
                var %k%EdVal =  %k%.enddate.GetValueOrDefault().AddDays(1);
                table = table.Where(x => x.%K% < %k%EdVal);
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
                    case "DateRangeFilter":
                        temp = temp4;
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

        public static string QueryableView(Dictionary<string, string> fields, string formats)
        {
            var temp1 = @"if (!string.IsNullOrEmpty(%k%))
            {
                sql += $' and %K% = @{c} ';
                AddParam('%k%', %k%);
                c++;
            }";
            if (File.Exists(formats + "filter.queryable.string.ini"))
            {
                temp1 = File.ReadAllText(formats + "filter.queryable.string.ini");
            }


            var temp2 = @"if (%k% > 0)
            {
                sql += $' and %K% = @{c} ';
                AddParam('%k%', %k%);
                c++;
            }";
            if (File.Exists(formats + "filter.queryable.number.ini"))
            {
                temp2 = File.ReadAllText(formats + "filter.queryable.number.ini");
            }

            var temp3 = @"if (%k%.HasValue)
            {
                var %k%Val =  %k%.GetValueOrDefault();
                sql += $' and %K% = @{c} ';
                AddParam('%k%', %k%Val);
                c++;
            }";

            if (File.Exists(formats + "filter.queryable.nullable.ini"))
            {
                temp3 = File.ReadAllText(formats + "filter.queryable.nullable.ini");
            }


            var temp4 = @"if (%k%.startdate.HasValue)
            {
                var %k%SdVal =  %k%.startdate.GetValueOrDefault();
                sql += $' and %K% >= @{c} ';
                AddParam('%k%', %k%SdVal);
                c++;
            }

            if (%k%.enddate.HasValue)
            {
                var %k%EdVal =  %k%.enddate.GetValueOrDefault().AddDays(1);
                sql += $' and %K% < @{c} ';
                AddParam('%k%', %k%EdVal);
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
                    case "DateRangeFilter":
                        temp = temp4;
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

        public static string QueryJoins(Dictionary<string, string> fields)
        {
            int cnt = 1;
            var txt = "";

            var joina = "";
            foreach (var field in fields)
            {
                var fx = field.Key.ToLower();
                if (fx.EndsWith("id"))
                {
                    var b = fx.Substring(0, field.Key.Length - 2) + "name";

                    joina += "LEFT JOIN <JOINTABLE> x" + cnt + " ON  x" + cnt + ".<JOINID> = w." + fx + Environment.NewLine;
                    cnt++;
                }
            }
            char[] trim = { ',' };
            return txt.Trim(trim).ToLower() + Environment.NewLine + joina;
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
                if (Program.mycase == "lower")
                {
                    txt += temp1.Replace("%K%", field.Key) + Environment.NewLine;
                }
                else
                {
                    txt += temp1.Replace("%K%", FirstToLower(field.Key)) + Environment.NewLine;
                }
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

            var temp3 = @" 
                        <div class='form-group'>
                            <label>%K%</label>
                            <input type='text' name='%k%2' id='%k%2' value='@ViewBag.%k%2' placeholder='%K%2' class='form-control'>
                        </div>
                   ";
            try
            {
                temp1 = File.ReadAllText(formats + "indexformitem.ini"); 
                temp3 = File.ReadAllText(formats + "indexformitem3.ini");
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
                var fk = field.Key;
                if (Program.mycase != "lower")
                {
                    fk = FirstToLower(fk);
                }
                var t = "";
                var t3 = "";

                if (field.Value == "DateRangeFilter")
                {
                    t = temp1.Replace("%k%", FirstToLower(fk));
                    t = t.Replace("%K%", fk) + Environment.NewLine;

                    t3 = temp3.Replace("%k%", FirstToLower(fk));
                    t3= t3.Replace("%K%", fk) + Environment.NewLine;
                }
                else
                {
                    t = temp1.Replace("%k%", FirstToLower(fk));
                    t = t.Replace("%K%", fk) + Environment.NewLine;
                }
                txt += t + t3;
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
                    txt += Environment.NewLine + "{'data': '" + FirstToLower(field.Key) + "' },";

                }

            }

            if (Program.mycase == "lower")
            {
                txt += Environment.NewLine + "{'data': 'recordstatustext' },";
                txt += Environment.NewLine + "{'data': 'createdat' },";
                txt += Environment.NewLine + "{'data': 'updatedat' },";
            }
            else
            {
                txt += Environment.NewLine + "{'data': 'RecordStatusText' },";
                txt += Environment.NewLine + "{'data': 'CreatedAt' },";
                txt += Environment.NewLine + "{'data': 'UpdatedAt' },";

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
                        case "DateRangeFilter":
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
                            def = "varchar(128) NULL";
                            break;
                        case "Guid":
                            def = "CHAR(36)";
                            break;
                        case "short":
                            def = "tinyint";
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
                        case "DateRangeFilter":
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
                        case "DateRangeFilter":
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

                    txt += "\t" + field.Key.ToLower() + " " + def + "," + Environment.NewLine;
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
                    t = temp1.Replace("%k%", field.Key.ToLower());
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
            char[] trim = { ',' };
            return txt.Trim(trim);
        }

        public static string Variables3(Dictionary<string, string> fields)
        {
            var txt = "";
            foreach (var field in fields)
            {
                if (field.Value == "DateRangeFilter")
                {

                    txt += "new DateRangeFilter(){ startdate = " + FirstToLower(field.Key) + ", enddate = " + FirstToLower(field.Key) + "2 },";
                }
                else
                {
                    txt += FirstToLower(field.Key) + ",";
                }
            }
            char[] trim = { ',' };
            return txt.Trim(trim);
        }
        public static string Variables(Dictionary<string, string> fields, string dot)
        {
            int cnt = 1;
            var txt = "";

            foreach (var field in fields)
            {
                txt += dot + "." + field.Key.ToLower() + ",";

                var fx = field.Key.ToLower();
                if (fx.EndsWith("id"))
                {
                    var b = fx.Substring(0, field.Key.Length - 2) + "name";

                    txt += "x" + cnt + ".<JOINFIELD> as " + b + ",";
                    cnt++;
                }
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

        public static string ID_Query(string idtype)
        {
            if (idtype.ToLower() == "guid") return " id is not null ";
            else return " id > 0 ";
        }

        public static string ID_Param(string idtype)
        {
            if (idtype.ToLower() == "guid") return " Guid? id = null";
            else return " %T% id = 0";
        }
        public static string ID_Filter(string idtype)
        {
            if (idtype.ToLower() == "guid") return "id != null";
            else return "id > 0";
        }

        public static string ID_Generate(string idtype)
        {
            if (idtype.ToLower() == "guid") return "entity.id = Guid.NewGuid()";
            else return "entity.id = 0";
        }
        public static string ID_Setup(string idtype)
        {
            if (idtype.ToLower() == "guid") return "false";
            else return "true";
        }
    }

}