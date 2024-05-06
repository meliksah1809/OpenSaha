using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using OpenSaha.Models;
using Pomelo.EntityFrameworkCore.MySql.Query.Internal;

namespace OpenSaha.Areas.Kontrol.Controllers
{
	[Area("Kontrol")]
	[Authorize(Policy = "UserYonetici")]
	public class SahalarController : Controller
	{
		private readonly SahaContext _db;
		public SahalarController(SahaContext db)
		{
			_db = db;
		}
		public int UserIdGetir()
		{
			var UserEmail = User.Identity.Name;
			var userid = _db.Kullanicis.FirstOrDefault(x => x.Email == UserEmail);
			return userid.Id;
		}

		public IActionResult Index(int id)
		{
			int userid = UserIdGetir();
			var saha = _db.Sahas.Include(x => x.SehirListe).Include(x => x.IlceListe).Where(x => x.KullaniciId == userid).OrderByDescending(x => x.Id).ToList();
			return View(saha);
		}

		[HttpGet]
		public IActionResult form(int id = 0)
		{
			ViewBag.Sehir = (from customer in this._db.SehirListes
							 orderby customer.SehirAdi
							 select new SelectListItem
							 {
								 Value = customer.Id.ToString(),
								 Text = customer.SehirAdi
							 }).ToList();
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
			ViewBag.Ilce = ilce;
			var UserEmail = User.Identity.Name;
			var userid = _db.Kullanicis.FirstOrDefault(x => x.Email == UserEmail);
			var kid = _db.Kullanicis.FirstOrDefault(x => x.Id == userid.Id);
			ViewBag.kid = kid.Id;
			ViewBag.yid = kid.YoneticiId;
			return View(saha);
		}

		[HttpPost]
		public IActionResult form(Saha p)
		{
			if (p.Id == 0)
			{
				p.Act = Aktif.Aktif;
				_db.Set<Saha>().Add(p);
				_db.SaveChanges();
			}
			else
			{
				var x = _db.Sahas.FirstOrDefault(x => x.Id == p.Id);
				x.Baslik = p.Baslik;
				x.Ucret = p.Ucret;
				x.Ozellik = p.Ozellik;
				x.Aciklama = p.Aciklama;
				x.SehirId = p.SehirId;
				x.SahaTipi = p.SahaTipi;
				x.IlceId = p.IlceId;
				x.AcilisSaat = p.AcilisSaat;
				x.KapanisSaat = p.KapanisSaat;
				x.YirmiDortSaat = p.YirmiDortSaat;
				_db.Set<Saha>().Update(x);
				_db.SaveChanges();
			}
			string yonlendir = "/kontrol/sahalar";
			return Redirect(yonlendir);
		}

		public IActionResult Cafe(int id)
		{
			var cfe = _db.Cafes.Where(x => x.SahaId == id);
			var ca = _db.Sahas.FirstOrDefault(x => x.Id == id);
			ViewBag.ca = ca.Baslik;
			return View(cfe.OrderByDescending(x => x.Id).ToList());
		}

		public IActionResult Ekipmanlar(int id)
		{
			var ekp = _db.Ekipmanlars.Include(x => x.Saha).Where(x => x.SahaId == id);
			var ek = _db.Sahas.FirstOrDefault(x => x.Id == id);
			ViewBag.ek = ek.Baslik;
			return View(ekp.ToList());
		}

		public IActionResult Rezervasyon(int id)
		{
			var rzv = _db.Rezervasyons.Include(x => x.Saha).Include(x => x.Kullanici).Where(x => x.SahaId == id);
			var sa = _db.Sahas.FirstOrDefault(x => x.Id == id);
			ViewBag.sa = sa.Baslik;
			ViewBag.id = sa.Id;
			return View(rzv.ToList());
		}

		public IActionResult Stoklar(int id)
		{
			var stk = _db.Cafes.Where(x => x.SahaId == id);
			return View(stk.ToList());
		}

		public IActionResult SahaResim()
		{
			return View();
		}

		public IActionResult deneme()
		{
			return View();
		}
	}
}