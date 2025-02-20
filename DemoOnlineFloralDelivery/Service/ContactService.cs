using DemoOnlineFloralDelivery.Models;

namespace DemoOnlineFloralDelivery.Service;

public interface ContactService
{
    public dynamic showAll();
    public bool Create(Contact contact);
}
