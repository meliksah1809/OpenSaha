using Microsoft.AspNetCore.Mvc;

namespace OpenSaha.Controllers
{
    public class HakkimizdaController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
