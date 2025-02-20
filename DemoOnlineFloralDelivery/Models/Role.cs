using System;
using System.Collections.Generic;

namespace DemoOnlineFloralDelivery.Models;

public partial class Role
{
    public int RoleId { get; set; }

    public string? RoleName { get; set; }

    public string? Description { get; set; }

    public DateTime? Created { get; set; }

    public virtual ICollection<Account> Accounts { get; set; } = new List<Account>();
}
