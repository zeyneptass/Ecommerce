using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Concrete
{
    public class ShippingInfo : IEntity
    {
        public int ShippingID { get; set; }
        public int OrderID { get; set; }
        public DateTime ShippingDate { get; set; }
        public string Carrier { get; set; }
        public string TrackingNumber { get; set; }

        // Navigation Property
        public Order Order { get; set; }
    }

}
