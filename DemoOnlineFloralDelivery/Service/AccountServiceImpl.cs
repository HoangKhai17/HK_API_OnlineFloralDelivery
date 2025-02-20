using Microsoft.EntityFrameworkCore;
using DemoOnlineFloralDelivery.Models;
using System.Diagnostics;
using Microsoft.Identity.Client;

namespace DemoOnlineFloralDelivery.Service;

public class AccountServiceImpl:AccountService
{
    private DatabaseContext db;
    private IConfiguration configuration;
    public AccountServiceImpl(DatabaseContext _db, IConfiguration _configuration)
    {
        db = _db;
        configuration = _configuration;
    }
    public bool Exists(string username)
    {
        return db.Accounts.Count(x => x.Username == username) > 0;
    }

    public dynamic find(int acccountId)
    {
        return db.Accounts.Where(a => a.AccountId == acccountId).Select(a => new
        {
            accountId = a.AccountId,
            userName = a.Username,
            email = a.Email,
            phone = a.Phone,
            address = a.Address,
            imageUrl = configuration["BaseUrl"] + "images/" + a.ImageUrl,
            created = a.Created,
            dob = a.Dob,
        }).ToList();
    }
    public dynamic findByName2(string Username)
    {
        Debug.WriteLine(Username + "thien1 : " + Username);
        return db.Accounts.Where(a => a.Username == Username).Select(a => new
        {
            accountId = a.AccountId,
            password = a.Password,
            userName = a.Username,
            email = a.Email,
            phone = a.Phone,
            roleId = a.RoleId,
            address = a.Address,
            imageUrl = configuration["BaseUrl"] + "images/" + a.ImageUrl,
            created = a.Created,
            dob = a.Dob,
            status = a.Status,
        }).FirstOrDefault()!;
    }

    public Account findById(int accountId)
    {
        return db.Accounts.AsNoTracking().SingleOrDefault(p => p.AccountId == accountId);
    }
    public Account findByName(string Username)
    {//first
        return db.Accounts.AsNoTracking().FirstOrDefault(p => p.Username == Username);
    }
    public bool login(string Username, string Password)
    {
        try
        {
            var account = db.Accounts.FirstOrDefault(a => a.Username == Username);
            if (account != null && account.Status == true)
            {

                Debug.WriteLine("adsdasdsadsadasd " + account.Username, account.Password);
                return BCrypt.Net.BCrypt.Verify(Password, account.Password);
            }
            return false;
        }
        catch
        {
            return false;
        }
    }


    public bool Register(Account account)
    {
        try
        {
            db.Accounts.Add(account);
            return db.SaveChanges() > 0;
        }
        catch
        {
            return false;
        }
    }

    public dynamic showAll()
    {
        return db.Accounts.OrderByDescending(a => a.AccountId).Select(a => new
        {
            accountId = a.AccountId,
            userName = a.Username,
            email = a.Email,
            phone = a.Phone,
            address = a.Address,
            imageUrl = configuration["BaseUrl"] + "images/" + a.ImageUrl,
            roleId = a.RoleId,
            roleName = a.Role.RoleName,
            created = a.Created,
            dob = a.Dob,
            status = a.Status
        }).ToList();
    }
    public bool Update(Account account)
    {
        try
        {
            db.Entry(account).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            return db.SaveChanges() > 0;
        }
        catch
        {
            return false;
        }
    }
    public dynamic Search(string keyword)
    {
        return db.Accounts.Where(a => a.Username.Contains(keyword)).Select(a => new
        {
            accountId = a.AccountId,
            userName = a.Username,
            email = a.Email,
            phone = a.Phone,
            address = a.Address,
            imageUrl = configuration["BaseUrl"] + "images/" + a.ImageUrl,
            roleId = a.RoleId,
            roleName = a.Role.RoleName,
            created = a.Created,
            dob = a.Dob,
        }).ToList();
    }

    public dynamic findAccountIdByUsername(string username)
    {
        return db.Accounts.Where(a => a.Username == username).Select(a => new
        {
            accountId = a.AccountId,
            userName = a.Username,
            email = a.Email,
            phone = a.Phone,
            address = a.Address,
            imageUrl = configuration["BaseUrl"] + "images/" + a.ImageUrl,
            created = a.Created,
            dob = a.Dob,
        }).FirstOrDefault()!;
    }
}
