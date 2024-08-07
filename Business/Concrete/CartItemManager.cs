using Business.Abstract;
using DataAccess.Abstract;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Concrete
{
    public class CartItemManager : ICartItemService
    {
        ICartItemDal _cartItemDal;

        public CartItemManager(ICartItemDal cartItemDal)
        {
            _cartItemDal = cartItemDal;
        }

        public void AddProductToCart(int userId, int productId, int quantity)
        {
            var cartItem = _cartItemDal.Get(c=> c.UserID==userId && c.ProductID==productId);
            if(cartItem != null)
            {
                cartItem.Quantity += quantity;
                _cartItemDal.Update(cartItem);
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

        public void DecreaseProductNumberInCart(int cartItemId)
        {
            var cartItem = _cartItemDal.Get(c=> c.CartItemID==cartItemId);
            if(cartItem != null && cartItem.Quantity>1)
            {
                cartItem.Quantity -= cartItem.Quantity;
                _cartItemDal.Update(cartItem);
            }
            else
            {
                throw new Exception("CartItem is not found or Quantity cannot be less than 1.");
            }
        }

        public void DeleteProductFromCart(int userId, int productId)
        {
            var cartItem = _cartItemDal.Get(c=> c.UserID == userId && c.ProductID==productId);
            if(cartItem != null)
            {
                _cartItemDal.Delete(cartItem);  
            }
            else
            {
                throw new Exception("CartItem is not found.");
            }
        }

        public void IncreaseProductNumberInCart(int cartItemId)
        {
            var cartItem = _cartItemDal.Get(c=> c.CartItemID==cartItemId);
            if(cartItem != null)
            {
                cartItem.Quantity += cartItem.Quantity;
                _cartItemDal.Update(cartItem);
            }
            else
            {
                throw new Exception("CartItem is not found.");
            }
        }
    }
}
