using System;
using System.Collections.Generic;

namespace KuaforRandevu.Models;

public partial class BlockedSlot
{
    public int Id { get; set; }

    public DateTime? BlockedDateTime { get; set; }

    public string? Reason { get; set; }
    public int AdminId { get; set; }
    public virtual Admin Admin { get; set; } = null!;

}
