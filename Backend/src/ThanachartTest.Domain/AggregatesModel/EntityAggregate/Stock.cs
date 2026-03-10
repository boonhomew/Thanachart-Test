using ThanachartTest.Domain.AggregatesModel.CommonAggregate;

namespace ThanachartTest.Domain.AggregatesModel.EntityAggregate
{
    public class Stock : BaseEntity
    {
        public Guid ProductId { get; set; }
        public int Quantity { get; set; }

        // Navigation property
        public Product? Product { get; set; }
    }
}
