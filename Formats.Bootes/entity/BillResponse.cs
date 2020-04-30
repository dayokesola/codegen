using Sterling.BaseLIB.Entity;
using Sterling.BaseLIB.Service;

namespace com.sbp.entity
{
    public class BillResponse
    {
        private string _responseText;
        public int RefId { get; set; }  
        public string CallerRefID { get; set; } 
        public string ResponseCode { get; set; }
        public string ResponseText
        {
            get { return _responseText; }
            set { _responseText = value.ToUpper(); }
        }

        public string BillerRef { get; set; }
        public string HashValue { get; set; }

        public void SetResponse(int i)
        {
            TabService tx = new TabService();
            Tab t = tx.GetTabEntry(99, i);
            ResponseCode = t.TabEnt.ToString("00");
            ResponseText = t.TabText;
        }

        public string BillerResp { get; set; }
    }
}