using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using OpenSaha.Models;
using OpenSaha.ViewModels.Account;
using System.Collections.Generic;
using X.PagedList;
namespace OpenSaha.Controllers
{
	public class SahalarController : Controller
	{
		private readonly SahaContext _db;
		public SahalarController(SahaContext db)
		{
			_db = db;
		}
		[HttpGet]
		public IActionResult Index(int page = 1, int id = 0) 
		{
			var sahaid = _db.Sahas.FirstOrDefault(x => x.Id == id);
			var urunlerr = _db.Sahas.Include(x => x.SehirListe).Include(x => x.IlceListe).Include(x => x.Kullanici).ToList();
			List<SelectListItem> degerler = (from s in _db.SehirListes.ToList()
											 select new SelectListItem
											 {
												 Text = s.SehirAdi,
												 Value = s.Id.ToString()
											 }).ToList();
			ViewBag.sehir = degerler;
			List<SelectListItem> ilce = (from i in _db.IlceListes.ToList()
										 select new SelectListItem
										 {
											 Text = i.IlceAdi,
											 Value = i.Id.ToString(),
										 }).ToList();
			ViewBag.ilce = ilce;
			//giriş kontrol
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
			var randevum = _db.Rezervasyons.Include(x => x.Saha).Include(x => x.Kullanici).ToList();
			return View(urunlerr.ToPagedList(page, 6));
		}
		[HttpGet]
		public IActionResult Rezervasyonlar(int id)
		{
            var UserEmail = User.Identity.Name;
            var user = _db.Kullanicis.FirstOrDefault(x => x.Email == UserEmail);
			var sha = _db.Sahas.FirstOrDefault(x => x.Id == id);
			if (sha == null)
				return RedirectToAction("Index", "Home");

			var kid = _db.Kullanicis.FirstOrDefault(x => x.Id == user.Id);
			ViewBag.kid = kid.Id;
			var yid = _db.Kullanicis.FirstOrDefault(x => x.YoneticiId == user.YoneticiId);
			ViewBag.yid = yid.YoneticiId;
			ViewBag.sha = sha.Id;
			ViewBag.baslik = sha.Baslik;
			
			return View();
		}
		[HttpPost]
		public IActionResult Rezervasyonlar(Rezervasyon r, int id)
		{
			Rezervasyon x = new Rezervasyon();	
			x.SahaId = id;
			x.KullaniciId = r.KullaniciId;
			x.RandevuBaslangic =r.RandevuBaslangic;
			x.RandevuBitis = r.RandevuBitis;
			x.EslesmeId = r.EslesmeId;
			x.Durum = Durum.Beklemede;
			x.YoneticiId = r.YoneticiId;
			string yon = "/profil/randevular";
			_db.Set<Rezervasyon>().Add(x);
			_db.SaveChanges();
			return Redirect(yon);
		}
	}
}