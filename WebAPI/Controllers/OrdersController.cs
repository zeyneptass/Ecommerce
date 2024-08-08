using Business.Abstract;
using Entities.Concrete;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        IOrderService _orderService;

        public OrdersController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        [HttpGet("getallorders")]
        public List<Order> GetAllOrders()
        {
            var result = _orderService.GetAllOrders();
            return result;
        }
        [HttpPost("addorders")]
        public IActionResult Add(Order order)
        {
            _orderService.Add(order);
            return Ok();
        }

    }
}
