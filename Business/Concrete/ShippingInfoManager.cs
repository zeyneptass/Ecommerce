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
    public class ShippingInfoManager : IShippingInfoService
    {
        IShippingInfoDal _shippingInfoDal;

        public ShippingInfoManager(IShippingInfoDal shippingInfoDal)
        {
            _shippingInfoDal = shippingInfoDal;
        }

        public void AddShippingInfo(ShippingInfo shippingInfo)
        {
            _shippingInfoDal.Add(shippingInfo);
        }

        public List<ShippingInfo> GetAll()
        {
            return _shippingInfoDal.GetAll();
        }
    }
}
