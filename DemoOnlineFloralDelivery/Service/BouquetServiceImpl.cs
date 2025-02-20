using DemoOnlineFloralDelivery.Models;
using static System.Net.Mime.MediaTypeNames;

namespace DemoOnlineFloralDelivery.Service;

public class BouquetServiceImpl : BouquetService
{
    private DatabaseContext db;
    private IConfiguration configuration;
    public BouquetServiceImpl(DatabaseContext _db, IConfiguration _configuration)
    {
        db = _db;
        configuration = _configuration;
    }

    //method hiện thị danh sách bó hoa(Bouquet) thoe ngày tạo mới nhất
    public dynamic findAll()
    {
        var query = from bouquet in db.Bouquets
                    join image in db.Images on bouquet.BouquetId equals image.BouquetId into imagesGroup
                    join bouquetEvent in db.BouquetEvents on bouquet.BouquetId equals bouquetEvent.BouquetId into bouquetEventGroup
                    from events in bouquetEventGroup.DefaultIfEmpty()
                    select new
                    {
                        bouquetId = bouquet.BouquetId,
                        bouquetName = bouquet.BouquetName,
                        description = bouquet.Description,
                        price = events != null && events.Event!.IsAction == true
                            ? bouquet.EventPrice
                            : events != null && events.Event!.IsAction == false
                                ? bouquet.SecondPrice
                                : bouquet.Price,
                        secondPrice = events != null && events.Event!.IsAction == true
                            ? bouquet.Price
                            : events != null && events.Event!.IsAction == false
                                ? 0
                                : bouquet.SecondPrice,
                        eventPrice = events != null && events.Event!.IsAction == true
                            ? bouquet.EventPrice
                            : 0,
                        quantity = bouquet.Quantity,
                        created = bouquet.Created,
                        categoryId = bouquet.CategoryId,
                        categoryName = bouquet.Category!.CategoryName,
                        status = bouquet.Status,
                        imageUrl = configuration["BaseUrl"] + "images/" + imagesGroup.OrderBy(i => i.ImageId).Select(i => i.ImageUrl).FirstOrDefault()
                    };

        return query.OrderByDescending(p => p.bouquetId).ToList();
    }


    // method hiện thị danh sách bó hoa thoe status là true hoặc false
    public dynamic findByStatus(bool status)
    {
        var query = from bouquet in db.Bouquets
                    join image in db.Images on bouquet.BouquetId equals image.BouquetId into imagesGroup
                    join bouquetEvent in db.BouquetEvents on bouquet.BouquetId equals bouquetEvent.BouquetId into bouquetEventGroup
                    from events in bouquetEventGroup.DefaultIfEmpty()
                    where bouquet.Status == status
                    select new
                    {
                        bouquetId = bouquet.BouquetId,
                        bouquetName = bouquet.BouquetName,
                        description = bouquet.Description,
                        price = events != null && events.Event!.IsAction == true
                            ? bouquet.EventPrice
                            : events != null && events.Event!.IsAction == false
                                ? bouquet.SecondPrice
                                : bouquet.Price,
                        secondPrice = events != null && events.Event!.IsAction == true
                            ? bouquet.Price
                            : events != null && events.Event!.IsAction == false
                                ? 0
                                : bouquet.SecondPrice,
                        eventPrice = events != null && events.Event!.IsAction == true
                            ? bouquet.EventPrice
                            : 0,
                        quantity = bouquet.Quantity,
                        created = bouquet.Created,
                        categoryId = bouquet.CategoryId,
                        categoryName = bouquet.Category!.CategoryName,
                        status = bouquet.Status,
                        imageUrl = configuration["BaseUrl"] + "images/" + imagesGroup.OrderBy(i => i.ImageId).Select(i => i.ImageUrl).FirstOrDefault()
                    };

        return query.OrderByDescending(p => p.bouquetId).ToList();
    }

