using System;
using System.Data;
using Sterling.MSSQL;
using com.sbp.utility;

namespace com.sbp.entity
{
    public class TransactionInfo
    {
        public int RefId { get; set; }
        public int AccountProfileID { get; set; }
        public int BillId { get; set; }
        public int ChannelId { get; set; }
        public int BillerId { get; set; }
        public decimal BillAmount { get; set; }
        public decimal BillFee { get; set; }
        public decimal BillVat { get; set; }
        public string CallerRefID { get; set; }
        public string SubscriberInfo1 { get; set; }
        public string SubscriberInfo2 { get; set; }
        public int StatusPost { get; set; }
        public int StatusServe { get; set; }
        public string Nuban { get; set; }
        public string SubAcctCode { get; set; }
        public string LedCode { get; set; }
        public string CurCode { get; set; }
        public string CusNum { get; set; }
        public string BraCode { get; set; }
        public string ResponseData { get; set; }
        public string ResponseCode { get; set; }
        public DateTime DateServeResponse { get; set; }
        public DateTime DateServeRequest { get; set; }
        public DateTime DatePostResponse { get; set; }
        public DateTime DatePostRequest { get; set; }
        public DateTime DateBillResponse { get; set; }
        public DateTime DateBillRequest { get; set; }
        public int StatusBill { get; set; }
        public int PrincipalAcctID { get; set; }
        public int FeeAcctID { get; set; }
        public int VatAcctID { get; set; }
        public string PriBC { get; set; }
        public string PriCN { get; set; }
        public string PriCC { get; set; }
        public string PriLC { get; set; }
        public string PriSC { get; set; }
        public string PriNB { get; set; }
        public string FeeBC { get; set; }
        public string FeeCN { get; set; }
        public string FeeCC { get; set; }
        public string FeeLC { get; set; }
        public string FeeSC { get; set; }
        public string FeeNB { get; set; }
        public string VatBC { get; set; }
        public string VatCN { get; set; }
        public string VatCC { get; set; }
        public string VatLC { get; set; }
        public string VatSC { get; set; }
        public string VatNB { get; set; }
        public string PriEC { get; set; }
        public string VatEC { get; set; }
        public string FeeEC { get; set; }
        public string BillName { get; set; }
        public string BillCode { get; set; }
        public string CategoryName { get; set; }
        public string CategoryCode { get; set; }
        public string BillGroupName { get; set; }
        public string BillGroupCode { get; set; }
        public string ChannelName { get; set; }
        public string ChannelCode { get; set; }
        public int ChannelTypeID { get; set; }
        public string BillerName { get; set; }
        public string BillerCode { get; set; }
        public string StatusPostText { get; set; }
        public string StatusPostColor { get; set; }
        public string StatusPostIcon { get; set; }
        public string StatusServeText { get; set; }
        public string StatusServeColor { get; set; }
        public string StatusServeIcon { get; set; }
        public string StatusBillText { get; set; }
        public string StatusBillColor { get; set; }
        public string StatusBillIcon { get; set; }

        public string SubscriberInfoText1 { get; set; }
        public string SubscriberInfoText2 { get; set; }

        public int StatusReversal { get; set; }
        public void Load()
        {
            string sql = @"SELECT * FROM vew_transactions WHERE RefId = @RefId";        
            Connect cn = new Connect();
            cn.SetSQL(sql);
            cn.AddParam("@RefId", RefId);

            DataSet ds = cn.Select();
            RefId = 0;        
            if(cn.num_rows > 0)
            {
                Set(ds.Tables[0].Rows[0]);
            }
        }

