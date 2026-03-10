namespace ThanachartTest.Domain.AggregatesModel.CartAggregate
{
    public class UpdateCartItemRequest
    {
        public Guid CartItemId { get; set; }
        public int Quantity { get; set; }
    }
}
