using KuaforRandevu.Models;
using Microsoft.AspNetCore.Mvc;

namespace KuaforRandevu.Controllers
{
    public class AppointmentController : Controller
    {
        private readonly KuaforRandevuDbContext _context;

        public AppointmentController(KuaforRandevuDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            return View();
        }


    }
}