    // method hiện thị danh sách tìm kiếm thoe giá của sản phẩm: ở đây chỉ cho một giá trị tìm kiếm. Lọc theo khoảng giá đó đổ lại. Làm vậy để tiện cho user có thể sử dụng, thay vì lọc thoe 2 khoảng giá.
    public dynamic findByPriceRange(int maxPrice)
    {
        var query = from bouquet in db.Bouquets
                    join image in db.Images on bouquet.BouquetId equals image.BouquetId into imagesGroup
                    join bouquetEvent in db.BouquetEvents on bouquet.BouquetId equals bouquetEvent.BouquetId into bouquetEventGroup
                    from events in bouquetEventGroup.DefaultIfEmpty()
                    where bouquet.Price <= maxPrice
                    select new
                    {
                        bouquetId = bouquet.BouquetId,
                        bouquetName = bouquet.BouquetName,
                        description = bouquet.Description,
                        price = events != null && events.Event!.IsAction == true
                            ? bouquet.EventPrice
                            : events != null && events.Event!.IsAction == false
                                ? bouquet.SecondPrice
                                : bouquet.Price,
                        secondPrice = events != null && events.Event!.IsAction == true
                            ? bouquet.Price
                            : events != null && events.Event!.IsAction == false
                                ? 0
                                : bouquet.SecondPrice,
                        eventPrice = events != null && events.Event!.IsAction == true
                            ? bouquet.EventPrice
                            : 0,
                        quantity = bouquet.Quantity,
                        created = bouquet.Created,
                        categoryId = bouquet.CategoryId,
                        categoryName = bouquet.Category!.CategoryName,
                        status = bouquet.Status,
                        imageUrl = configuration["BaseUrl"] + "images/" + imagesGroup.OrderBy(i => i.ImageId).Select(i => i.ImageUrl).FirstOrDefault()
                    };

        return query.OrderByDescending(p => p.price).ToList();
    }

    // method tìm kiếm bouquet theo categoryId
    public dynamic findByCategoryId(int categoryId)
    {
        var query = from bouquet in db.Bouquets
                    join image in db.Images on bouquet.BouquetId equals image.BouquetId into imagesGroup
                    join bouquetEvent in db.BouquetEvents on bouquet.BouquetId equals bouquetEvent.BouquetId into bouquetEventGroup
                    from events in bouquetEventGroup.DefaultIfEmpty()
                    where bouquet.CategoryId == categoryId
                    select new
                    {
                        bouquetId = bouquet.BouquetId,
                        bouquetName = bouquet.BouquetName,
                        description = bouquet.Description,
                        price = events != null && events.Event!.IsAction == true
                            ? bouquet.EventPrice
                            : events != null && events.Event!.IsAction == false
                                ? bouquet.SecondPrice
                                : bouquet.Price,
                        secondPrice = events != null && events.Event!.IsAction == true
                            ? bouquet.Price
                            : events != null && events.Event!.IsAction == false
                                ? 0
                                : bouquet.SecondPrice,
                        eventPrice = events != null && events.Event!.IsAction == true
                            ? bouquet.EventPrice
                            : 0,
                        quantity = bouquet.Quantity,
                        created = bouquet.Created,
                        categoryId = bouquet.CategoryId,
                        categoryName = bouquet.Category!.CategoryName,
                        status = bouquet.Status,
                        imageUrl = configuration["BaseUrl"] + "images/" + imagesGroup.OrderBy(i => i.ImageId).Select(i => i.ImageUrl).FirstOrDefault()
                    };

        return query.OrderByDescending(p => p.bouquetId).ToList();
    }


    // tìm kiếm bouquet dựa vào bouquet id
    public dynamic findById(int bouquetId)
    {
        var query = from bouquet in db.Bouquets
                    join image in db.Images on bouquet.BouquetId equals image.BouquetId into imagesGroup
                    where bouquet.BouquetId == bouquetId
                    select new
                    {
                        bouquetId = bouquet.BouquetId,
                        bouquetName = bouquet.BouquetName,
                        description = bouquet.Description,
                        price = bouquet.Price,
                        secondPrice = bouquet.SecondPrice,
                        eventPrice = bouquet.EventPrice,
                        quantity = bouquet.Quantity,
                        created = bouquet.Created,
                        categoryId = bouquet.CategoryId,
                        categoryName = bouquet.Category!.CategoryName,
                        status = bouquet.Status,
                        imageUrl = configuration["BaseUrl"] + "images/" + imagesGroup.OrderBy(i => i.ImageId).Select(i => i.ImageUrl).FirstOrDefault()
                    };

        return query.OrderByDescending(p => p.bouquetId).ToList();
    }

    // thêm mới một bouquet
    public bool Create(Bouquet bouquet)
    {
        try
        {
            db.Bouquets.Add(bouquet);
            return db.SaveChanges() > 0;
        }
        catch
        {
            return false;
        }
    }

    // update 1 bouquet
    public bool Update(Bouquet bouquet)
    {
        try
        {
            db.Entry(bouquet).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            return db.SaveChanges() > 0;
        }
        catch
        {
            return false;
        }
    }

