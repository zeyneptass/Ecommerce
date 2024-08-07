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
        void AddProductToCart(int userId, int productId, int quantity);  
        void DeleteProductFromCart(int userId, int productId);  
        void IncreaseProductNumberInCart (int cartItemId);  
        void DecreaseProductNumberInCart (int cartItemId); 
    }
}
