using Business.Abstract;
using Entities.Concrete;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        IProductService _productService;

        public ProductsController(IProductService productService)
        {
            _productService = productService;
        }

        // https://localhost:7167/api/Products/getallproducts 
        [HttpGet("getallproducts")]
        public List<Product> GetAllProducts()
        {
            var result = _productService.GetAllProducts();
            return result;
        }
        // https://localhost:7167/api/Products/addproduct 
        [HttpPost("addproduct")] 
        public IActionResult Add(Product product)
        {
            _productService.Add(product);
            return Ok();
        }

        // https://localhost:7167/api/Products/updateproduct 
        [HttpPut("updateproduct")]
        public IActionResult Update(Product product)
        {
            _productService.Update(product);
            return Ok();
        }

        // https://localhost:7167/api/Products/deleteproduct/id 
        [HttpDelete("deleteproduct/{id}")]
        public IActionResult Delete(int id)
        {
            var product = new Product { ProductID = id };
            _productService.Delete(product);
            return Ok();
        }
    }
}
