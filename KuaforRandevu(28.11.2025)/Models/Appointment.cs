using System;
using System.Collections.Generic;

namespace KuaforRandevu.Models;

public partial class Appointment
{
    public int Id { get; set; }

    public string? CustomerName { get; set; }

    public string? Phone { get; set; }

    public DateTime? AppointmentDateTime { get; set; }

    public string? Status { get; set; }

    public int? AdminId { get; set; }

    public virtual Admin? Admin { get; set; }
}
