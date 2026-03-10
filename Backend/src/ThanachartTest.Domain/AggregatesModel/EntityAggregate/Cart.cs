using ThanachartTest.Domain.AggregatesModel.CommonAggregate;

namespace ThanachartTest.Domain.AggregatesModel.EntityAggregate
{
    public class Cart : BaseEntity
    {
        public string Status { get; set; } = "ACTIVE"; // ACTIVE, CHECKOUT, EXPIRED

        // Navigation properties
        public ICollection<CartItem> CartItems { get; set; } = new List<CartItem>();
    }
}
