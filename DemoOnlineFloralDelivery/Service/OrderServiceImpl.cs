﻿using DemoOnlineFloralDelivery.Models;

namespace DemoOnlineFloralDelivery.Service;

public class OrderServiceImpl : OrderService
{
    private DatabaseContext db;
    private IConfiguration configuration;
    public OrderServiceImpl(DatabaseContext _db, IConfiguration _configuration)
    {
        db = _db;
        configuration = _configuration;
    }
    public bool created(Order order)
    {
        try
        {
            db.Orders.Add(order);
            return db.SaveChanges() > 0;
        }
        catch
        {
            return false;
        }
    }

    public dynamic findAll()
    {
        return db.Orders.Select(p => new
        {
            OrderId = p.OrderId,
            AccountId = p.AccountId,
            PaymentMethod = p.PaymentMethod,
            TotalOrder = p.TotalOrder,
            OrderDate = p.OrderDate,
            OrderTime = p.OrderTime,
            Status =    p.Status,
            Username = p.Account!.Username
        }).OrderByDescending(p => p.OrderId).ToList();
    }

    public dynamic findByAccountId(int accountId)
    {
        return db.Orders
       .Where(p => p.Status == "0" && p.AccountId == accountId)
       .Select(p => new
       {
           OrderId = p.OrderId,
           AccountId = p.AccountId,
           PaymentMethod = p.PaymentMethod,
           TotalOrder = p.TotalOrder,
           OrderDate = p.OrderDate,
           OrderTime = p.OrderTime,

           Status = p.Status,
           Username = p.Account!.Username,
           OrderDetails = p.OrderDetails.Select(od => new
           {
               OrderId = od.OrderId,
               BouquetId = od.BouquetId,
               BoquetName = od.Bouquet.BouquetName,
               Quantity = od.Quantity,
               TotalPrice = od.TotalPrice
           }).ToList()
       })
       .OrderByDescending(p => p.OrderId)
       .ToList();
    }

    public dynamic findByAccountId2(int accountId)
    {
        return db.Orders
       .Where(p => p.Status == "1" && p.AccountId == accountId)
       .Select(p => new
       {
           OrderId = p.OrderId,
           AccountId = p.AccountId,
           PaymentMethod = p.PaymentMethod,
           TotalOrder = p.TotalOrder,
           OrderDate = p.OrderDate,
           OrderTime = p.OrderTime,
           Status = p.Status,
           Username = p.Account!.Username,
           OrderDetails = p.OrderDetails.Select(od => new
           {
               OrderId = od.OrderId,
               BouquetId = od.BouquetId,
               BoquetName = od.Bouquet.BouquetName,
               Quantity = od.Quantity,
               TotalPrice = od.TotalPrice
           }).ToList()
       })
       .OrderByDescending(p => p.OrderId)
       .ToList();
    }


    // update lại status
    public bool UpdateOrderStatus(int orderId)
    {
        try
        {
            var existingOrder = db.Orders.FirstOrDefault(o => o.OrderId == orderId);
            if (existingOrder != null)
            {
                existingOrder.Status = "1";

                // Save changes to the database
                return db.SaveChanges() > 0;
            }
            else
            {
                // Return false if the order with the specified OrderId does not exist
                return false;
            }
        }
        catch
        {
            return false;
        }
    }

    public dynamic findByOrderIdAdmin(int orderId)
    {
        return db.Orders
       .Where(p => p.OrderId == orderId)
       .Select(p => new
       {
           OrderId = p.OrderId,
           AccountId = p.AccountId,
           PaymentMethod = p.PaymentMethod,
           TotalOrder = p.TotalOrder,
           OrderDate = p.OrderDate,
           OrderTime = p.OrderTime,
           Status = p.Status,
           Username = p.Account!.Username,
           OrderDetails = p.OrderDetails.Select(od => new
           {
               OrderId = od.OrderId,
               BouquetId = od.BouquetId,
               BoquetName = od.Bouquet.BouquetName,
               Quantity = od.Quantity,
               TotalPrice = od.TotalPrice
           }).ToList()
       })
       .OrderByDescending(p => p.OrderId)
       .ToList();
    }
}
