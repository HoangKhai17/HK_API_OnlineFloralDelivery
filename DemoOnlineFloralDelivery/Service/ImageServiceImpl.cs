using DemoOnlineFloralDelivery.Models;
using Microsoft.EntityFrameworkCore;

namespace DemoOnlineFloralDelivery.Service;

public class ImageServiceImpl : ImageService
{
    private DatabaseContext db;
    private IConfiguration configuration;
    public ImageServiceImpl(DatabaseContext _db, IConfiguration _configuration)
    {
        db = _db;
        configuration = _configuration;
    }
    public dynamic findAll()
    {
        return db.Images.OrderByDescending(p => p.ImageId).Select(p => new
        {
            ImageId = p.ImageId,
            ImageUrl = configuration["BaseUrl"] + "images/" + p.ImageUrl,
            bouquetId = p.BouquetId,
        }).ToList();
    }

    public dynamic findById(int bouquetId)
    {
        return db.Images.Where(p => p.BouquetId == bouquetId).OrderByDescending(p => p.ImageId).Select(p => new
        {
            ImageId = p.ImageId,
            ImageUrl = configuration["BaseUrl"] + "images/" + p.ImageUrl,
            bouquetId = p.BouquetId

        }).ToList();
    }

    public dynamic findByImageId(int imageId)
    {
        return db.Images.Where(p => p.ImageId == imageId).Select(p => new
        {
            ImageId = p.ImageId,
            ImageUrl = configuration["BaseUrl"] + "images/" + p.ImageUrl,
            bouquetId = p.BouquetId

        }).FirstOrDefault()!;
    }

    // thêm mới một anh 
    public bool Create(Image img)
    {
        try
        {
            db.Images.Add(img);
            return db.SaveChanges() > 0;
        }
        catch
        {
            return false;
        }
    }

    // update hinh anh
    public bool Update(Image img)
    {
        try
        {
            db.Entry(img).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            return db.SaveChanges() > 0;
        }
        catch
        {
            return false;
        }
    }

    public bool Delete(int imageId)
    {
        try
        {
            db.Images.Remove(db.Images.Find(imageId)!);
            return db.SaveChanges() > 0;
        }
        catch
        {
            return false;
        }
    }
    public Image findImageId(int? id)
    {
        return db.Images.AsNoTracking().FirstOrDefault(p => p.ImageId == id);
    }
}