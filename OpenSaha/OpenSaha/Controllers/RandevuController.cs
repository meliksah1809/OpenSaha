using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using OpenSaha.Helpers;
using OpenSaha.Models;
using X.PagedList;

namespace OpenSaha.Controllers
{
	public class RandevuController : Controller
	{
		private readonly SahaContext _db;
		public RandevuController(SahaContext db)
		{
			_db = db;
		}
		[HttpGet]
		[Authorize]
		public IActionResult Index()
		{
			List<SelectListItem> degerler = (from s in _db.Sahas.ToList()
											 select new SelectListItem
											 {
												 Text = s.Baslik,
												 Value = s.Id.ToString()
											 }).ToList();
			ViewBag.dgr = degerler;
			int userid = UserIdGetir();
			ViewBag.userid = userid;
			return View();
		}
		[HttpPost]
		public IActionResult Index(Rezervasyon rez)
		{		
			var ktg = _db.Sahas.Where(x => x.Id == rez.Saha.Id).FirstOrDefault();
			rez.Saha = ktg;
			if (!ModelState.IsValid)
			{
				var kontrol = _db.Rezervasyons.Where(x=>x.RandevuBaslangic == rez.RandevuBaslangic && x.Saha.Id == rez.Saha.Id).ToList();
				if (kontrol.Count() > 0)
				{
					//ARKADAŞ SENİN KULLANICI HESABIN VAR GİRİŞ YAP!
				}
				else
				{
					var UserMail = User.Identity.Name;
					var dgr = _db.Kullanicis.FirstOrDefault(x => x.Email == UserMail);
					Rezervasyon kullanici = new Rezervasyon();
					kullanici.SahaId = rez.SahaId;
					kullanici.RandevuBaslangic = rez.RandevuBaslangic;
					kullanici.RandevuBitis = rez.RandevuBitis;
					rez.Act = Aktif.Aktif;
					dgr.Id = rez.KullaniciId;
					_db.Set<Rezervasyon>().Add(rez);
					_db.SaveChanges();
				}
			}
			else
			{
				//GEREKLİ ALANLARI DOLDURUNUZ!
			}
			return RedirectToAction("Index", "Home");
		}
		public int UserIdGetir()
		{
			var UserEmail = User.Identity.Name;
			var user = _db.Kullanicis.FirstOrDefault(x => x.Email == UserEmail);
			return user.Id;
		}
		[Authorize]
		[HttpGet]
		public IActionResult Randevularım(int page = 1)
		{
			var UserMail = User.Identity.Name;
			var dgr = _db.Kullanicis.FirstOrDefault(x => x.Email == UserMail);
			var randevum = _db.Rezervasyons.Include(x=>x.Saha).Include(x=>x.Kullanici).Where(x=>x.KullaniciId==dgr.Id).ToList();
			return View(randevum.ToPagedList(page,5));
		}
	}
}