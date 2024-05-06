using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using OpenSaha.Models;
using OpenSaha.ViewModels.Konrtol;
using System.Security.Cryptography;

namespace OpenSaha.Areas.Kontrol.Controllers
{
	[Area("Kontrol")]
	[Authorize(Policy = "UserYonetici")]
	public class RezervasyonController : Controller
	{
		private readonly SahaContext _db;
		public RezervasyonController(SahaContext db)
		{
			_db = db;
		}
		public IActionResult Index(int Did)
		{
			var usermail = User.Identity.Name;
			var dgr = _db.Kullanicis.FirstOrDefault(x => x.Email == usermail);
			var rzv = _db.Rezervasyons.Include(x => x.Kullanici).Include(x => x.Saha).Include(x => x.EkipmanRezervasyons).Include(x => x.CafeTakips).Where(x => x.YoneticiId == dgr.YoneticiId).OrderByDescending(x => x.RandevuBaslangic).ToList();
			if (Did == 1)
			{
				rzv = _db.Rezervasyons.Include(x => x.Kullanici).Include(x => x.Saha).Include(x => x.EkipmanRezervasyons).Include(x => x.CafeTakips).Where(x => x.YoneticiId == dgr.YoneticiId && x.Durum == Durum.Beklemede).OrderByDescending(x => x.RandevuBaslangic).ToList();
			}
			else if (Did == 2)
			{
				rzv = _db.Rezervasyons.Include(x => x.Kullanici).Include(x => x.Saha).Include(x => x.EkipmanRezervasyons).Include(x => x.CafeTakips).Where(x => x.YoneticiId == dgr.YoneticiId && x.Durum == Durum.Onaylandi).OrderByDescending(x => x.RandevuBaslangic).ToList();
			}
			else if (Did == 3)
			{
				rzv = _db.Rezervasyons.Include(x => x.Kullanici).Include(x => x.Saha).Include(x => x.EkipmanRezervasyons).Include(x => x.CafeTakips).Where(x => x.YoneticiId == dgr.YoneticiId && x.Durum == Durum.Iptal).OrderByDescending(x => x.RandevuBaslangic).ToList();
			}
			List<KontrolRezervasyonViewModel> sonucList = new List<KontrolRezervasyonViewModel>();
			foreach (var item in rzv)
			{
				KontrolRezervasyonViewModel sonuc = new KontrolRezervasyonViewModel();
				double ekipmanUcret = item.EkipmanRezervasyons.Sum(x => x.Ucret);
				double cafeUcret = item.CafeTakips.Sum(x => x.Ucret);
				double ToplamUcret = (ekipmanUcret + cafeUcret + item.Saha.Ucret);
				sonuc.Id = item.Id;
				sonuc.YoneticiId = item.YoneticiId;
				sonuc.SahaId = item.SahaId;
				sonuc.Kullanici = item.Kullanici.Isim + " " + item.Kullanici.Soyisim;
				sonuc.Saha = item.Saha.Baslik;
				sonuc.RandevuBitis = item.RandevuBitis;
				sonuc.RandevuBaslangic = item.RandevuBaslangic;
				sonuc.Durum = item.Durum.ToString();
				sonuc.EkipmanUcret = ekipmanUcret;
				sonuc.CafeUcret = cafeUcret;
				sonuc.SahaUcret = item.Saha.Ucret;
				sonuc.ToplamUcret = ToplamUcret;
				sonucList.Add(sonuc);
			}
			return View(sonucList);
		}

		[HttpGet]
		public IActionResult Form(int id, int sid)
		{
			var sahaid = _db.Sahas.FirstOrDefault(x => x.Id == id);
			var usermail = User.Identity.Name;
			var dgr = _db.Kullanicis.Include(x => x.Sahas).FirstOrDefault(x => x.Email == usermail);
			List<SelectListItem> sha = (from s in _db.Sahas.Where(x => x.Kullanici.Id == dgr.Id).ToList()
										select new SelectListItem
										{
											Text = s.Baslik,
											Value = s.Id.ToString()
										}).ToList();
			ViewBag.sha = sha;
			if (sahaid != null)
			{
				ViewBag.sbaslik = sahaid.Baslik;
				ViewBag.sid = sahaid.Id;
			}
			Rezervasyon rez = new Rezervasyon();
			if (id != 0)
			{
				rez = _db.Rezervasyons.Include(x => x.Kullanici).Include(x => x.Saha).Include(x=>x.Kullanici).FirstOrDefault(x => x.Id == id);
			}
			ViewBag.yid = dgr.YoneticiId;
			return View(rez);
		}

