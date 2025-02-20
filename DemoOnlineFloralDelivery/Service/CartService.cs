using DemoOnlineFloralDelivery.Models;

namespace DemoOnlineFloralDelivery.Service;

public interface CartService
{
    //Hiển thị danh sách toàn bộ Cart
    public dynamic findAllCart();

    public dynamic findByAccountName(string accountName);
    //Thêm(add) Cart
    public bool addCart(Cart Cart);
    //Cập nhật(update) Cart
    public bool updateCart(Cart Cart);

    public bool DeleteCart(int cartId);
    public bool DeleteCartByAccountId(int accountId);

    public dynamic findByCartId(int cartId);
}
