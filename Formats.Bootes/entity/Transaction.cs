using System;
using System.Data;
using Sterling.MSSQL;

namespace com.sbp.entity
{
    public class Transaction
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
        public int StatusBill { get; set; }
        public DateTime DateBillRequest { get; set; }
        public DateTime DateBillResponse { get; set; }
        public DateTime DatePostRequest { get; set; }
        public DateTime DatePostResponse { get; set; }
        public DateTime DateServeRequest { get; set; }
        public DateTime DateServeResponse { get; set; }
        public string ResponseCode { get; set; }
        public string ResponseData { get; set; }
        public string BraCode { get; set; }
        public string CusNum { get; set; }
        public string CurCode { get; set; }
        public string LedCode { get; set; }
        public string SubAcctCode { get; set; }
        public string Nuban { get; set; }


        public void Load()
        {
            string sql = @"SELECT RefId, AccountProfileID, BillId, ChannelId, BillerId, BillAmount, BillFee, 
BillVat, CallerRefID, SubscriberInfo1, SubscriberInfo2, StatusPost, StatusServe, StatusBill, DateBillRequest, 
DateBillResponse, DatePostRequest, DatePostResponse, DateServeRequest, DateServeResponse, ResponseCode, ResponseData, bra_code, cus_num, cur_code, led_code, sub_acct_code, nuban FROM tbl_trnx WHERE RefId = @RefId";
            Connect cn = new Connect();
            cn.SetSQL(sql);
            cn.AddParam("@RefId", RefId);

            DataSet ds = cn.Select();
            RefId = 0;
            if (cn.num_rows > 0)
            {
                Set(ds.Tables[0].Rows[0]);
            }
        }

        public void Set(DataRow dr)
        {
            RefId = Convert.ToInt32(dr["RefId"]);
            AccountProfileID = Convert.ToInt32(dr["AccountProfileID"]);
            BillId = Convert.ToInt32(dr["BillId"]);
            ChannelId = Convert.ToInt32(dr["ChannelId"]);
            BillerId = Convert.ToInt32(dr["BillerId"]);
            BillAmount = Convert.ToDecimal(dr["BillAmount"]);
            BillFee = Convert.ToDecimal(dr["BillFee"]);
            BillVat = Convert.ToDecimal(dr["BillVat"]);
            CallerRefID = Convert.ToString(dr["CallerRefID"]);
            SubscriberInfo1 = Convert.ToString(dr["SubscriberInfo1"]);
            SubscriberInfo2 = Convert.ToString(dr["SubscriberInfo2"]);
            StatusPost = Convert.ToInt32(dr["StatusPost"]);
            StatusServe = Convert.ToInt32(dr["StatusServe"]);
            StatusBill = Convert.ToInt32(dr["StatusBill"]);
            DateBillRequest = Convert.ToDateTime(dr["DateBillRequest"]);
            DateBillResponse = Convert.ToDateTime(dr["DateBillResponse"]);
            DatePostRequest = Convert.ToDateTime(dr["DatePostRequest"]);
            DatePostResponse = Convert.ToDateTime(dr["DatePostResponse"]);
            DateServeRequest = Convert.ToDateTime(dr["DateServeRequest"]);
            DateServeResponse = Convert.ToDateTime(dr["DateServeResponse"]);
            ResponseCode = Convert.ToString(dr["ResponseCode"]);
            ResponseData = Convert.ToString(dr["ResponseData"]);
            BraCode = Convert.ToString(dr["bra_code"]);
            CusNum = Convert.ToString(dr["cus_num"]);
            CurCode = Convert.ToString(dr["cur_code"]);
            LedCode = Convert.ToString(dr["led_code"]);
            SubAcctCode = Convert.ToString(dr["sub_acct_code"]);
            Nuban = Convert.ToString(dr["nuban"]);
        }

