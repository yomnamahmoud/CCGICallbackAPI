namespace Comviva.Billing.Library.Entities
{
    public class CallbackCCG
    {
        public int ID { get; set; }
        public string MSISDN { get; set; }
        public string Result { get; set; }
        public string Reason { get; set; }
        public string ProductId { get; set; }
        public string TransID { get; set; }
        public string TPCGID { get; set; }
        public string Songname { get; set; }
    }
}