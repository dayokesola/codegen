using System;
using System.Data;
using Sterling.MSSQL;

namespace com.sbp.entity
{
    public class Bill
    {
        public int BillID { get; set; }
        public string BillName { get; set; }
        public string BillCode { get; set; }
        public int CategoryID { get; set; }
        public decimal BillAmount { get; set; }
        public decimal BillCharge { get; set; }
        public int Statusflag { get; set; }
        public int SortID { get; set; }
        public string SubscriberInfo1 { get; set; }
        public string SubscriberInfo2 { get; set; }
        public string BillCurrency { get; set; }


        public int BillStatus { get; set; }
        public string CategoryName { get; set; }
        public string CategoryCode { get; set; }
        public int CategoryStatus { get; set; }
        public string BillGroupName { get; set; }
        public string BillGroupCode { get; set; }
        public int BillGroupStatus { get; set; }
        public int ChannelId { get; set; }
        public string ChannelName { get; set; }
        public int ContractID { get; set; }
        public string ContractName { get; set; }
        public string ContractBillCode { get; set; }
        public int BillSort { get; set; }
        public int CategorySort { get; set; }
        public int BillGroupSort { get; set; }
        public decimal MinValue { get; set; }
        public decimal MaxValue { get; set; }
        public int BillGroupID { get; set; }


        public int BillerID { get; set; }
        public string BillerName { get; set; }
        public string ChannelCode { get; set; }
        public string BillerCode { get; set; }

        public int ChannelTypeID { get; set; }

        public void Load()
        {
            string sql = @"SELECT BillID, BillName, BillCode, CategoryID, BillAmount, BillCharge, Statusflag, SortID, 
SubscriberInfo1, SubscriberInfo2, BillCurrency FROM tbl_bills WHERE BillID = @BillID";
            Connect cn = new Connect();
            cn.SetSQL(sql);
            cn.AddParam("@BillID", BillID);

            DataSet ds = cn.Select();
            BillID = 0;
            if (cn.num_rows > 0)
            {
                Set(ds.Tables[0].Rows[0]);
            }
        }

        public void Set(DataRow dr)
        {
            BillID = Convert.ToInt32(dr["BillID"]);
            BillName = Convert.ToString(dr["BillName"]);
            BillCode = Convert.ToString(dr["BillCode"]);
            CategoryID = Convert.ToInt32(dr["CategoryID"]);
            BillAmount = Convert.ToDecimal(dr["BillAmount"]);
            BillCharge = Convert.ToDecimal(dr["BillCharge"]);
            Statusflag = Convert.ToInt32(dr["Statusflag"]);
            SortID = Convert.ToInt32(dr["SortID"]);
            SubscriberInfo1 = Convert.ToString(dr["SubscriberInfo1"]);
            SubscriberInfo2 = Convert.ToString(dr["SubscriberInfo2"]);
            BillCurrency = Convert.ToString(dr["BillCurrency"]);
        }
        public void LoadView()
        {
            string sql = @"SELECT BillID, BillName, BillCode, CategoryID, BillCharge, BillAmount, billStatus, BillCurrency,
CategoryName, CategoryCode, CategoryStatus, BillGroupName, BillGroupCode, BillGroupStatus, ChannelId, ChannelName, 
ContractID, contractName, ContractBillCode, BillSort, CategorySort, BillGroupSort, MinValue, MaxValue, SubscriberInfo1, 
SubscriberInfo2, BillGroupID, BillerID, BillerName, ChannelCode, BillerCode,ChannelTypeID FROM vew_bills WHERE BillID = @BillID and ChannelId = @ChannelId";
            Connect cn = new Connect();
            cn.SetSQL(sql);
            cn.AddParam("@BillID", BillID);
            cn.AddParam("@ChannelId", ChannelId);

            DataSet ds = cn.Select();
            BillID = 0;
            if (cn.num_rows > 0)
            {
                SetView(ds.Tables[0].Rows[0]);
            }
        }

