using System;
using System.Linq;
using System.Collections.Generic;
using System.IO;

namespace nboni.CodeGen
{

    public static class Stringer
    {
        public static string DetailView(Dictionary<string, string> fields, string formats)
        {
            string temp0 = "      <dt class='col-sm-4'>%K%</dt>\r\n                                <dd class='col-sm-8'>{{ form.%K% }}</dd>";
            string temp1 = "";
            try
            {
                temp1 = File.ReadAllText(formats + "detailview.ini");
            }
            catch
            {
                temp1 = temp0;
            }
            string txt = "";
            foreach (KeyValuePair<string, string> field in fields)
            {
                string t0 = temp1.Replace("%k%", FirstToLower(field.Key));
                txt = txt + t0.Replace("%K%", field.Key) + Environment.NewLine;
            }
            char[] trim = new char[1] { ',' };
            return txt.Trim(trim);
        }

        public static string Document(Dictionary<string, string> fields)
        {
            string txt = "";
            foreach (KeyValuePair<string, string> field in fields)
            {
                txt = txt + "/// <param name='" + FirstToLower(field.Key) + "'></param>" + Environment.NewLine;
            }
            char[] trim = new char[1] { ',' };
            return txt.Trim(trim);
        }

        public static string FactorMapper(Dictionary<string, string> fields)
        {
            string txt = "";
            foreach (KeyValuePair<string, string> field in fields)
            {
                txt = txt + field.Key + " = obj." + field.Key + "," + Environment.NewLine;
            }
            return txt;
        }

        public static string FieldDataList(Dictionary<string, string> fields)
        {
            string txt = "";
            foreach (KeyValuePair<string, string> field2 in fields)
            {
                txt = txt + field2.Key + ",";
            }
            char[] trim = new char[1] { ',' };
            return txt.Trim(trim);
        }

