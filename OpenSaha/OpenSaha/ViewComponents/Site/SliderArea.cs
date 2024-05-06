using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewComponents;

namespace OpenSaha.ViewComponents.Site
{
    public class SliderArea:ViewComponent
    {
        
        public IViewComponentResult Invoke()
        {

            return View();
        }
    }
}
