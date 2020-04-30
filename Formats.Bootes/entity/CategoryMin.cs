using System;
using System.Data;

namespace com.sbp.entity
{
    public class CategoryMin
    {
        public int CID { get; set; }
        public int BGID { get; set; }
        public string CName { get; set; }
        public string CInfo { get; set; }
        public string CIcon { get; set; }


         
        public void Set(DataRow dr)
        {
            CID = Convert.ToInt32(dr["CategoryID"]);
            BGID = Convert.ToInt32(dr["billgroupid"]);
            CName = Convert.ToString(dr["CategoryName"]);
            CInfo = Convert.ToString(dr["CategoryInfo"]);
            CIcon = Convert.ToString(dr["CategoryIcon"]);
        }
         
    }
}