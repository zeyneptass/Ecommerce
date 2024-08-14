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

        // https://localhost:7167/api/Orders/getAllOrders
        [HttpGet("getAllOrders")]
        public IActionResult GetAllOrders()
        {
            var result = _orderService.GetAllOrders();
            if (result != null)
            {
                return Ok(result);
            }
            return NotFound("Sipariş bulunamadı.");
        } 

        // Yeni bir sipariş ekler Orders veritabanına
        // https://localhost:7167/api/Orders/addNewOrder
        [HttpPost("addNewOrder")]
        public IActionResult AddNewOrder(Order order)
        {
            try
            {
                _orderService.AddNewOrder(order);
                return Ok("Yeni sipariş başarıyla eklendi.");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

    }
}
