using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace OpenSaha.Areas.Admin.Controllers
{
	[Area("Admin")]
    [Authorize(Policy = "Yonetici")]
    public class SliderController : Controller
	{
		public IActionResult Index()
		{
			return View();
		}
	}
}
