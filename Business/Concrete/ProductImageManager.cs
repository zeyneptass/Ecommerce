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
    public class ProductImageManager : IProductImageService
    {
        IProductImageDal _productImageDal;

        public ProductImageManager(IProductImageDal productImageDal)
        {
            _productImageDal = productImageDal;
        }

        public async Task<ProductImage> AddAsync(ProductImage productImage)
        {
            return await _productImageDal.AddAsync(productImage);
        }

        public async Task DeleteAsync(int imageId)
        {
            var productImage = await _productImageDal.GetByIdAsync(imageId);
            if (productImage != null)
            {
                await _productImageDal.DeleteAsync(productImage);
            }
        }
        public async Task<List<ProductImage>> GetAllByProductIdAsync(int productId)
        {
            return await _productImageDal.GetAllByProductIdAsync(productId);
        }

        public async Task<ProductImage> GetByIdAsync(int imageId)
        {
            return await _productImageDal.GetByIdAsync(imageId);
        }


        public async Task<ProductImage> UpdateAsync(ProductImage productImage)
        {
            return await _productImageDal.UpdateAsync(productImage);
        }
    }
}
