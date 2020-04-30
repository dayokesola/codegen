using Sterling.MSSQL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace com.sbp.entity
{
    public class ContractBillGroup
    {
        public int BillGroupId { get; set; }
        public int BillerID { get; set; }
        public string BillGroupBillerCode { get; set; }


        public void Load()
        {
            string sql = @"SELECT BillGroupId, BillerID, BillGroupBillerCode FROM tbl_contract_bill_groups 
WHERE BillGroupId = @BillGroupId and BillerID = @BillerID";
            Connect cn = new Connect();
            cn.SetSQL(sql);
            cn.AddParam("@BillGroupId", BillGroupId);
            cn.AddParam("@BillerID", BillerID);

            DataSet ds = cn.Select();
            BillGroupId = 0;
            if (cn.num_rows > 0)
            {
                Set(ds.Tables[0].Rows[0]);
            }
        }

        public void Set(DataRow dr)
        {
            BillGroupId = Convert.ToInt32(dr["BillGroupId"]);
            BillerID = Convert.ToInt32(dr["BillerID"]);
            BillGroupBillerCode = Convert.ToString(dr["BillGroupBillerCode"]);
        }

        public int Insert()
        {
            string sql = @"INSERT INTO tbl_contract_bill_groups (BillGroupId, BillerID, BillGroupBillerCode)  
VALUES ( @BillGroupId, @BillerID, @BillGroupBillerCode) ";
            Connect cn = new Connect();
            cn.SetSQL(sql);
            cn.AddParam("@BillGroupBillerCode", BillGroupBillerCode);
            cn.AddParam("@BillGroupId", BillGroupId);
            cn.AddParam("@BillerID", BillerID);
             
            return cn.Update();
        }

        public int Update()
        {
            string sql = @"UPDATE tbl_contract_bill_groups SET BillGroupBillerCode = @BillGroupBillerCode 
WHERE BillGroupId = @BillGroupId and BillerID = @BillerID";
            Connect cn = new Connect();
            cn.SetSQL(sql);
            cn.AddParam("@BillGroupBillerCode", BillGroupBillerCode);
            cn.AddParam("@BillGroupId", BillGroupId);
            cn.AddParam("@BillerID", BillerID);
            return cn.Update();
        }

        public int Delete()
        {
            string sql = "DELETE FROM tbl_contract_bill_groups WHERE BillGroupId = @BillGroupId and BillerID = @BillerID";
            Connect cn = new Connect();
            cn.SetSQL(sql);
            cn.AddParam("@BillGroupId", BillGroupId);
            cn.AddParam("@BillerID", BillerID);

            return cn.Delete();
        }
    }
}
