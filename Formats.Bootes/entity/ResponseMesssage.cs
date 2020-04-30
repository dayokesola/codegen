using Sterling.BaseLIB.Entity;
using Sterling.BaseLIB.Service;

namespace com.sbp.entity
{
    public class ResponseMesssage
    {
        public string ResponseCode { get; set; }
        public string ResponseText { get; set; }

        public void Set(int i)
        {
            TabService tx = new TabService();
            Tab t = tx.GetTabEntry(99, i);
            ResponseCode = t.TabEnt.ToString("00");
            ResponseText = t.TabText;
        }
    }
}