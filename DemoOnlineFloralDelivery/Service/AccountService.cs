using DemoOnlineFloralDelivery.Models;

namespace DemoOnlineFloralDelivery.Service;

public interface AccountService
{
    public dynamic showAll();
    public bool Register(Account account);
    public bool Update(Account account);
    public dynamic find(int accountId);
    public dynamic findByName2(string Username);

    public dynamic Search(string keyword);
    public bool login(string Username, string Password);
    public Account findById(int accountId);
    public Account findByName(string Username);
    public bool Exists(string username);

    // tìm kiếm theo username
    public dynamic findAccountIdByUsername(string username);
}
