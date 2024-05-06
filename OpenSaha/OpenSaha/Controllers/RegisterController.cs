using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using OpenSaha.Helpers;
using OpenSaha.Models;
using OpenSaha.ViewModels.Account;
using System.Security.Claims;

namespace OpenSaha.Controllers
{
    public class RegisterController : Controller
    {
        private readonly SahaContext _db;
        public RegisterController(SahaContext db)
        {
            _db = db;
        }
        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Index(RegisterViewModel request)
        {
            if (ModelState.IsValid)
            {
                string telefon = Helper.TelefonNo(request.Telefon);
                var kontrol = _db.Kullanicis.Where(x => x.Email == request.Email || x.Telefon == telefon).ToList();
                if (kontrol.Count() > 0)
                {
                    //ARKADAŞ SENİN KULLANICI HESABIN VAR GİRİŞ YAP!
                }
                else
                {
                    Kullanici kullanici = new Kullanici();
                    kullanici.Email = request.Email;
                    kullanici.Isim = request.Isim;
                    kullanici.Soyisim = request.Soyisim;
                    kullanici.Telefon = telefon;
                    kullanici.Password = Security.PasswordSifrele(request.Password);
                    _db.Set<Kullanici>().Add(kullanici);
                    _db.SaveChanges();
                    //var claims = new List<Claim>
                    //{
                    //    new Claim(ClaimTypes.Name, request.Email)
                    //};
                    //var userIdentity = new ClaimsIdentity(claims, "Giris");
                    //await _signin.SignInAsync(kullanici, true);
                    //return RedirectToAction("Index", "Home");
                }
            }
            else
            {
                //GEREKLİ ALANLARI DOLDURUNUZ!
            }
            return RedirectToAction("Index", "Home");
        }
    }
}