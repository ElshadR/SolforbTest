namespace SolforbTest.Models
{
    public class SearchOrderModel
    {
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int ProviderId { get; set; }
        public string ProviderName { get; set; }
        public string OrderItemName { get; set; }
        public string OrderItemUnit { get; set; }
        public string Number { get; set; }
    }
}
