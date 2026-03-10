using ThanachartTest.Domain.AggregatesModel.CartAggregate;
using ThanachartTest.Domain.AggregatesModel.CartAggregate.Interfaces;
using ThanachartTest.Domain.AggregatesModel.CommonAggregate.Interfaces;
using ThanachartTest.Domain.AggregatesModel.EntityAggregate;
using ThanachartTest.Domain.AggregatesModel.ProductAggregate.Interfaces;
using ThanachartTest.Domain.AggregatesModel.StockAggregate.Interfaces;

namespace ThanachartTest.Domain.Services
{
    public class CartService : ICartService
    {
        private readonly ICartRepository _cartRepository;
        private readonly ICartItemRepository _cartItemRepository;
        private readonly IProductRepository _productRepository;
        private readonly IStockRepository _stockRepository;
        private readonly IUnitOfWork _unitOfWork;

        public CartService(
            ICartRepository cartRepository,
            ICartItemRepository cartItemRepository,
            IProductRepository productRepository,
            IStockRepository stockRepository,
            IUnitOfWork unitOfWork)
        {
            _cartRepository = cartRepository;
            _cartItemRepository = cartItemRepository;
            _productRepository = productRepository;
            _stockRepository = stockRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<CartModel> GetActiveCartAsync()
        {
            var cart = await _cartRepository.GetActiveCartAsync();

            if (cart == null)
            {
                cart = new Cart
                {
                    Id = Guid.NewGuid(),
                    Status = "ACTIVE",
                    CreatedAt = DateTime.UtcNow,
                    CreatedBy = "System"
                };
                await _cartRepository.AddCartAsync(cart);
                await _unitOfWork.SaveChangesAsync();
            }

            return await MapToCartDto(cart);
        }

        public async Task<CartModel> AddToCartAsync(AddToCartRequest request)
        {
            var cart = await _cartRepository.GetActiveCartAsync();
            if (cart == null)
            {
                cart = new Cart
                {
                    Id = Guid.NewGuid(),
                    Status = "ACTIVE",
                    CreatedAt = DateTime.UtcNow,
                    CreatedBy = "System"
                };
                await _cartRepository.AddCartAsync(cart);
            }

            var product = await _productRepository.GetProductByIdAsync(request.ProductId);
            if (product == null)
                throw new Exception("Product not found");

            var stock = await _stockRepository.GetByProductIdAsync(request.ProductId);
            if (stock == null || stock.Quantity < request.Quantity)
                throw new Exception("Insufficient stock");

            var existingCartItem = await _cartItemRepository.GetCartItemAsync(cart.Id, request.ProductId);

            if (existingCartItem != null)
            {
                var newQuantity = existingCartItem.Quantity + request.Quantity;
                if (stock.Quantity < newQuantity)
                    throw new Exception("Insufficient stock");

                existingCartItem.Quantity = newQuantity;
                existingCartItem.UpdatedAt = DateTime.UtcNow;
                existingCartItem.UpdatedBy = "System";
                await _cartItemRepository.UpdateCartItemAsync(existingCartItem);
            }
            else
            {
                var cartItem = new CartItem
                {
                    Id = Guid.NewGuid(),
                    CartId = cart.Id,
                    ProductId = request.ProductId,
                    Quantity = request.Quantity,
                    Price = product.ProductPrice ?? 0,
                    CreatedAt = DateTime.UtcNow,
                    CreatedBy = "System"
                };
                await _cartItemRepository.AddCartItemAsync(cartItem);
            }

            await _unitOfWork.SaveChangesAsync();

            cart = await _cartRepository.GetCartByIdWithItemsAsync(cart.Id);
            return await MapToCartDto(cart!);
        }

        public async Task<CartModel> UpdateCartItemAsync(UpdateCartItemRequest request)
        {
            var cartItem = await _cartItemRepository.GetCartItemByIdAsync(request.CartItemId);
            if (cartItem == null)
                throw new Exception("Cart item not found");

            if (request.Quantity <= 0)
            {
                await _cartItemRepository.DeleteCartItemAsync(cartItem);
            }
            else
            {
                var stock = await _stockRepository.GetByProductIdAsync(cartItem.ProductId);
                if (stock == null || stock.Quantity < request.Quantity)
                    throw new Exception("Insufficient stock");

                cartItem.Quantity = request.Quantity;
                cartItem.UpdatedAt = DateTime.UtcNow;
                cartItem.UpdatedBy = "System";
                await _cartItemRepository.UpdateCartItemAsync(cartItem);
            }

            await _unitOfWork.SaveChangesAsync();

            var cart = await _cartRepository.GetCartByIdWithItemsAsync(cartItem.CartId);
            return await MapToCartDto(cart!);
        }

        public async Task<CartModel> RemoveCartItemAsync(Guid cartItemId)
        {
            var cartItem = await _cartItemRepository.GetCartItemByIdAsync(cartItemId);
            if (cartItem == null)
                throw new Exception("Cart item not found");

            var cartId = cartItem.CartId;
            await _cartItemRepository.DeleteCartItemAsync(cartItem);
            await _unitOfWork.SaveChangesAsync();

            var cart = await _cartRepository.GetCartByIdWithItemsAsync(cartId);
            return await MapToCartDto(cart!);
        }

        public async Task<CartModel> ClearCartAsync()
        {
            var cart = await _cartRepository.GetActiveCartAsync();
            if (cart == null)
                throw new Exception("No active cart found");

            cart = await _cartRepository.GetCartByIdWithItemsAsync(cart.Id);

            foreach (var item in cart!.CartItems.ToList())
            {
                await _cartItemRepository.DeleteCartItemAsync(item);
            }

            await _unitOfWork.SaveChangesAsync();

            cart = await _cartRepository.GetCartByIdWithItemsAsync(cart.Id);
            return await MapToCartDto(cart!);
        }

        private async Task<CartModel> MapToCartDto(Cart cart)
        {
            cart = await _cartRepository.GetCartByIdWithItemsAsync(cart.Id);

            var cartModel = new CartModel
            {
                Id = cart!.Id,
                Status = cart.Status,
                Items = new List<CartItemModel>()
            };

            foreach (var item in cart.CartItems)
            {
                var product = await _productRepository.GetProductByIdAsync(item.ProductId);
                if (product != null)
                {
                    cartModel.Items.Add(new CartItemModel
                    {
                        Id = item.Id,
                        ProductId = item.ProductId,
                        ProductName = product.ProductName,
                        ProductSku = product.ProductSku,
                        Quantity = item.Quantity,
                        Price = item.Price
                    });
                }
            }

            return cartModel;
        }
    }
}
