namespace ThanachartTest.Domain.AggregatesModel.CartAggregate
{
    public class CartModel
    {
        public Guid Id { get; set; }
        public string Status { get; set; } = string.Empty;
        public List<CartItemModel> Items { get; set; } = new();
        public decimal TotalAmount => Items.Sum(x => x.TotalPrice);
    }
}
