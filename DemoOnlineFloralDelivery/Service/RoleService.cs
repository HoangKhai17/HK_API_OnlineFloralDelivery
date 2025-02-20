using DemoOnlineFloralDelivery.Models;

namespace DemoOnlineFloralDelivery.Service;

public interface RoleService
{
    public dynamic showAll();
    public bool Create(Role role);
    public bool Update(Role role);
}
