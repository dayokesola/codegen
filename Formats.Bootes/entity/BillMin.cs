using System;
using System.Data;

namespace com.sbp.entity
{
    public class BillMin
    {
        public int BID { get; set; }
        public int CID { get; set; }
        public string BName { get; set; }
        public decimal BAmt { get; set; }
        public decimal BCharge { get; set; }
        public string SInfo1 { get; set; }
        public string SInfo2 { get; set; }
        public string BCur { get; set; }

 
        public void Set(DataRow dr)
        {
            BID = Convert.ToInt32(dr["BillID"]);
            BName = Convert.ToString(dr["BillName"]);
            CID = Convert.ToInt32(dr["CategoryID"]);
            BAmt = Convert.ToDecimal(dr["BillAmount"]);
            BCharge = Convert.ToDecimal(dr["BillCharge"]);
            SInfo1 = Convert.ToString(dr["SubscriberInfo1"]);
            SInfo2 = Convert.ToString(dr["SubscriberInfo2"]);
            BCur = Convert.ToString(dr["BillCurrency"]);
        }

       
    }
}