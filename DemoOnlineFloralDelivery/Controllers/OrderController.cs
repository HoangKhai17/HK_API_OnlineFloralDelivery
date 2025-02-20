using DemoOnlineFloralDelivery.Models;
using DemoOnlineFloralDelivery.Service;
using Microsoft.AspNetCore.Cors.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using System;

namespace DemoOnlineFloralDelivery.Controllers;
[Route("api/order")]
public class OrderController : Controller
{
    private OrderService orderService;

    public OrderController(OrderService _orderService)
    {
        orderService = _orderService;

    }


[Consumes("application/json")]
[Produces("application/json")]
[HttpPost("created")]
public async Task<IActionResult> Created([FromBody] Order order)
    {
        try
        {
            var status = orderService.created(order);
            return Ok(new { status });
        }
        catch
        {
            return BadRequest();
        }
    }

    [Produces("application/json")]
    [HttpGet("findall")]
    public IActionResult FindAll()
    {
        try
        {
            var fillAll = orderService.findAll();
            return Ok(fillAll);
        }
        catch (Exception ex)
        {
            // ma 400: la co loi roi
            return BadRequest(ex);
        }
    }

    [Produces("application/json")]
    [HttpGet("findByAccountId/{accountId}")]
    public IActionResult FindByAccountId(int accountId)
    {
        try
        {
            var fillAll = orderService.findByAccountId(accountId);
            return Ok(fillAll);
        }
        catch (Exception ex)
        {
            // ma 400: la co loi roi
            return BadRequest(ex);
        }
    }

    [Produces("application/json")]
    [HttpGet("findByAccountId2/{accountId}")]
    public IActionResult FindByAccountId2(int accountId)
    {
        try
        {
            var fillAll = orderService.findByAccountId2(accountId);
            return Ok(fillAll);
        }
        catch (Exception ex)
        {
            // ma 400: la co loi roi
            return BadRequest(ex);
        }
    }

    [Produces("application/json")]
    [HttpGet("findByOrderIdAdmin/{orderId}")]
    public IActionResult FindByOrderIdAdmin(int orderId)
    {
        try
        {
            var fillAll = orderService.findByOrderIdAdmin(orderId);
            return Ok(fillAll);
        }
        catch (Exception ex)
        {
            // ma 400: la co loi roi
            return BadRequest(ex);
        }
    }
    

    [Consumes("application/json")]
    [Produces("application/json")]
    [HttpGet("updateOrderStatus/{orderId}")]
    public IActionResult UpdateOrderStatus(int orderId)
    {
        try
        {
            return Ok(new
            {
                status = orderService.UpdateOrderStatus(orderId)
            });
        }
        catch
        {
            return BadRequest();
        }
    }
}
