using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using OpenSaha.Models;
using OpenSaha.ViewModels.Account;
using X.PagedList;

namespace OpenSaha.Controllers
{
	public class TakimlarController : Controller
	{
		private readonly SahaContext _db;
		public TakimlarController(SahaContext db)
		{
			_db = db;
		}
		public IActionResult Index(int page = 1)
		{
			var urunlerr = _db.Takims.Include(x => x.SehirListe).Include(x => x.Kullanici).Include(x => x.IlceListe).ToList(); 
			int userid = UserIdGetir();
			//if (!string.IsNullOrEmpty(ara))
			//{
			//	urunlerr = urunlerr.Where(x => x.Baslik.Contains(ara)).ToList();
			//}
			return View(urunlerr.ToPagedList(page, 6));			
		}
		[HttpGet]
		[Authorize]
		public IActionResult TakimOlustur(int id)
		{
			ViewBag.Sehir = (from customer in this._db.SehirListes
							 orderby customer.SehirAdi
							 select new SelectListItem
							 {
								 Value = customer.Id.ToString(),
								 Text = customer.SehirAdi
							 }).ToList();
			List<SelectListItem> ilce = new List<SelectListItem>();
			Takim saha = new Takim();
			if (id != 0)
			{
				saha = _db.Takims.Include(x => x.Kullanici).FirstOrDefault(x => x.Id == id);
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
			return View(saha);
		}

		[HttpPost]
		public IActionResult TakimOlustur(Takim t)
		{
			var ktg = _db.SehirListes.Where(x => x.Id == t.SehirListe.Id).FirstOrDefault();
			var ktgi = _db.IlceListes.Where(y => y.Id == t.IlceListe.Id).FirstOrDefault();
			var KId = _db.Takims.FirstOrDefault(x => x.KullaniciId == t.KullaniciId);
			var TakimAd = _db.Takims.FirstOrDefault(x => x.Baslik == t.Baslik);
			t.IlceListe = ktgi;
			t.SehirListe = ktg;
			//if (KId != null)
			//{
			//	return RedirectToAction("kayitol", "kayit");
			//}
			if (TakimAd != null)
			{
				return RedirectToAction("kayitol", "kayit");
			}
			else
			{
				TakimAd.Act = Aktif.Aktif;
				_db.Takims.Add(t);
				_db.SaveChanges();
			}
			return RedirectToAction("Index");
		}
		
		public IActionResult RakipBul(int page = 1)
		{
			var urunlerr = _db.TakimTakvims.Include(x => x.Takim.SehirListe).Include(x => x.Kullanici).Include(x => x.Takim.IlceListe).ToList();
			int userid = UserIdGetir();
			var tkm = _db.Takims.Where(x => x.KullaniciId == userid).Count(); 
			ViewBag.tkm = tkm; 
			//if (!string.IsNullOrEmpty(ara))
			//{
			//	urunlerr = urunlerr.Where(x => x.Baslik.Contains(ara)).ToList();
			//}
			return View(urunlerr.ToPagedList(page, 6)); ;
		}

		public int UserIdGetir()
		{
			var UserEmail = User.Identity.Name;
			var user = _db.Kullanicis.FirstOrDefault(x => x.Email == UserEmail);
			return user.Id;
		}

		public IActionResult Eslesme(int id)
		{
			Eslesmeler eslesmeler = new Eslesmeler();
			int userid = UserIdGetir();
			eslesmeler.TakimTakvim =_db.TakimTakvims.Where(x=>x.KullaniciId == userid).Include(x => x.Takim.SehirListe).Include(x => x.Kullanici).Include(x => x.Takim.IlceListe).FirstOrDefault();
			return View(eslesmeler);
		}
	}
}