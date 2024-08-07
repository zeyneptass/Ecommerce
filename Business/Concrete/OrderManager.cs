using Business.Abstract;
using DataAccess.Abstract;
using DataAccess.Concrete.EntityFramework;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Concrete
{
    public class OrderManager:IOrderService
    {
        IOrderDal _orderDal;
        IProductService _productService;

        public OrderManager(IOrderDal orderDal)
        {
            _orderDal = orderDal;
        }
        public OrderManager(IOrderDal orderDal, IProductService productService)
        {
            _orderDal = orderDal;
            _productService = productService;
        }

        public void Add(Order order)
        {
            _orderDal.Add(order);
            UpdateStockAfterOrder(order);
        }

        public List<Order> GetAll()
        {
            return _orderDal.GetAll();
        }

        public void UpdateStockAfterOrder(Order order)
        {
            foreach (var orderItem in order.OrderItems)
            {
                var product = _productService.GetAll().FirstOrDefault(p => p.ProductID == orderItem.ProductID);
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
}
