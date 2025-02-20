using DemoOnlineFloralDelivery.Models;

namespace DemoOnlineFloralDelivery.Service;

public class ContactServiceImpl : ContactService
{
    private DatabaseContext db;
    public ContactServiceImpl(DatabaseContext _db)
    {
        db = _db;
    }

    public dynamic showAll()
    {
        return db.Contacts.OrderByDescending(ct => ct.ContactId).Select(ct => new
        {
            contactId = ct.ContactId,
            Name = ct.Name,
            Email = ct.Email,
            Subject = ct.Subject,
            Message = ct.Message,
            Created = ct.Created,
        }).ToList();
    }

    public bool Create(Contact contact)
    {
        try
        {
            db.Contacts.Add(contact);
            return db.SaveChanges() > 0;
        }
        catch
        {
            return false;
        }
    }
}
