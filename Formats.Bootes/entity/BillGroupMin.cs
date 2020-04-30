using System;
using System.Data;

namespace com.sbp.entity
{
    public class BillGroupMin
    {
        public int BGID { get; set; }
        public string BGName { get; set; }
        public string BGInfo { get; set; }
        public string BGIcon { get; set; }


         
        public void Set(DataRow dr)
        {
            BGID = Convert.ToInt32(dr["BillGroupID"]);
            BGName = Convert.ToString(dr["BillGroupName"]);
            BGInfo = Convert.ToString(dr["BillGroupInfo"]);
            BGIcon = Convert.ToString(dr["BillGroupIcon"]);
        }
         
    }
}