		[HttpPost]
		public IActionResult Form(Rezervasyon r, string email)
		{
			if (r.Id == 0)
			{
				Rezervasyon x = new Rezervasyon();
				if (!_db.Rezervasyons.Any(x => x.RandevuBaslangic == r.RandevuBaslangic))
				{
					var kullanici = _db.Kullanicis.FirstOrDefault(x => x.Email == email);
					if (kullanici != null)
					{
						var usermail = User.Identity.Name;
						var dgr = _db.Kullanicis.FirstOrDefault(x => x.Email == usermail);
						TempData["success"] = "Profil başarıyla güncellendi";
						x.Act = Aktif.Aktif;
						x.SahaId = r.SahaId;
						x.RandevuBaslangic = r.RandevuBaslangic;
						x.RandevuBitis = r.RandevuBitis;
						x.KullaniciId = kullanici.Id;
						x.EslesmeId = r.EslesmeId;
						x.Durum = Durum.Beklemede;
						x.YoneticiId = dgr.YoneticiId ?? 0;
						_db.Set<Rezervasyon>().Add(x);
						_db.SaveChanges();
						string yonlendir = "/kontrol/Rezervasyon/?Did=1";
                        return Redirect(yonlendir);
					}
				}
				else
				{
					TempData["danger"] = "Profil Güncellenemedi";
				}
			}
			else
			{
                var kullanici = _db.Kullanicis.FirstOrDefault(x => x.Email == email);
                var x = _db.Rezervasyons.Include(x => x.Saha).FirstOrDefault(x => x.Id == r.Id);
				x.SahaId = r.SahaId;
				x.RandevuBaslangic = r.RandevuBaslangic;
				x.RandevuBitis = r.RandevuBitis;
				x.KullaniciId = kullanici.Id;
				x.EslesmeId = r.EslesmeId;
				_db.Set<Rezervasyon>().Update(x);
				_db.SaveChanges();
				string yonlendir = "/kontrol/Rezervasyon/?Did=1";
				return Redirect(yonlendir);
			}
			ViewBag.ErrorMessage = TempData["ErrorMessage"];
			return View();
		}

		[HttpGet]
		public IActionResult Odeme(int id)
		{
			var usermail = User.Identity.Name;
			var dgr = _db.Kullanicis.FirstOrDefault(x => x.Email == usermail);
			var kid = _db.Kullanicis.FirstOrDefault(x => x.YoneticiId == dgr.YoneticiId);
			var gd = _db.Rezervasyons.Include(x => x.Kullanici).FirstOrDefault(x => x.Id == id);
			double ekipmanUcret = _db.EkipmanRezervasyons.Where(x => x.RezervasyonId == id).Sum(x => x.Ucret);
			double cafeUcret = _db.CafeTakips.Where(x => x.RezervasyonId == id).Sum(x => x.Ucret);
			double sahaUcret = _db.Rezervasyons.Where(x => x.Id == id).Sum(x => x.Saha.Ucret);
			double ToplamUcret = (ekipmanUcret + cafeUcret + sahaUcret);
			ViewBag.toplam = ToplamUcret;
			KontrolOdemeViewModel ode = new KontrolOdemeViewModel();
			Odeme o = new Odeme();
			ode.SahaUcret = sahaUcret;
			ode.KafeUcret = cafeUcret;
			ode.EkipmanUcret = ekipmanUcret;
			ode.RezervasyonId = id;
			ode.OdemeTipleri = o.OdemeTipleri;
			ode.SahaId = gd.SahaId;
			ode.Kullanici = gd.Kullanici.Isim + " " + gd.Kullanici.Soyisim;
			ode.YoneticiId = kid.YoneticiId.GetValueOrDefault();
			return View(ode);
		}

