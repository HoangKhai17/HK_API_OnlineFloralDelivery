using DemoOnlineFloralDelivery.Models;

namespace DemoOnlineFloralDelivery.Service;

public interface BouquetService
{
    //method hiện thị danh sách bó hoa(Bouquet) thoe ngày tạo mới nhất
    public dynamic findAll();

    // method hiện thị danh sách bó hoa thoe status là true hoặc false
    public dynamic findByStatus(bool status);
    // method hiện thị danh sách tìm kiếm thoe giá của sản phẩm
    public dynamic findByPriceRange(int maxPrice);
    // method tìm kiếm bouquet theo categoryId
    public dynamic findByCategoryId(int categoryId);

    // method tìm kiếm bouquet theo categoryId
    public dynamic findByBouquetName(string bouquetName);
    // created bouquet
    public bool Create(Bouquet bouquet);
    public bool Update(Bouquet bouquet);

    // tìm kiếm bouquet
    public dynamic findById(int bouquetId);

    // method ở giao diện user
    public dynamic findNewBouquet();

    public dynamic findFavoriteBouquet();

    public dynamic findCateogryBouquet();

    public dynamic findByEventId(int eventId);

    public dynamic findByBouquetId(int bouquetId);

}
