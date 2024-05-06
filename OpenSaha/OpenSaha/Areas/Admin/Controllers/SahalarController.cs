using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using OpenSaha.Models;

namespace OpenSaha.Areas.Admin.Controllers
{
    [Authorize(Policy = "Yonetici")]
    [Area("Admin")]
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
            var user = _db.Kullanicis.FirstOrDefault(x => x.Email == UserEmail);
            return user.Id;
        }

        public IActionResult Index()
		{
			var saha = _db.Sahas.Where(x => (x.Act == Aktif.Aktif)).Include(x => x.SehirListe).Include(x => x.IlceListe).OrderByDescending(x => x.Id).ToList();
			return View(saha);
		}

		[HttpGet]
		public IActionResult Form(int id)
		{
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
                                                 Text = s.Isim +" "+ s.Soyisim,
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
			return View(saha);
		}

		[HttpPost]
		public IActionResult Form(Saha p, int id)
		{
            if (p.Id == 0)
			{
				Saha x = new Saha();
                x.Act = Aktif.Aktif;
                x.Baslik = p.Baslik;
				x.Aciklama = p.Aciklama;
                x.Ozellik = p.Ozellik;
                x.KullaniciId = p.KullaniciId;
                x.AcilisSaat = p.AcilisSaat; 
                x.KapanisSaat = p.KapanisSaat;
                x.YirmiDortSaat = p.YirmiDortSaat;
                x.Ucret = p.Ucret;
				x.SehirId = p.SehirId;
				x.IlceId = p.IlceId;
                x.SahaTipi = p.SahaTipi;
                x.YoneticiId = p.YoneticiId;

                _db.Set<Saha>().Add(x);
				_db.SaveChanges();
			}
			else
			{
				var x = _db.Sahas.FirstOrDefault(x => x.Id == id);
                p.Act = Aktif.Aktif;
                x.Baslik = p.Baslik;
                x.Ozellik = p.Ozellik;
				x.Aciklama = p.Aciklama;
				x.SehirId = p.SehirId;
				x.IlceId = p.IlceId;
                x.KullaniciId = p.KullaniciId;
                x.AcilisSaat = p.AcilisSaat;
                x.KapanisSaat = p.KapanisSaat;
                x.YirmiDortSaat = p.YirmiDortSaat;
                x.Ucret = p.Ucret;
                x.SahaTipi = p.SahaTipi;
                x.YoneticiId = p.YoneticiId;

                _db.Set<Saha>().Update(x);
				_db.SaveChanges();
			}
            string yon = "/Admin/Sahalar";
            return Redirect(yon);
        }

		[HttpGet]
		public IActionResult Rezervasyon(int code, int id = 0)
		{
			var saha = _db.Rezervasyons.Include(x => x.Saha).Include(x => x.Kullanici).Where(x => x.SahaId == id);
			//if (sahaid != 0)
			//{
			//	saha = saha.Where(x => x.YoneticiId == code);
			//}
			return View(saha.ToList());
		}

		public IActionResult Stoklar(int code, int id = 0)
		{
			var stok = _db.Cafes.Where(x => x.YoneticiId == code);
			if (id != 0)
			{
				stok = stok.Where(x => x.SahaId == id);

			}

			return View(stok.ToList());
		}

		public IActionResult Cafe(int id = 0)
		{
			var cafess = _db.Cafes.Where(x => x.SahaId == id);
			//if (code != 0)
			//{
			//	cafess = cafess.Where(x => x.YoneticiId == code);
			//}
			return View(cafess.ToList());
		}

		public IActionResult Ekipmanlar(int sahaid, int id = 0)
		{
			var ekp = _db.Ekipmanlars.Include(x => x.Saha).Where(x => x.SahaId == id).OrderByDescending(x=>x.Id);
			//if (sahaid != 0)
			//{
			//	ekp = ekp.Where(x => x.Id == id);
			//}
			return View(ekp.ToList());
		}
	}
}
