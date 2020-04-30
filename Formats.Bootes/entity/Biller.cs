using System;
using System.Data;
using Sterling.MSSQL;

namespace com.sbp.entity
{
    public class Biller
    {
        public int BillerId { get; set; }
        public string BillerName { get; set; }
        public string BillerCode { get; set; }
        public string BillerKey { get; set; }
        public int Statusflag { get; set; }
        public DateTime DateAdded { get; set; }
        public string InvokeClass { get; set; }
        public string BillerProfile1 { get; set; }
        public string BillerProfile2 { get; set; }
        public string BillerProfile3 { get; set; } 


        public string BillGroupBillerCode { get; set; }
        public int ChannelTypeID { get; set; }
        public string ChannelTypeBillerCode { get; set; }
        public int BillGroupId { get; set; }

        public void Load()
        {
            string sql = @"SELECT BillerId, BillerName, BillerCode, BillerKey, Statusflag, DateAdded, 
InvokeClass, BillerProfile1, BillerProfile2, BillerProfile3 FROM tbl_billers WHERE BillerId = @BillerId";
            Connect cn = new Connect();
            cn.SetSQL(sql);
            cn.AddParam("@BillerId", BillerId);

            DataSet ds = cn.Select();
            BillerId = 0;
            if (cn.num_rows > 0)
            {
                Set(ds.Tables[0].Rows[0]);
            }
        }

        public void LoadView()
        {
            string sql = @"SELECT BillerId, BillerName, BillerCode, Statusflag, DateAdded, InvokeClass, 
BillerProfile1, BillerProfile2, BillerProfile3, billGroupBillerCode, ChannelTypeID, ChannelTypeBillerCode, BillGroupId 
FROM vew_billers where BillerId = @BillerId and ChannelTypeID = @ChannelTypeID and BillGroupId = @BillGroupId ";        
            Connect cn = new Connect();
            cn.SetSQL(sql);
            cn.AddParam("@BillerId", BillerId);
            cn.AddParam("@ChannelTypeID", ChannelTypeID);
            cn.AddParam("@BillGroupId", BillGroupId);

            DataSet ds = cn.Select();
            BillerId = 0;    
            if(cn.num_rows > 0)
            {
                SetView(ds.Tables[0].Rows[0]);
            }
        }

        public void Set(DataRow dr)
        {
            BillerId = Convert.ToInt32(dr["BillerId"]);
            BillerName = Convert.ToString(dr["BillerName"]);
            BillerCode = Convert.ToString(dr["BillerCode"]);
            BillerKey = Convert.ToString(dr["BillerKey"]);
            Statusflag = Convert.ToInt32(dr["Statusflag"]);
            DateAdded = Convert.ToDateTime(dr["DateAdded"]);
            InvokeClass = Convert.ToString(dr["InvokeClass"]);
            BillerProfile1 = Convert.ToString(dr["BillerProfile1"]);
            BillerProfile2 = Convert.ToString(dr["BillerProfile2"]);
            BillerProfile3 = Convert.ToString(dr["BillerProfile3"]); 
        }

        public void SetView(DataRow dr)
        {
            BillerId = Convert.ToInt32(dr["BillerId"]);
            BillerName = Convert.ToString(dr["BillerName"]);
            BillerCode = Convert.ToString(dr["BillerCode"]);
            Statusflag = Convert.ToInt32(dr["Statusflag"]);
            DateAdded = Convert.ToDateTime(dr["DateAdded"]);
            InvokeClass = Convert.ToString(dr["InvokeClass"]);
            BillerProfile1 = Convert.ToString(dr["BillerProfile1"]);
            BillerProfile2 = Convert.ToString(dr["BillerProfile2"]);
            BillerProfile3 = Convert.ToString(dr["BillerProfile3"]);
            BillGroupBillerCode = Convert.ToString(dr["billGroupBillerCode"]);
            ChannelTypeID = Convert.ToInt32(dr["ChannelTypeID"]);
            ChannelTypeBillerCode = Convert.ToString(dr["ChannelTypeBillerCode"]);
            BillGroupId = Convert.ToInt32(dr["BillGroupId"]);
        }

        public int Insert()
        {
            string sql = @"INSERT INTO tbl_billers (BillerName, BillerCode, BillerKey, Statusflag, DateAdded, InvokeClass, BillerProfile1, BillerProfile2, BillerProfile3)  VALUES ( @BillerName, @BillerCode, @BillerKey, @Statusflag, @DateAdded, @InvokeClass, @BillerProfile1, @BillerProfile2, @BillerProfile3) ";
            Connect cn = new Connect();
            cn.SetSQL(sql);
            cn.AddParam("@BillerName", BillerName);
            cn.AddParam("@BillerCode", BillerCode);
            cn.AddParam("@BillerKey", BillerKey);
            cn.AddParam("@Statusflag", Statusflag);
            cn.AddParam("@DateAdded", DateAdded);
            cn.AddParam("@InvokeClass", InvokeClass);
            cn.AddParam("@BillerProfile1", BillerProfile1);
            cn.AddParam("@BillerProfile2", BillerProfile2);
            cn.AddParam("@BillerProfile3", BillerProfile3);
            cn.AddParam("@BillerId", BillerId);

            BillerId = Convert.ToInt32(cn.Insert());
            return BillerId;
        }

        public int Update()
        {
            string sql = @"UPDATE tbl_billers SET BillerName = @BillerName, BillerCode = @BillerCode, BillerKey = @BillerKey, Statusflag = @Statusflag, DateAdded = @DateAdded, InvokeClass = @InvokeClass, BillerProfile1 = @BillerProfile1, BillerProfile2 = @BillerProfile2, BillerProfile3 = @BillerProfile3 WHERE BillerId = @BillerId";
            Connect cn = new Connect();
            cn.SetSQL(sql);
            cn.AddParam("@BillerName", BillerName);
            cn.AddParam("@BillerCode", BillerCode);
            cn.AddParam("@BillerKey", BillerKey);
            cn.AddParam("@Statusflag", Statusflag);
            cn.AddParam("@DateAdded", DateAdded);
            cn.AddParam("@InvokeClass", InvokeClass);
            cn.AddParam("@BillerProfile1", BillerProfile1);
            cn.AddParam("@BillerProfile2", BillerProfile2);
            cn.AddParam("@BillerProfile3", BillerProfile3);
            cn.AddParam("@BillerId", BillerId);

            return cn.Update();
        }

        public int Delete()
        {
            string sql = "DELETE FROM tbl_billers WHERE BillerId = @BillerId";
            Connect cn = new Connect();
            cn.SetSQL(sql);
            cn.AddParam("@BillerId", BillerId);

            return cn.Delete();
        } 
    }
}