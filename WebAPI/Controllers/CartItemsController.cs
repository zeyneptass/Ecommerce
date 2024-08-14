using Business.Abstract;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CartItemsController : ControllerBase
    {
        ICartItemService _cartItemService;

        public CartItemsController(ICartItemService cartItemService)
        {
            _cartItemService = cartItemService;
        }

        // https://localhost:7167/api/CartItems/getCartItemsByUserId 
        [HttpGet("getCartItemsByUserId")]
        public IActionResult GetCartItemsByUserId(int userId)
        {
            var result = _cartItemService.GetCartItemsByUserId(userId);
            if (result != null)
            {
                return Ok(result);
            }
            return NotFound();
        }

        // https://localhost:7167/api/CartItems/addProductToCart 
        [HttpPost("addProductToCart")]
        public IActionResult AddProductToCart(int userId, int productId, int quantity)
        {
            _cartItemService.AddProductToCart(userId, productId, quantity);
            return Ok("Ürün sepete eklendi.");
        }

        // https://localhost:7167/api/CartItems/increaseCartItemQuantity 
        [HttpPut("increaseCartItemQuantity")]
        public IActionResult IncreaseCartItemQuantity(int cartItemId, int quantity)
        {
            try
            {
                _cartItemService.IncreaseCartItemQuantity(cartItemId, quantity);
                return Ok("Ürünün miktarı artırıldı.");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // https://localhost:7167/api/CartItems/decreaseCartItemQuantity 
        [HttpPut("decreaseCartItemQuantity")]
        public IActionResult DecreaseCartItemQuantity(int cartItemId, int quantity)
        {
            try
            {
                _cartItemService.DecreaseCartItemQuantity(cartItemId, quantity);
                return Ok("Ürünün miktarı azaltıldı.");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // https://localhost:7167/api/CartItems/updateCartItemQuantity 
        [HttpPut("updateCartItemQuantity")]
        public IActionResult UpdateCartItemQuantity(int cartItemId, int quantity)
        {
            _cartItemService.UpdateCartItemQuantity(cartItemId, quantity);
            return Ok("Ürünün miktarı güncellendi.");
        }


        // https://localhost:7167/api/CartItems/deleteProductFromCart?productId 
        [HttpDelete("deleteProductFromCart/{productId}")]
        public IActionResult DeleteProductFromCart(int productId)
        {
            _cartItemService.DeleteProductFromCart(productId);
            return Ok("Ürün sepetten silindi.");
        }

        // https://localhost:7167/api/CartItems/confirmCart
        [HttpPost("confirmCart")]
        public IActionResult ConfirmCart(int userId)
        {
            try
            {
                _cartItemService.ConfirmCart(userId);
                return Ok("Sepet onaylandı ve sipariş oluşturuldu.");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
