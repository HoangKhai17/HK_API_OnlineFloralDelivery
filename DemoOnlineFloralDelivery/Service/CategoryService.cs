using DemoOnlineFloralDelivery.Models;

namespace DemoOnlineFloralDelivery.Service;

public interface CategoryService
{
    //Hiển thị danh sách toàn bộ Categories
    public dynamic findAllCategories();
    //Thêm(add) Categories
    public bool addCategories(Category categories);
    //Cập nhật(update) Categories
    public bool updateCategories(Category categories);
    //Tìm kiếm(search) Categories theo Categories_id
    public dynamic searchCategoriesId(int id);
    
  
}
