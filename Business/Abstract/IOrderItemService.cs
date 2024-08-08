using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Abstract
{
    public interface IOrderItemService
    {
        List<OrderItem> GetOrderItems();
        void UpdateStockAfterOrder(OrderItem orderItem);
    }
}
