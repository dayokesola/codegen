using System;
using System.Data;
using Sterling.MSSQL;

namespace com.sbp.entity
{
    public class WSEndPoint
    {
        public int WSID { get; set; }
        public string WSName { get; set; }
        public string WSUrl { get; set; }
        public DateTime LastPingDate { get; set; }
        public int LastPingValue { get; set; }
        public int Statusflag { get; set; }


        public void Load()
        {
            string sql = @"SELECT WSID, WSName, WSUrl, LastPingDate, LastPingValue, Statusflag FROM tbl_ws WHERE WSID = @WSID";
            Connect cn = new Connect();
            cn.SetSQL(sql);
            cn.AddParam("@WSID", WSID);

            DataSet ds = cn.Select();
            WSID = 0;
            if (cn.num_rows > 0)
            {
                Set(ds.Tables[0].Rows[0]);
            }
        }

        public void Set(DataRow dr)
        {
            WSID = Convert.ToInt32(dr["WSID"]);
            WSName = Convert.ToString(dr["WSName"]);
            WSUrl = Convert.ToString(dr["WSUrl"]);
            LastPingDate = Convert.ToDateTime(dr["LastPingDate"]);
            LastPingValue = Convert.ToInt32(dr["LastPingValue"]);
            Statusflag = Convert.ToInt32(dr["Statusflag"]);
        }

        public int Insert()
        {
            string sql = @"INSERT INTO tbl_ws (WSName, WSUrl, LastPingDate, LastPingValue, Statusflag)  VALUES ( @WSName, @WSUrl, @LastPingDate, @LastPingValue, @Statusflag) ";
            Connect cn = new Connect();
            cn.SetSQL(sql);
            cn.AddParam("@WSName", WSName);
            cn.AddParam("@WSUrl", WSUrl);
            cn.AddParam("@LastPingDate", LastPingDate);
            cn.AddParam("@LastPingValue", LastPingValue);
            cn.AddParam("@Statusflag", Statusflag);
            cn.AddParam("@WSID", WSID);

            WSID = Convert.ToInt32(cn.Insert());
            return WSID;
        }

        public int Update()
        {
            string sql = @"UPDATE tbl_ws SET WSName = @WSName, WSUrl = @WSUrl, LastPingDate = @LastPingDate, LastPingValue = @LastPingValue, Statusflag = @Statusflag WHERE WSID = @WSID";
            Connect cn = new Connect();
            cn.SetSQL(sql);
            cn.AddParam("@WSName", WSName);
            cn.AddParam("@WSUrl", WSUrl);
            cn.AddParam("@LastPingDate", LastPingDate);
            cn.AddParam("@LastPingValue", LastPingValue);
            cn.AddParam("@Statusflag", Statusflag);
            cn.AddParam("@WSID", WSID);

            return cn.Update();
        }

        public int Delete()
        {
            string sql = "DELETE FROM tbl_ws WHERE WSID = @WSID";
            Connect cn = new Connect();
            cn.SetSQL(sql);
            cn.AddParam("@WSID", WSID);

            return cn.Delete();
        }
    }
}