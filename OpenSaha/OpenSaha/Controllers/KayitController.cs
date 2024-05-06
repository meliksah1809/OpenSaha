using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NuGet.Versioning;
using OpenSaha.Helpers;
using OpenSaha.Models;
using OpenSaha.ViewModels.Account;
using System.Security.Claims;
using System.Security.Cryptography.X509Certificates;

namespace OpenSaha.Controllers
{
    public class KayitController : Controller
    {
        private readonly SahaContext _db;
        public KayitController(SahaContext db)
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
        [HttpGet]
        public IActionResult GirisYap()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> GirisYap(Kullanici g)
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
                return RedirectToAction("Index", "Home");
            }
            else
            {
                return View();
            }
        }
        // BURADA YÖNETİCİ HESABI OLUŞTURMA VARDI FAKAT KULLANICININ GERÇEKTEN SAHA SAHİBİ OLDUĞU BİLİNEMEDİĞİ İÇİN BU KALDIRILDI
        //[HttpGet]
        //public IActionResult KayitOl()
        //{
        //    return View();
        //}
        //[HttpPost]
        //public async Task<IActionResult> KayitOl(RegisterViewModel request)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        string telefon = Helper.TelefonNo(request.Telefon);
        //        var kontrol = _db.Kullanicis.Where(x => x.Email == request.Email || x.Telefon == telefon).ToList();
        //        if (kontrol.Count() > 0)
        //        {
        //            //ARKADAŞ SENİN KULLANICI HESABIN VAR GİRİŞ YAP!
        //        }
        //        else
        //        {
        //            YonetimTablosu yt = new YonetimTablosu();
        //            yt.SahaSayisi = request.SahaSayisi;
        //            yt.YoneticiSayisi = request.YoneticiSayisi;
        //            yt.Act = Aktif.Aktif;
        //            _db.Set<YonetimTablosu>().Add(yt);
        //            _db.SaveChanges();
        //            Kullanici kullanici = new Kullanici();
        //            kullanici.Email = request.Email;
        //            kullanici.Isim = request.Isim;
        //            kullanici.Soyisim = request.Soyisim;
        //            kullanici.Telefon = telefon;
        //            kullanici.UserType = UserType.SahaYonetici;
        //            kullanici.YoneticiId = yt.Id;
        //            kullanici.Password = Security.PasswordSifrele(request.Password);
        //            _db.Set<Kullanici>().Add(kullanici);
        //            _db.SaveChanges();
        //        }
        //    }
        //    else
        //    {
        //        //GEREKLİ ALANLARI DOLDURUNUZ!
        //    }
        //    return RedirectToAction("Index", "Kayit");
        //}
        [HttpGet]
        public async Task<IActionResult> LogOut()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Index", "Home");
        }
    }
}