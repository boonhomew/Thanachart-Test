using ThanachartTest.Domain.AggregatesModel.CommonAggregate;

namespace ThanachartTest.Domain.AggregatesModel.EntityAggregate
{
    public class Product : BaseEntity
    {
        public string ProductSku { get; set; } = string.Empty;
        public string ProductName { get; set; } = string.Empty;
        public decimal? ProductPrice { get; set; }

        // Navigation property
        public Stock? Stock { get; set; }
    }
}
