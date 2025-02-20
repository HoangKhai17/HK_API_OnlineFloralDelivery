using DemoOnlineFloralDelivery.Models;

namespace DemoOnlineFloralDelivery.Service;

public interface DeliveryService
{
    public dynamic findAllDelivery();
    public bool created(Delivery delivery);
}
