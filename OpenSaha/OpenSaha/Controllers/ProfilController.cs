using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using OpenSaha.Helpers;
using OpenSaha.Models;
using OpenSaha.ViewModels.Account;
using System.Reflection.Metadata;
using X.PagedList;

namespace OpenSaha.Controllers
{
	public class ProfilController : Controller
	{
		private readonly SahaContext _db;
		public ProfilController(SahaContext db)
		{
			_db = db;
		}

		public IActionResult Index()
		{
			ProfilAnasayfa prf = new ProfilAnasayfa();
			var UserMail = User.Identity.Name;
			prf.Kullanici = _db.Kullanicis.FirstOrDefault(x => x.Email == UserMail);
			prf.Rezervasyon = _db.Rezervasyons.Include(x => x.Kullanici).Include(x => x.Saha).Where(x => x.KullaniciId == prf.Kullanici.Id).OrderByDescending(x => x.RandevuBaslangic).Take(5).ToList();
			return View(prf);
		}
		public IActionResult Edit()
		{
			var UserMail = User.Identity.Name;
			var dgr = _db.Kullanicis.FirstOrDefault(x => x.Email == UserMail);
			return View(dgr);
		}
		[HttpPost]
		public IActionResult Edit(Kullanici request)
		{
			var UserMail = User.Identity.Name;
			var dgr = _db.Kullanicis.FirstOrDefault(x => x.Email == UserMail);
			dgr.Isim = request.Isim;
			dgr.Soyisim = request.Soyisim;
			dgr.Email = request.Email;
			dgr.Telefon = request.Telefon;
			dgr.DogumTarihi = request.DogumTarihi;
			_db.Set<Kullanici>().Update(dgr);
			_db.SaveChanges();
			return RedirectToAction("Index");
		}
		public IActionResult Sifre()
		{
			var UserMail = User.Identity.Name;
			var dgr = _db.Kullanicis.FirstOrDefault(x => x.Email == UserMail);
			return View();
		}
		[HttpPost]
		public IActionResult Sifre(SifreDegistir sifre)
		{
			var UserMail = User.Identity.Name;
			var dgr = _db.Kullanicis.FirstOrDefault(x => x.Email == UserMail);
			if (dgr.Password == sifre.OldPassword)
			{
				string yeniSifre = Security.PasswordSifrele(sifre.PasswordNew);
				dgr.Password = yeniSifre;
				_db.Set<Kullanici>().Update(dgr);
				_db.SaveChanges();
			}
			else
			{
				//Eski şifre hatalı girildi!
			}
			return RedirectToAction("Index");
		}
		public IActionResult Update(Kullanici d)
		{
			var kul = _db.Kullanicis.Find(d.Id);
			kul.Isim = d.Isim;
			_db.SaveChanges();
			return RedirectToAction("Index");
		}

		public IActionResult Randevular(int page = 1)
		{
			var UserMail = User.Identity.Name;
			var dgr = _db.Kullanicis.FirstOrDefault(x => x.Email == UserMail);
			var randevum = _db.Rezervasyons.Include(x => x.Saha).Include(x => x.Kullanici).Where(x => x.KullaniciId == dgr.Id && x.Durum == Durum.Beklemede && x.Act == Aktif.Aktif).OrderByDescending(x => x.RandevuBaslangic).ToList();
			return View(randevum.ToPagedList(page, 5));
		}
		[HttpGet]
		public IActionResult TakimOlustur()
		{
			List<SelectListItem> sehir = (from s in _db.SehirListes.ToList()
										  select new SelectListItem
										  {
											  Text = s.SehirAdi,
											  Value = s.Id.ToString()
										  }).ToList();
			List<SelectListItem> ilce = (from i in _db.IlceListes.ToList()
										 select new SelectListItem
										 {
											 Text = i.IlceAdi,
											 Value = i.Id.ToString(),
										 }).ToList();
			ViewBag.ilce = ilce;
			ViewBag.Sehir = sehir;
			return View();
		}
		[HttpPost]
		public IActionResult TakimOlustur(Takim t)
		{
			bool hatavar = false;
			if (!String.IsNullOrEmpty(t.Baslik))
			{
				var kontrol = _db.Takims.FirstOrDefault(x => x.Baslik.Contains(t.Baslik));
				if (kontrol == null)
				{
					if (t.Id == 0)
					{
						hatavar = false;
						var UserMail = User.Identity.Name;
						var dgr = _db.Kullanicis.FirstOrDefault(x => x.Email == UserMail);
						t.KullaniciId = dgr.Id;
						t.Act = Aktif.Aktif;
						_db.Set<Takim>().Add(t);
						_db.SaveChanges();
					}
					else
					{
						hatavar = true;
						var ekip = _db.Takims.FirstOrDefault(x => x.Id == t.Id);
						ekip.KullaniciId = t.KullaniciId;
						ekip.Baslik = t.Baslik;
						ekip.SehirId = t.SehirId;
						ekip.IlceId = t.IlceId;
						ekip.Kadro = t.Kadro;
						_db.Set<Takim>().Update(ekip);
						_db.SaveChanges();
					}
				}
			}
			ViewBag.hatavar = hatavar;
			return RedirectToAction("Index");
		}

