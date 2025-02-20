using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json;
using DemoOnlineFloralDelivery.Helpers;
using DemoOnlineFloralDelivery.Models;
using DemoOnlineFloralDelivery.Service;
using Microsoft.Identity.Client;
using OnlineFloralDeliveryNew.Helpers;
using System.Diagnostics;

namespace DemoOnlineFloralDelivery.Controllers;
[Route("api/account")]
public class AccountController : Controller
{
    private AccountService accountService;
    private IWebHostEnvironment webHostEnvironment;
    private IConfiguration configuration;

    public AccountController(AccountService _accountService, IWebHostEnvironment _webHostEnvironment,IConfiguration _configuration)
    {
        accountService = _accountService;
        webHostEnvironment = _webHostEnvironment;
        configuration = _configuration;
    }

    // SHOW ALL ROLES
    [Produces("application/json")]
    [HttpGet("showAll")]
    public IActionResult showAll()
    {
        try
        {
            var rs = accountService.showAll();
            return Ok(rs);
        }
        catch (Exception ex)
        {
            // ma 400: la co loi roi
            return BadRequest(ex);
        }
    }

    [Produces("application/json")]
    [HttpGet("detail/{accountId}")]
    public IActionResult SearchById(int accountId)
    {
        try
        {
            var find = accountService.find(accountId);
            return Ok(find);
        }
        catch (Exception ex)
        {
            // ma 400: la co loi roi
            return BadRequest(ex);
        }
    }
    [Produces("application/json")]
    [HttpGet("findbyusername/{username}")]
    public IActionResult FindByUsername(string username)
    {
        try
        {
            var find = accountService.findByName(username);
            return Ok(find);
        }
        catch (Exception ex)
        {
            // ma 400: la co loi roi
            return BadRequest(ex);
        }
    }
    [Produces("application/json")]
    [HttpGet("findUsername/{Username}")]
    public IActionResult findUsername(string Username)
    {
        try
        {
            var find = accountService.findByName2(Username);
            return Ok(find);
        }
        catch (Exception ex)
        {
            // ma 400: la co loi roi
            return BadRequest(ex);
        }
    }
    [Produces("application/json")]
    [HttpGet("search/{keyword}")]
    public IActionResult SearchByName(string keyword)
    {
        try
        {
            var find = accountService.Search(keyword);
            return Ok(find);
        }
        catch (Exception ex)
        {
            // ma 400: la co loi roi
            return BadRequest(ex);
        }
    }

    [Produces("application/json")]
    [HttpPost("register/{codeConfirm}")]
    public IActionResult Register([FromBody] Account account, string codeConfirm)
    {
        try
        {
            Debug.WriteLine(account.Username + account.Password);
            if (account.Username != null && accountService.Exists(account.Username))
            {
                ModelState.AddModelError("Username", "Username was existed");
            }

            account.Password = BCrypt.Net.BCrypt.HashPassword(account.Password);
            bool result = accountService.Register(account);
            var mailHelper = new MailHelper(configuration);
            var content = $"Congratulations, you have successfully registered, please enter the code below to activate your account: {codeConfirm}";
            mailHelper.Send(configuration["Gmail:Username"]!, account.Email!, "Registration confirmation", content);
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
    [Produces("application/json")]
    [HttpPost("login")]
    public IActionResult Login([FromBody] Account account)
    {
        try
        {
            bool rs = accountService.login(account.Username, account.Password);
            return Ok(new
            {
                Result = rs
            });
        }
        catch (Exception ex)
        {
            return BadRequest(ex);
        }
    }

    [Consumes("multipart/form-data")]
    [Produces("application/json")]
    [HttpPut("update")]
    public IActionResult update(IFormFile imageUrl, string strAccount)
    {
        try
        {
            var account = JsonConvert.DeserializeObject<Account>(strAccount);
            Debug.WriteLine(imageUrl.Name);
            if (account != null)
            {
                var fileName = FileHelper.generateFileName(imageUrl.FileName);
                var path = Path.Combine(webHostEnvironment.WebRootPath, "images", fileName);
                using (var fileStream = new FileStream(path, FileMode.Create))
                {
                    imageUrl.CopyTo(fileStream);
                }
                account.ImageUrl = fileName;
            }
            else
            {
                account!.ImageUrl = accountService.findById(account.AccountId).ImageUrl;
            }
            bool rs = accountService.Update(account);
            return Ok(new
            {
                Results = rs
            });
        }
        catch
        {
            return BadRequest();
        }
    }
    //get
    [Consumes("multipart/form-data")]
    [Produces("application/json")]
    [HttpPut("updateAccount")]
    public IActionResult updateAccount(string strAccount)
    {
        try
        {

            var account = JsonConvert.DeserializeObject<Account>(strAccount);


            account.ImageUrl = accountService.findById(account.AccountId).ImageUrl;

            if (account.Password == null || account.Password == "")
            {
                account.Password = accountService.findById(account.AccountId).Password;
            }
            else
            {
                account.Password = BCrypt.Net.BCrypt.HashPassword(account.Password);
            }
            bool rs = accountService.Update(account);
            return Ok(new
            {
                Results = rs
            });
        }
        catch
        {
            return BadRequest();
        }
    }
    [Produces("application/json")]
    [HttpGet("updateStatus/{Username}")]
    public IActionResult updateStatus(string Username)
    {
        try
        {
            Account account = new Account();
            account = accountService.findByName(Username);
            account.Status = true;
            bool rs = accountService.Update(account);
            return Ok(new
            {
                Results = rs
            });
        }
        catch
        {
            return BadRequest();
        }
    }

   
    // SHOW ALL ROLES
    [Produces("application/json")]
    [HttpGet("findAccountIdByUsername/{username}")]
    public IActionResult FindAccoutIdByUsername(string username)
    {
        try
        {
            var rs = accountService.findAccountIdByUsername(username);
            return Ok(rs);
        }
        catch (Exception ex)
        {
            // ma 400: la co loi roi
            return BadRequest(ex);
        }
    }

    [Produces("application/json")]
    [HttpGet("updateStatus2/{Username}")]
    public IActionResult updateStatus2(string Username)
    {
        try
        {
            Account account = new Account();
            account = accountService.findByName(Username);
            account.Status = false;
            bool rs = accountService.Update(account);
            return Ok(new
            {
                Results = rs
            });
        }
        catch
        {
            return BadRequest();
        }
    }

    [Produces("application/json")]
    [HttpPost("registerAccount")]
    public IActionResult RegisterAccount([FromBody] Account account)
    {
        try
        {
            if (account.Username != null && accountService.Exists(account.Username))
            {
                return BadRequest(new
                {
                    Result = false
                });
            }
            account.Password = BCrypt.Net.BCrypt.HashPassword(account.Password);
            bool result = accountService.Register(account);
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