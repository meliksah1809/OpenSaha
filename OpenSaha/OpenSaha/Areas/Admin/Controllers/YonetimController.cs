using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace OpenSaha.Areas.Admin.Controllers
{
	[Area("Admin")]
    [Authorize(Policy = "Yonetici")]
    public class YonetimController : Controller
	{
		public IActionResult Index()
		{
			return View();
		}
	}
}
