using Microsoft.EntityFrameworkCore;
using DemoOnlineFloralDelivery.Models;

namespace DemoOnlineFloralDelivery.Service;

public class EventServiceImpl : EventService
{
    private DatabaseContext db;
    private IConfiguration configuration;

    public EventServiceImpl(DatabaseContext _db, IConfiguration _configuration)
    {
        db = _db;
        configuration = _configuration;
    }
    //Hiển thị danh sách toàn bộ Event
    public dynamic findAllEvent()
    {
        return db.Events.OrderByDescending(p => p.EventId).Select(p => new
        {
            EventId = p.EventId,
            EventName = p.EventName,
            Description = p.Description,
            StartDate = p.StartDate,
            EndDate = p.EndDate,
            IsAction = p.IsAction,
            ImageUrl = configuration["BaseUrl"] + "images/" + p.ImageUrl,
        }).OrderByDescending(p => p.EventId).ToList();
    }
    //Thêm(add) Event
    public bool addEvent(Event _event)
    {
        try
        {
            db.Events.Add(_event);
            return db.SaveChanges() > 0;
        }
        catch
        {
            return false;
        }
    }
    //Cập nhật(update) Event
    public bool updateEvent(Event _event)
    {
        try
        {
            db.Entry(_event).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            return db.SaveChanges() > 0;
        }
        catch
        {
            return false;
        }
    }
    //Hiển thị danh sách Event theo IsAction
    public dynamic findEventIsAction()
    {
        return db.Events.Where(p => p.IsAction == true).OrderByDescending(p => p.EventId).Select(p => new
        {
            EventId = p.EventId,
            EventName = p.EventName,
            Description = p.Description,
            StartDate = p.StartDate,
            EndDate = p.EndDate,
            IsAction = p.IsAction,
            ImageUrl = configuration["BaseUrl"] + "images/" + p.ImageUrl,
        }).OrderByDescending(p => p.EventId).ToList();
    }
    //Tìm kiếm(search) Event theo Event_name
    public dynamic searcEventName(string keyword)
    {
        return db.Events.Where(p => p.EventName!.Contains(keyword)).Select(p => new
        {
            EventId = p.EventId,
            EventName = p.EventName,
            Description = p.Description,
            StartDate = p.StartDate,
            EndDate = p.EndDate,
            IsAction = p.IsAction,
            ImageUrl = configuration["BaseUrl"] + "images/" + p.ImageUrl,
        }).ToList();
    }
    //Tìm kiếm(search) Event theo Event_id
    public dynamic searchEventId(int id)
    {
        return db.Events.Where(p => p.EventId == id).Select(p => new
        {
            EventId = p.EventId,
            EventName = p.EventName,
            Description = p.Description,
            StartDate = p.StartDate,
            EndDate = p.EndDate,
            IsAction = p.IsAction,
            ImageUrl = configuration["BaseUrl"] + "images/" + p.ImageUrl,
        }).FirstOrDefault()!;
    }

    public Event findEventId(int id)
    {
        return db.Events.AsNoTracking().FirstOrDefault(p => p.EventId == id)!;
    }

    public dynamic findEventIsActionUser()
    {
        return db.Events.Where(p => p.IsAction == true).OrderByDescending(p => p.EventId).Select(p => new
        {
            EventId = p.EventId,
            EventName = p.EventName,
            Description = p.Description,
            StartDate = p.StartDate,
            EndDate = p.EndDate,
            IsAction = p.IsAction,
            ImageUrl = configuration["BaseUrl"] + "images/" + p.ImageUrl,
        }).OrderByDescending(p => p.EventId).Take(1).ToList();
    }

    public dynamic findEvenUserDealOfMonth()
    {
        return db.Events.Where(p => p.EventId == 1).Select(p => new
        {
            EventId = p.EventId,
            EventName = p.EventName,
            Description = p.Description,
            StartDate = p.StartDate,
            EndDate = p.EndDate,
            IsAction = p.IsAction,
            ImageUrl = configuration["BaseUrl"] + "images/" + p.ImageUrl,
        }).FirstOrDefault()!;
    }
}
