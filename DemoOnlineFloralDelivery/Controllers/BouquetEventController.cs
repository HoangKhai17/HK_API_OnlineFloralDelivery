using DemoOnlineFloralDelivery.Models;
using DemoOnlineFloralDelivery.Service;
using Microsoft.AspNetCore.Cors.Infrastructure;
using Microsoft.AspNetCore.Mvc;
namespace DemoOnlineFloralDelivery.Controllers;
[Route("api/bouquetevent")]
public class BouquetEventController : Controller
{
    private BouquetEventService bouquetEventService;
    private IWebHostEnvironment webHostEnvironment;
    private DatabaseContext db;
    public BouquetEventController(BouquetEventService _bouquetEventService, IWebHostEnvironment _webHostEnvironment, DatabaseContext _db)
    {
        bouquetEventService = _bouquetEventService;
        webHostEnvironment = _webHostEnvironment;
        db = _db;
    }
    [Produces("application/json")]
    [HttpGet("findByEventId/{eventid}")]
    public IActionResult FindByEventId(int eventid)
    {
        try
        {
            var fillAll = bouquetEventService.findByEventId(eventid);
            return Ok(fillAll);
        }
        catch (Exception ex)
        {
            // ma 400: la co loi roi
            return BadRequest(ex);
        }
    }

    // tao moi mot bouquet
    [Produces("application/json")]
    [HttpPost("created")]
    // cùng lúc nhận hình ảnh và chuỗi JSON
    public IActionResult Created([FromBody] BouquetEvent bouquetEvent)
    {
        try
        {
            // Kiểm tra xem bouquetId đã tồn tại trong bảng bouquetevent hay chưa
            bool bouquetExists = db.BouquetEvents.Any(be => be.BouquetId == bouquetEvent.BouquetId);

            // Nếu bouquetId đã tồn tại, trả về lỗi BadRequest
            if (bouquetExists)
            {
                return BadRequest("BouquetId already exists in bouquetevent table.");
            }

            // Nếu bouquetId không tồn tại, thực hiện thêm dữ liệu vào bảng bouquetevent
            bool result = bouquetEventService.Created(bouquetEvent);
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

    [Produces("application/json")]
    [HttpDelete("delete/{bouquetId}")]
    public IActionResult DeleteByBouquetId(int bouquetId)
    {
        try
        {
            return Ok(new
            {
                Result = bouquetEventService.DeleteByBouquetId(bouquetId)
            }) ;
        }
        catch (Exception ex)
        {
            // ma 400: la co loi roi
            return BadRequest(ex);
        }
      
    }
}
