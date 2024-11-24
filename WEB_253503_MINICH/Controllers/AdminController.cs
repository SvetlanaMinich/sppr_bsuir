using Microsoft.AspNetCore.Mvc;

namespace WEB_253503_MINICH.UI.Controllers
{
    public class AdminController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
