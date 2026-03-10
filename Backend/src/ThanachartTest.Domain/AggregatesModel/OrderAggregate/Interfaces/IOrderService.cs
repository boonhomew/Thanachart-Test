
namespace ThanachartTest.Domain.AggregatesModel.OrderAggregate.Interfaces
{
    public interface IOrderService
    {
        Task<CheckoutResponse> CheckoutAsync();

    }
}
