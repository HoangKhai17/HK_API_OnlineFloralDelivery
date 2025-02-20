using DemoOnlineFloralDelivery.Models;
using Microsoft.EntityFrameworkCore;

namespace DemoOnlineFloralDelivery.Service;

public class CartServiceImpl: CartService
{
    private DatabaseContext db;
    private IConfiguration configuration;
    public CartServiceImpl(DatabaseContext _db, IConfiguration _configuration)
    {
        db = _db;
        configuration = _configuration;
    }
    //Hiển thị danh sách toàn bộ Cart
    public dynamic findAllCart()
    {
        return db.Carts
             .Join(db.Bouquets, cart => cart.BouquetId, bouquet => bouquet.BouquetId,
                   (cart, bouquet) => new { Cart = cart, Bouquet = bouquet })
             .Join(db.Images, temp => temp.Bouquet.BouquetId, image => image.BouquetId,
                   (temp, image) => new
                   {
                       CartId = temp.Cart.CartId,
                       AccountId = temp.Cart.AccountId,
                       AccountName = temp.Cart.Account!.Username,
                       BouquetId = temp.Cart.BouquetId,
                       BouquetName = temp.Cart.Bouquet!.BouquetName,
                       Price = temp.Cart.Bouquet.Price,
                       Quantity = temp.Cart.Quantity,
                       TotalPrice = temp.Cart.TotalPrice,
                       ImageUrl = configuration["BaseUrl"] + "images/" + image.ImageUrl
                   })
             .GroupBy(p => p.BouquetId)
             .Select(group => new
             {
                 CartId = group.First().CartId,
                 AccountId = group.First().AccountId,
                 AccountName = group.First().AccountName,
                 BouquetId = group.First().BouquetId,
                 BouquetName = group.First().BouquetName,
                 Price = group.First().Price,
                 Quantity = group.First().Quantity,
                 TotalPrice = group.First().TotalPrice,
                 ImageUrl = configuration["BaseUrl"] + "images/" + group.First().ImageUrl
             })
             .OrderByDescending(p => p.CartId)
             .ToList();
    }
    //Thêm(add) Cart
    public bool addCart(Cart _Cart)
    {
        try
        {
            db.Carts.Add(_Cart);
            return db.SaveChanges() > 0;
        }
        catch
        {
            return false;
        }
    }
    //Cập nhật(update) Cart
    public bool updateCart(Cart _Cart)
    {
        try
        {
            db.Entry(_Cart).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            return db.SaveChanges() > 0;
        }
        catch
        {
            return false;
        }
    }

    public dynamic findByAccountName(string accountName)
    {
        var query = from cart in db.Carts
                    join bouquet in db.Bouquets on cart.BouquetId equals bouquet.BouquetId
                    join image in db.Images on bouquet.BouquetId equals image.BouquetId into imagesGroup
                    join bouquetEvent in db.BouquetEvents on bouquet.BouquetId equals bouquetEvent.BouquetId into bouquetEventGroup
                    from events in bouquetEventGroup.DefaultIfEmpty()
                    where cart.Account!.Username == accountName
                    select new
                    {
                        CartId = cart.CartId,
                        AccountId = cart.AccountId,
                        AccountName = cart.Account!.Username,
                        BouquetId = cart.BouquetId,
                        BouquetName = bouquet.BouquetName,
                        Price = events != null && events.Event!.IsAction == true
                            ? bouquet.EventPrice
                            : events != null && events.Event!.IsAction == false
                                ? bouquet.SecondPrice
                                : bouquet.Price,
                        Quantity = cart.Quantity,
                        TotalPrice = cart.TotalPrice,
                        ImageUrl = configuration["BaseUrl"] + "images/" + imagesGroup.OrderBy(i => i.ImageId).Select(i => i.ImageUrl).FirstOrDefault()
                    };

        return query.OrderByDescending(p => p.CartId).ToList();
    }

    public bool DeleteCart(int cartId)
    {
        try
        {
            var carts = db.Carts.Where(be => be.CartId == cartId);
            db.Carts.RemoveRange(carts);
            return db.SaveChanges() > 0;
        }
        catch
        {
            return false;
        }
    }

    public bool DeleteCartByAccountId(int accountId)
    {
        try
        {
            var carts = db.Carts.Where(be => be.AccountId == accountId);
            db.Carts.RemoveRange(carts);
            return db.SaveChanges() > 0;
        }
        catch
        {
            return false;
        }
    }

    public dynamic findByCartId(int cartId)
    {
        return db.Carts
            .Join(db.Bouquets, cart => cart.BouquetId, bouquet => bouquet.BouquetId,
                  (cart, bouquet) => new { Cart = cart, Bouquet = bouquet })
            .Join(db.Images, temp => temp.Bouquet.BouquetId, image => image.BouquetId,
                  (temp, image) => new
                  {
                      CartId = temp.Cart.CartId,
                      AccountId = temp.Cart.AccountId,
                      AccountName = temp.Cart.Account!.Username,
                      BouquetId = temp.Cart.BouquetId,
                      BouquetName = temp.Cart.Bouquet!.BouquetName,
                      Price = temp.Cart.Bouquet.Price,
                      Quantity = temp.Cart.Quantity,
                      TotalPrice = temp.Cart.TotalPrice,
                      ImageUrl = configuration["BaseUrl"] + "images/" + image.ImageUrl
                  })
            .Where(p => p.CartId == cartId)
            .GroupBy(p => p.BouquetId)
            .Select(group => new
            {
                CartId = group.First().CartId,
                AccountId = group.First().AccountId,
                AccountName = group.First().AccountName,
                BouquetId = group.First().BouquetId,
                BouquetName = group.First().BouquetName,
                Price = group.First().Price,
                Quantity = group.First().Quantity,
                TotalPrice = group.First().TotalPrice,
                ImageUrl = group.First().ImageUrl
            })
            .OrderByDescending(p => p.CartId)
            .FirstOrDefault()!;
    }
}