		[HttpPost]
		public IActionResult Odeme(Odeme request, int id)
		{
			Odeme x = new Odeme();
			x.SahaUcret = request.SahaUcret;
			x.KafeUcret = request.KafeUcret;
			x.EkipmanUcret = request.EkipmanUcret;
			x.RezervasyonId = request.RezervasyonId;
			x.OdemeTipleri = request.OdemeTipleri;
			x.SahaId = request.SahaId;
			x.YoneticiId = request.YoneticiId.GetValueOrDefault();
			x.Act = Aktif.Aktif;
			_db.Set<Odeme>().Add(x);
			_db.SaveChanges();
			//var odta = DateTime.Now.ToString();
			//HttpContext.Session.SetString("odta", odta);
			var onay = new Rezervasyon();
			onay = _db.Rezervasyons.FirstOrDefault(x => x.Id == request.RezervasyonId);
			if (onay != null)
			{
				onay.Durum = Durum.Onaylandi;
			};
			_db.Set<Rezervasyon>().Update(onay);
			_db.SaveChanges();
			var puan = new Kullanici();
			puan = _db.Kullanicis.FirstOrDefault(x => x.Id == onay.KullaniciId);
			if (onay.Durum == Durum.Onaylandi)
			{
				puan.Puan = puan.Puan + 5;
			}
			_db.Set<Kullanici>().Update(puan);
			_db.SaveChanges();
			string yonlen = "/kontrol/Rezervasyon";
			return Redirect(yonlen);
		}

		public IActionResult Tesxt(int id)
		{
			Test test = new Test();
			test.CafeTakip = new List<CafeTakibi>();
			test.EkipmanTakip = new List<EkipmanTakibi>();
			var cafe = _db.Odemes.Include(x => x.Saha).Where(x => x.RezervasyonId == id);
			var ekipman = _db.Odemes.Include(x => x.Saha).Where(x => x.RezervasyonId == id);
			foreach (var item in cafe)
			{
				CafeTakibi cafesonuc = new CafeTakibi();
				test.CafeTakip.Add(cafesonuc);
			}
			foreach (var item in ekipman)
			{
				EkipmanTakibi ekipmansonuc = new EkipmanTakibi();
				test.EkipmanTakip.Add(ekipmansonuc);
			}
			return View(test);
		}

		[HttpGet]
		public IActionResult EkipmanRez(int id, int yid, int sid)
		{
			List<SelectListItem> degerler = (from s in _db.Ekipmanlars.Where(x => x.SahaId == sid).ToList()
											 select new SelectListItem
											 {
												 Text = s.Baslik,
												 Value = s.Id.ToString()
											 }).ToList();
			ViewBag.yid = yid;
			ViewBag.ekp = degerler;
			ViewBag.rezid = id;
			return View();
		}

		[HttpPost]
		public IActionResult EkipmanRez(EkipmanRezervasyon request)
		{
			var ekipman = _db.Ekipmanlars.FirstOrDefault(x => x.Id == request.EkipmanId);
			if (request.Adet != 0)
			{
				if (request.Id == 0)
				{
					request.Ucret = (ekipman.Ucret * request.Adet);
					request.Act = Aktif.Aktif;
					_db.Set<EkipmanRezervasyon>().Add(request);
					_db.SaveChanges();
				}
				//var ekipman = _db.Ekipmanlars.FirstOrDefault(x => x.Id == er.Id);
				//if(ekipman != null)
				//{
				//    var ucret = ekipman.Ucret;
				//    ViewBag.ucret = ucret;
				//}
				//if (er.Id == 0)
				//{
				//    er.Act = Aktif.Aktif;
				//    _db.Set<EkipmanRezervasyon>().Add(er);
				//    _db.SaveChanges();
				//}
			}
			string yonlendir = "/kontrol/Rezervasyon/?Did=1";
			return Redirect(yonlendir);
		}

		public IActionResult EkipmanDuzenle(int id)
		{
			var ekprez = _db.EkipmanRezervasyons.Include(x => x.Ekipmanlar).Where(x => x.RezervasyonId == id).OrderByDescending(x => x.Id).ToList();
			ViewBag.ekprez = ekprez;
			return View(ekprez);
		}

		[HttpGet]
		public IActionResult EkipmanSil()
		{
			string yonlen = "/kontrol/Rezervasyon";
			return Redirect(yonlen);
		}

		[HttpPost]
		public IActionResult EkipmanSil(Rezervasyon ekp)
		{
			string yonlen = "/kontrol/Rezervasyon";
			return Redirect(yonlen);
		}

		public IActionResult CafeDuzenle(int id)
		{
			var ekprez = _db.CafeTakips.Include(x => x.Cafe).Where(x => x.RezervasyonId == id && x.Act == Aktif.Aktif).OrderByDescending(x => x.Id).ToList();
			ViewBag.ekprez = ekprez;
			return View(ekprez);
		}

        public IActionResult CafeSil(int id)
        {
            var x = _db.CafeTakips.FirstOrDefault(x => x.Id == id && x.Act != Aktif.Silinmis);
            if (x != null)
            {
                x.Act = Aktif.Silinmis;
                _db.Set<CafeTakip>().Update(x);
                _db.SaveChanges();
            }
            string yonlen = "/kontrol/Rezervasyon/?Did=1";
            return Redirect(yonlen);
        }

