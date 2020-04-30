using System;
using System.Data;
using Sterling.MSSQL;

namespace com.sbp.entity
{
    public class BillerGroup
    {
        public int BillerGroupID { get; set; }
        public string BillerGroupName { get; set; }
        public int Statusflag { get; set; }
        public int SortID { get; set; }
        public string BillerGroupInfo { get; set; }
        public string BillerGroupIcon { get; set; }


        public void Load()
        {
            string sql = "SELECT BillerGroupID, BillerGroupName, Statusflag, SortID," +
                         " BillerGroupInfo, BillerGroupIcon FROM tbl_biller_groups " +
                         "WHERE BillerGroupID = @BillerGroupID";
            Connect cn = new Connect();
            cn.SetSQL(sql);
            cn.AddParam("@BillerGroupID", BillerGroupID);

            DataSet ds = cn.Select();
            BillerGroupID = 0;
            if (cn.num_rows > 0)
            {
                Set(ds.Tables[0].Rows[0]);
            }
        }

        public void Set(DataRow dr)
        {
            BillerGroupID = Convert.ToInt32(dr["BillerGroupID"]);
            BillerGroupName = Convert.ToString(dr["BillerGroupName"]);
            Statusflag = Convert.ToInt32(dr["Statusflag"]);
            SortID = Convert.ToInt32(dr["SortID"]);
            BillerGroupInfo = Convert.ToString(dr["BillerGroupInfo"]);
            BillerGroupIcon = Convert.ToString(dr["BillerGroupIcon"]);
        }

        public int Insert()
        {
            string sql = "INSERT INTO tbl_biller_groups (BillerGroupName, Statusflag, SortID, BillerGroupInfo, BillerGroupIcon)  VALUES ( @BillerGroupName, @Statusflag, @SortID, @BillerGroupInfo, @BillerGroupIcon) ";
            Connect cn = new Connect();
            cn.SetSQL(sql);
            cn.AddParam("@BillerGroupName", BillerGroupName);
            cn.AddParam("@Statusflag", Statusflag);
            cn.AddParam("@SortID", SortID);
            cn.AddParam("@BillerGroupInfo", BillerGroupInfo);
            cn.AddParam("@BillerGroupIcon", BillerGroupIcon);
            cn.AddParam("@BillerGroupID", BillerGroupID);

            BillerGroupID = Convert.ToInt32(cn.Insert());
            return BillerGroupID;
        }

        public int Update()
        {
            string sql = "UPDATE tbl_biller_groups SET BillerGroupName = @BillerGroupName, Statusflag = @Statusflag, SortID = @SortID, BillerGroupInfo = @BillerGroupInfo, BillerGroupIcon = @BillerGroupIcon WHERE BillerGroupID = @BillerGroupID";
            Connect cn = new Connect();
            cn.SetSQL(sql);
            cn.AddParam("@BillerGroupName", BillerGroupName);
            cn.AddParam("@Statusflag", Statusflag);
            cn.AddParam("@SortID", SortID);
            cn.AddParam("@BillerGroupInfo", BillerGroupInfo);
            cn.AddParam("@BillerGroupIcon", BillerGroupIcon);
            cn.AddParam("@BillerGroupID", BillerGroupID);

            return cn.Update();
        }

        public int Delete()
        {
            string sql = "DELETE FROM tbl_biller_groups WHERE BillerGroupID = @BillerGroupID";
            Connect cn = new Connect();
            cn.SetSQL(sql);
            cn.AddParam("@BillerGroupID", BillerGroupID);

            return cn.Delete();
        }

        public void SetView(DataRow dr)
        {
            BillerGroupID = Convert.ToInt32(dr["BillerGroupID"]);
            BillerGroupName = Convert.ToString(dr["BillerGroupName"]);
            BillerGroupStatus = Convert.ToString(dr["BillerGroupStatus"]);
            BillerGroupInfo = Convert.ToString(dr["BillerGroupInfo"]);
            BillerGroupIcon = Convert.ToString(dr["BillerGroupIcon"]);
        }

        public string BillerGroupStatus { get; set; }
    }
}