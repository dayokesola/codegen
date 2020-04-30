using System;
using System.Data;
using Sterling.MSSQL;

namespace com.sbp.entity
{
    public class Category
    {
        public int CategoryID { get; set; }
        public int BillGroupID { get; set; }
        public string CategoryName { get; set; }
        public string CategoryCode { get; set; }
        public int Statusflag { get; set; }
        public int SortID { get; set; }
        public string CategoryInfo { get; set; }
        public string CategoryIcon { get; set; }
         


        public string Billgroupname { get; set; }
        public string Billgroupcode { get; set; }

        public void Load()
        {
            string sql = @"SELECT CategoryID, BillGroupID, CategoryName, CategoryCode, Statusflag, SortID, CategoryInfo, CategoryIcon FROM tbl_categories WHERE CategoryID = @CategoryID";
            Connect cn = new Connect();
            cn.SetSQL(sql);
            cn.AddParam("@CategoryID", CategoryID);

            DataSet ds = cn.Select();
            CategoryID = 0;
            if (cn.num_rows > 0)
            {
                Set(ds.Tables[0].Rows[0]);
            }
        }

        public void Set(DataRow dr)
        {
            CategoryID = Convert.ToInt32(dr["CategoryID"]);
            BillGroupID = Convert.ToInt32(dr["billgroupid"]);
            CategoryName = Convert.ToString(dr["CategoryName"]);
            CategoryCode = Convert.ToString(dr["CategoryCode"]);
            Statusflag = Convert.ToInt32(dr["Statusflag"]);
            SortID = Convert.ToInt32(dr["SortID"]);
            CategoryInfo = Convert.ToString(dr["CategoryInfo"]);
            CategoryIcon = Convert.ToString(dr["CategoryIcon"]);
        }

        public void SetView(DataRow dr)
        {
            Set(dr);
            Billgroupname = Convert.ToString(dr["billgroupname"]);
            Billgroupcode = Convert.ToString(dr["billgroupcode"]);
        }

        public int Insert()
        {
            string sql = @"INSERT INTO tbl_categories (BillGroupID, CategoryName, CategoryCode, Statusflag, SortID, CategoryInfo, CategoryIcon)  VALUES ( @BillGroupID, @CategoryName, @CategoryCode, @Statusflag, @SortID, @CategoryInfo, @CategoryIcon) ";
            Connect cn = new Connect();
            cn.SetSQL(sql);
            cn.AddParam("@BillGroupID", BillGroupID);
            cn.AddParam("@CategoryName", CategoryName);
            cn.AddParam("@CategoryCode", CategoryCode);
            cn.AddParam("@Statusflag", Statusflag);
            cn.AddParam("@SortID", SortID);
            cn.AddParam("@CategoryInfo", CategoryInfo);
            cn.AddParam("@CategoryIcon", CategoryIcon);
            cn.AddParam("@CategoryID", CategoryID);

            CategoryID = Convert.ToInt32(cn.Insert());
            return CategoryID;
        }

        public int Update()
        {
            string sql = @"UPDATE tbl_categories SET BillGroupID = @BillGroupID, 
CategoryName = @CategoryName, CategoryCode = @CategoryCode, Statusflag = @Statusflag, 
SortID = @SortID, CategoryInfo = @CategoryInfo, CategoryIcon = @CategoryIcon WHERE CategoryID = @CategoryID";
            Connect cn = new Connect();
            cn.SetSQL(sql);
            cn.AddParam("@BillGroupID", BillGroupID);
            cn.AddParam("@CategoryName", CategoryName);
            cn.AddParam("@CategoryCode", CategoryCode);
            cn.AddParam("@Statusflag", Statusflag);
            cn.AddParam("@SortID", SortID);
            cn.AddParam("@CategoryInfo", CategoryInfo);
            cn.AddParam("@CategoryIcon", CategoryIcon);
            cn.AddParam("@CategoryID", CategoryID);

            return cn.Update();
        }

        public int Delete()
        {
            string sql = "DELETE FROM tbl_categories WHERE CategoryID = @CategoryID";
            Connect cn = new Connect();
            cn.SetSQL(sql);
            cn.AddParam("@CategoryID", CategoryID);

            return cn.Delete();
        }
    }
}