using Microsoft.AspNetCore.Mvc;

namespace OpenSaha.Controllers
{
	public class Hata404Controller : Controller
	{
		public IActionResult Index()
		{
			return View();
		}
	}
}
