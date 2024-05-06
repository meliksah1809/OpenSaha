using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using OpenSaha.Helpers;
using OpenSaha.Models;
using OpenSaha.ViewModels.Account;

namespace OpenSaha.Areas.Admin.Controllers
{
	[Area("Admin")]
	[Authorize(Policy = "Yonetici")]
	public class KullaniciController : Controller
	{
		private readonly SahaContext _db;
		public KullaniciController(SahaContext db)
		{
			_db = db;
		}

		public IActionResult Index()
		{
			var kullanici = _db.Kullanicis.ToList();
			return View(kullanici);
		}

		[HttpGet]
		public IActionResult Form()
		{
			return View();
		}

		[HttpPost]
		public IActionResult Form(RegisterViewModel request)
		{
			if (ModelState.IsValid)
			{
				string telefon = Helper.TelefonNo(request.Telefon);
				var kontrol = _db.Kullanicis.Where(x => x.Email == request.Email || x.Telefon == telefon).ToList();
				if (kontrol.Count() > 0)
				{
					//kullanici var
				}
				else
				{
					YonetimTablosu yonetim = new YonetimTablosu();
					Kullanici kullanici = new Kullanici();
					if (request.UserType == UserType.Kullanici)
					{
						kullanici.Act = Aktif.Aktif;
						//kullanici.UserType = request.UserType;    
						kullanici.Email = request.Email;
						kullanici.Isim = request.Isim;
						kullanici.Soyisim = request.Soyisim;
						kullanici.Telefon = telefon;
						kullanici.UserType = request.UserType;
						kullanici.Password = Security.PasswordSifrele(request.Password);
						_db.Set<Kullanici>().Add(kullanici);
						_db.SaveChanges();
					}
					else
					{
						yonetim.YoneticiSayisi = request.YoneticiSayisi;
						yonetim.SahaSayisi = request.SahaSayisi;
						yonetim.Act = Aktif.Aktif;
						_db.Set<YonetimTablosu>().Add(yonetim);
						_db.SaveChanges();

						kullanici.YoneticiId = yonetim.Id;
						kullanici.Act = Aktif.Aktif;
						//kullanici.UserType = request.UserType;    
						kullanici.Email = request.Email;
						kullanici.Isim = request.Isim;
						kullanici.Soyisim = request.Soyisim;
						kullanici.Telefon = telefon;
						kullanici.UserType = request.UserType;
						kullanici.Password = Security.PasswordSifrele(request.Password);
						_db.Set<Kullanici>().Add(kullanici);
						_db.SaveChanges();
					}
				}
			}
			else
			{
				//GEREKLİ ALANLARI DOLDURUNUZ!
			}
			string yon = "/Admin/kullanici";
			return Redirect(yon);
		}

		[HttpGet]
		public IActionResult KullaniciGuncelle(int id)
		{
			var kull = _db.Kullanicis.FirstOrDefault(x => x.Id == id);
			return View(kull);
		}
		[HttpPost]
		public IActionResult KullaniciGuncelle(Kullanici kul)
		{
			var dgr = _db.Kullanicis.FirstOrDefault(x => x.Id == kul.Id);
			//dgr.UserType = kul.UserType;    
			dgr.Isim = kul.Isim;
			dgr.Soyisim = kul.Soyisim;
			dgr.Telefon = kul.Telefon;
			dgr.Email = kul.Email;
			dgr.YoneticiId = kul.YoneticiId;
			dgr.Act = kul.Act;

			_db.Set<Kullanici>().Update(dgr);
			_db.SaveChanges();
			string yon = "/Admin/kullanici";
			return Redirect(yon);
		}

		public IActionResult Takimlar(int id)
		{
			var takimlar = _db.Takims.Include(x => x.Kullanici).Include(x => x.SehirListe).Include(x => x.IlceListe).Where(x => x.KullaniciId == id);
			return View(takimlar.ToList());
		}

		public IActionResult Puan(int id)
		{
			var puan = _db.KullaniciPuanIslemleris.Where(x => x.KullaniciId == id);
			return View(puan.ToList());
		}

		[HttpGet]
		public IActionResult TakimGuncelle(int id)
		{
			var takim = _db.Takims.Include(x => x.SehirListe).Include(x => x.IlceListe).Include(x => x.Kullanici).FirstOrDefault(x => x.Id == id);
			ViewBag.Sehir = (from customer in this._db.SehirListes
							 orderby customer.SehirAdi
							 select new SelectListItem
							 {
								 Value = customer.Id.ToString(),
								 Text = customer.SehirAdi
							 }).ToList();
			List<SelectListItem> degerler = (from s in _db.Kullanicis.ToList()
											 select new SelectListItem
											 {
												 Text = s.Isim + " " + s.Soyisim,
												 Value = s.Id.ToString()
											 }).ToList();
			ViewBag.dgr = degerler;
			List<SelectListItem> degerlerr = (from s in _db.YonetimTablosus.ToList()
											  select new SelectListItem
											  {
												  Text = s.Id + "",
												  Value = s.Id.ToString()
											  }).ToList();
			ViewBag.dgrr = degerlerr;

			List<SelectListItem> ilce = new List<SelectListItem>();
			Saha saha = new Saha();
			if (id != 0)
			{
				saha = _db.Sahas.Include(x => x.Kullanici).FirstOrDefault(x => x.Id == id);
				ilce = (from customer in this._db.IlceListes
						where customer.SehirId == Convert.ToInt32(saha.SehirId)
						orderby customer.IlceAdi
						select new SelectListItem
						{
							Value = customer.Id.ToString(),
							Text = customer.IlceAdi
						}).ToList();
			}
			int userid = UserIdGetir();
			ViewBag.userid = userid;
			ViewBag.Ilce = ilce;
			return View(takim);
		}

		[HttpPost]
		public IActionResult TakimGuncelle(Takim kul)
		{
			if (kul.Id != 0)
			{
				var x = _db.Takims.FirstOrDefault(x => x.Id == kul.Id);
				x.Act = Aktif.Aktif;
				x.Baslik = kul.Baslik;
				x.Kadro = kul.Kadro;
				x.SehirId = kul.SehirId;
				x.IlceId = kul.IlceId;

				_db.Set<Takim>().Update(x);
				_db.SaveChanges();
			}
			string yon = "/Admin/kullanici";
			return Redirect(yon);
		}

		public int UserIdGetir()
		{
			var UserEmail = User.Identity.Name;
			var user = _db.Kullanicis.FirstOrDefault(x => x.Email == UserEmail);
			return user.Id;

		}
	}
}
