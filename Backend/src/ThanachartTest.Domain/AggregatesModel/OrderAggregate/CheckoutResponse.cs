namespace ThanachartTest.Domain.AggregatesModel.OrderAggregate
{
    public class CheckoutResponse
    {
        public Guid OrderId { get; set; }
        public decimal TotalAmount { get; set; }
        public string Status { get; set; } = string.Empty;
        public string Message { get; set; } = string.Empty;
    }
}
