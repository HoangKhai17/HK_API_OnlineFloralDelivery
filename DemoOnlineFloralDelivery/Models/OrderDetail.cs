using System;
using System.Collections.Generic;

namespace DemoOnlineFloralDelivery.Models;

public partial class OrderDetail
{
    public int OrderId { get; set; }

    public int BouquetId { get; set; }

    public int? Quantity { get; set; }

    public decimal? TotalPrice { get; set; }

    public DateTime? Created { get; set; }

    public virtual Bouquet Bouquet { get; set; } = null!;

    public virtual Order Order { get; set; } = null!;
}
