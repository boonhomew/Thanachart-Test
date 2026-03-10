
using ThanachartTest.Domain.AggregatesModel.CartAggregate.Interfaces;
using ThanachartTest.Domain.AggregatesModel.CommonAggregate.Interfaces;
using ThanachartTest.Domain.AggregatesModel.EntityAggregate;
using ThanachartTest.Domain.AggregatesModel.OrderAggregate;
using ThanachartTest.Domain.AggregatesModel.OrderAggregate.Interfaces;
using ThanachartTest.Domain.AggregatesModel.StockAggregate.Interfaces;

namespace ThanachartTest.Domain.Services
{
    public class OrderService : IOrderService
    {
        private readonly IOrderRepository _orderRepository;
        private readonly ICartRepository _cartRepository;
        private readonly IStockRepository _stockRepository;
        private readonly IUnitOfWork _unitOfWork;

        public OrderService(
            IOrderRepository orderRepository,
            ICartRepository cartRepository,
            IStockRepository stockRepository,
            IUnitOfWork unitOfWork)
        {
            _orderRepository = orderRepository;
            _cartRepository = cartRepository;
            _stockRepository = stockRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<CheckoutResponse> CheckoutAsync()
        {
            await _unitOfWork.BeginTransactionAsync();

            try
            {
                var cart = await _cartRepository.GetActiveCartAsync();
                if (cart == null)
                    throw new Exception("No active cart found");

                cart = await _cartRepository.GetCartByIdWithItemsAsync(cart.Id);

                if (!cart!.CartItems.Any())
                    throw new Exception("Cart is empty");

                // Verify stock availability
                foreach (var cartItem in cart.CartItems)
                {
                    var stock = await _stockRepository.GetByProductIdAsync(cartItem.ProductId);
                    if (stock == null || stock.Quantity < cartItem.Quantity)
                        throw new Exception($"Insufficient stock for product {cartItem.ProductId}");
                }

                // Deduct stock
                foreach (var cartItem in cart.CartItems)
                {
                    var stock = await _stockRepository.GetByProductIdAsync(cartItem.ProductId);
                    stock!.Quantity -= cartItem.Quantity;
                    stock.UpdatedAt = DateTime.UtcNow;
                    stock.UpdatedBy = "System";
                    await _stockRepository.UpdateStockAsync(stock);
                }

                // Create order
                var totalAmount = cart.CartItems.Sum(x => x.Quantity * x.Price);
                var order = new Order
                {
                    Id = Guid.NewGuid(),
                    CartId = cart.Id,
                    TotalAmount = totalAmount,
                    Status = "PENDING",
                    Price = totalAmount,
                    CreatedAt = DateTime.UtcNow,
                    CreatedBy = "System"
                };

                // Create order items
                foreach (var cartItem in cart.CartItems)
                {
                    var orderItem = new OrderItem
                    {
                        Id = Guid.NewGuid(),
                        OrderId = order.Id,
                        ProductId = cartItem.ProductId,
                        Quantity = cartItem.Quantity,
                        Price = cartItem.Price,
                        CreatedAt = DateTime.UtcNow,
                        CreatedBy = "System"
                    };
                    order.OrderItems.Add(orderItem);
                }

                await _orderRepository.AddOrderAsync(order);

                // Update cart status
                cart.Status = "CHECKOUT";
                cart.UpdatedAt = DateTime.UtcNow;
                cart.UpdatedBy = "System";
                await _cartRepository.UpdateCartAsync(cart);

                await _unitOfWork.SaveChangesAsync();
                await _unitOfWork.CommitTransactionAsync();

                return new CheckoutResponse
                {
                    OrderId = order.Id,
                    TotalAmount = totalAmount,
                    Status = "SUCCESS",
                    Message = "Order created successfully"
                };
            }
            catch (Exception ex)
            {
                await _unitOfWork.RollbackTransactionAsync();
                return new CheckoutResponse
                {
                    OrderId = Guid.Empty,
                    TotalAmount = 0,
                    Status = "FAILED",
                    Message = ex.Message
                };
            }
        }
    }
}
