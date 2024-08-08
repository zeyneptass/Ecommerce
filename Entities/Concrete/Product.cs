using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Concrete
{
    public class Product : IEntity
    {
        public int ProductID { get; set; }
        public int CategoryID { get; set; }
        public string ProductName { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public int StockQuantity { get; set; }

        //// Navigation Properties
        //public Category Category { get; set; }
        //public ICollection<ProductImage> ProductImages { get; set; }
        //public ICollection<OrderItem> OrderItems { get; set; }
    }

}
