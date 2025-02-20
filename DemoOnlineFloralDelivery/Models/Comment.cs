using System;
using System.Collections.Generic;

namespace DemoOnlineFloralDelivery.Models;

public partial class Comment
{
    public int CommentId { get; set; }

    public string? Content { get; set; }

    public DateTime? Created { get; set; }

    public int? AccountId { get; set; }

    public int? BouquetId { get; set; }

    public virtual Account? Account { get; set; }

    public virtual Bouquet? Bouquet { get; set; }
}
