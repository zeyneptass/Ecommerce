using Core.DataAccess;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Abstract
{
    public interface IProductImageDal: IEntityRepository<ProductImage>
    {
        Task DeleteAsync(ProductImage entity);
        Task<List<ProductImage>> GetAllByProductIdAsync(int productId);
        Task<ProductImage> GetByIdAsync(int imageId); // GetByIdAsync fonksiyonunu DeleteAsync fonksyionu içerisinde kullandım.
        Task<ProductImage> AddAsync(ProductImage productImage);
        Task<ProductImage> UpdateAsync(ProductImage productImage);
    }
}
