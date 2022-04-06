namespace SolforbTest.Models
{
    public class EditOrderModel
    {
        public int Id { get; set; }
        public string Number { get; set; }
        public DateTime Date { get; set; }
        public int ProviderId { get; set; }
        public string Data { get; set; }

        public List<OrderItemModel> OrderItems { get; set; }
    }
}
