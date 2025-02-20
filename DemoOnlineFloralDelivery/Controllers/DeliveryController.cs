using Microsoft.AspNetCore.Mvc;
using DemoOnlineFloralDelivery.Service;
using DemoOnlineFloralDelivery.Models;
using OnlineFloralDeliveryNew.Helpers;

namespace DemoOnlineFloralDelivery.Controllers;
[Route("api/delivery")]
public class DeliveryController : Controller
{
    private DeliveryService deliveryService;
    private IWebHostEnvironment webHostEnvironment;
    private IConfiguration configuration;
    public DeliveryController(DeliveryService _deliveryService, IWebHostEnvironment _webHostEnvironment, IConfiguration _configuration)
    {
        deliveryService = _deliveryService;
        webHostEnvironment = _webHostEnvironment;
        configuration = _configuration;
    }
    //Hiển thị danh sách tất cả Order
    [Produces("application/json")]
    [HttpGet("findAll")]
    public IActionResult findAllDelivery()
    {
        try
        {
            return Ok(deliveryService.findAllDelivery());
        }
        catch
        {
            return BadRequest();
        }
    }
    //
    [Consumes("application/json")]
    [Produces("application/json")]
    [HttpPost("created")]
    public IActionResult Created([FromBody] Delivery delivery)
    {
        try
        {
            return Ok(new
            {
                status = deliveryService.created(delivery)
            });
        }
        catch
        {
            return BadRequest();
        }
    }
}
