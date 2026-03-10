namespace ThanachartTest.Domain.AggregatesModel.CartAggregate
{
    public class AddToCartRequest
    {
        public Guid ProductId { get; set; }
        public int Quantity { get; set; }
    }
}
