using DataAccess.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Concrete.EntityFramework
{
    public class EfOrderDal : IOrderDal
    {
        IOrderDal _orderDal;

        public EfOrderDal(IOrderDal orderDal)
        {
            _orderDal = orderDal;
        }
    }
}
