using Core.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Concrete
{
    public class ShippingInfo : IEntity
    {
        [Key]
        public int ShippingID { get; set; }
        public int OrderID { get; set; }
        public DateTime ShippingDate { get; set; }
        public string Carrier { get; set; }
        public string TrackingNumber { get; set; }
        public string Address { get; set; } 
        public string City { get; set; } 
        public string Country { get; set; } 
    }
}
