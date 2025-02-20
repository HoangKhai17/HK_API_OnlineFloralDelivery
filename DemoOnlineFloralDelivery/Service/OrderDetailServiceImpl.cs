using DemoOnlineFloralDelivery.Models;
using Microsoft.EntityFrameworkCore;

namespace DemoOnlineFloralDelivery.Service;

public class OrderDetailServiceImpl : OrderDetailService
{
    private DatabaseContext db;
    private IConfiguration configuration;
    public OrderDetailServiceImpl(DatabaseContext _db, IConfiguration _configuration)
    {
        db = _db;
        configuration = _configuration;
    }
    public bool created(List<OrderDetail> orderDetails)
    {
        try
        {
            db.OrderDetails.AddRange(orderDetails);
            return db.SaveChanges() > 0;
        }
        catch
        {
            return false;
        }
    }
}
