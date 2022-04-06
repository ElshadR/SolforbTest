using System.ComponentModel.DataAnnotations.Schema;

namespace SolforbTest.Infrastructure.Db.Models
{
    public class Order
    {
        public int Id { get; set; }
        public string Number { get; set; }
        [Column(TypeName = "datetime2(7)")]
        public DateTime Date { get; set; }
        public Provider Provider { get; set; }
        public int ProviderId { get; set; }
        public ICollection<OrderItem> OrderItems{ get; set; }
    }
}
