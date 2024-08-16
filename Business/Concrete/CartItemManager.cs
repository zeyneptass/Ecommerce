using Business.Abstract;
using DataAccess.Abstract;
using Entities.Concrete;

public class CartItemManager : ICartItemService
{
    ICartItemDal _cartItemDal;
    IProductDal _productDal;
    IOrderService _orderService;
    IShippingInfoService _shippingInfoService;

    public CartItemManager(ICartItemDal cartItemDal, IProductDal productDal, IOrderService orderService, IShippingInfoService shippingInfoService)
    {
        _cartItemDal = cartItemDal;
        _productDal = productDal;
        _orderService = orderService;
        _shippingInfoService = shippingInfoService;
    }

    public void AddProductToCart(int userId, int productId, int quantity)
    {
        var existingCartItem = _cartItemDal.Get(c => c.UserID == userId && c.ProductID == productId);

        if (existingCartItem != null)
        {
            existingCartItem.Quantity += quantity;
            _cartItemDal.Update(existingCartItem);
        }
        else
        {
            var newCartItem = new CartItem
            {
                UserID = userId,
                ProductID = productId,
                Quantity = quantity
            };
            _cartItemDal.Add(newCartItem);
        }
    }

    public void DecreaseCartItemQuantity(int cartItemId, int quantity = 1)
    {
        var cartItem = _cartItemDal.Get(c => c.CartItemID == cartItemId);
        if (cartItem != null)
        {
            if (cartItem.Quantity >= quantity)
            {
                cartItem.Quantity -= quantity;
                _cartItemDal.Update(cartItem);
            }
            else
            {
                throw new Exception("Sepette bu kadar ürün bulunmamaktadır.");
            }

            if (cartItem.Quantity <= 0)
            {
                throw new Exception("Sepette zaten hiç ürün yok");
            }
        }
        else
        {
            throw new Exception("Sepette böyle bir ürün bulunmamaktadır.");
        }
    }

    public void DeleteProductFromCart(int productId)
    {
        var cartItem = _cartItemDal.Get(c => c.ProductID == productId);
        if (cartItem != null)
        {
            IncreaseProductStock(productId, cartItem.Quantity);
            _cartItemDal.Delete(cartItem);
        }
    }

    public List<CartItem> GetCartItemsByUserId(int userId)
    {
        return _cartItemDal.GetAll(c => c.UserID == userId).ToList();
    }

    public void IncreaseCartItemQuantity(int cartItemId, int quantity = 1)
    {
        var cartItem = _cartItemDal.Get(c => c.CartItemID == cartItemId);
        if (cartItem != null)
        {
            var product = _productDal.Get(p => p.ProductID == cartItem.ProductID);

            if (product != null && product.StockQuantity > 0)
            {
                if (product.StockQuantity >= quantity)
                {
                    cartItem.Quantity += quantity;
                    _cartItemDal.Update(cartItem);
                }
                else
                {
                    throw new Exception("Stokta yeterli ürün bulunmamaktadır.");
                }
            }
            else
            {
                throw new Exception("Bu ürün stokta bulunmamaktadır.");
            }
        }
    }

    public void UpdateCartItemQuantity(int cartItemId, int quantity)
    {
        var cartItem = _cartItemDal.Get(c => c.CartItemID == cartItemId);
        if (cartItem != null)
        {
            cartItem.Quantity = quantity;
            _cartItemDal.Update(cartItem);
        }
        else
        {
            throw new Exception("Sepette böyle bir ürün bulunmamaktadır.");
        }
    }

    public void ConfirmCart(int userId)
    {
        var cartItems = _cartItemDal.GetAll(c => c.UserID == userId).ToList();

        if (!cartItems.Any())
        {
            throw new Exception("Sepetinizde ürün bulunmamaktadır.");
        }

        var order = CreateOrder(cartItems);
        _orderService.AddNewOrder(order);

        AddShippingInfo(order, cartItems);
        ClearCart(cartItems);
        UpdateStockAfterOrder(cartItems);
    }

    private Order CreateOrder(List<CartItem> cartItems)
    {
        return new Order
        {
            UserID = cartItems.First().UserID, 
            OrderDate = DateTime.Now,
            TotalAmount = cartItems.Sum(c => c.Quantity * _productDal.Get(p => p.ProductID == c.ProductID).Price),
            ShippingAddress = "Default Address", 
            ShippingCity = "Default City",
            ShippingCountry = "Default Country",
            OrderItems = cartItems.Select(c => new OrderItem
            {
                ProductID = c.ProductID,
                Quantity = c.Quantity,
                UnitPrice = _productDal.Get(p => p.ProductID == c.ProductID).Price
            }).ToList()
        };
    }

    private void AddShippingInfo(Order order, List<CartItem> cartItems)
    {
        var shippingInfo = new ShippingInfo
        {
            OrderID = order.OrderID,
            ShippingDate = DateTime.Now,
            Carrier = "Default Carrier", 
            TrackingNumber = "Default Tracking Number",
            Address = order.ShippingAddress,
            City = order.ShippingCity,
            Country = order.ShippingCountry
        };

        _shippingInfoService.AddShippingInfo(shippingInfo);
    }

    private void ClearCart(List<CartItem> cartItems)
    {
        foreach (var cartItem in cartItems)
        {
            _cartItemDal.Delete(cartItem);
        }
    }

    private void UpdateStockAfterOrder(List<CartItem> cartItems)
    {
        foreach (var cartItem in cartItems)
        {
            var product = _productDal.Get(p => p.ProductID == cartItem.ProductID);
            if (product != null)
            {
                if (product.StockQuantity >= cartItem.Quantity)
                {
                    DecreaseProductStock(cartItem.ProductID, cartItem.Quantity);
                }
                else
                {
                    throw new Exception($"Ürün ID {cartItem.ProductID} için yeterli stok bulunmamaktadır.");
                }
            }
            else
            {
                throw new Exception($"Ürün ID {cartItem.ProductID} bulunmamaktadır.");
            }
        }
    }

    private void DecreaseProductStock(int productId, int quantity)
    {
        var product = _productDal.Get(p => p.ProductID == productId);
        if (product != null && product.StockQuantity >= quantity)
        {
            product.StockQuantity -= quantity;
            _productDal.Update(product);
        }
    }

    private void IncreaseProductStock(int productId, int quantity)
    {
        var product = _productDal.Get(p => p.ProductID == productId);
        if (product != null)
        {
            product.StockQuantity += quantity;
            _productDal.Update(product);
        }
    }
}