		[HttpGet]
		public IActionResult MusaitGunler()
		{
			var UserMail = User.Identity.Name;
			var dgr = _db.Kullanicis.FirstOrDefault(x => x.Email == UserMail);
			List<SelectListItem> takim = (from s in _db.Takims.Where(x => x.KullaniciId == dgr.Id).ToList()
										  select new SelectListItem
										  {
											  Text = s.Baslik,
											  Value = s.Id.ToString()
										  }).ToList();	
			List<SelectListItem> saha = (from s in _db.Sahas.Where(x => x.KullaniciId == dgr.Id).ToList()
										  select new SelectListItem
										  {
											  Text = s.Baslik,
											  Value = s.Id.ToString()
										  }).ToList();
			ViewBag.saha = saha;
			ViewBag.Takim = takim;
			ViewBag.userid = dgr.Id;
			return View();
		}
		[HttpPost]
		public IActionResult MusaitGunler(TakimTakvim z)
		{
			var tkm = _db.Takims.Where(x => x.Id == z.Id).FirstOrDefault();
			z.Takim = tkm;
			var kontrol = _db.Takims.Where(x => x.Id == z.KullaniciId).ToList();
			if (!ModelState.IsValid)
			{
				var UserMail = User.Identity.Name;
				var dgr = _db.Kullanicis.FirstOrDefault(x => x.Email == UserMail);
				TakimTakvim takim = new TakimTakvim();
				z.Act = Aktif.Aktif;
				takim.SahaId = z.SahaId;
				takim.TarihBaslangic = z.TarihBaslangic;
				takim.TarihBitis = z.TarihBitis;
				takim.TakimId = z.TakimId;
				dgr.Id = z.KullaniciId;
				_db.Set<TakimTakvim>().Add(z);
				_db.SaveChanges();
			}
			else
			{
			}
			return RedirectToAction("Index");
		}
		public IActionResult Takimlarim()
		{
			var UserMail = User.Identity.Name;
			var dgr = _db.Kullanicis.FirstOrDefault(x => x.Email == UserMail);
			var takim = _db.Takims.Where(x => x.KullaniciId == dgr.Id).Include(x => x.Kullanici).Include(x => x.SehirListe).Include(x => x.IlceListe).ToList();
			return View(takim);
		}
		[HttpGet]
		public IActionResult TakimGuncelle(int id)
		{
			List<SelectListItem> sehir = (from s in _db.SehirListes.ToList()
										  select new SelectListItem
										  {
											  Text = s.SehirAdi,
											  Value = s.Id.ToString()
										  }).ToList();
			ViewBag.Sehir = sehir;
			List<SelectListItem> ilce = (from i in _db.IlceListes.ToList()
										 select new SelectListItem
										 {
											 Text = i.IlceAdi,
											 Value = i.Id.ToString(),
										 }).ToList();
			ViewBag.ilce = ilce;
			ViewBag.id = id;
			int userid = UserIdGetir();
			ViewBag.userid = userid;
			var dgr = _db.Takims.FirstOrDefault(x => x.Id == id);
			return View(dgr);
		}
		[HttpPost]
		public IActionResult TakimGuncelle(Takim g, int id)
		{
			var UserMail = User.Identity.Name;
			var takm = _db.Takims.FirstOrDefault(x => x.Kullanici.Email == UserMail);
			takm.SehirId = g.SehirId;
			takm.IlceId = g.IlceId;
			takm.Kadro = g.Kadro;
			takm.Baslik = g.Baslik;
			takm.Act = Aktif.Aktif;
			_db.Set<Takim>().Update(takm);
			_db.SaveChanges();
			return View(takm);
		}
		public IActionResult MusaitGunlerim(int page = 1)
		{
			int userid = UserIdGetir();
			var urunlerr = _db.TakimTakvims.Where(x => x.KullaniciId == userid && x.Act == Aktif.Aktif).Include(x => x.Takim.SehirListe).Include(x => x.Kullanici).Include(x => x.Takim.IlceListe).ToList();
			//if (!string.IsNullOrEmpty(ara))
			//{
			//	urunlerr = urunlerr.Where(x => x.Baslik.Contains(ara)).ToList();
			//}
			return View(urunlerr.ToPagedList(page, 6));
		}
		public IActionResult MusaitSil(int id)
		{
            var z = _db.TakimTakvims.FirstOrDefault(x => x.Id == id && x.Act != Aktif.Silinmis);
            if (z != null)
            {
                z.Act = Aktif.Silinmis;
                _db.Set<TakimTakvim>().Update(z);
                _db.SaveChanges();
            }
            return RedirectToAction("randevular");
        }
		public int UserIdGetir()
		{
			var UserEmail = User.Identity.Name;
			var user = _db.Kullanicis.FirstOrDefault(x => x.Email == UserEmail);
			return user.Id;
		}
		public IActionResult RezervasyonSil(int id)
		{
			var x = _db.Rezervasyons.FirstOrDefault(x => x.Id == id && x.Act != Aktif.Silinmis);
			if (x != null)
			{
				x.Durum = Durum.Iptal;
				x.Act= Aktif.Silinmis;
				_db.Set<Rezervasyon>().Update(x);
				_db.SaveChanges();
			}
			return RedirectToAction("randevular");
		}
	}
}