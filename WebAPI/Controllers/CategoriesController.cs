using Business.Abstract;
using Business.Concrete;
using DataAccess.Concrete.EntityFramework;
using Entities.Concrete;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        ICategoryService _categoryService;

        public CategoriesController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        // https://localhost:7167/api/Categories/getallcategories

        [HttpGet("getallcategories")]
        public List<Category> GetAllCategories()
        {
            var result = _categoryService.GetAllCategories();
            return result;
            
        }
        [HttpPost("addcategory")]
        public IActionResult Add(Category category)
        {
            _categoryService.Add(category);
            return Ok();  // 200 OK ile döner
        }

        [HttpPut("updatecategory")]
        public IActionResult Update(Category category)
        {
            _categoryService.Update(category);
            return Ok();  // 200 OK ile döner
        }
        [HttpDelete("deletecategory/{id}")]
        public IActionResult Delete(int id)
        {
            var category = new Category { CategoryID = id };
            _categoryService.Delete(category);
            return Ok();  // 200 OK ile döner
        }
    }
}
