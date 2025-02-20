using System;
using System.Collections.Generic;

namespace DemoOnlineFloralDelivery.Models;

public partial class Event
{
    public int EventId { get; set; }

    public string? EventName { get; set; }

    public string? Description { get; set; }

    public string? ImageUrl { get; set; }

    public DateTime? StartDate { get; set; }

    public DateTime? EndDate { get; set; }

    public bool? IsAction { get; set; }

    public virtual ICollection<BouquetEvent> BouquetEvents { get; set; } = new List<BouquetEvent>();
}
