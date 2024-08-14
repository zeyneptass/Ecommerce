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
    }
}
