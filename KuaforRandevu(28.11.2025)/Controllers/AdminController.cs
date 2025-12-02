using Microsoft.AspNetCore.Mvc;
using KuaforRandevu.Models;
using System.Linq;

namespace KuaforRandevu.Controllers
{
    public class AdminController : Controller
    {
        private readonly KuaforRandevuDbContext _context;

        public AdminController(KuaforRandevuDbContext context)
        {
            _context = context;
        }

        // Admin login sayfası
        public IActionResult AdminLogin()
        {
            return View();
        }

        [HttpPost]
        public IActionResult AdminLogin(string username, string password)
        {
            var admin = _context.Admins.FirstOrDefault(a => a.Username == username && a.Password == password);
            if (admin != null)
            {
                // Login başarılı → AdminPanel sayfasına yönlendir
                return RedirectToAction("AdminPanel", new { adminId = admin.Id });
            }

            ViewBag.Error = "Kullanıcı adı veya şifre hatalı";
            return View();
        }

        public IActionResult AdminPanel(int adminId)
        {
            // Adminin randevularını çek
            var appointments = _context.Appointments
                .Where(a => a.AdminId == adminId)
                .OrderBy(a => a.AppointmentDateTime)
                .ToList();

            // Adminin kapalı saatlerini çek
            var blockedSlots = _context.BlockedSlots
                .Where(b => b.AdminId == adminId)
                .OrderBy(b => b.BlockedDateTime)
                .ToList();

            ViewBag.AdminId = adminId;

            return View(new AdminPanelViewModel
            {
                Appointments = appointments,
                BlockedSlots = blockedSlots
            });
        }

        // -------------------------
        // RANDEVU İPTAL
        // -------------------------
        [HttpPost]
        public IActionResult CancelAppointment(int appointmentId)
        {
            var app = _context.Appointments.FirstOrDefault(a => a.Id == appointmentId);
            if (app != null)
            {
                app.Status = "İptal";
                _context.SaveChanges();
                return Json(new { success = true });
            }
            return Json(new { success = false, message = "Randevu bulunamadı" });
        }

        // -------------------------
        // BOŞ SAAT BLOKLA
        // -------------------------
        [HttpPost]
        public IActionResult BlockSlot(int adminId, DateTime dateTime, string reason)
        {
            bool exists = _context.BlockedSlots.Any(b => b.AdminId == adminId && b.BlockedDateTime == dateTime);
            if (exists)
                return Json(new { success = false, message = "Bu saat zaten bloklanmış" });

            var slot = new BlockedSlot
            {
                AdminId = adminId,
                BlockedDateTime = dateTime,
                Reason = reason
            };
            _context.BlockedSlots.Add(slot);
            _context.SaveChanges();

            return Json(new { success = true });
        }
    }

    // -------------------------
    // VIEW MODEL
    // -------------------------
    public class AdminPanelViewModel
    {
        public List<Appointment> Appointments { get; set; }
        public List<BlockedSlot> BlockedSlots { get; set; }
    }
}

