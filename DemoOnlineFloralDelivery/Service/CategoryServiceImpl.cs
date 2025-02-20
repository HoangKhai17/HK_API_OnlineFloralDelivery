using DemoOnlineFloralDelivery.Models;

namespace DemoOnlineFloralDelivery.Service;

public class CategoryServiceImpl:CategoryService
{
    private DatabaseContext db;
    public CategoryServiceImpl(DatabaseContext _db)
    {
        db = _db;
    }
    //Hiển thị danh sách toàn bộ Categories
    public dynamic findAllCategories()
    {
        return db.Categories.Select(p => new
        {
            CategoryId = p.CategoryId,
            CategoryName = p.CategoryName,
            Created = p.Created,
        }).OrderByDescending(p => p.CategoryId).ToList();
    }
    //Thêm(add) Categories
    public bool addCategories(Category _categories)
    {
        try
        {
            db.Categories.Add(_categories);
            return db.SaveChanges() > 0;
        }
        catch
        {
            return false;
        }
    }
    //Cập nhật(update) Categories
    public bool updateCategories(Category _categories)
    {
        try
        {
            db.Entry(_categories).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            return db.SaveChanges() > 0;
        }
        catch
        {
            return false;
        }
    }
    //Tìm kiếm(search) Categories theo Categories_id
    public dynamic searchCategoriesId(int id)
    {
        return db.Categories.Where(p => p.CategoryId == id).Select(p => new
        {
            CategoryId = p.CategoryId,
            CategoryName = p.CategoryName,
            Created = p.Created,
        }).OrderByDescending(p => p.CategoryId).FirstOrDefault()!;
    }

  
}