    public dynamic findByBouquetName(string bouquetName)
    {
        var query = from bouquet in db.Bouquets
                    join image in db.Images on bouquet.BouquetId equals image.BouquetId into imagesGroup
                    join bouquetEvent in db.BouquetEvents on bouquet.BouquetId equals bouquetEvent.BouquetId into bouquetEventGroup
                    from events in bouquetEventGroup.DefaultIfEmpty()
                    where bouquet.BouquetName!.Contains(bouquetName)
                    select new
                    {
                        bouquetId = bouquet.BouquetId,
                        bouquetName = bouquet.BouquetName,
                        description = bouquet.Description,
                        price = events != null && events.Event!.IsAction == true
                            ? bouquet.EventPrice
                            : events != null && events.Event!.IsAction == false
                                ? bouquet.SecondPrice
                                : bouquet.Price,
                        secondPrice = events != null && events.Event!.IsAction == true
                            ? bouquet.Price
                            : events != null && events.Event!.IsAction == false
                                ? 0
                                : bouquet.SecondPrice,
                        eventPrice = events != null && events.Event!.IsAction == true
                            ? bouquet.EventPrice
                            : 0,
                        quantity = bouquet.Quantity,
                        created = bouquet.Created,
                        categoryId = bouquet.CategoryId,
                        categoryName = bouquet.Category!.CategoryName,
                        status = bouquet.Status,
                        imageUrl = configuration["BaseUrl"] + "images/" + imagesGroup.OrderBy(i => i.ImageId).Select(i => i.ImageUrl).FirstOrDefault()
                    };

        return query.OrderByDescending(p => p.bouquetId).ToList();
    }


    // method ở lấy sản phẩm ra ở trang home cho user
    public dynamic findNewBouquet()
    {
        var query = from bouquet in db.Bouquets
                    join image in db.Images on bouquet.BouquetId equals image.BouquetId into imagesGroup
                    join bouquetEvent in db.BouquetEvents on bouquet.BouquetId equals bouquetEvent.BouquetId into bouquetEventGroup
                    from events in bouquetEventGroup.DefaultIfEmpty()
                    select new
                    {
                        bouquetId = bouquet.BouquetId,
                        bouquetName = bouquet.BouquetName,
                        description = bouquet.Description,
                        price = events != null && events.Event!.IsAction == true
                            ? bouquet.EventPrice
                            : events != null && events.Event!.IsAction == false
                                ? bouquet.SecondPrice
                                : bouquet.Price,
                        secondPrice = events != null && events.Event!.IsAction == true
                            ? bouquet.Price
                            : events != null && events.Event!.IsAction == false
                                ? 0
                                : bouquet.SecondPrice,
                        eventPrice = events != null && events.Event!.IsAction == true
                            ? bouquet.EventPrice
                            : 0,
                        quantity = bouquet.Quantity,
                        created = bouquet.Created,
                        categoryId = bouquet.CategoryId,
                        categoryName = bouquet.Category!.CategoryName,
                        status = bouquet.Status,
                        imageUrl = configuration["BaseUrl"] + "images/" + imagesGroup.OrderBy(i => i.ImageId).Select(i => i.ImageUrl).FirstOrDefault()
                    };

        var filteredBouquets = query.Where(p => p.status == true).OrderByDescending(p => p.bouquetId).Take(6).ToList();

        return filteredBouquets;
    }

    public dynamic findFavoriteBouquet()
    {
        var query = from bouquet in db.Bouquets
                    join image in db.Images on bouquet.BouquetId equals image.BouquetId into imagesGroup
                    join bouquetEvent in db.BouquetEvents on bouquet.BouquetId equals bouquetEvent.BouquetId into bouquetEventGroup
                    from events in bouquetEventGroup.DefaultIfEmpty()
                    select new
                    {
                        bouquetId = bouquet.BouquetId,
                        bouquetName = bouquet.BouquetName,
                        description = bouquet.Description,
                        price = events != null && events.Event!.IsAction == true
                            ? bouquet.EventPrice
                            : events != null && events.Event!.IsAction == false
                                ? bouquet.SecondPrice
                                : bouquet.Price,
                        secondPrice = events != null && events.Event!.IsAction == true
                            ? bouquet.Price
                            : events != null && events.Event!.IsAction == false
                                ? 0
                                : bouquet.SecondPrice,
                        eventPrice = events != null && events.Event!.IsAction == true
                            ? bouquet.EventPrice
                            : 0,
                        created = bouquet.Created,
                        categoryId = bouquet.CategoryId,
                        categoryName = bouquet.Category!.CategoryName,
                        status = bouquet.Status,
                        imageUrl = configuration["BaseUrl"] + "images/" + imagesGroup.OrderBy(i => i.ImageId).Select(i => i.ImageUrl).FirstOrDefault()
                    };

        var orderedBouquets = query.Where(p => p.status == true).OrderByDescending(p => p.bouquetId).ToList();

        var queryWithQuantity = from bouquet in orderedBouquets
                                join orderDetail in db.OrderDetails on bouquet.bouquetId equals orderDetail.BouquetId into orderDetailGroup
                                select new
                                {
                                    bouquetId = bouquet.bouquetId,
                                    bouquetName = bouquet.bouquetName,
                                    description = bouquet.description,
                                    price = bouquet.price,
                                    secondPrice = bouquet.secondPrice,
                                    eventPrice = bouquet.eventPrice,
                                    created = bouquet.created,
                                    categoryId = bouquet.categoryId,
                                    categoryName = bouquet.categoryName,
                                    status = bouquet.status,
                                    imageUrl = bouquet.imageUrl,
                                    totalQuantity = orderDetailGroup.Sum(o => o.Quantity) // Tổng quantity của mỗi bouquet
                                };

        var filteredBouquets = queryWithQuantity.OrderByDescending(p => p.totalQuantity).Take(3).ToList();

        return filteredBouquets;
    }

