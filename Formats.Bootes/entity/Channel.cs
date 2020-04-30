using System;
using System.Data;
using Sterling.MSSQL;

namespace com.sbp.entity
{
    public class Channel
    {
        public int ChannelId { get; set; }
        public string ChannelName { get; set; }
        public string ChannelCode { get; set; }
        public string ChannelKey { get; set; }
        public int Statusflag { get; set; }
        public DateTime DateAdded { get; set; }
        public int ChannelTypeID { get; set; }

        public Channel()
        {
        }

        public Channel(int id)
        {
            ChannelId = id;
            Load();
        }
        public void Load()
        {
            string sql = @"SELECT ChannelId, ChannelName, ChannelCode, Statusflag, DateAdded, 
ChannelTypeID,ChannelKey FROM tbl_channels WHERE ChannelId = @ChannelId";
            Connect cn = new Connect();
            cn.SetSQL(sql);
            cn.AddParam("@ChannelId", ChannelId);

            DataSet ds = cn.Select();
            ChannelId = 0;
            if (cn.num_rows > 0)
            {
                Set(ds.Tables[0].Rows[0]);
            }
        }

        public void Set(DataRow dr)
        {
            ChannelId = Convert.ToInt32(dr["ChannelId"]);
            ChannelName = Convert.ToString(dr["ChannelName"]);
            ChannelCode = Convert.ToString(dr["ChannelCode"]);
            ChannelKey = Convert.ToString(dr["ChannelKey"]);
            Statusflag = Convert.ToInt32(dr["Statusflag"]); 
            DateAdded = Convert.ToDateTime(dr["DateAdded"]);
            ChannelTypeID = Convert.ToInt32(dr["ChannelTypeID"]);
        }

        public int Insert()
        {
            string sql = @"INSERT INTO tbl_channels 
(ChannelName, ChannelCode, Statusflag, DateAdded, ChannelTypeID,ChannelKey)  
VALUES ( @ChannelName, @ChannelCode, @Statusflag, @DateAdded, @ChannelTypeID,@ChannelKey) ";
            Connect cn = new Connect();
            cn.SetSQL(sql);
            cn.AddParam("@ChannelName", ChannelName);
            cn.AddParam("@ChannelCode", ChannelCode);
            cn.AddParam("@Statusflag", Statusflag);
            cn.AddParam("@DateAdded", DateAdded);
            cn.AddParam("@ChannelTypeID", ChannelTypeID);
            cn.AddParam("@ChannelKey", ChannelKey);
            cn.AddParam("@ChannelId", ChannelId);

            ChannelId = Convert.ToInt32(cn.Insert());
            return ChannelId;
        }

        public int Update()
        {
            string sql = @"UPDATE tbl_channels SET ChannelName = @ChannelName, ChannelCode = @ChannelCode, 
Statusflag = @Statusflag, DateAdded = @DateAdded, ChannelTypeID = @ChannelTypeID, ChannelKey = @ChannelKey WHERE ChannelId = @ChannelId";
            Connect cn = new Connect();
            cn.SetSQL(sql);
            cn.AddParam("@ChannelName", ChannelName);
            cn.AddParam("@ChannelCode", ChannelCode);
            cn.AddParam("@Statusflag", Statusflag);
            cn.AddParam("@DateAdded", DateAdded);
            cn.AddParam("@ChannelTypeID", ChannelTypeID);
            cn.AddParam("@ChannelKey", ChannelKey);
            cn.AddParam("@ChannelId", ChannelId);

            return cn.Update();
        }

        public int Delete()
        {
            string sql = "DELETE FROM tbl_channels WHERE ChannelId = @ChannelId";
            Connect cn = new Connect();
            cn.SetSQL(sql);
            cn.AddParam("@ChannelId", ChannelId);

            return cn.Delete();
        }
    }
}