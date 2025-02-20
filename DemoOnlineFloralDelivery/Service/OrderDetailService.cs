using DemoOnlineFloralDelivery.Models;

namespace DemoOnlineFloralDelivery.Service;

public interface OrderDetailService
{
    public bool created(List<OrderDetail> orderDetails);
}