        [HttpGet]
		public IActionResult CafeTakip(int id, int yid, int sid)
		{
			List<SelectListItem> degerler = (from s in _db.Cafes.Where(x => x.SahaId == sid).ToList()
											 select new SelectListItem
											 {
												 Text = s.Baslik,
												 Value = s.Id.ToString()
											 }).ToList();
			ViewBag.yid = yid;
			ViewBag.ekp = degerler;
			ViewBag.rezid = id;
			return View();
		}

		[HttpPost]
		public IActionResult CafeTakip(CafeTakip request)
		{
			var cafe = _db.Cafes.FirstOrDefault(x => x.Id == request.CafeId);
			if (request.Adet != 0)
			{
				if (request.Id == 0)
				{
					request.Ucret = (cafe.Fiyat * request.Adet);
					request.Act = Aktif.Aktif;
					_db.Set<CafeTakip>().Add(request);
					_db.SaveChanges();
					var stoktkp = new Cafe();
					stoktkp = _db.Cafes.FirstOrDefault(x => x.Id == request.CafeId);
					if (stoktkp != null)
					{
						stoktkp.Adet = stoktkp.Adet - request.Adet;
					};
					_db.Set<Cafe>().Update(stoktkp);
					_db.SaveChanges();
					var stoktakip = new StokTakip();
					stoktakip.CafeId = request.CafeId;
					stoktakip.Adet = request.Adet;
					stoktakip.Islem = Islem.Cikti;
					stoktakip.BirimFiyat = cafe.Fiyat;
					stoktakip.Barkod = cafe.Barkod;
					stoktakip.YoneticiId = cafe.YoneticiId;
					stoktakip.Act = Aktif.Aktif;
					stoktakip.Tarih = DateTime.Now;
					_db.Set<StokTakip>().Add(stoktakip);
					_db.SaveChanges();
				}
				else
				{
					var x = _db.CafeTakips.FirstOrDefault(x => x.Id == request.Id);
					x.Ucret = (cafe.Fiyat * request.Adet);
					x.Adet = request.Adet;
					x.CafeId = request.CafeId;
					_db.Set<CafeTakip>().Update(x);
					_db.SaveChanges();
				}
			}
			string yonlen = "/kontrol/Rezervasyon/?Did=1";
			return Redirect(yonlen);
		}

		[HttpGet]
		public IActionResult Sil(int id)
		{
			var usermail = User.Identity.Name;
			var dgr = _db.Kullanicis.FirstOrDefault(x => x.Email == usermail);
			var kid = _db.Kullanicis.FirstOrDefault(x => x.YoneticiId == dgr.YoneticiId);
			var gd = _db.Rezervasyons.Include(x => x.Kullanici).FirstOrDefault(x => x.Id == id);
			double ekipmanUcret = _db.EkipmanRezervasyons.Where(x => x.RezervasyonId == id).Sum(x => x.Ucret);
			double cafeUcret = _db.CafeTakips.Where(x => x.RezervasyonId == id).Sum(x => x.Ucret);
			double sahaUcret = _db.Rezervasyons.Where(x => x.Id == id).Sum(x => x.Saha.Ucret);
			double ToplamUcret = (ekipmanUcret + cafeUcret + sahaUcret);
			KontrolOdemeViewModel ode = new KontrolOdemeViewModel();
			Odeme o = new Odeme();
			ode.SahaUcret = sahaUcret;
			ode.KafeUcret = cafeUcret;
			ode.EkipmanUcret = ekipmanUcret;
			ode.RezervasyonId = id;
			ode.OdemeTipleri = o.OdemeTipleri;
			ode.SahaId = gd.SahaId;
			ode.Kullanici = gd.Kullanici.Isim + " " + gd.Kullanici.Soyisim;
			ode.YoneticiId = kid.YoneticiId.GetValueOrDefault();
			return View(ode);
		}

		[HttpPost]
		public IActionResult Sil(Odeme id)
		{
			var red = new Rezervasyon();
			red = _db.Rezervasyons.FirstOrDefault(x => x.Id == id.RezervasyonId);
			if (red != null)
			{
				red.Durum = Durum.Iptal;
				_db.Set<Rezervasyon>().Update(red);
				_db.SaveChanges();
			};
			string yonlen = "/kontrol/Rezervasyon";
			return Redirect(yonlen);
		}
	}
}