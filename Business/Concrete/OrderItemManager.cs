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
    public class OrderItemManager : IOrderItemService
    {
        IOrderItemDal _orderItemDal;

        IProductService _productService;

        public OrderItemManager(IOrderItemDal orderItemDal, IProductService productService)
        {
            _productService = productService;
            _orderItemDal = orderItemDal;
        }

        public OrderItemManager(IOrderItemDal orderItemDal)
        {
            _orderItemDal = orderItemDal;
        }

        public List<OrderItem> GetOrderItems()
        {
            return _orderItemDal.GetAll();
        }

        // Sipariş verdikten sonra ürün stok adedini günceller:
        public void UpdateStockAfterOrder(OrderItem orderItem) 
        {
            var product = _productService.GetAllProducts().FirstOrDefault(p => p.ProductID == orderItem.ProductID);
            if (product != null)
            {
                var newStockQuantity = product.StockQuantity - orderItem.Quantity;
                _productService.UpdateStockQuantity(orderItem.ProductID, newStockQuantity);
            }
            else
            {
                throw new Exception("Product is not found.");
            }
        }

     
    }
}
