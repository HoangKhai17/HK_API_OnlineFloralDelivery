using DemoOnlineFloralDelivery.Models;
using DemoOnlineFloralDelivery.Service;
using Microsoft.AspNetCore.Mvc;

namespace DemoOnlineFloralDelivery.Controllers;
[Route("api/orderDetail")]
public class OrderDetailController : Controller
{
    private OrderDetailService orderDetailService;

    public OrderDetailController(OrderDetailService _orderDetailService)
    {
        orderDetailService = _orderDetailService;

    }
    [Consumes("application/json")]
    [Produces("application/json")]
    [HttpPost("created")]
    public IActionResult Created([FromBody] List<OrderDetail> orderDetails)
    {
        try
        {
            return Ok(new
            {
                status = orderDetailService.created(orderDetails)
            });
        }
        catch
        {
            return BadRequest();
        }
    }
}
