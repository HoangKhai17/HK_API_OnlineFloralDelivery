using Microsoft.AspNetCore.Mvc;
using DemoOnlineFloralDelivery.Models;
using DemoOnlineFloralDelivery.Service;

namespace DemoOnlineFloralDelivery.Controllers;
[Route("api/contact")]
public class ContactController : Controller
{
    private ContactService contactService;
    public ContactController(ContactService _contactService)
    {
        contactService = _contactService;
    }
    // SHOW ALL ROLES
    [Produces("application/json")]
    [HttpGet("showAll")]
    public IActionResult showAll()
    {
        try
        {
            var rs = contactService.showAll();
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
    public IActionResult Create([FromBody] Contact contact)
    {
        try
        {
            bool result = contactService.Create(contact);
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
}
