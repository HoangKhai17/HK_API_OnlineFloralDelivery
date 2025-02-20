using DemoOnlineFloralDelivery.Models;

namespace DemoOnlineFloralDelivery.Service;

public class BouquetEventServiceImpl : BouquetEventService
{
    private DatabaseContext db;
    private IConfiguration configuration;
    public BouquetEventServiceImpl(DatabaseContext _db, IConfiguration _configuration)
    {
        db = _db;
        configuration = _configuration;
    }

    public bool Created(BouquetEvent bouquetEvent)
    {
        try
        {
            db.BouquetEvents.Add(bouquetEvent);
            return db.SaveChanges() > 0;
        }
        catch
        {
            return false;
        }
    }

    public bool DeleteByBouquetId(int bouquetId)
    {
        try
        {
            // Tìm tất cả các bản ghi trong bảng bouquetevent có bouquetId tương ứng
            var bouquetEventsToDelete = db.BouquetEvents.Where(be => be.BouquetId == bouquetId);

            // Xóa tất cả các bản ghi tìm được
            db.BouquetEvents.RemoveRange(bouquetEventsToDelete);

            // Lưu các thay đổi vào cơ sở dữ liệu
            return db.SaveChanges() > 0;
        }
        catch
        {
            return false;
        }
    }


    public dynamic findByEventId(int eventId)
    {
        var query = from bouquetEvent in db.BouquetEvents
                    where bouquetEvent.EventId == eventId
                    join bouquet in db.Bouquets on bouquetEvent.BouquetId equals bouquet.BouquetId
                    join image in db.Images on bouquet.BouquetId equals image.BouquetId into imagesGroup
                    select new
                    {
                        bouquetId = bouquet.BouquetId,
                        bouquetName = bouquet.BouquetName,
                        description = bouquet.Description,
                        price = bouquetEvent.Event != null && bouquetEvent.Event.IsAction == true
                            ? bouquet.EventPrice
                            : bouquetEvent.Event != null && bouquetEvent.Event.IsAction == false
                                ? bouquet.SecondPrice
                                : bouquet.Price,
                        secondPrice = bouquetEvent.Event != null && bouquetEvent.Event.IsAction == true
                            ? bouquet.Price
                            : bouquetEvent.Event != null && bouquetEvent.Event.IsAction == false
                                ? 0
                                : bouquet.SecondPrice,
                        eventPrice = bouquet.EventPrice,
                        quantity = bouquet.Quantity,
                        created = bouquet.Created,
                        categoryId = bouquet.CategoryId,
                        categoryName = bouquet.Category!.CategoryName,
                        status = bouquet.Status,
                        imageUrl = configuration["BaseUrl"] + "images/" + imagesGroup.OrderBy(i => i.ImageId).Select(i => i.ImageUrl).FirstOrDefault(),
                        EventId = bouquetEvent.EventId,
                        EventName = bouquetEvent.Event!.EventName
                    };

        return query.OrderByDescending(p => p.bouquetId).ToList();
    }
}


