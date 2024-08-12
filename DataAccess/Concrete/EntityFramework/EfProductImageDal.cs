using Core.DataAccess.EntityFramework;
using DataAccess.Abstract;
using Entities.Concrete;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Concrete.EntityFramework
{
    public class EfProductImageDal : EfEntityRepositoryBase<ProductImage, EcommerceContext>, IProductImageDal
    {
        public async Task DeleteAsync(ProductImage entity)
        {
            using (var context = new EcommerceContext())
            {
                context.Entry(entity).State = EntityState.Deleted;
                await context.SaveChangesAsync();
            } 
        }

        public async Task<List<ProductImage>> GetAllByProductIdAsync(int productId)
        {
            using (var context = new EcommerceContext())
            {
                return await context.ProductImages
                    .Where(pi => pi.ProductID == productId)
                    .ToListAsync();
            }
        }
        public async Task<ProductImage> GetByIdAsync(int imageId)
        {
            using (var context = new EcommerceContext())
            {
                return await context.ProductImages.FirstOrDefaultAsync(pi => pi.ImageID == imageId);
            }
        }
        public async Task<ProductImage> AddAsync(ProductImage productImage)
        {
            using (var context = new EcommerceContext())
            {
                await context.ProductImages.AddAsync(productImage);
                await context.SaveChangesAsync();
                return productImage;
            }
        }
        public async Task<ProductImage> UpdateAsync(ProductImage productImage)
        {
            using (var context = new EcommerceContext())
            {
                context.Entry(productImage).State = EntityState.Modified;
                await context.SaveChangesAsync();
                return productImage;
            }
        }

    }
}
