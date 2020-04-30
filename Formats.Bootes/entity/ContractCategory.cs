using Sterling.MSSQL;
using System;
using System.Data;

namespace com.sbp.entity
{
    public class ContractCategory
    {
        public int CategoryID { get; set; }
        public int BillerID { get; set; }
        public string CategoryBillerCode { get; set; }

        public void Load()
        {
            string sql = "SELECT CategoryID, BillerID, CategoryBillerCode FROM tbl_contract_categories WHERE CategoryID = @CategoryID and BillerID = @BillerID";
            Connect cn = new Connect();
            cn.SetSQL(sql);
            cn.AddParam("@CategoryID", CategoryID);
            cn.AddParam("@BillerID", BillerID);

            DataSet ds = cn.Select();
            CategoryID = 0;
            BillerID = 0;
            if (cn.num_rows > 0)
            {
                Set(ds.Tables[0].Rows[0]);
            }
        }

        public void Set(DataRow dr)
        {
            CategoryID = Convert.ToInt32(dr["CategoryID"]);
            BillerID = Convert.ToInt32(dr["BillerID"]);
            CategoryBillerCode = Convert.ToString(dr["CategoryBillerCode"]);
        }

        public int Insert()
        {
            string sql = "INSERT INTO tbl_contract_categories (CategoryID, BillerID, CategoryBillerCode)  VALUES ( @CategoryID, @BillerID, @CategoryBillerCode) ";
            Connect cn = new Connect();
            cn.SetSQL(sql);
            cn.AddParam("@CategoryBillerCode", CategoryBillerCode);
            cn.AddParam("@CategoryID", CategoryID);
            cn.AddParam("@BillerID", BillerID);
            return cn.Update();
        }

        public int Update()
        {
            string sql = "UPDATE tbl_contract_categories SET CategoryBillerCode = @CategoryBillerCode WHERE CategoryID = @CategoryID and BillerID = @BillerID";
            Connect cn = new Connect();
            cn.SetSQL(sql);
            cn.AddParam("@CategoryBillerCode", CategoryBillerCode);
            cn.AddParam("@CategoryID", CategoryID);
            cn.AddParam("@BillerID", BillerID);

            return cn.Update();
        }

        public int Delete()
        {
            string sql = "DELETE FROM tbl_contract_categories WHERE CategoryID = @CategoryID and BillerID = @BillerID";
            Connect cn = new Connect();
            cn.SetSQL(sql);
            cn.AddParam("@CategoryID", CategoryID);
            cn.AddParam("@BillerID", BillerID);

            return cn.Delete();
        }
    }
}