using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Abstract
{
    public interface IProductImageService
    {
        Task<List<ProductImage>> GetAllByProductIdAsync(int productId);
        Task<ProductImage> GetByIdAsync(int imageId);
        Task<ProductImage> AddAsync(ProductImage productImage);
        Task<ProductImage> UpdateAsync(ProductImage productImage);
        Task DeleteAsync(int imageId);
    }
}
