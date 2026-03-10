using ThanachartTest.Domain.AggregatesModel.CommonAggregate;

namespace ThanachartTest.Domain.AggregatesModel.EntityAggregate
{
    public class Order : BaseEntity
    {
        public Guid CartId { get; set; }
        public decimal TotalAmount { get; set; }
        public string Status { get; set; } = "PENDING"; // PENDING, PAID
        public decimal Price { get; set; }

        // Navigation properties
        public Cart? Cart { get; set; }
        public ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>();
    }
}
