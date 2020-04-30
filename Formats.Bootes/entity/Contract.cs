using System;
using System.Data;
using Sterling.MSSQL;

namespace com.sbp.entity
{
    public class Contract
    {
        public int ContractID { get; set; }
        public string ContractName { get; set; }
        public int BillerID { get; set; }
        public int CategoryID { get; set; }
        public int Statusflag { get; set; }
        public DateTime DateAdded { get; set; }


        public void Load()
        {
            string sql = @"SELECT ContractID, contractName, BillerID, CategoryID ,Statusflag, DateAdded 
    FROM vew_contracts WHERE ContractID  = @ContractID ";        
            Connect cn = new Connect();
            cn.SetSQL(sql);
            cn.AddParam("@ContractID", ContractID);
            DataSet ds = cn.Select(); 
            ContractID = 0;        
            if(cn.num_rows > 0)
            {
                Set(ds.Tables[0].Rows[0]);
            }
        }

        public void Set(DataRow dr)
        {
            ContractID = Convert.ToInt32(dr["ContractID"]);
            ContractName = Convert.ToString(dr["contractName"]);
            BillerID = Convert.ToInt32(dr["BillerID"]);
            CategoryID = Convert.ToInt32(dr["CategoryID"]);
            Statusflag = Convert.ToInt32(dr["Statusflag"]); 
        DateAdded = Convert.ToDateTime(dr["DateAdded"]);  
        }

        public int Update()
        {
            string sql = @"UPDATE tbl_contracts SET Statusflag = @Statusflag WHERE ContractID = @ContractID";
            Connect cn = new Connect();
            cn.SetSQL(sql);
            cn.AddParam("@Statusflag", Statusflag);
            cn.AddParam("@ContractID", ContractID);
            return cn.Update();
        }

            public int Insert()
        {
            string sql = @"INSERT INTO tbl_contracts ( BillerID, CategoryID, Statusflag)  
VALUES ( @BillerID, @CategoryID, @Statusflag) "; 
            Connect cn = new Connect();
            cn.SetSQL(sql);  
            cn.AddParam("@BillerID", BillerID);
            cn.AddParam("@CategoryID", CategoryID); 
            cn.AddParam("@Statusflag", Statusflag); 
            ContractID = Convert.ToInt32(cn.Insert());
	        return ContractID;            
        }
    }
}