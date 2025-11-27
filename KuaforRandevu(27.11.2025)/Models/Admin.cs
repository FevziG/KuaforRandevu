using System;
using System.Collections.Generic;

namespace KuaforRandevu.Models;

public partial class Admin
{
    public int Id { get; set; }

    public string? Username { get; set; }

    public string? Password { get; set; }

    public byte[]? Photo { get; set; }

    public virtual ICollection<Appointment> Appointments { get; set; } = new List<Appointment>();

    public virtual ICollection<BlockedSlot> BlockedSlots { get; set; } = new List<BlockedSlot>();
}
