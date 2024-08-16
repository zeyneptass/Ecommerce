using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Abstract
{
    public interface ICartItemService
    {
        List<CartItem> GetCartItemsByUserId(int userId);
        void UpdateCartItemQuantity(int cartItemId, int quantity);
        void AddProductToCart(int userId, int productId, int quantity);  
        void DeleteProductFromCart(int productId);
        void IncreaseCartItemQuantity(int cartItemId, int quantity);
        void DecreaseCartItemQuantity(int cartItemId, int quantity);
        void ConfirmCart(int userId);

    }
}
