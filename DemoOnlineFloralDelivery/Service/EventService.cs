using DemoOnlineFloralDelivery.Models;

namespace DemoOnlineFloralDelivery.Service;

public interface EventService
{
    //Hiển thị danh sách toàn bộ Event
    public dynamic findAllEvent();
    //Thêm(add) Event
    public bool addEvent(Event Event);
    //Cập nhật(update) Event
    public bool updateEvent(Event Event);
    //Hiển thị danh sách Event theo IsAction
    public dynamic findEventIsAction();
    //Tìm kiếm(search) Event theo Event_name
    public dynamic searcEventName(string keyword);
    //Tìm kiếm(search) Event theo Event_id
    public dynamic searchEventId(int id);
    public Event findEventId(int id);

    public dynamic findEventIsActionUser();

    public dynamic findEvenUserDealOfMonth();

}
