using System;
using System.Collections.Generic;

namespace KuaforRandevu.Models;

public partial class Appointment
{
    public int AppointmentId { get; set; }

    public string? CustomesName { get; set; }

    public string? Phone { get; set; }

    public DateOnly? AppointmentDate { get; set; }

    public TimeOnly? AppointmentTime { get; set; }

    public string? Status { get; set; }
}
