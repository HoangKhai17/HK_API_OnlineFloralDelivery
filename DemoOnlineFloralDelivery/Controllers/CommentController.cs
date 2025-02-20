using Microsoft.AspNetCore.Mvc;
using DemoOnlineFloralDelivery.Models;
using DemoOnlineFloralDelivery.Service;

namespace DemoOnlineFloralDelivery.Controllers;
[Route("api/comment")]
public class CommentController : Controller
{
    private CommentService commentService;

    public CommentController(CommentService _commentService)
    {
        commentService = _commentService;

    }
   
    //Hiển thị danh sách tất cả Comment
    [Produces("application/json")]
    [HttpGet("findAllComment")]
    public IActionResult FindAllComment()
    {
        try
        {
            return Ok(commentService.findAllComment());
        }
        catch
        {
            return BadRequest();
        }
    }
    //Hiển thị danh sách tất cả Comment theo bouquetId

    [Produces("application/json")]
    [HttpGet("findByBouquetId/{bouquetId}")]
    public IActionResult FindByBouquetId(int bouquetId)
    {
        try
        {
            return Ok(commentService.findByBouquetId(bouquetId));
        }
        catch
        {
            return BadRequest();
        }
    }
    //Thêm Comment
    [Consumes("application/json")]
    [Produces("application/json")]
    [HttpPost("addComment")]
    public IActionResult AddComment([FromBody] Comment _comment)
    {
        try
        {
            return Ok(new
            {
                status = commentService.addComment(_comment)
            });
        }
        catch
        {
            return BadRequest();
        }
    }
    //Sửa comment
    [Consumes("application/json")]
    [Produces("application/json")]
    [HttpPut("updateComment")]
    public IActionResult UpdateComment([FromBody] Comment _comment)
    {
        try
        {
            return Ok(new
            {
                status = commentService.updateComment(_comment)
            });
        }
        catch
        {
            return BadRequest();
        }
    }
    //Xóa comment
    [Produces("application/json")]
    [HttpDelete("deleteComment/{CommentId}")]
    public IActionResult DeleteComment(int CommentId)
    {
        try
        {
            return Ok(new
            {
                status = commentService.deleteComment(CommentId)
            });
        }
        catch
        {
            return BadRequest();
        }
    }
}