    public dynamic findCateogryBouquet()
    {
        var query = from bouquet in db.Bouquets
                    join image in db.Images on bouquet.BouquetId equals image.BouquetId into imagesGroup
                    join bouquetEvent in db.BouquetEvents on bouquet.BouquetId equals bouquetEvent.BouquetId into bouquetEventGroup
                    from events in bouquetEventGroup.DefaultIfEmpty()
                    select new
                    {
                        bouquetId = bouquet.BouquetId,
                        bouquetName = bouquet.BouquetName,
                        description = bouquet.Description,
                        price = events != null && events.Event!.IsAction == true
                            ? bouquet.EventPrice
                            : events != null && events.Event!.IsAction == false
                                ? bouquet.SecondPrice
                                : bouquet.Price,
                        secondPrice = events != null && events.Event!.IsAction == true
                            ? bouquet.Price
                            : events != null && events.Event!.IsAction == false
                                ? 0
                                : bouquet.SecondPrice,
                        eventPrice = events != null && events.Event!.IsAction == true
                            ? bouquet.EventPrice
                            : 0,
                        quantity = bouquet.Quantity,
                        created = bouquet.Created,
                        categoryId = bouquet.CategoryId,
                        categoryName = bouquet.Category!.CategoryName,
                        status = bouquet.Status,
                        imageUrl = configuration["BaseUrl"] + "images/" + imagesGroup.OrderBy(i => i.ImageId).Select(i => i.ImageUrl).FirstOrDefault()
                    };

        var groupedBouquets = query.Where(p => p.status == true)
                                  .GroupBy(p => p.categoryId)
                                  .Select(group => group.OrderByDescending(p => p.created).FirstOrDefault())
                                  .ToList();

        return groupedBouquets;
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
                    };

        return query.OrderByDescending(p => p.bouquetId).ToList();
    }

    public dynamic findByBouquetId(int bouquetId)
    {
        var query = from bouquet in db.Bouquets
                    join image in db.Images on bouquet.BouquetId equals image.BouquetId into imagesGroup
                    join bouquetEvent in db.BouquetEvents on bouquet.BouquetId equals bouquetEvent.BouquetId into bouquetEventGroup
                    from events in bouquetEventGroup.DefaultIfEmpty()
                    where bouquet.BouquetId == bouquetId
                    select new
                    {
                        bouquetId = bouquet.BouquetId,
                        bouquetName = bouquet.BouquetName,
                        description = bouquet.Description,
                        price = events != null && events.Event!.IsAction == true
                            ? bouquet.EventPrice
                            : events != null && events.Event!.IsAction == false
                                ? bouquet.SecondPrice
                                : bouquet.Price,
                        secondPrice = events != null && events.Event!.IsAction == true
                            ? bouquet.Price
                            : events != null && events.Event!.IsAction == false
                                ? 0
                                : bouquet.SecondPrice,
                        eventPrice = events != null && events.Event!.IsAction == true
                            ? bouquet.EventPrice
                            : 0,
                        quantity = bouquet.Quantity,
                        created = bouquet.Created,
                        categoryId = bouquet.CategoryId,
                        categoryName = bouquet.Category!.CategoryName,
                        status = bouquet.Status,
                        imageUrl = configuration["BaseUrl"] + "images/" + imagesGroup.OrderBy(i => i.ImageId).Select(i => i.ImageUrl).FirstOrDefault()
                    };

        return query.OrderByDescending(p => p.bouquetId).FirstOrDefault()!;
    }
}
