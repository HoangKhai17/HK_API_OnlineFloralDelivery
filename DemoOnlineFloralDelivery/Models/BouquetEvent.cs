using System;
using System.Collections.Generic;

namespace DemoOnlineFloralDelivery.Models;

public partial class BouquetEvent
{
    public int BouqueteventId { get; set; }

    public int? BouquetId { get; set; }

    public int? EventId { get; set; }

    public virtual Bouquet? Bouquet { get; set; }

    public virtual Event? Event { get; set; }
}
