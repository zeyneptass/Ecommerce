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
    public class ProductManager : IProductService
    {
        IProductDal _productDal;

        public ProductManager(IProductDal productDal)
        {
            _productDal = productDal;
        }

        public void Add(Product product)
        {
            _productDal.Add(product);
        }

        public void Delete(Product product)
        {
            _productDal.Delete(product);
        }
        public List<Product> GetAll()
        {
            return _productDal.GetAll();
        }
        public void Update(Product product)
        {
            _productDal.Update(product);
        }
        public void UpdateStockQuantity(int productId, int newStockQuantity)
        {
            var product = _productDal.Get(p => p.ProductID== productId);
            if (product !=null)
            {
                product.StockQuantity = newStockQuantity;
                _productDal.Update(product);
            }
            else
            {
                throw new Exception("Product is not found.");
            }
        }
    }
}
