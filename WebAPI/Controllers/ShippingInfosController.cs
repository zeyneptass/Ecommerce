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

        [HttpGet("getshippinginfos")]
        public IActionResult GetAll()
        {
            var shippingInfos = _shippingInfoService.GetAll();
            return Ok(shippingInfos);
        }

        [HttpPost("addshippinginfo")]
        public IActionResult AddShippingInfo(ShippingInfo shippingInfo)
        {
            _shippingInfoService.AddShippingInfo(shippingInfo);
           //  return CreatedAtAction("GetAll", new { }, shippingInfo);
           return Ok();
        }
    }
}
