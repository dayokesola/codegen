using System;
using System.Data;
using Sterling.MSSQL;

namespace com.sbp.entity
{
    public class ContractResponse
    {
        public int BillerId { get; set; }
        public string BillerReference { get; set; }
        public int ResponseCode { get; set; }
        public string ResponseText { get; set; }


        public void Load()
        {
            string sql = @"SELECT BillerId, BillerReference, ResponseCode, ResponseText 
    FROM vew_contract_responses WHERE BillerId = @BillerId and ( BillerReference = @BillerReference or billerreference = '-1')
order by responsecode  ";        
            Connect cn = new Connect();
            cn.SetSQL(sql);
            cn.AddParam("@BillerId", BillerId);
            cn.AddParam("@BillerReference", BillerReference); 
            DataSet ds = cn.Select();
            BillerId = 0; 
            BillerReference = "0";        
            if(cn.num_rows > 0)
            {
                Set(ds.Tables[0].Rows[0]);
            }
        }

        public void Set(DataRow dr)
        {
            BillerId = Convert.ToInt32(dr["BillerId"]);
            BillerReference = Convert.ToString(dr["BillerReference"]);
            ResponseCode = Convert.ToInt32(dr["ResponseCode"]);
            ResponseText = Convert.ToString(dr["ResponseText"]);
        }

           
    }

    public class AccountProfile
    {
        public int AccountProfileID { get; set; }
        public int PrincipalAcctID { get; set; }
        public int FeeAcctID { get; set; }
        public int VatAcctID { get; set; }
        public DateTime DateAdded { get; set; }
        public int Statusflag { get; set; }
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


        public void Load()
        {
            string sql = @"SELECT AccountProfileID, PrincipalAcctID, FeeAcctID, VatAcctID, DateAdded, Statusflag, 
pri_BC, pri_CN, pri_CC, pri_LC, pri_SC, pri_NB, 
fee_BC, fee_CN, fee_CC, fee_LC, fee_SC, fee_NB, 
vat_BC, vat_CN, vat_CC, vat_LC, vat_SC, vat_NB ,
pri_EC, vat_EC, fee_EC 
FROM vew_account_profile WHERE AccountProfileID = @AccountProfileID ";        
            Connect cn = new Connect();
            cn.SetSQL(sql);
            cn.AddParam("@AccountProfileID", AccountProfileID);
            DataSet ds = cn.Select(); 
            AccountProfileID = 0;        
            if(cn.num_rows > 0)
            {
                Set(ds.Tables[0].Rows[0]);
            }
        }

        public void Set(DataRow dr)
        {
            AccountProfileID = Convert.ToInt32(dr["AccountProfileID"]);
            PrincipalAcctID = Convert.ToInt32(dr["PrincipalAcctID"]);
            FeeAcctID = Convert.ToInt32(dr["FeeAcctID"]);
            VatAcctID = Convert.ToInt32(dr["VatAcctID"]);
            DateAdded = Convert.ToDateTime(dr["DateAdded"]);
            Statusflag = Convert.ToInt32(dr["Statusflag"]);
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
        }
    }
}