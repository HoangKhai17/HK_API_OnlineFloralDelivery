using System;
using System.Collections.Generic;

namespace DemoOnlineFloralDelivery.Models;

public partial class Image
{
    public int? ImageId { get; set; }

    public string? ImageUrl { get; set; }

    public int? BouquetId { get; set; }

    public virtual Bouquet? Bouquet { get; set; }
}
