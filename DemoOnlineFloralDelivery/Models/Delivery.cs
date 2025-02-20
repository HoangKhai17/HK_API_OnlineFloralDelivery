using System;
using System.Collections.Generic;

namespace DemoOnlineFloralDelivery.Models;

public partial class Delivery
{
    public int DeliveryId { get; set; }

    public int? OrderId { get; set; }

    public string? RecipientName { get; set; }

    public string? RecipientAddress { get; set; }

    public string? RecipientPhone { get; set; }

    public string? Message { get; set; }

    public virtual Order? Order { get; set; }
}
