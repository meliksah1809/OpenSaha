using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace OpenSaha.Areas.Kontrol.Controllers
{

    [Area("Kontrol")]
    [Authorize(Policy = "UserYonetici")]
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
