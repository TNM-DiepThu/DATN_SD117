using Microsoft.AspNetCore.Mvc;

namespace AppView.Controllers
{
    public class ThongKeController : Controller
    {
        public IActionResult ThongKe()
        {
            return View();
        }
    }
}
