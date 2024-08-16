using Business.Abstract;
using Core.Services;
using DataAccess.Abstract;
using Entities.Concrete;
using Newtonsoft.Json;
using StackExchange.Redis;
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
        RedisService _redisService;

        public ProductImageManager(IProductImageDal productImageDal, RedisService redisService)
        {
            _productImageDal = productImageDal;
            _redisService = redisService;
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
        // GetAllByProductIdAsync yönteminde ürün resimlerini Redis'te önbelleğe aldım.
        public async Task<List<ProductImage>> GetAllByProductIdAsync(int productId)
        {
            var cachedImages = await _redisService.GetDatabase().StringGetAsync($"ProductImages:{productId}");

            if (!cachedImages.IsNullOrEmpty)
            {
                return JsonConvert.DeserializeObject<List<ProductImage>>(cachedImages);
            }

            var images = await _productImageDal.GetAllByProductIdAsync(productId);
                      
            await _redisService.GetDatabase().StringSetAsync($"ProductImages:{productId}", JsonConvert.SerializeObject(images), TimeSpan.FromMinutes(5));
             // ürün resimleri 5 dakika boyunca Redis'te önbelleğe alınıyor.

            return images;
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
