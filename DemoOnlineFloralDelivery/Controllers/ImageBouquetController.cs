using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json;
using DemoOnlineFloralDelivery.Helpers;
using DemoOnlineFloralDelivery.Models;
using DemoOnlineFloralDelivery.Service;
using System.Diagnostics;

namespace DemoOnlineFloralDelivery.Controllers;
[Route("api/image")]
public class ImageBouquetController : Controller
{
    private BouquetService bouquetService;
    private ImageService imageService;
    private IWebHostEnvironment webHostEnvironment;
    public ImageBouquetController(BouquetService _bouquetService, ImageService _imageService, IWebHostEnvironment _webHostEnvironment)
    {
        bouquetService = _bouquetService;
        imageService = _imageService;
        webHostEnvironment = _webHostEnvironment;
    }
    //method hiện thị danh sách bó hoa(Bouquet) thoe ngày tạo mới nhất
    [Produces("application/json")]
    [HttpGet("findall")]
    public IActionResult FindAll()
    {
        try
        {
            var fillAll = imageService.findAll();
            return Ok(fillAll);
        }
        catch (Exception ex)
        {
            // ma 400: la co loi roi
            return BadRequest(ex);
        }
    }

    [Produces("application/json")]
    [HttpGet("findById/{bouquetId}")]
    public IActionResult FindById(int bouquetId)
    {
        try
        {
            var fillAll = imageService.findById(bouquetId);
            return Ok(fillAll);
        }
        catch (Exception ex)
        {
            // ma 400: la co loi roi
            return BadRequest(ex);
        }
    }

    [Produces("application/json")]
    [HttpGet("findByImageId/{imageId}")]
    public IActionResult FindByImageId(int imageId)
    {
        try
        {
            var fillAll = imageService.findByImageId(imageId);
            return Ok(fillAll);
        }
        catch (Exception ex)
        {
            // ma 400: la co loi roi
            return BadRequest(ex);
        }
    }


    [Consumes("multipart/form-data")]
    [Produces("application/json")]
    [HttpPost("created")]

    public IActionResult Created(IFormFile imageUrl, string strImg)
    {

        try
        {
            var fileName = FileHelper.generateFileName(imageUrl.FileName);
            var path = Path.Combine(webHostEnvironment.WebRootPath, "images", fileName);
            using (var fileStream = new FileStream(path, FileMode.Create))
            {
                imageUrl.CopyTo(fileStream);
            }
            var _image = JsonConvert.DeserializeObject<Image>(strImg);
            _image.ImageUrl = fileName;
            Debug.WriteLine("Create: " + _image.ImageUrl + _image.BouquetId);
            bool result = imageService.Create(_image);
            Debug.WriteLine(result);
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
    [HttpPut("update")]
    public IActionResult UpdateEvent(IFormFile imageUrl, string strImg)
    {
        try
        {
            var _image = JsonConvert.DeserializeObject<Image>(strImg

           );

            if (imageUrl != null)
            {
                var fileName = FileHelper.generateFileName(imageUrl.FileName);
                var path = Path.Combine(webHostEnvironment.WebRootPath, "images", fileName);
                using (var fileStream = new FileStream(path, FileMode.Create))
                {
                    imageUrl.CopyTo(fileStream);
                }
                _image.ImageUrl = fileName;
            }
            else
            {
                _image.ImageUrl = imageService.findImageId(_image.BouquetId).ImageUrl;
            }

            bool result = imageService.Update(_image);
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
    [Produces("application/json")]
    [HttpDelete("delete/{id}")]
    public IActionResult Delete(int id)
    {
        try
        {
            return Ok(new
            {
                status = imageService.Delete(id)
            });
        }
        catch
        {
            return BadRequest();
        }
    }
}


////method update bouquet
//[Consumes("multipart/form-data")]
//    [Produces("application/json")]
//    [HttpPost("update")]
//    public IActionResult Update(IFormFile imageUrl, string strimg)
//    {
//        try
//        {
//            var image = JsonConvert.DeserializeObject<Image>(strimg);
//            // neu co anh thi thi moi thuc hien update
//            if (imageUrl != null)
//            {
//                var fileName = FileHelper.generateFileName(imageUrl.FileName);
//                var path = Path.Combine(webHostEnvironment.WebRootPath, "images", fileName);
//                using (var fileStream = new FileStream(path, FileMode.Create))
//                {
//                    imageUrl.CopyTo(fileStream);

//                }
//                // loai lai ten file moi
//                image!.ImageUrl = fileName;
//            }
//            else
//            {
//                image!.ImageUrl = imageService.findByImageId(image.ImageId).ImageUrl;
//            }

//            bool result = imageService.Update(image);
//            return Ok(new
//            {
//                Result = result
//            });
//        }
//        catch (Exception ex)
//        {
//            // ma 400: la co loi roi
//            return BadRequest(ex);
//        }
//    }
//}