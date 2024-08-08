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
    public class OrderManager : IOrderService
    {
        IOrderDal _orderDal;
        IOrderItemService _orderItemService;

        public OrderManager(IOrderDal orderDal, IOrderItemService orderItemService)
        {
            _orderDal = orderDal;
            _orderItemService = orderItemService;
        }
        public OrderManager(IOrderDal orderDal)
        {
            _orderDal = orderDal;
        }

        public void Add(Order order)
        {
            _orderDal.Add(order);

            // Iterate through OrderItems and update stock for each
            foreach (var orderItem in order.OrderItems)
            {
                _orderItemService.UpdateStockAfterOrder(orderItem);
            }
        }

        public List<Order> GetAllOrders()
        {
            return _orderDal.GetAll();
        }
    }
}