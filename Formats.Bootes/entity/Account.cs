using com.sbp.utility;
using Sterling.BaseLIB.Utility;
using System;
using System.Data;

namespace com.sbp.entity
{
    public class Account
    {
        public string BRACODE { get; set; }
        public string DESENG { get; set; }
        public string CUSNUM { get; set; }
        public string CURCODE { get; set; }
        public string LEDCODE { get; set; }
        public string SUBACCTCODE { get; set; }
        public string CUSSHONAME { get; set; }
        public string ADDLINE1 { get; set; }
        public string ADDLINE2 { get; set; }
        public string MOBNUM { get; set; }
        public string EMAIL { get; set; }
        public string RESTIND { get; set; }
        public string ACCTNO { get; set; }
        public string MAPACCNO { get; set; }
        public string IBANACCNO { get; set; }
        public string TELNUM { get; set; }
        public string TELNUM2 { get; set; }
        public string ACCTTYPE { get; set; }
        public DateTime DATEOPEN { get; set; }
        public DateTime DATESTACHA { get; set; }
        public string STACODE { get; set; }
        public decimal CLEBAL { get; set; }
        public decimal CRNTBAL { get; set; }
        public string CUSCLASS { get; set; }
        public decimal FORAMT { get; set; }
        public decimal BALLIM { get; set; }
        public decimal LPERAVEBAL { get; set; }
        public decimal PREDAYCRNTBAL { get; set; }
        public string ACCOUNTCATEGORY { get; set; }
        public decimal TOTBLOFUND { get; set; }
        public decimal RISKLIMIT { get; set; }
        public DateTime DATEBALCHA { get; set; }


        public void Load()
        {
            
        }

        public void Set(DataRow dr)
        {
            BRACODE = Convert.ToString(dr["BRA_CODE"]);
            DESENG = Convert.ToString(dr["DES_ENG"]);
            CUSNUM = Convert.ToString(dr["CUS_NUM"]);
            CURCODE = Convert.ToString(dr["CUR_CODE"]);
            LEDCODE = Convert.ToString(dr["LED_CODE"]);
            SUBACCTCODE = Convert.ToString(dr["SUB_ACCT_CODE"]);
            CUSSHONAME = Convert.ToString(dr["CUS_SHO_NAME"]);
            ADDLINE1 = Convert.ToString(dr["ADD_LINE1"]);
            ADDLINE2 = Convert.ToString(dr["ADD_LINE2"]);
            MOBNUM = Convert.ToString(dr["MOB_NUM"]);
            EMAIL = Convert.ToString(dr["EMAIL"]);
            RESTIND = Convert.ToString(dr["REST_IND"]);
            ACCTNO = Convert.ToString(dr["ACCT_NO"]);
            MAPACCNO = Convert.ToString(dr["MAP_ACC_NO"]);
            IBANACCNO = Convert.ToString(dr["IBAN_ACC_NO"]);
            TELNUM = Convert.ToString(dr["TEL_NUM"]);
            TELNUM2 = Convert.ToString(dr["TEL_NUM_2"]);
            ACCTTYPE = Convert.ToString(dr["ACCT_TYPE"]);
            DATEOPEN = Convert.ToDateTime(dr["DATE_OPEN"]);
            DATESTACHA = Convert.ToDateTime(dr["DATE_STA_CHA"]);
            STACODE = Convert.ToString(dr["STA_CODE"]);
            CLEBAL = Convert.ToDecimal(dr["CLE_BAL"]);
            CRNTBAL = Convert.ToDecimal(dr["CRNT_BAL"]);
            FORAMT = Convert.ToDecimal(dr["FOR_AMT"]);
            BALLIM = Convert.ToDecimal(dr["BAL_LIM"]);
            LPERAVEBAL = Convert.ToDecimal(dr["LPER_AVE_BAL"]);
            PREDAYCRNTBAL = Convert.ToDecimal(dr["PRE_DAY_CRNT_BAL"]);
            ACCOUNTCATEGORY = Convert.ToString(dr["ACCOUNTCATEGORY"]);
            TOTBLOFUND = Convert.ToDecimal(dr["TOT_BLO_FUND"]);
            RISKLIMIT = Convert.ToInt32(dr["RISK_LIMIT"]);
            DATEBALCHA = Convert.ToDateTime(dr["DATE_BAL_CHA"]);
        }


        public void SetT24(DataRow dr)
        {
            BRACODE = Convert.ToString(dr["BranchCode"]);
            DESENG = Convert.ToString(dr["Branch"]);
            CUSNUM = Convert.ToString(dr["CustomerId"]);
            CURCODE = Convert.ToString(dr["AccountCurrency"]);
            LEDCODE = Convert.ToString(dr["Category"]);
            SUBACCTCODE = "0";
            CUSSHONAME = Convert.ToString(dr["AccountName"]);
            ADDLINE1 = "";
            ADDLINE2 = "";
            MOBNUM = Convert.ToString(dr["Mobile1"]);
            EMAIL = Convert.ToString(dr["Email"]);
            RESTIND = Convert.ToString(dr["AcctRest"]);
            ACCTNO = Convert.ToString(dr["AccountNumber"]);
            MAPACCNO = Convert.ToString(dr["AccountNumber"]);
            IBANACCNO = Convert.ToString(dr["AccountNumber"]);
            TELNUM = Convert.ToString(dr["Mobile1"]);
            TELNUM2 = Convert.ToString(dr["Mobile1"]);
            ACCTTYPE = Convert.ToString(dr["CustomerClass"]);
            DATEOPEN = Gizmo.ToDateTime_8Char(dr["DateOpened"]);
            DATESTACHA = Gizmo.ToDateTime_8Char(dr["DateOpened"]);
            STACODE = Convert.ToString(dr["AccountStatus"]);
            CLEBAL = Convert.ToDecimal(dr["WorkingBalance"]);
            CRNTBAL = Convert.ToDecimal(dr["OnlineActualBalance"]);
            BALLIM = Convert.ToDecimal(dr["LockedAmount"]);
            ACCOUNTCATEGORY = Convert.ToString(dr["CustomerClass"]);
            DATEBALCHA = Gizmo.ToDateTime_8Char(dr["LastMVTDate"]);
        }
        public void LoadByNuban()
        {
            string core = Gizmo.AppSetting("core");
            switch(core)
            {
                case "banks": 
                    new ErrorLog("checking banks for " + MAPACCNO);
                    LoadByNubanBanks();
                    break;
                case "t24":
                    new ErrorLog("checking t24 for " + MAPACCNO);
                    LoadByNubanT24();
                    break;
            }
        }

        public void LoadByNubanT24()
        {
            BillPaymentLIB.CBS_T24.banksSoapClient cb = new BillPaymentLIB.CBS_T24.banksSoapClient();
            DataSet ds = cb.T24GetAccountFullInfo(MAPACCNO);
            try
            {
                SetT24(ds.Tables[0].Rows[0]);
            }
            catch (Exception ex)
            {
                BRACODE = "";
                new ErrorLog(ex);
            }
        }

        public void LoadByNubanBanks()
        {
            BillPaymentLIB.CBS_Banks.banksSoapClient cb = new BillPaymentLIB.CBS_Banks.banksSoapClient();
            DataSet ds = cb.getAccountByNUBANAll(MAPACCNO);
            try
            {
                Set(ds.Tables[0].Rows[0]);
            }
            catch(Exception ex)
            {
                BRACODE = "";
                new ErrorLog(ex);
            }
        }

        public void LoadByNubanDMZ()
        {
            LoadByNuban();
        }

        public decimal AvailableBal()
        {
            return CLEBAL;
        }
    }
}