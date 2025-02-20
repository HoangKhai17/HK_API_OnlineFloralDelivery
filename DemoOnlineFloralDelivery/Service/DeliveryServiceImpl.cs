using DemoOnlineFloralDelivery.Models;

namespace DemoOnlineFloralDelivery.Service;

public class DeliveryServiceImpl : DeliveryService
{
    private DatabaseContext db;
    private IConfiguration configuration;
    public DeliveryServiceImpl(DatabaseContext _db, IConfiguration _configuration)
    {
        db = _db;
        configuration = _configuration;
    }


    public dynamic findAllDelivery()
    {

        return db.Deliveries.Select(p => new
        {
            DeliveryId = p.DeliveryId,
            Message = p.Message,
            RecipientName = p.RecipientName,
            RecipientAddress = p.RecipientAddress,
            RecipientPhone = p.RecipientPhone,
            OrderId = p.OrderId,
            AccountId = p.Order.Account.Username,
            PaymentMethod = p.Order.PaymentMethod,
            TotalOrder = p.Order.TotalOrder,
            OrderDate = p.Order.OrderDate,
            OrderTime = p.Order.OrderTime,
            Status = p.Order.Status,
            Username = p.Order.Account.Username
        }).OrderByDescending(p => p.DeliveryId).ToList();
    }
    public bool created(Delivery delivery)
    {
        try
        {
            db.Deliveries.Add(delivery);
            return db.SaveChanges() > 0;
        }
        catch
        {
            return false;
        }
    }
}
