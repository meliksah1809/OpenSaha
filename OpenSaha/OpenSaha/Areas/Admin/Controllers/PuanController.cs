using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace OpenSaha.Areas.Admin.Controllers
{
	[Area("Admin")]
    [Authorize(Policy = "Yonetici")]
    [Authorize]
	public class PuanController : Controller
	{
		public IActionResult Index()
		{
			return View();
		}
	}
}
