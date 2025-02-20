using DemoOnlineFloralDelivery.Models;

namespace DemoOnlineFloralDelivery.Service;

public interface OrderService
{
    public bool created(Order order);

    public dynamic findAll();

    public dynamic findByAccountId(int accountId);
    public dynamic findByAccountId2(int accountId);
    public dynamic findByOrderIdAdmin(int orderId);
    public bool UpdateOrderStatus(int orderId);
}
