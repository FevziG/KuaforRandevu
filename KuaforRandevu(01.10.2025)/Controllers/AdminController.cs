using Microsoft.AspNetCore.Mvc;

namespace KuaforRandevu.Controllers
{
    public class AdminController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
