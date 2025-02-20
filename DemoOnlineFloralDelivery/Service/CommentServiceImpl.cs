using DemoOnlineFloralDelivery.Models;
using Microsoft.EntityFrameworkCore;

namespace DemoOnlineFloralDelivery.Service;

public class CommentServiceImpl:CommentService
{
    private DatabaseContext db;
    public CommentServiceImpl(DatabaseContext _db)
    {
        db = _db;

    }
    //Hiển thị danh sách toàn bộ Comment
    public dynamic findAllComment()
    {
        return db.Comments.Select(p => new
        {
            CommentId = p.CommentId,
            Content = p.Content,
            Created = p.Created,
            AccountId = p.AccountId,
            AccountName = p.Account!.Username,
            BouquetId = p.BouquetId,
            BouquetName = p.Bouquet!.BouquetName
        }).OrderByDescending(p => p.CommentId).ToList();
    }

    public dynamic findByBouquetId(int bouquetId)
    {
        return db.Comments.Where(p=>p.BouquetId == bouquetId).Select(p => new
        {
            CommentId = p.CommentId,
            Content = p.Content,
            Created = p.Created,
            AccountId = p.AccountId,
            AccountName = p.Account!.Username,
            BouquetId = p.BouquetId,
            BouquetName = p.Bouquet!.BouquetName
        }).OrderByDescending(p => p.CommentId).ToList();
    }
 
    //Cập nhật(update) Comment
    public bool updateComment(Comment _Comment)
    {
        try
        {
            db.Entry(_Comment).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            return db.SaveChanges() > 0;
        }
        catch
        {
            return false;
        }
    }
    //Xóa(Delete) Comment
    public bool deleteComment(int CommentId)
    {
        try
        {
            db.Comments.Remove(db.Comments.Find(CommentId)!);
            return db.SaveChanges() > 0;
        }
        catch
        {
            return false;
        }
    }

    public bool addComment(Comment _comment)
    {
        try
        {
            using (var db = new DatabaseContext()) // Thay YourDbContext bằng DbContext của bạn
            {
                // Kiểm tra tính hợp lệ của dữ liệu
                if (string.IsNullOrEmpty(_comment.Content) || _comment.AccountId <= 0 || _comment.BouquetId <= 0)
                {
                    throw new ArgumentException("Invalid comment data.");
                }

                db.Comments.Add(_comment);
                return db.SaveChanges() > 0;
            }
        }
        catch (DbUpdateException ex)
        {
            // Xử lý lỗi khi lưu dữ liệu vào cơ sở dữ liệu
            // Ví dụ: ghi log, thông báo lỗi, v.v.
            // Hoặc throw lỗi để truyền thông tin lỗi cho lớp trên
            throw new Exception("An error occurred while saving data.", ex);
        }
        catch (ArgumentException ex)
        {
            // Xử lý lỗi khi dữ liệu đầu vào không hợp lệ
            // Ví dụ: ghi log, thông báo lỗi, v.v.
            // Hoặc throw lỗi để truyền thông tin lỗi cho lớp trên
            throw ex;
        }
        catch (Exception ex)
        {
            // Xử lý các lỗi khác nếu có
            // Ví dụ: ghi log, thông báo lỗi, v.v.
            // Hoặc throw lỗi để truyền thông tin lỗi cho lớp trên
            throw ex;
        }
    }
}
