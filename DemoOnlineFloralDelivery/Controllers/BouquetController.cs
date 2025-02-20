using Microsoft.AspNetCore.Mvc;
using DemoOnlineFloralDelivery.Models;
using DemoOnlineFloralDelivery.Service;

namespace DemoOnlineFloralDelivery.Controllers;
[Route("api/bouquet")]
public class BouquetController : Controller
{
    private BouquetService bouquetService;
    private IWebHostEnvironment webHostEnvironment;
    public BouquetController(BouquetService _bouquetService, IWebHostEnvironment _webHostEnvironment)
    {
        bouquetService = _bouquetService;
        webHostEnvironment = _webHostEnvironment;
    }
    //method hiện thị danh sách bó hoa(Bouquet) thoe ngày tạo mới nhất
    [Produces("application/json")]
    [HttpGet("findall")]
    public IActionResult FindAll()
    {
        try
        {
            var fillAll = bouquetService.findAll();
            return Ok(fillAll);
        }
        catch (Exception ex)
        {
            // ma 400: la co loi roi
            return BadRequest(ex);
        }
    }

    [Produces("application/json")]
    [HttpGet("findByBouquetName/{bouquetName}")]
    public IActionResult FindByBoquetName(string bouquetName)
    {
        try
        {
            var fillAll = bouquetService.findByBouquetName(bouquetName);
            return Ok(fillAll);
        }
        catch (Exception ex)
        {
            // ma 400: la co loi roi
            return BadRequest(ex);
        }
    }

    // method hiện thị danh sách bó hoa thoe status là true hoặc false
    [Produces("application/json")]
    [HttpGet("findByStatus/{status}")]
    public IActionResult findByStatus(bool status)
    {
        try
        {
            var findByStatus = bouquetService.findByStatus(status);
            return Ok(findByStatus);
        }
        catch (Exception ex)
        {
            // ma 400: la co loi roi
            return BadRequest(ex);
        }
    }

    // method hiện thị danh sách tìm kiếm thoe giá của sản phẩm
    [Produces("application/json")]
    [HttpGet("findByPriceRange/{price}")]
    public IActionResult FindByPriceRange(int price)
    {
        try
        {
            var findByPriceRange = bouquetService.findByPriceRange(price);
            return Ok(findByPriceRange);
        }
        catch (Exception ex)
        {
            // ma 400: la co loi roi
            return BadRequest(ex);
        }
    }



    // method tìm kiếm bouquet theo categoryId
    [Produces("application/json")]
    [HttpGet("findByCategoryId/{categoryId}")]
    public IActionResult FindByCategoryId(int categoryId)
    {
        try
        {

            if (categoryId == -1)
            {
                var fillAll = bouquetService.findAll();
                return Ok(fillAll);
            }
            else
            {
                var findByCategoryId = bouquetService.findByCategoryId(categoryId);
                return Ok(findByCategoryId);
            }
           
        }
        catch (Exception ex)
        {
            // ma 400: la co loi roi
            return BadRequest(ex);
        }
    }

    // method tìm kiếm bouquet theo categoryId
    [Produces("application/json")]
    [HttpGet("findById/{bouquetId}")]
    public IActionResult FindById(int bouquetId)
    {
        try
        {
            var findById = bouquetService.findById(bouquetId);
            return Ok(findById);
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
    public IActionResult Created([FromBody] Bouquet bouquet)
    {
        try
        {
            bool result = bouquetService.Create(bouquet);
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

    // update bouquet
    [Produces("application/json")]
    [HttpPost("update")]
    public IActionResult Update([FromBody] Bouquet bouquet)
    {
        try
        {
            bool result = bouquetService.Update(bouquet);
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


    //method hiện thị danh sách 6 bó hoa(Bouquet)mới nhất
    [Produces("application/json")]
    [HttpGet("findnewbouquet")]
    public IActionResult FindNewBouquet()
    {
        try
        {
            var fillAll = bouquetService.findNewBouquet();
            return Ok(fillAll);
        }
        catch (Exception ex)
        {
            // ma 400: la co loi roi
            return BadRequest(ex);
        }
    }

    //method hiện thị danh sách 3 bó hoa(Bouquet) yêu thích nhất (được bán nhiều nhất)
    [Produces("application/json")]
    [HttpGet("findfavoritebouquet")]
    public IActionResult FindFavoriteBouquet()
    {
        try
        {
            var fillAll = bouquetService.findFavoriteBouquet();
            return Ok(fillAll);
        }
        catch (Exception ex)
        {
            // ma 400: la co loi roi
            return BadRequest(ex);
        }
    }

    //method hiện thị danh sách bó hoa(Bouquet) theo từng loại category
    [Produces("application/json")]
    [HttpGet("findcategoryidbouquet")]
    public IActionResult FindCateogryIdBouquet()
    {
        try
        {
            var fillAll = bouquetService.findCateogryBouquet();
            return Ok(fillAll);
        }
        catch (Exception ex)
        {
            // ma 400: la co loi roi
            return BadRequest(ex);
        }
    }

    [Produces("application/json")]
    [HttpGet("findByEventId/{eventId}")]
    public IActionResult FindByEventId(int eventId)
    {
        try
        {
            var fillAll = bouquetService.findByEventId(eventId);
            return Ok(fillAll);
        }
        catch (Exception ex)
        {
            // ma 400: la co loi roi
            return BadRequest(ex);
        }
    }


    // method tìm kiếm bouquet theo categoryId
    [Produces("application/json")]
    [HttpGet("findByBouquetId/{bouquetId}")]
    public IActionResult FindByBouquetId(int bouquetId)
    {
        try
        {
            var findById = bouquetService.findByBouquetId(bouquetId);
            return Ok(findById);
        }
        catch (Exception ex)
        {
            // ma 400: la co loi roi
            return BadRequest(ex);
        }
    }
}
