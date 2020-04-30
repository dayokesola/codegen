using System;
using System.Data;
using Sterling.MSSQL;

namespace com.sbp.entity
{
    public class ContractBill
    {
        public int BillID { get; set; }
        public int ContractID { get; set; }
        public string ContractBillCode { get; set; }
        public decimal MinValue { get; set; }
        public decimal MaxValue { get; set; }
         
        public string ContractName { get; set; }
        public DateTime DateAdded { get; set; }
        public string BillName { get; set; }
        public string BillCode { get; set; }
        public int SortID { get; set; }


        public void Load()
        {
            string sql = @"SELECT BillID, ContractBillCode, ContractID, MinValue, MaxValue, 
contractName, DateAdded, BillName, BillCode, SortID FROM vew_contract_bills 
WHERE BillID = @BillID and ContractID = @ContractID";
            Connect cn = new Connect();
            cn.SetSQL(sql);
            cn.AddParam("@BillID", BillID);
            cn.AddParam("@ContractID", ContractID);

            DataSet ds = cn.Select();
            BillID = 0;
            if (cn.num_rows > 0)
            {
                Set(ds.Tables[0].Rows[0]);
            }
        }

        public void Set(DataRow dr)
        {
            BillID = Convert.ToInt32(dr["BillID"]);
            ContractID = Convert.ToInt32(dr["ContractID"]);
            ContractBillCode = Convert.ToString(dr["ContractBillCode"]);
            MinValue = Convert.ToDecimal(dr["MinValue"]);
            MaxValue = Convert.ToDecimal(dr["MaxValue"]);

            ContractName = Convert.ToString(dr["contractName"]);
            DateAdded = Convert.ToDateTime(dr["DateAdded"]);
            BillName = Convert.ToString(dr["BillName"]);
            BillCode = Convert.ToString(dr["BillCode"]);
            SortID = Convert.ToInt32(dr["SortID"]);    
        }

        public int Insert()
        {
            string sql = @"INSERT INTO tbl_contract_bills 
(BillID, ContractID, ContractBillCode, MinValue, MaxValue)  
VALUES ( @BillID, @ContractID, @ContractBillCode, @MinValue, @MaxValue) ";
            Connect cn = new Connect();
            cn.SetSQL(sql);
            cn.AddParam("@ContractBillCode", ContractBillCode);
            cn.AddParam("@MinValue", MinValue);
            cn.AddParam("@MaxValue", MaxValue);
            cn.AddParam("@BillID", BillID);
            cn.AddParam("@ContractID", ContractID);
            return cn.Update();
        }

        public int Update()
        {
            string sql = @"UPDATE tbl_contract_bills SET ContractBillCode = @ContractBillCode, 
MinValue = @MinValue, MaxValue = @MaxValue WHERE BillID = @BillID and ContractID = @ContractID";
            Connect cn = new Connect();
            cn.SetSQL(sql);
            cn.AddParam("@ContractBillCode", ContractBillCode);
            cn.AddParam("@MinValue", MinValue);
            cn.AddParam("@MaxValue", MaxValue);
            cn.AddParam("@BillID", BillID);
            cn.AddParam("@ContractID", ContractID);

            return cn.Update();
        }

        public int Delete()
        {
            string sql = "DELETE FROM tbl_contract_bills " +
                         "WHERE BillID = @BillID and ContractID = @ContractID";
            Connect cn = new Connect();
            cn.SetSQL(sql);
            cn.AddParam("@BillID", BillID);
            cn.AddParam("@ContractID", ContractID);

            return cn.Delete();
        }
    }
}