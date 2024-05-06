using Microsoft.AspNetCore.Mvc;

namespace OpenSaha.ViewComponents.Site
{
    public class SiteFooter : ViewComponent
    { 
        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}
