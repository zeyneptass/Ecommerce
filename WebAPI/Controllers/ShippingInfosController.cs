using Business.Abstract;
using Entities.Concrete;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ShippingInfosController : ControllerBase
    {
        IShippingInfoService _shippingInfoService;

        public ShippingInfosController(IShippingInfoService shippingInfoService)
        {
            _shippingInfoService = shippingInfoService;
        }



        // https://localhost:7167/api/ShippingInfo/getAll
        [HttpGet("getshippinginfos")]
        public IActionResult GetAll()
        {
            var result = _shippingInfoService.GetAll();
            if (result != null)
            {
                return Ok(result);
            }
            return NotFound();
        }

        // https://localhost:7167/api/ShippingInfo/addShippingInfo
        [HttpPost("addShippingInfo")]
        public IActionResult AddShippingInfo([FromBody] ShippingInfo shippingInfo)
        {
            if (shippingInfo != null)
            {
                _shippingInfoService.AddShippingInfo(shippingInfo);
                return Ok("Shipping info eklendi.");
            }
            return BadRequest("Geçersiz shipping info.");
        }
    }
}
