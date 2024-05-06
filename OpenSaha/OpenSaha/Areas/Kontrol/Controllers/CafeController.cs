using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using OpenSaha.Models;

namespace OpenSaha.Areas.Kontrol.Controllers
{
    [Area("kontrol")]
    [Authorize(Policy = "UserYonetici")]
    public class CafeController : Controller
    {
        private readonly SahaContext _db;
        public CafeController(SahaContext db)
        {
            _db = db;
        }

        public IActionResult Index()
        {
            var usermail = User.Identity.Name;
            var dgr = _db.Kullanicis.FirstOrDefault(x => x.Email == usermail);
            var cafem = _db.Cafes.Include(x => x.Saha).Where(x => x.YoneticiId == dgr.YoneticiId).OrderByDescending(x=>x.Id);
            return View(cafem.ToList());
        }
        [HttpGet]
        public IActionResult form(int id)
        {
            var caf = _db.Cafes.FirstOrDefault(x => x.Id == id);

            var usermail = User.Identity.Name;
            var dgr = _db.Kullanicis.FirstOrDefault(x => x.Email == usermail);
            List<SelectListItem> sahaa = (from s in _db.Sahas.Where(x => x.Kullanici.Id == dgr.Id).ToList()
                                          select new SelectListItem
                                          {
                                              Text = s.Baslik,
                                              Value = s.Id.ToString()
                                          }).ToList();

            ViewBag.sahas = sahaa;
            ViewBag.yonetici = dgr.YoneticiId;
            ViewBag.id = id;

            return View(caf);

        }
        [HttpPost]
        public IActionResult Form(Cafe c)
        {
            int son = 0;
            if (c.Id == 0)
            {
                c.Act = Aktif.Aktif;
                c.GuncellemeTarih = DateTime.Now;
                c.Tarih = DateTime.Now;
                _db.Set<Cafe>().Add(c);
                _db.SaveChanges();
                var stk = new StokTakip();
				stk.CafeId = c.Id;
				stk.Adet = c.Adet;
				stk.Islem = Islem.Girdi;
				stk.BirimFiyat = c.Fiyat;
				stk.Barkod = c.Barkod;
				stk.YoneticiId = c.YoneticiId;
				stk.Act = Aktif.Aktif;
				stk.Tarih = DateTime.Now;
				_db.Set<StokTakip>().Add(stk);
				_db.SaveChanges();

			}
            else
            {
                var x = _db.Cafes.FirstOrDefault(x => x.Id == c.Id);
                x.Act = Aktif.Aktif;
                x.Baslik = c.Baslik;
                x.Fiyat = c.Fiyat;
                son = c.Adet - x.Adet;
                x.Adet = c.Adet;
                x.GuncellemeTarih = DateTime.Now;
                x.YoneticiId = c.YoneticiId;
                x.Barkod = c.Barkod;
                x.Stoklu = c.Stoklu;
                x.SahaId = c.SahaId;

                _db.Set<Cafe>().Update(x);
                _db.SaveChanges();
            }

            if (son >= 1)
            {
                var stoktakip = new StokTakip();
                stoktakip.CafeId = c.Id;
                stoktakip.Adet = son;
                stoktakip.Islem = Islem.Girdi;
                stoktakip.BirimFiyat = c.Fiyat;
                stoktakip.Barkod = c.Barkod;
                stoktakip.YoneticiId = c.YoneticiId;
                stoktakip.Act = Aktif.Aktif;
                stoktakip.Tarih = DateTime.Now;
                _db.Set<StokTakip>().Add(stoktakip);
                _db.SaveChanges();
            }
            else if(son <= -1)
            {
				var stoktakip = new StokTakip();
				stoktakip.CafeId = c.Id;
				stoktakip.Adet = Math.Abs(son);
				stoktakip.Islem = Islem.Cikti;
				stoktakip.BirimFiyat = c.Fiyat;
				stoktakip.Barkod = c.Barkod;
				stoktakip.YoneticiId = c.YoneticiId;
				stoktakip.Act = Aktif.Aktif;
				stoktakip.Tarih = DateTime.Now;
				_db.Set<StokTakip>().Add(stoktakip);
				_db.SaveChanges();
			}
            string yonlendir = "/kontrol/Cafe";
            return Redirect(yonlendir);
        }
    }
} 