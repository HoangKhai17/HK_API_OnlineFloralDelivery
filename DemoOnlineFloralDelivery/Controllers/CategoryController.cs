using Microsoft.AspNetCore.Mvc;
using DemoOnlineFloralDelivery.Models;
using DemoOnlineFloralDelivery.Service;

namespace DemoOnlineFloralDelivery.Controllers;
[Route("api/category")]
public class CategoryController : Controller
{
    private CategoryService categoriesService;

    public CategoryController(CategoryService _categoriesService)
    {
        categoriesService = _categoriesService;

    }
    //Hiển thị danh sách tất cả Category
    [Produces("application/json")]
    [HttpGet("findAllCategories")]
    public IActionResult FindAllCategories()
    {
        try
        {
            return Ok(categoriesService.findAllCategories());
        }
        catch
        {
            return BadRequest();
        }
    }
    //Thêm Category
    [Consumes("application/json")]
    [Produces("application/json")]
    [HttpPost("addCategories")]
    public IActionResult AddCategories([FromBody] Category _category)
    {
        try
        {
            return Ok(new
            {
                status = categoriesService.addCategories(_category)
            });
        }
        catch
        {
            return BadRequest();
        }
    }
    //Sửa Category
    [Consumes("application/json")]
    [Produces("application/json")]
    [HttpPut("updateCategory")]
    public IActionResult UpdateCategories([FromBody] Category _category)
    {
        try
        {
            return Ok(new
            {
                status = categoriesService.updateCategories(_category)
            });
        }
        catch
        {
            return BadRequest();
        }
    }

    //Tìm kiếm Categories theo Categories_id
    [Produces("application/json")]
    [HttpGet("SearchCategoriesId/{id}")]
    public IActionResult SearchCategoriesId(int id)
    {
        try
        {
            return Ok(categoriesService.searchCategoriesId(id));
        }
        catch
        {
            return BadRequest();
        }
    }
}
