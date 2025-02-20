using DemoOnlineFloralDelivery.Models;

namespace DemoOnlineFloralDelivery.Service;

public class RoleServiceImpl:RoleService
{
    private DatabaseContext db;
    public RoleServiceImpl(DatabaseContext _db)
    {
        db = _db;
    }
    public bool Create(Role role)
    {
        try
        {
            db.Roles.Add(role);
            return db.SaveChanges() > 0;
        }
        catch
        {
            return false;
        }
    }

    public dynamic showAll()
    {
        return db.Roles.Select(r => new
        {
            roleId = r.RoleId,
            roleName = r.RoleName,
            description = r.Description,
            created = r.Created,
        }).ToList();
    }

    public bool Update(Role role)
    {
        try
        {
            db.Entry(role).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            return db.SaveChanges() > 0;
        }
        catch
        {
            return false;
        }
    }
}
