using System;
using System.Collections.Generic;

namespace KuaforRandevu.Models;

public partial class BlockedSlot
{
    public int BlockId { get; set; }

    public DateOnly? BlockDate { get; set; }

    public TimeOnly? BlockTime { get; set; }

    public string? Reason { get; set; }
}