        public int Insert()
        {
            string sql = @"INSERT INTO tbl_trnx (AccountProfileID, BillId, ChannelId, BillerId, BillAmount,
BillFee, BillVat, CallerRefID, SubscriberInfo1, SubscriberInfo2, StatusPost, StatusServe, StatusBill,
DateBillRequest, DateBillResponse, DatePostRequest, DatePostResponse, DateServeRequest, DateServeResponse,
ResponseCode, ResponseData, bra_code, cus_num, cur_code, led_code, sub_acct_code, nuban)  
VALUES ( @AccountProfileID, @BillId, @ChannelId, @BillerId, @BillAmount, @BillFee, @BillVat, @CallerRefID, 
@SubscriberInfo1, @SubscriberInfo2, @StatusPost, @StatusServe, @StatusBill, @DateBillRequest, @DateBillResponse,
@DatePostRequest, @DatePostResponse, @DateServeRequest, @DateServeResponse, @ResponseCode, @ResponseData, @bra_code, @cus_num, @cur_code, @led_code, @sub_acct_code, @nuban) ";
            Connect cn = new Connect();
            cn.SetSQL(sql);
            cn.AddParam("@AccountProfileID", AccountProfileID);
            cn.AddParam("@BillId", BillId);
            cn.AddParam("@ChannelId", ChannelId);
            cn.AddParam("@BillerId", BillerId);
            cn.AddParam("@BillAmount", BillAmount);
            cn.AddParam("@BillFee", BillFee);
            cn.AddParam("@BillVat", BillVat);
            cn.AddParam("@CallerRefID", CallerRefID);
            cn.AddParam("@SubscriberInfo1", SubscriberInfo1);
            cn.AddParam("@SubscriberInfo2", SubscriberInfo2);
            cn.AddParam("@StatusPost", StatusPost);
            cn.AddParam("@StatusServe", StatusServe);
            cn.AddParam("@StatusBill", StatusBill);
            cn.AddParam("@DateBillRequest", DateBillRequest);
            cn.AddParam("@DateBillResponse", DateBillResponse);
            cn.AddParam("@DatePostRequest", DatePostRequest);
            cn.AddParam("@DatePostResponse", DatePostResponse);
            cn.AddParam("@DateServeRequest", DateServeRequest);
            cn.AddParam("@DateServeResponse", DateServeResponse);
            cn.AddParam("@ResponseCode", ResponseCode);
            cn.AddParam("@ResponseData", ResponseData);
            cn.AddParam("@bra_code", BraCode);
            cn.AddParam("@cus_num", CusNum);
            cn.AddParam("@cur_code", CurCode);
            cn.AddParam("@led_code", LedCode);
            cn.AddParam("@sub_acct_code", SubAcctCode);
            cn.AddParam("@nuban", Nuban);
            cn.AddParam("@RefId", RefId);

            RefId = Convert.ToInt32(cn.Insert());
            return RefId;
        }

        public int Update()
        {
            string sql = @"UPDATE tbl_trnx SET AccountProfileID = @AccountProfileID, BillId = @BillId, 
ChannelId = @ChannelId, BillerId = @BillerId, BillAmount = @BillAmount, BillFee = @BillFee, BillVat = @BillVat,
CallerRefID = @CallerRefID, SubscriberInfo1 = @SubscriberInfo1, SubscriberInfo2 = @SubscriberInfo2, 
StatusPost = @StatusPost, StatusServe = @StatusServe, StatusBill = @StatusBill, DateBillRequest = @DateBillRequest,
DateBillResponse = @DateBillResponse, DatePostRequest = @DatePostRequest, DatePostResponse = @DatePostResponse, 
DateServeRequest = @DateServeRequest, DateServeResponse = @DateServeResponse, ResponseCode = @ResponseCode, ResponseData = @ResponseData, bra_code = @bra_code, cus_num = @cus_num, cur_code = @cur_code, led_code = @led_code, sub_acct_code = @sub_acct_code, nuban = @nuban WHERE RefId = @RefId";
            Connect cn = new Connect();
            cn.SetSQL(sql);
            cn.AddParam("@AccountProfileID", AccountProfileID);
            cn.AddParam("@BillId", BillId);
            cn.AddParam("@ChannelId", ChannelId);
            cn.AddParam("@BillerId", BillerId);
            cn.AddParam("@BillAmount", BillAmount);
            cn.AddParam("@BillFee", BillFee);
            cn.AddParam("@BillVat", BillVat);
            cn.AddParam("@CallerRefID", CallerRefID);
            cn.AddParam("@SubscriberInfo1", SubscriberInfo1);
            cn.AddParam("@SubscriberInfo2", SubscriberInfo2);
            cn.AddParam("@StatusPost", StatusPost);
            cn.AddParam("@StatusServe", StatusServe);
            cn.AddParam("@StatusBill", StatusBill);
            cn.AddParam("@DateBillRequest", DateBillRequest);
            cn.AddParam("@DateBillResponse", DateBillResponse);
            cn.AddParam("@DatePostRequest", DatePostRequest);
            cn.AddParam("@DatePostResponse", DatePostResponse);
            cn.AddParam("@DateServeRequest", DateServeRequest);
            cn.AddParam("@DateServeResponse", DateServeResponse);
            cn.AddParam("@ResponseCode", ResponseCode);
            cn.AddParam("@ResponseData", ResponseData);
            cn.AddParam("@bra_code", BraCode);
            cn.AddParam("@cus_num", CusNum);
            cn.AddParam("@cur_code", CurCode);
            cn.AddParam("@led_code", LedCode);
            cn.AddParam("@sub_acct_code", SubAcctCode);
            cn.AddParam("@nuban", Nuban);
            cn.AddParam("@RefId", RefId);

            return cn.Update();
        }

        public int Delete()
        {
            string sql = "DELETE FROM tbl_trnx WHERE RefId = @RefId";
            Connect cn = new Connect();
            cn.SetSQL(sql);
            cn.AddParam("@RefId", RefId);

            return cn.Delete();
        }
    }
}