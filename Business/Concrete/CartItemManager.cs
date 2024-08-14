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
        IProductDal _productDal;

        public CartItemManager(ICartItemDal cartItemDal, IProductDal productDal)
        {
            _cartItemDal = cartItemDal;
            _productDal = productDal;
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

            DecreaseProductStock(productId, quantity);
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

                    IncreaseProductStock(cartItem.ProductID, quantity);
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

                        DecreaseProductStock(cartItem.ProductID, quantity);
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
                var currentQuantity = cartItem.Quantity;
                cartItem.Quantity = quantity;
                _cartItemDal.Update(cartItem);

                var quantityDifference = currentQuantity - quantity;

                if (quantityDifference > 0)
                {
                    IncreaseProductStock(cartItem.ProductID, quantityDifference);
                }
                else if (quantityDifference < 0)
                {
                    DecreaseProductStock(cartItem.ProductID, -quantityDifference);
                }
            }
        }

        //  Ürünün stok miktarını azaltır:
        private void DecreaseProductStock(int productId, int quantity)
        {
            var product = _productDal.Get(p => p.ProductID == productId);
            if (product != null && product.StockQuantity >= quantity)
            {
                product.StockQuantity -= quantity;
                _productDal.Update(product);
            }
        }

        //  Ürünün stok miktarını arttırır:
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
}
