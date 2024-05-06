using Microsoft.AspNetCore.Mvc;

namespace OpenSaha.ViewComponents.Site
{
	public class PartialLogin : ViewComponent
	{
		public IViewComponentResult Invoke()
		{
			return View();
		}
	}
}