        public void Set(DataRow dr)
        {
            RefId = Gizmo.GetInt(dr["RefId"]);
            AccountProfileID = Gizmo.GetInt(dr["AccountProfileID"]);
            BillId = Gizmo.GetInt(dr["BillId"]);
            ChannelId = Gizmo.GetInt(dr["ChannelId"]);
            BillerId = Gizmo.GetInt(dr["BillerId"]);
            BillAmount = Convert.ToDecimal(dr["BillAmount"]);
            BillFee = Convert.ToDecimal(dr["BillFee"]);
            BillVat = Convert.ToDecimal(dr["BillVat"]);
            CallerRefID = Convert.ToString(dr["CallerRefID"]);
            SubscriberInfo1 = Convert.ToString(dr["SubscriberInfo1"]);
            SubscriberInfo2 = Convert.ToString(dr["SubscriberInfo2"]);
            StatusPost = Gizmo.GetInt(dr["StatusPost"]);
            StatusServe = Gizmo.GetInt(dr["StatusServe"]);
            Nuban = Convert.ToString(dr["nuban"]);
            SubAcctCode = Convert.ToString(dr["sub_acct_code"]);
            LedCode = Convert.ToString(dr["led_code"]);
            CurCode = Convert.ToString(dr["cur_code"]);
            CusNum = Convert.ToString(dr["cus_num"]);
            BraCode = Convert.ToString(dr["bra_code"]);
            ResponseData = Convert.ToString(dr["ResponseData"]);
            ResponseCode = Convert.ToString(dr["ResponseCode"]);
            DateServeResponse = Convert.ToDateTime(dr["DateServeResponse"]);
            DateServeRequest = Convert.ToDateTime(dr["DateServeRequest"]);
            DatePostResponse = Convert.ToDateTime(dr["DatePostResponse"]);
            DatePostRequest = Convert.ToDateTime(dr["DatePostRequest"]);
            DateBillResponse = Convert.ToDateTime(dr["DateBillResponse"]);
            DateBillRequest = Convert.ToDateTime(dr["DateBillRequest"]);
            StatusBill = Gizmo.GetInt(dr["StatusBill"]);
            
            PrincipalAcctID = Gizmo.GetInt(dr["PrincipalAcctID"]);
            FeeAcctID = Gizmo.GetInt(dr["FeeAcctID"]);
            VatAcctID = Gizmo.GetInt(dr["VatAcctID"]);
            PriBC = Convert.ToString(dr["pri_BC"]);
            PriCN = Convert.ToString(dr["pri_CN"]);
            PriCC = Convert.ToString(dr["pri_CC"]);
            PriLC = Convert.ToString(dr["pri_LC"]);
            PriSC = Convert.ToString(dr["pri_SC"]);
            PriNB = Convert.ToString(dr["pri_NB"]);
            FeeBC = Convert.ToString(dr["fee_BC"]);
            FeeCN = Convert.ToString(dr["fee_CN"]);
            FeeCC = Convert.ToString(dr["fee_CC"]);
            FeeLC = Convert.ToString(dr["fee_LC"]);
            FeeSC = Convert.ToString(dr["fee_SC"]);
            FeeNB = Convert.ToString(dr["fee_NB"]);
            VatBC = Convert.ToString(dr["vat_BC"]);
            VatCN = Convert.ToString(dr["vat_CN"]);
            VatCC = Convert.ToString(dr["vat_CC"]);
            VatLC = Convert.ToString(dr["vat_LC"]);
            VatSC = Convert.ToString(dr["vat_SC"]);
            VatNB = Convert.ToString(dr["vat_NB"]);
            PriEC = Convert.ToString(dr["pri_EC"]);
            VatEC = Convert.ToString(dr["vat_EC"]);
            FeeEC = Convert.ToString(dr["fee_EC"]);
            BillName = Convert.ToString(dr["BillName"]);
            BillCode = Convert.ToString(dr["BillCode"]);
            CategoryName = Convert.ToString(dr["CategoryName"]);
            CategoryCode = Convert.ToString(dr["CategoryCode"]);
            BillGroupName = Convert.ToString(dr["BillGroupName"]);
            BillGroupCode = Convert.ToString(dr["BillGroupCode"]);
            ChannelName = Convert.ToString(dr["ChannelName"]);
            ChannelCode = Convert.ToString(dr["ChannelCode"]);
            ChannelTypeID = Gizmo.GetInt(dr["ChannelTypeID"]);
            BillerName = Convert.ToString(dr["BillerName"]);
            BillerCode = Convert.ToString(dr["BillerCode"]);
            StatusPostText = Convert.ToString(dr["StatusPostText"]);
            StatusPostColor = Convert.ToString(dr["StatusPostColor"]);
            StatusPostIcon = Convert.ToString(dr["StatusPostIcon"]);
            StatusServeText = Convert.ToString(dr["StatusServeText"]);
            StatusServeColor = Convert.ToString(dr["StatusServeColor"]);
            StatusServeIcon = Convert.ToString(dr["StatusServeIcon"]);
            StatusBillText = Convert.ToString(dr["StatusBillText"]);
            StatusBillColor = Convert.ToString(dr["StatusBillColor"]);
            StatusBillIcon = Convert.ToString(dr["StatusBillIcon"]);
            StatusReversal = Gizmo.GetInt(dr["StatusReversal"]);

            SubscriberInfoText1 = Convert.ToString(dr["SubscriberInfoText1"]);
            SubscriberInfoText2 = Convert.ToString(dr["SubscriberInfoText2"]);

        }

        public string CustomerAccount()
        {
            return Nuban;
        }

        public string PrincipalAccount()
        {
            return  PriSC;
        }
        public string FeeAccount()
        {
            return  FeeSC;
        }
        public string VatAccount()
        {
            return  VatSC;
        }
    }
}