using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using OpenSaha.Helpers;
using OpenSaha.Models;
using OpenSaha.ViewModels.Account;
using System.Diagnostics.Eventing.Reader;
using System.Security.Claims;

namespace OpenSaha.Controllers
{
    public class LoginController : Controller
    {
        private readonly SahaContext _db;
        public LoginController(SahaContext db)
        {
            _db = db;
        }
        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Index(LoginViewModel request)
        {
            if (!String.IsNullOrEmpty(request.EmailPhone) || !String.IsNullOrEmpty(request.Password))
            {
                if (long.TryParse(request.EmailPhone, out _))
                {
                    request.EmailPhone = Helper.TelefonNo(request.EmailPhone);
                }
                string Password = Security.PasswordSifrele(request.Password);
                var bilgiler = _db.Kullanicis.FirstOrDefault(x => (x.Email == request.EmailPhone || x.Telefon == request.EmailPhone) && x.Password == Password);
                if (bilgiler != null)
                {
                    request.UserType = bilgiler.UserType;
                    var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name,bilgiler.Email),
                    new Claim("usertype", request.UserType.ToString())
                };
                    var useridentity = new ClaimsIdentity(claims, "Giris");
                    ClaimsPrincipal principal = new ClaimsPrincipal(useridentity);
                    await HttpContext.SignInAsync(principal);
                    //HttpContext.Session.SetString("kullanici", k.Isi
                    //m);
                    if (request.UserType == UserType.SahaYonetici)
                    {
                        string yon = "~/kontrol";
                        return Redirect(yon);
                    }else if (request.UserType == UserType.Yonetici)
                    {
                        string yon = "~/Admin";
                        return Redirect(yon);
                    }
                    return RedirectToAction("Index", "Home");
                }
            }
            else
            {
                //Lütfen gerekli alanları doldurunuz.....
            }
            return RedirectToAction("Index", "Home");
        }
    }
}