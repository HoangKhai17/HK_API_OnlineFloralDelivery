using DemoOnlineFloralDelivery.Models;

namespace DemoOnlineFloralDelivery.Service;

public interface CommentService
{
    //Hiển thị danh sách toàn bộ Comment
    public dynamic findAllComment();

    public dynamic findByBouquetId(int bouquetId);

    //Thêm(add) Comment
    public bool addComment(Comment _comment);
    //Cập nhật(update) Comment
    public bool updateComment(Comment comment);
    //Xóa(Delete) Comment
    public bool deleteComment(int CommentId);
}
