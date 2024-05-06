using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OpenSaha.Models;
using System.Security.Claims;

namespace OpenSaha.ViewComponents.Site
{
    public class SiteMenu : ViewComponent
    {
        private readonly SahaContext _db;
        public SiteMenu(SahaContext db)
        {
            _db = db;
        }
        [HttpGet]
        public IViewComponentResult Invoke()
        {
            bool girisYapmis = false;
            string isim = "";
            var UserMail = User.Identity.Name;
            if (UserMail != null)
            {
                girisYapmis = true;
                var user = _db.Kullanicis.FirstOrDefault(x => x.Email == UserMail);
                isim = user.Isim + " " + user.Soyisim;
            }
            ViewBag.girisYapmis = girisYapmis;
            ViewBag.isim = isim;
            return View();
        }

		[HttpGet]
		public IViewComponentResult GirisYap()
		{

			return View();
		}
		[HttpPost]
		public async Task<IViewComponentResult> GirisYap(Kullanici g)
		{

			var bilgiler = _db.Kullanicis.FirstOrDefault(x => ((x.Isim == g.Isim) || (x.Email == g.Email)) && (x.Password == g.Password));
			if (bilgiler != null)
			{
				var claims = new List<Claim>
				{
					new Claim(ClaimTypes.Name,bilgiler.Email)
				};
				var useridentity = new ClaimsIdentity(claims, "Giris");
				ClaimsPrincipal principal = new ClaimsPrincipal(useridentity);
				await HttpContext.SignInAsync(principal);
				//HttpContext.Session.SetString("kullanici", k.Isi
				//m);
				return View("Index", "Home");
			}
			else
			{
				return View();
			}
		}


	}

}
