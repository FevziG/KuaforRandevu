using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using KuaforRandevu.Models;

namespace KuaforRandevu.Controllers;

public class HomeController : Controller
{
    private readonly KuaforRandevuDbContext _context;

    public HomeController(KuaforRandevuDbContext context)
    {
        _context = context;
    }

    public IActionResult Index()
    {
        var admins = _context.Admins.ToList();
        return View(admins);
    }

    public IActionResult GetSlots(int adminId)
    {
        var blocked = _context.BlockedSlots
            .Where(b => b.AdminId == adminId)
            .Select(b => b.BlockedDateTime)
            .ToList();

        // ❗ Artık status filtrelemesi YOK — iptal hariç tüm randevular DOLU kabul edilir.
        var appointments = _context.Appointments
            .Where(a => a.AdminId == adminId && a.Status != "İptal")
            .Select(a => a.AppointmentDateTime)
            .ToList();

        var slots = new List<object>();

        for (int i = 0; i < 14; i++)
        {
            var date = DateTime.Today.AddDays(i);

            if (date.DayOfWeek == DayOfWeek.Sunday)
                continue;

            for (int hour = 9; hour <= 18; hour++)
            {
                var time = date.AddHours(hour);

                string status = "Müsait";

                if (appointments.Contains(time)) status = "Dolu";
                if (blocked.Contains(time)) status = "Kapalı";

                slots.Add(new
                {
                    Date = date.ToString("yyyy-MM-dd"),
                    Time = time.ToString("HH:mm"),
                    FullDateTime = time,
                    Status = status
                });
            }
        }

        return Json(slots);
    }

    [HttpPost]
    public IActionResult CreateAppointment(int adminId, DateTime dateTime, string name, string phone)
    {
        // ❗ Tarihi LOCAL olarak işaretliyoruz → SQL’de saat geri görünmez
       // dateTime = DateTime.SpecifyKind(dateTime, DateTimeKind.Local);
       dateTime = dateTime.AddHours(3);
        var app = new Appointment
        {
            AdminId = adminId,
            AppointmentDateTime = dateTime,
            CustomerName = name,
            Phone = phone,
            Status = "Randevulu" // Bekliyor da olsa artık o saat kilitli
        };

        _context.Appointments.Add(app);
        _context.SaveChanges();

        return Json(new { success = true });
    }

}
