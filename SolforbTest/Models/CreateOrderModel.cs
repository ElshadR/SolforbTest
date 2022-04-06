namespace SolforbTest.Models
{
    public class CreateOrderModel
    {
        public string Number { get; set; }
        public DateTime Date { get; set; }
        public int ProviderId { get; set; }

        public List<OrderItemModel> OrderItems { get; set; }
    }
}
