using ThanachartTest.Domain.AggregatesModel.CommonAggregate;

namespace ThanachartTest.Domain.AggregatesModel.EntityAggregate
{
    public class OrderItem : BaseEntity
    {
        public Guid OrderId { get; set; }
        public Guid ProductId { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }

        // Navigation properties
        public Order? Order { get; set; }
        public Product? Product { get; set; }
    }
}
