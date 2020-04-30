using System;
using System.Data;
using Sterling.MSSQL;

namespace com.sbp.entity
{
    public class BillGroup
    {
        public int BillGroupID { get; set; }
        public string BillGroupName { get; set; }
        public string BillGroupCode { get; set; }
        public int Statusflag { get; set; }
        public int SortID { get; set; }
        public string BillGroupInfo { get; set; }
        public string BillGroupIcon { get; set; }


        public void Load()
        {
            string sql = @"SELECT BillGroupID, BillGroupName, BillGroupCode, Statusflag, SortID, BillGroupInfo, BillGroupIcon FROM tbl_bill_groups WHERE BillGroupID = @BillGroupID";
            Connect cn = new Connect();
            cn.SetSQL(sql);
            cn.AddParam("@BillGroupID", BillGroupID);

            DataSet ds = cn.Select();
            BillGroupID = 0;
            if (cn.num_rows > 0)
            {
                Set(ds.Tables[0].Rows[0]);
            }
        }

        public void Set(DataRow dr)
        {
            BillGroupID = Convert.ToInt32(dr["BillGroupID"]);
            BillGroupName = Convert.ToString(dr["BillGroupName"]);
            BillGroupCode = Convert.ToString(dr["BillGroupCode"]);
            Statusflag = Convert.ToInt32(dr["Statusflag"]);
            SortID = Convert.ToInt32(dr["SortID"]);
            BillGroupInfo = Convert.ToString(dr["BillGroupInfo"]);
            BillGroupIcon = Convert.ToString(dr["BillGroupIcon"]);
        }

        public int Insert()
        {
            string sql = @"INSERT INTO tbl_bill_groups (BillGroupName, BillGroupCode, Statusflag, SortID, BillGroupInfo, BillGroupIcon)  VALUES ( @BillGroupName, @BillGroupCode, @Statusflag, @SortID, @BillGroupInfo, @BillGroupIcon) ";
            Connect cn = new Connect();
            cn.SetSQL(sql);
            cn.AddParam("@BillGroupName", BillGroupName);
            cn.AddParam("@BillGroupCode", BillGroupCode);
            cn.AddParam("@Statusflag", Statusflag);
            cn.AddParam("@SortID", SortID);
            cn.AddParam("@BillGroupInfo", BillGroupInfo);
            cn.AddParam("@BillGroupIcon", BillGroupIcon);
            cn.AddParam("@BillGroupID", BillGroupID);

            BillGroupID = Convert.ToInt32(cn.Insert());
            return BillGroupID;
        }

        public int Update()
        {
            string sql = @"UPDATE tbl_bill_groups SET BillGroupName = @BillGroupName, BillGroupCode = @BillGroupCode, Statusflag = @Statusflag, SortID = @SortID, BillGroupInfo = @BillGroupInfo, BillGroupIcon = @BillGroupIcon WHERE BillGroupID = @BillGroupID";
            Connect cn = new Connect();
            cn.SetSQL(sql);
            cn.AddParam("@BillGroupName", BillGroupName);
            cn.AddParam("@BillGroupCode", BillGroupCode);
            cn.AddParam("@Statusflag", Statusflag);
            cn.AddParam("@SortID", SortID);
            cn.AddParam("@BillGroupInfo", BillGroupInfo);
            cn.AddParam("@BillGroupIcon", BillGroupIcon);
            cn.AddParam("@BillGroupID", BillGroupID);

            return cn.Update();
        }

        public int Delete()
        {
            string sql = "DELETE FROM tbl_bill_groups WHERE BillGroupID = @BillGroupID";
            Connect cn = new Connect();
            cn.SetSQL(sql);
            cn.AddParam("@BillGroupID", BillGroupID);

            return cn.Delete();
        }
    }
}