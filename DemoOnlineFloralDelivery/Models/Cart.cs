using System;
using System.Collections.Generic;

namespace DemoOnlineFloralDelivery.Models;

public partial class Cart
{
    public int CartId { get; set; }

    public int? AccountId { get; set; }

    public int? BouquetId { get; set; }

    public int? Quantity { get; set; }

    public decimal? TotalPrice { get; set; }

    public virtual Account? Account { get; set; }

    public virtual Bouquet? Bouquet { get; set; }
}
