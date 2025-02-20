using DemoOnlineFloralDelivery.Models;

namespace DemoOnlineFloralDelivery.Service;

public interface ImageService
{
    public dynamic findAll();

    public dynamic findById(int bouquetId);
    public dynamic findByImageId(int imageId);

    public bool Create(Image img);
    public bool Update(Image img);

    public bool Delete(int imageId);
    //Tìm kiếm(find) image theo image_id để không update ảnh cũ
    public Image findImageId(int? id);

}
