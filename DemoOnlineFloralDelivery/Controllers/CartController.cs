using Microsoft.AspNetCore.Mvc;
using DemoOnlineFloralDelivery.Models;
using DemoOnlineFloralDelivery.Service;

namespace DemoOnlineFloralDelivery.Controllers;
[Route("api/cart")]
public class CartController : Controller
{
    private CartService cartService;
    private DatabaseContext db;
    public CartController(CartService _cartService, DatabaseContext _db)
    {
        cartService = _cartService;
        db = _db;
    }
    //Hiển thị danh sách tất cả Cart
    [Produces("application/json")]
    [HttpGet("findAllCart")]
    public IActionResult FindAllCart()
    {
        try
        {
            return Ok(cartService.findAllCart());
        }
        catch
        {
            return BadRequest();
        }
    }

    //Hiển thị danh sách theo accountName
    [Produces("application/json")]
    [HttpGet("findByAccountName/{accountName}")]
    public IActionResult FindByAccountName(string accountName)
    {
        try
        {
            return Ok(cartService.findByAccountName(accountName));
        }
        catch
        {
            return BadRequest();
        }
    }

    //Thêm Cart
    [Consumes("application/json")]
    [Produces("application/json")]
    [HttpPost("addCart")]
    public IActionResult AddCart([FromBody] Cart _Cart)
    {
        try
        {
            // Kiểm tra xem bouquetId và accountId đã tồn tại trong bảng cart hay chưa
            bool isExist = db.Carts.Any(be => be.BouquetId == _Cart.BouquetId &&  be.AccountId == _Cart.AccountId);
            if (isExist)
            {
                // Nếu đã tồn tại, trả về lỗi
                return BadRequest(new { status = "BouquetId and AccountId already exist in cart." });
            }

            // Nếu không tồn tại, thêm dữ liệu vào bảng cart
            return Ok(new { status = cartService.addCart(_Cart) });
        }
        catch
        {
            return BadRequest();
        }
    }


    //Sửa Cart
    [Consumes("application/json")]
    [Produces("application/json")]
    [HttpPut("updateCart")]
    public IActionResult UpdateCart([FromBody] Cart _Cart)
    {
        try
        {
            return Ok(new
            {
                status = cartService.updateCart(_Cart)
            });
        }
        catch
        {
            return BadRequest();
        }
    }

    [Produces("application/json")]
    [HttpDelete("deleteCart/{cartId}")]
    public IActionResult DeleteCart(int cartId)
    {
        try
        {
            return Ok(new
            {
                Result = cartService.DeleteCart(cartId)
            });
        }
        catch (Exception ex)
        {
            // ma 400: la co loi roi
            return BadRequest(ex);
        }
    }


    [Produces("application/json")]
    [HttpDelete("deleteCartByAccountId/{accountId}")]
    public IActionResult DeleteCartByAccountId(int accountId)
    {
        try
        {
            return Ok(new
            {
                Result = cartService.DeleteCartByAccountId(accountId)
            });
        }
        catch (Exception ex)
        {
            // ma 400: la co loi roi
            return BadRequest(ex);
        }
    }

    //Hiển thị danh sách theo accountName
    [Produces("application/json")]
    [HttpGet("findByCartId/{cartId}")]
    public IActionResult FindByCartId(int cartId)
    {
        try
        {
            return Ok(cartService.findByCartId(cartId));
        }
        catch
        {
            return BadRequest();
        }
    }
}