        public void SetView(DataRow dr)
        {
            BillID = Convert.ToInt32(dr["BillID"]);
            BillName = Convert.ToString(dr["BillName"]);
            BillCode = Convert.ToString(dr["BillCode"]);
            CategoryID = Convert.ToInt32(dr["CategoryID"]);
            BillCharge = Convert.ToDecimal(dr["BillCharge"]);
            BillAmount = Convert.ToDecimal(dr["BillAmount"]);
            BillStatus = Convert.ToInt32(dr["billStatus"]);
            BillCurrency = Convert.ToString(dr["BillCurrency"]);
            CategoryName = Convert.ToString(dr["CategoryName"]);
            CategoryCode = Convert.ToString(dr["CategoryCode"]);
            CategoryStatus = Convert.ToInt32(dr["CategoryStatus"]);
            BillGroupName = Convert.ToString(dr["BillGroupName"]);
            BillGroupCode = Convert.ToString(dr["BillGroupCode"]);
            BillGroupStatus = Convert.ToInt32(dr["BillGroupStatus"]);
            ChannelId = Convert.ToInt32(dr["ChannelId"]);
            ChannelName = Convert.ToString(dr["ChannelName"]);
            ContractID = Convert.ToInt32(dr["ContractID"]);
            ContractName = Convert.ToString(dr["contractName"]);
            ContractBillCode = Convert.ToString(dr["ContractBillCode"]);
            BillSort = Convert.ToInt32(dr["BillSort"]);
            CategorySort = Convert.ToInt32(dr["CategorySort"]);
            BillGroupSort = Convert.ToInt32(dr["BillGroupSort"]);
            MinValue = Convert.ToDecimal(dr["MinValue"]);
            MaxValue = Convert.ToDecimal(dr["MaxValue"]);
            SubscriberInfo1 = Convert.ToString(dr["SubscriberInfo1"]);
            SubscriberInfo2 = Convert.ToString(dr["SubscriberInfo2"]);
            BillGroupID = Convert.ToInt32(dr["BillGroupID"]);
            BillerID = Convert.ToInt32(dr["BillerID"]);
            BillerName = Convert.ToString(dr["BillerName"]);
            ChannelCode = Convert.ToString(dr["ChannelCode"]);
            BillerCode = Convert.ToString(dr["BillerCode"]);
            ChannelTypeID = Convert.ToInt32(dr["ChannelTypeID"]); 
        }


        public int Insert()
        {
            string sql = @"INSERT INTO tbl_bills (BillName, BillCode, CategoryID, BillAmount, BillCharge, Statusflag, SortID, SubscriberInfo1, SubscriberInfo2, BillCurrency)  VALUES ( @BillName, @BillCode, @CategoryID, @BillAmount, @BillCharge, @Statusflag, @SortID, @SubscriberInfo1, @SubscriberInfo2, @BillCurrency) ";
            Connect cn = new Connect();
            cn.SetSQL(sql);
            cn.AddParam("@BillName", BillName);
            cn.AddParam("@BillCode", BillCode);
            cn.AddParam("@CategoryID", CategoryID);
            cn.AddParam("@BillAmount", BillAmount);
            cn.AddParam("@BillCharge", BillCharge);
            cn.AddParam("@Statusflag", Statusflag);
            cn.AddParam("@SortID", SortID);
            cn.AddParam("@SubscriberInfo1", SubscriberInfo1);
            cn.AddParam("@SubscriberInfo2", SubscriberInfo2);
            cn.AddParam("@BillCurrency", BillCurrency);
            cn.AddParam("@BillID", BillID);

            BillID = Convert.ToInt32(cn.Insert());
            return BillID;
        }

        public int Update()
        {
            string sql = @"UPDATE tbl_bills SET BillName = @BillName, BillCode = @BillCode, CategoryID = @CategoryID, BillAmount = @BillAmount, BillCharge = @BillCharge, Statusflag = @Statusflag, SortID = @SortID, SubscriberInfo1 = @SubscriberInfo1, SubscriberInfo2 = @SubscriberInfo2, BillCurrency = @BillCurrency WHERE BillID = @BillID";
            Connect cn = new Connect();
            cn.SetSQL(sql);
            cn.AddParam("@BillName", BillName);
            cn.AddParam("@BillCode", BillCode);
            cn.AddParam("@CategoryID", CategoryID);
            cn.AddParam("@BillAmount", BillAmount);
            cn.AddParam("@BillCharge", BillCharge);
            cn.AddParam("@Statusflag", Statusflag);
            cn.AddParam("@SortID", SortID);
            cn.AddParam("@SubscriberInfo1", SubscriberInfo1);
            cn.AddParam("@SubscriberInfo2", SubscriberInfo2);
            cn.AddParam("@BillCurrency", BillCurrency);
            cn.AddParam("@BillID", BillID);

            return cn.Update();
        }

        public int Delete()
        {
            string sql = "DELETE FROM tbl_bills WHERE BillID = @BillID";
            Connect cn = new Connect();
            cn.SetSQL(sql);
            cn.AddParam("@BillID", BillID);

            return cn.Delete();
        }
    }
}