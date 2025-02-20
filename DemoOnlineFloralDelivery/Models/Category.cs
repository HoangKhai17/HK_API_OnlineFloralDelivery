using System;
using System.Collections.Generic;

namespace DemoOnlineFloralDelivery.Models;

public partial class Category
{
    public int CategoryId { get; set; }

    public string? CategoryName { get; set; }

    public DateTime? Created { get; set; }

    public virtual ICollection<Bouquet> Bouquets { get; set; } = new List<Bouquet>();
}
