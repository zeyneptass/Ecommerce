using Business.Abstract;
using Entities.Concrete;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductImagesController : ControllerBase
    {
        IProductImageService _productImageService;

        public ProductImagesController(IProductImageService productImageService)
        {
            _productImageService = productImageService;
        }

        // GET: api/ProductImage/getallbyproductid/{productId}
        // https://localhost:7167/api/ProductImages/getallbyproductid/{productId}
        [HttpGet("getallbyproductid/{productId}")]
        public async Task<IActionResult> GetAllByProductId(int productId)
        {
            var result = await _productImageService.GetAllByProductIdAsync(productId);
            if (result != null)
            {
                return Ok(result);
            }
            return NotFound();
        }

        // GET: api/ProductImage/getbyid/{imageId}
        // https://localhost:7167/api/ProductImages/getbyid/{imageId}
        [HttpGet("getbyid/{imageId}")]
        public async Task<IActionResult> GetById(int imageId)
        {
            var image = await _productImageService.GetByIdAsync(imageId);
            if (image == null)
            {
                return NotFound();
            }
            return Ok(image);
        }

        // POST: api/ProductImage/addproductimage
        // https://localhost:7167/api/ProductImages/addproductimage 
        [HttpPost("addproductimage")]
        public async Task<IActionResult> Add([FromBody] ProductImage productImage)
        {
            var addedImage = await _productImageService.AddAsync(productImage);
            return CreatedAtAction("GetById", new { imageId = addedImage.ImageID }, addedImage);
        }

        // PUT: api/ProductImage/updateproductimage/{imageId}
        // https://localhost:7167/api/ProductImages/updateproductimage/{imageId}
        [HttpPut("updateproductimage/{imageId}")]
        public async Task<IActionResult> Update(int imageId, [FromBody] ProductImage productImage)
        {
            if (imageId != productImage.ImageID)
            {
                return BadRequest();
            }
            var result = await _productImageService.UpdateAsync(productImage);
            if (result != null)
            {
                return Ok(result);
            }
            return NotFound();
        }

        // DELETE: api/ProductImage/deleteproductimage/{imageId}
        // https://localhost:7167/api/ProductImages/deleteproductimage/{imageId}
        [HttpDelete("deleteproductimage/{imageId}")]
        public async Task<IActionResult> Delete(int imageId)
        {
            var existingImage = await _productImageService.GetByIdAsync(imageId);
            if (existingImage == null)
            {
                return NotFound();
            }

            await _productImageService.DeleteAsync(imageId);
            return NoContent();
        }
    }
}