        public static string FieldDataCode(Dictionary<string, string> fields)
        {
            string txt = "";
            using (Dictionary<string, string>.Enumerator enumerator = fields.GetEnumerator())
            {
                while (enumerator.MoveNext())
                {
                    switch (enumerator.Current.Value)
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
            string t = template.Replace("%k%", FirstToLower(field));
            return t.Replace("%K%", field) + Environment.NewLine;
        }

        public static string JObjectHelper(Dictionary<string, string> fields)
        {
            string txt = "";
            foreach (KeyValuePair<string, string> field in fields)
            {
                txt = txt + "jo.Add('" + FirstToLower(field.Key) + "', " + FirstToLower(field.Key) + ");" + Environment.NewLine;
            }
            char[] trim = new char[1] { ',' };
            return txt.Trim(trim);
        }

        public static string JSModel(Dictionary<string, string> fields, string idtype)
        {
            string txt = "id: 0,";
            if (idtype == "Guid")
            {
                txt = "id: '00000000-0000-0000-0000-000000000000',";
            }
            foreach (KeyValuePair<string, string> field in fields)
            {
                switch (field.Value)
                {
                    case "Guid":
                        txt = txt + Environment.NewLine + FirstToLower(field.Key) + ": '00000000-0000-0000-0000-000000000000',";
                        break;
                    case "int":
                    case "long":
                    case "int?":
                    case "long?":
                    case "short":
                    case "short?":
                        txt = txt + Environment.NewLine + FirstToLower(field.Key) + ": 0,";
                        break;
                    case "DateRangeFilter":
                        txt = txt + Environment.NewLine + FirstToLower(field.Key) + ": '',";
                        txt = txt + Environment.NewLine + FirstToLower(field.Key) + "2: '',";
                        break;
                    default:
                        txt = txt + Environment.NewLine + FirstToLower(field.Key) + ": '',";
                        break;
                }
            }
            char[] trim = new char[1] { ',' };
            return txt.Trim(trim);
        }

        public static string MappingColumns(Dictionary<string, string> fields)
        {
            string txt = "";
            foreach (KeyValuePair<string, string> field in fields)
            {
                txt = txt + "x.Column(y => y." + field.Key + ").WithName('" + field.Key.ToLower() + "');" + Environment.NewLine;
            }
            return txt;
        }

        public static string MigrationMapper(Dictionary<string, string> fields)
        {
            string txt = "";
            foreach (KeyValuePair<string, string> field in fields)
            {
                txt = txt + "builder.Property(o => o." + field.Key + ").HasColumnName('" + field.Key.ToLower() + "'); " + Environment.NewLine;
            }
            txt += Environment.NewLine;
            foreach (KeyValuePair<string, string> field2 in fields)
            {
                string value = field2.Value;
                string text = value;
                if (text == "string")
                {
                    txt = txt + "builder.Property(o => o." + field2.Key + ").HasColumnType(\"varchar\").HasMaxLength(128);" + Environment.NewLine;
                }
            }
            txt += Environment.NewLine;
            foreach (KeyValuePair<string, string> field3 in fields)
            {
                if (field3.Key.ToLower() == "id")
                {
                    if (!(field3.Value.ToLower() == "int") && !(field3.Value.ToLower() == "long") && !(field3.Value.ToLower() == "short"))
                    {
                        txt = txt + "builder.Property(o => o.id).HasDefaultValueSql(\"UUID()\"); " + Environment.NewLine;
                    }
                }
                else if (field3.Key.ToLower().EndsWith("id"))
                {
                    txt = txt + "builder.HasIndex(o => o." + field3.Key + "); " + Environment.NewLine;
                }
            }
            return txt;
        }

        public static string ModelVariables(Dictionary<string, string> fields)
        {
            string txt = "";
            foreach (KeyValuePair<string, string> field in fields)
            {
                txt = ((!(field.Value.ToLower() == "daterangefilter")) ? (txt + "model." + field.Key + ",") : (txt + "model." + field.Key + ".ToDateRangeFilter(),"));
            }
            char[] trim = new char[1] { ',' };
            return txt.Trim(trim);
        }

        public static string Paginator(Dictionary<string, string> fields)
        {
            string txt = "";
            foreach (KeyValuePair<string, string> field in fields)
            {
                txt = txt + FirstToLower(field.Key) + " = ViewBag." + FirstToLower(field.Key) + ", " + Environment.NewLine;
            }
            char[] trim = new char[1] { ',' };
            return txt.Trim(trim);
        }

        public static string Params(Dictionary<string, string> fields, int mode = 1)
        {
            string txt = "";
            foreach (KeyValuePair<string, string> field in fields)
            {
                string def = "";
                string fv = field.Value;
                string nuf = "";
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
                    txt = txt + nuf + " " + FirstToLower(field.Key) + " = " + def + ",";
                    txt = txt + nuf + " " + FirstToLower(field.Key) + "2 = " + def + ",";
                }
                else
                {
                    txt = txt + fv + " " + FirstToLower(field.Key) + " = " + def + ",";
                }
            }
            char[] trim = new char[1] { ',' };
            return txt.Trim(trim);
        }

        public static string Properties(Dictionary<string, string> fields, int mode = 1)
        {
            string txt = "";
            string defstring = " = string.Empty;";
            foreach (KeyValuePair<string, string> field in fields)
            {
                string def = "";
                if (field.Value.ToLower() == "string")
                {
                    def = defstring;
                }
                string datatype = field.Value;
                if (field.Value.ToLower() == "daterangefilter")
                {
                    datatype = "DateTime";
                }
                txt += "\r\n        /// <summary>\r\n        /// \r\n        /// </summary>";
                txt = txt + Environment.NewLine + "\t\tpublic " + datatype + " " + field.Key + " { get; set; }" + def + Environment.NewLine + Environment.NewLine;
                if (field.Value.ToLower() == "daterangefilter" && mode == 3)
                {
                    txt += "\r\n        /// <summary>\r\n        /// \r\n        /// </summary>";
                    txt = txt + Environment.NewLine + "\t\tpublic " + datatype + " " + field.Key + "2 { get; set; }" + def + Environment.NewLine + Environment.NewLine;
                }
                if (mode == 2)
                {
                    if (field.Key.EndsWith("Id"))
                    {
                        string b = field.Key.Substring(0, field.Key.Length - 2) + "Name";
                        txt += "/// <summary>\r\n            /// \r\n            /// </summary>";
                        txt = txt + Environment.NewLine + "\t\tpublic string " + b + " { get; set; }" + Environment.NewLine + Environment.NewLine;
                    }
                    if (field.Key.EndsWith("id"))
                    {
                        string b2 = field.Key.Substring(0, field.Key.Length - 2) + "name";
                        txt += "/// <summary>\r\n            /// \r\n            /// </summary>";
                        txt = txt + Environment.NewLine + "\t\tpublic string " + b2 + " { get; set; }" + Environment.NewLine + Environment.NewLine;
                    }
                }
            }
            char[] trim = new char[1] { ',' };
            return txt.Trim(trim);
        }

        public static string PropertiesNpoco(Dictionary<string, string> fields, int mode = 1)
        {
            string txt = "";
            string defstring = " = string.Empty;";
            foreach (KeyValuePair<string, string> field in fields)
            {
                string def = "";
                if (field.Value.ToLower() == "string")
                {
                    def = defstring;
                }
                string datatype = field.Value;
                if (field.Value.ToLower() == "daterangefilter")
                {
                    datatype = "DateTime";
                }
                txt += "\r\n        /// <summary>\r\n        /// \r\n        /// </summary>";
                txt = txt + Environment.NewLine + "\t\t[Column('" + field.Key.ToLower() + "')]" + Environment.NewLine + "\t\tpublic " + datatype + " " + field.Key + " { get; set; }" + def + Environment.NewLine + Environment.NewLine;
                if (field.Value.ToLower() == "daterangefilter" && mode == 3)
                {
                    txt += "\r\n        /// <summary>\r\n        /// \r\n        /// </summary>";
                    txt = txt + Environment.NewLine + "\t\t[Column('" + field.Key.ToLower() + "')]" + Environment.NewLine + "\t\tpublic " + datatype + " " + field.Key + "2 { get; set; }" + def + Environment.NewLine + Environment.NewLine;
                }
                if (mode == 2)
                {
                    if (field.Key.EndsWith("Id"))
                    {
                        string b = field.Key.Substring(0, field.Key.Length - 2) + "Name";
                        txt += "/// <summary>\r\n            /// \r\n            /// </summary>";
                        txt = txt + Environment.NewLine + "\t\t[Column('" + b.ToLower() + "')]" + Environment.NewLine + "\t\tpublic string " + b + " { get; set; }" + Environment.NewLine + Environment.NewLine;
                    }
                    if (field.Key.EndsWith("id"))
                    {
                        string b2 = field.Key.Substring(0, field.Key.Length - 2) + "name";
                        txt += "/// <summary>\r\n            /// \r\n            /// </summary>";
                        txt = txt + Environment.NewLine + "\t\t[Column('" + b2.ToLower() + "')]" + Environment.NewLine + "\t\tpublic string " + b2 + " { get; set; } = string.Empty;" + Environment.NewLine + Environment.NewLine;
                    }
                }
            }
            char[] trim = new char[1] { ',' };
            return txt.Trim(trim);
        }

        public static string Queryable(Dictionary<string, string> fields)
        {
            string temp1 = "if (!string.IsNullOrEmpty(%k%))\r\n            {\r\n                table = table.Where(x => x.%K% == %k%);\r\n            }";
            string temp2 = "if (%k% > 0)\r\n            {\r\n                table = table.Where(x => x.%K% == %k%);\r\n            }";
            string temp3 = "if (%k%.HasValue)\r\n            {\r\n                var %k%Val =  %k%.GetValueOrDefault();\r\n                table = table.Where(x => x.%K% == %k%Val);\r\n            }";
            string temp4 = "if (%k%.startdate.HasValue)\r\n            {\r\n                var %k%SdVal =  %k%.startdate.GetValueOrDefault();\r\n                table = table.Where(x => x.%K% >= %k%SdVal);\r\n            }\r\n\r\n            if (%k%.enddate.HasValue)\r\n            {\r\n                var %k%EdVal =  %k%.enddate.GetValueOrDefault().AddDays(1);\r\n                table = table.Where(x => x.%K% < %k%EdVal);\r\n            }";
            string txt = "";
            foreach (KeyValuePair<string, string> field in fields)
            {
                string temp5 = "";
                switch (field.Value)
                {
                    case "string":
                        temp5 = temp1;
                        break;
                    case "int":
                    case "long":
                    case "decimal":
                    case "double":
                        temp5 = temp2;
                        break;
                    case "bool":
                    case "DateTime":
                        temp5 = temp3;
                        break;
                    case "DateRangeFilter":
                        temp5 = temp4;
                        break;
                    default:
                        temp5 = temp3;
                        break;
                }
                string t = temp5.Replace("%k%", FirstToLower(field.Key));
                t = t.Replace("%K%", field.Key) + Environment.NewLine;
                txt += t;
            }
            char[] trim = new char[1] { ',' };
            return txt.Trim(trim);
        }

        public static string QueryableView(Dictionary<string, string> fields, string formats)
        {
            string temp1 = "if (!string.IsNullOrEmpty(%k%))\r\n            {\r\n                sql += $' and %K% = @{c} ';\r\n                AddParam('%k%', %k%);\r\n                c++;\r\n            }";
            if (File.Exists(formats + "filter.queryable.string.ini"))
            {
                temp1 = File.ReadAllText(formats + "filter.queryable.string.ini");
            }
            string temp2 = "if (%k% > 0)\r\n            {\r\n                sql += $' and %K% = @{c} ';\r\n                AddParam('%k%', %k%);\r\n                c++;\r\n            }";
            if (File.Exists(formats + "filter.queryable.number.ini"))
            {
                temp2 = File.ReadAllText(formats + "filter.queryable.number.ini");
            }
            string temp3 = "if (%k%.HasValue)\r\n            {\r\n                var %k%Val =  %k%.GetValueOrDefault();\r\n                sql += $' and %K% = @{c} ';\r\n                AddParam('%k%', %k%Val);\r\n                c++;\r\n            }";
            if (File.Exists(formats + "filter.queryable.nullable.ini"))
            {
                temp3 = File.ReadAllText(formats + "filter.queryable.nullable.ini");
            }
            string temp4 = "if (%k%.startdate.HasValue)\r\n            {\r\n                var %k%SdVal =  %k%.startdate.GetValueOrDefault();\r\n                sql += $' and %K% >= @{c} ';\r\n                AddParam('%k%', %k%SdVal);\r\n                c++;\r\n            }\r\n\r\n            if (%k%.enddate.HasValue)\r\n            {\r\n                var %k%EdVal =  %k%.enddate.GetValueOrDefault().AddDays(1);\r\n                sql += $' and %K% < @{c} ';\r\n                AddParam('%k%', %k%EdVal);\r\n                c++;\r\n            }";
            string txt = "";
            foreach (KeyValuePair<string, string> field in fields)
            {
                string temp5 = "";
                switch (field.Value)
                {
                    case "string":
                        temp5 = temp1;
                        break;
                    case "int":
                    case "short":
                    case "long":
                    case "tinyint":
                    case "decimal":
                    case "double":
                        temp5 = temp2;
                        break;
                    case "bool":
                    case "DateTime":
                        temp5 = temp3;
                        break;
                    case "DateRangeFilter":
                        temp5 = temp4;
                        break;
                    default:
                        temp5 = temp3;
                        break;
                }
                string t = temp5.Replace("%k%", FirstToLower(field.Key));
                t = t.Replace("%K%", field.Key) + Environment.NewLine;
                txt += t;
            }
            char[] trim = new char[1] { ',' };
            return txt.Trim(trim);
        }

        public static string QueryJoins(Dictionary<string, string> fields)
        {
            int cnt = 1;
            string txt = "";
            string joina = "";
            foreach (KeyValuePair<string, string> field in fields)
            {
                string fx = field.Key.ToLower();
                if (fx.EndsWith("id"))
                {
                    string b = fx.Substring(0, field.Key.Length - 2) + "name";
                    joina = joina + "LEFT JOIN <JOINTABLE> x" + cnt + " ON  x" + cnt + ".<JOINID> = w." + fx + Environment.NewLine;
                    cnt++;
                }
            }
            char[] trim = new char[1] { ',' };
            return txt.Trim(trim).ToLower() + Environment.NewLine + joina;
        }

        public static string QueryString(Dictionary<string, string> fields)
        {
            string txt = "";
            foreach (KeyValuePair<string, string> field in fields)
            {
                txt = txt + FirstToLower(field.Key) + " = " + FirstToLower(field.Key) + ", " + Environment.NewLine;
            }
            char[] trim = new char[1] { ',' };
            return txt.Trim(trim);
        }

        public static string SaveFields(Dictionary<string, string> fields, string formats)
        {
            string temp1 = "<div class='form-group col-sm-12 col-md-6 col-lg-3'>\r\n                            @Html.LabelFor(model => model.%K%, htmlAttributes: new { @class = 'req control-label' })\r\n                            @Html.EditorFor(model => model.%K%, new { htmlAttributes = new { @class = 'form-control' } })\r\n                        </div> ";
            try
            {
                temp1 = File.ReadAllText(formats + "saveformitem.ini");
            }
            catch
            {
                temp1 = "<div class='form-group col-sm-12 col-md-6 col-lg-3'>\r\n                            @Html.LabelFor(model => model.%K%, htmlAttributes: new { @class = 'req control-label' })\r\n                            @Html.EditorFor(model => model.%K%, new { htmlAttributes = new { @class = 'form-control' } })\r\n                        </div> ";
            }
            string txt = "";
            foreach (KeyValuePair<string, string> field in fields)
            {
                string t0 = temp1.Replace("%K%", field.Key);
                txt = txt + t0.Replace("%k%", FirstToLower(field.Key)) + Environment.NewLine;
            }
            char[] trim = new char[1] { ',' };
            return txt.Trim(trim);
        }

        public static string SearchFields(Dictionary<string, string> fields, string formats)
        {
            string temp1 = " \r\n                        <div class='form-group'>\r\n                            <label>%K%</label>\r\n                            <input type='text' name='%k%' id='%k%' value='@ViewBag.%k%' placeholder='%K%' class='form-control'>\r\n                        </div>\r\n                   ";
            string temp3 = " \r\n                        <div class='form-group'>\r\n                            <label>%K%</label>\r\n                            <input type='text' name='%k%2' id='%k%2' value='@ViewBag.%k%2' placeholder='%K%2' class='form-control'>\r\n                        </div>\r\n                   ";
            try
            {
                temp1 = File.ReadAllText(formats + "indexformitem.ini");
                temp3 = File.ReadAllText(formats + "indexformitem3.ini");
            }
            catch
            {
                temp1 = " \r\n                        <div class='form-group'>\r\n                            <label>%K%</label>\r\n                            <input type='text' name='%k%' id='%k%' value='@ViewBag.%k%' placeholder='%K%' class='form-control'>\r\n                        </div>\r\n                   ";
            }
            string txt = "";
            foreach (KeyValuePair<string, string> field in fields)
            {
                string fk = field.Key;
                if (Program.mycase != "lower")
                {
                    fk = FirstToLower(fk);
                }
                string t = "";
                string t3 = "";
                if (field.Value == "DateRangeFilter")
                {
                    t = temp1.Replace("%k%", FirstToLower(fk));
                    t = t.Replace("%K%", fk) + Environment.NewLine;
                    t3 = temp3.Replace("%k%", FirstToLower(fk));
                    t3 = t3.Replace("%K%", fk) + Environment.NewLine;
                }
                else
                {
                    t = temp1.Replace("%k%", FirstToLower(fk));
                    t = t.Replace("%K%", fk) + Environment.NewLine;
                }
                txt = txt + t + t3;
            }
            char[] trim = new char[1] { ',' };
            return txt.Trim(trim);
        }

        public static string TableColumns(Dictionary<string, string> fields)
        {
            string txt = "";
            foreach (KeyValuePair<string, string> field in fields)
            {
                txt = txt + Environment.NewLine + "{'data': '" + FirstToLower(field.Key) + "' },";
            }
            txt = txt + Environment.NewLine + "{'data': 'recordStatus' },";
            txt = txt + Environment.NewLine + "{'data': 'createdAt' },";
            txt = txt + Environment.NewLine + "{'data': 'updatedAt' },";
            char[] trim = new char[1] { ',' };
            return txt.Trim(trim);
        }

        public static string TableHeader(Dictionary<string, string> fields)
        {
            string temp1 = " <th>%K%</th> ";
            string txt = "";
            foreach (KeyValuePair<string, string> field2 in fields)
            {
                txt += getHeader(field2.Key, temp1);
            }
            txt += getHeader("Status", temp1);
            txt += getHeader("Created", temp1);
            txt += getHeader("Updated", temp1);
            char[] trim = new char[1] { ',' };
            return txt.Trim(trim);
        }

        public static string TableParams(Dictionary<string, string> _fields, string idType, string db)
        {
            string txt = "";
            string _id = "Id";
            if (Program.mycase == "lower")
            {
                _id = "id";
            }
            Dictionary<string, string> fields = new Dictionary<string, string> { { _id, idType } };
            foreach (KeyValuePair<string, string> _f in _fields)
            {
                fields.Add(_f.Key, _f.Value);
            }
            if (db == "SQLSERVER")
            {
                foreach (KeyValuePair<string, string> field in fields)
                {
                    string def = "";
                    string fv = field.Value;
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
                    if (field.Key.ToLower() == "id" && (field.Value.ToLower() == "int" || field.Value.ToLower() == "long" || field.Value.ToLower() == "short"))
                    {
                        def += " NOT NULL IDENTITY(1,1)";
                    }
                    txt = txt + "'" + field.Key.ToLower() + "' " + def + "," + Environment.NewLine;
                }
            }
            if (db == "MYSQL")
            {
                foreach (KeyValuePair<string, string> field2 in fields)
                {
                    string def2 = "";
                    string fv2 = field2.Value;
                    switch (field2.Value)
                    {
                        case "string":
                            def2 = "varchar(128) NULL";
                            break;
                        case "Guid":
                            def2 = "CHAR(36)";
                            break;
                        case "short":
                            def2 = "tinyint";
                            break;
                        case "int":
                            def2 = "int";
                            break;
                        case "int?":
                            def2 = "int NULL";
                            break;
                        case "decimal":
                            def2 = "decimal(18,2) NOT NULL";
                            break;
                        case "long":
                            def2 = "bigint";
                            break;
                        case "long?":
                            def2 = "bigint NULL";
                            break;
                        case "double":
                            def2 = "float NOT NULL";
                            break;
                        case "bool":
                            def2 = "bit NOT NULL";
                            break;
                        case "DateTime":
                        case "DateRangeFilter":
                            def2 = "datetime NOT NULL";
                            break;
                        case "DateTime?":
                            def2 = "datetime NULL";
                            break;
                        default:
                            def2 = field2.Value;
                            break;
                    }
                    if (field2.Key.ToLower() == "id" && (field2.Value.ToLower() == "int" || field2.Value.ToLower() == "long" || field2.Value.ToLower() == "short"))
                    {
                        def2 += " NOT NULL AUTO_INCREMENT";
                    }
                    txt = txt + field2.Key.ToLower() + " " + def2 + "," + Environment.NewLine;
                }
            }
            if (db == "POSTGRES")
            {
                foreach (KeyValuePair<string, string> field3 in fields)
                {
                    string def3 = "";
                    string fv3 = field3.Value;
                    switch (field3.Value)
                    {
                        case "string":
                            def3 = "character varying(128)";
                            break;
                        case "Guid":
                            def3 = "uuid";
                            break;
                        case "short":
                            def3 = "smallint";
                            break;
                        case "int":
                            def3 = "integer";
                            break;
                        case "int?":
                            def3 = "integerv NULL";
                            break;
                        case "decimal":
                            def3 = "decimal(18,2) NOT NULL";
                            break;
                        case "long":
                            def3 = "bigint";
                            break;
                        case "long?":
                            def3 = "bigint NULL";
                            break;
                        case "double":
                            def3 = "double precision";
                            break;
                        case "bool":
                            def3 = "boolean";
                            break;
                        case "bool?":
                            def3 = "boolean NULL";
                            break;
                        case "DateTime":
                        case "DateRangeFilter":
                            def3 = "timestamp without time zone";
                            break;
                        case "DateTime?":
                            def3 = "timestamp without time zone NULL";
                            break;
                        default:
                            def3 = field3.Value;
                            break;
                    }
                    if (field3.Key.ToLower() == "id")
                    {
                        if (field3.Value.ToLower() == "int")
                        {
                            def3 = "serial NOT NULL";
                        }
                        if (field3.Value.ToLower() == "long")
                        {
                            def3 = "bigserial NOT NULL";
                        }
                        if (field3.Value.ToLower() == "short")
                        {
                            def3 = "smallserial NOT NULL";
                        }
                    }
                    txt = txt + "\t" + field3.Key.ToLower() + " " + def3 + "," + Environment.NewLine;
                }
            }
            return txt;
        }

        public static string TableRow(Dictionary<string, string> fields)
        {
            string temp1 = "  <td data-label='%K%'>\r\n                                        @Html.DisplayFor(modelItem => item.%K%)\r\n                                    </td >";
            string txt = "";
            foreach (KeyValuePair<string, string> field in fields)
            {
                string t = "";
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
            char[] trim = new char[1] { ',' };
            return txt.Trim(trim);
        }

        public static Dictionary<string, string> Transform(string v4)
        {
            Dictionary<string, string> fields = new Dictionary<string, string>();
            try
            {
                if (string.IsNullOrEmpty(v4))
                {
                    fields.Add("name", "string");
                    return fields;
                }
                char[] sep1 = new char[1] { ';' };
                string[] bit1 = v4.Split(sep1, StringSplitOptions.RemoveEmptyEntries);
                if (bit1.Length == 0)
                {
                    return fields;
                }
                char[] sep2 = new char[1] { ':' };
                string[] array = bit1;
                foreach (string bit2 in array)
                {
                    string[] bit3 = bit2.Split(sep2, StringSplitOptions.RemoveEmptyEntries);
                    if (bit3.Count() == 1)
                    {
                        fields.Add(bit3[0], "string");
                    }
                    if (bit3.Count() == 2)
                    {
                        fields.Add(bit3[0], bit3[1]);
                    }
                }
            }
            catch
            {
            }
            return fields;
        }

        public static string UpperVariables(Dictionary<string, string> fields)
        {
            string txt = "";
            foreach (KeyValuePair<string, string> field2 in fields)
            {
                txt = txt + field2.Key + ",";
            }
            char[] trim = new char[1] { ',' };
            return txt.Trim(trim);
        }

        public static string Variables(Dictionary<string, string> fields)
        {
            string txt = "";
            foreach (KeyValuePair<string, string> field2 in fields)
            {
                txt = txt + FirstToLower(field2.Key) + ",";
            }
            char[] trim = new char[1] { ',' };
            return txt.Trim(trim);
        }

        public static string Variables3(Dictionary<string, string> fields)
        {
            string txt = "";
            foreach (KeyValuePair<string, string> field in fields)
            {
                txt = ((!(field.Value == "DateRangeFilter")) ? (txt + FirstToLower(field.Key) + ",") : (txt + "new DateRangeFilter(){ startdate = " + FirstToLower(field.Key) + ", enddate = " + FirstToLower(field.Key) + "2 },"));
            }
            char[] trim = new char[1] { ',' };
            return txt.Trim(trim);
        }

        public static string Variables(Dictionary<string, string> fields, string dot)
        {
            int cnt = 1;
            string txt = "";
            foreach (KeyValuePair<string, string> field in fields)
            {
                txt = txt + dot + "." + field.Key.ToLower() + ",";
                string fx = field.Key.ToLower();
                if (fx.EndsWith("id"))
                {
                    string b = fx.Substring(0, field.Key.Length - 2) + "name";
                    txt = txt + "x" + cnt + ".<JOINFIELD> as " + b + ",";
                    cnt++;
                }
            }
            char[] trim = new char[1] { ',' };
            return txt.Trim(trim).ToLower();
        }

        public static string ViewBag(Dictionary<string, string> fields)
        {
            string txt = "";
            foreach (KeyValuePair<string, string> field in fields)
            {
                txt = txt + "ViewBag." + FirstToLower(field.Key) + " = " + FirstToLower(field.Key) + ";" + Environment.NewLine;
            }
            char[] trim = new char[1] { ',' };
            return txt.Trim(trim);
        }

        public static string ID_Query(string idtype)
        {
            if (idtype.ToLower() == "guid")
            {
                return " id is not null ";
            }
            return " id > 0 ";
        }

        public static string ID_Param(string idtype)
        {
            if (idtype.ToLower() == "guid")
            {
                return " Guid? id = null";
            }
            return " %T% id = 0";
        }

        public static string ID_Filter(string idtype)
        {
            if (idtype.ToLower() == "guid")
            {
                return "id != null";
            }
            return "id > 0";
        }

        public static string ID_Generate(string idtype)
        {
            if (idtype.ToLower() == "guid")
            {
                return "entity.id = Guid.NewGuid()";
            }
            return "entity.id = 0";
        }

        public static string ID_Setup(string idtype)
        {
            if (idtype.ToLower() == "guid")
            {
                return "false";
            }
            return "true";
        }


        public static string ID_Default(string idtype)
        {
            if (idtype.ToLower() == "guid")
            {
                return "Guid.NewGuid()";
            }
            if (idtype.ToLower() == "datetime")
            {
                return "DateTime.Today";
            }
            if (idtype.ToLower() == "string")
            {
                return "''";
            }
            if (idtype.ToLower() == "decimal")
            {
                return "0m";
            }
            return "0"; 
        }
    }
}