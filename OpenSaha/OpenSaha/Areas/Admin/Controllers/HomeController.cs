using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace OpenSaha.Areas.Admin.Controllers
{
    [Authorize(Policy = "Yonetici")]
    [Area("Admin")]
    public class HomeController : Controller
	{
		public IActionResult Index()
		{
			return View();
		}
	}
}
