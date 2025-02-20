using Microsoft.AspNetCore.Mvc;
using DemoOnlineFloralDelivery.Models;
using DemoOnlineFloralDelivery.Service;

namespace DemoOnlineFloralDelivery.Controllers;
[Route("api/role")]

public class RoleController : Controller
{
    private RoleService roleService;
    public RoleController(RoleService _roleService)
    {
        roleService = _roleService;
    }

    // SHOW ALL ROLES
    [Produces("application/json")]
    [HttpGet("showAll")]
    public IActionResult showAll()
    {
        try
        {
            var rs = roleService.showAll();
            return Ok(rs);
        }
        catch (Exception ex)
        {
            // ma 400: la co loi roi
            return BadRequest(ex);
        }
    }

    // ADD NEW ROLE
    [Produces("application/json")]
    [HttpPost("addnew")]
    // cùng lúc nhận hình ảnh và chuỗi JSON
    public IActionResult Create([FromBody] Role role)
    {
        try
        {
            bool result = roleService.Create(role);
            return Ok(new
            {
                Result = result
            });
        }
        catch (Exception ex)
        {
            // mã 400: có lỗi xảy ra
            return BadRequest(ex);
        }
    }


    // UPDATE
    [Produces("application/json")]
    [HttpPut("update")]
    public IActionResult Update([FromBody] Role role)
    {
        try
        {
            bool result = roleService.Update(role);
            return Ok(new
            {
                Result = result
            });
        }
        catch (Exception ex)
        {
            // Mã 400: Có lỗi xảy ra
            return BadRequest(ex);
        }
    }
}
