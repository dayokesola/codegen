namespace com.sbp.entity
{
    public class BillRequest
    {
        public int BillId { get; set; }
        public int ChannelId { get; set; }
        public string BillAmount { get; set; }
        public string BillAccount { get; set; }
        public string CallerRefID { get; set; }
        public string SubscriberInfo1 { get; set; }
        public string SubscriberInfo2 { get; set; } 
        public string HashValue { get; set; }


        public int RefId { get; set; }
        public string ProductId { get; set; }
        public string ChannelIndicator { get; set; }
    }
}