using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json;
using DemoOnlineFloralDelivery.Helpers;
using DemoOnlineFloralDelivery.Models;
using DemoOnlineFloralDelivery.Service;

namespace DemoOnlineFloralDelivery.Controllers;
[Route("api/event")]
public class EventController : Controller
{
    private EventService eventService;
    private IWebHostEnvironment webHostEnvironment;
    public EventController(EventService _eventService, IWebHostEnvironment _webHostEnvironment)
    {
        eventService = _eventService;
        webHostEnvironment = _webHostEnvironment;
    }
    //Hiển thị danh sách tất cả Event
    [Produces("application/json")]
    [HttpGet("findAllEvent")]
    public IActionResult FindAllEvent()
    {
        try
        {
            return Ok(eventService.findAllEvent());
        }
        catch
        {
            return BadRequest();
        }
    }
    //Thêm Event từ Angular
    [Consumes("multipart/form-data")]
    [Produces("application/json")]
    [HttpPost("addEvent")]
    public IActionResult Ad(IFormFile imageUrl, string strEvent)
    {
        try
        {
            var fileName = FileHelper.generateFileName(imageUrl.FileName);
            var path = Path.Combine(webHostEnvironment.WebRootPath, "images", fileName);
            using (var fileStream = new FileStream(path, FileMode.Create))
            {
                imageUrl.CopyTo(fileStream);
            }
            var _event = JsonConvert.DeserializeObject<Event>(strEvent);
            _event!.ImageUrl = fileName;
            bool result = eventService.addEvent(_event);
            return Ok(new
            {
                Result = result
            });
        }
        catch
        {
            return BadRequest();
        }
    }

    //Cập nhật Event từ Angular
    [Consumes("multipart/form-data")]
    [Produces("application/json")]
    [HttpPut("updateEvent")]
    public IActionResult UpdateEvent(IFormFile imageUrl, string strEvent)
    {
        try
        {
            var _event = JsonConvert.DeserializeObject<Event>(strEvent

           );

            if (imageUrl != null)
            {
                var fileName = FileHelper.generateFileName(imageUrl.FileName);
                var path = Path.Combine(webHostEnvironment.WebRootPath, "images", fileName);
                using (var fileStream = new FileStream(path, FileMode.Create))
                {
                    imageUrl.CopyTo(fileStream);
                }
                _event!.ImageUrl = fileName;
            }
            else
            {
                _event!.ImageUrl = eventService.findEventId(_event.EventId).ImageUrl;
            }

            bool result = eventService.updateEvent(_event);
            return Ok(new
            {
                Result = result
            });
        }
        catch
        {
            return BadRequest();
        }
    }

    //Hiển thị danh sách Event theo IsAction
    [Produces("application/json")]
    [HttpGet("findEventIsAction")]
    public IActionResult FindEventIsAction()
    {
        try
        {
            return Ok(eventService.findEventIsAction());
        }
        catch
        {
            return BadRequest();
        }
    }
    //Tìm kiếm Event theo Event_name
    [Produces("application/json")]
    [HttpGet("searchEventName/{keyword}")]
    public IActionResult SearchEventName(string keyword)
    {
        try
        {
            return Ok(eventService.searcEventName(keyword));
        }
        catch
        {
            return BadRequest();
        }
    }
    //Tìm kiếm Event theo Event_id
    [Produces("application/json")]
    [HttpGet("SearchEventId/{id}")]
    public IActionResult SearchEventId(int id)
    {
        try
        {
            return Ok(eventService.searchEventId(id));
        }
        catch
        {
            return BadRequest();
        }
    }

    //Hiển thị danh sách Event theo IsAction
    [Produces("application/json")]
    [HttpGet("findEventIsActionUser")]
    public IActionResult FindEventIsActionUser()
    {
        try
        {
            return Ok(eventService.findEventIsActionUser());
        }
        catch
        {
            return BadRequest();
        }
    }

    [Produces("application/json")]
    [HttpGet("findEvenUserDealOfMonth")]
    public IActionResult FindEvenUserDealOfMonth()
    {
        try
        {
            return Ok(eventService.findEvenUserDealOfMonth());
        }
        catch
        {
            return BadRequest();
        }
    }
}
