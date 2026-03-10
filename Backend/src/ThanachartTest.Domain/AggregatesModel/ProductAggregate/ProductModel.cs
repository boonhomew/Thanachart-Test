namespace ThanachartTest.Domain.AggregatesModel.ProductAggregate
{
    public class ProductModel
    {
        public Guid Id { get; set; }
        public string ProductSku { get; set; } = string.Empty;
        public string ProductName { get; set; } = string.Empty;
        public decimal? ProductPrice { get; set; }
        public int StockQuantity { get; set; }
    }
}
