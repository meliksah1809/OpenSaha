using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using OpenSaha.Models;

namespace OpenSaha.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Policy ="Yonetici")]
    public class CafeController : Controller
    {
        private readonly SahaContext _db;
        public CafeController(SahaContext db)
        {
            _db = db;
        }
        public IActionResult Index()
        {
            var cafess = _db.Cafes.Include(x=>x.Saha).OrderByDescending(x=>x.Id).ToList();
            return View(cafess);
        }
        [HttpGet]
        public IActionResult Form(int id, int sahaid)
        {
            var caf = _db.Cafes.FirstOrDefault(x => x.Id == id);
            List<SelectListItem> yonetici = (from s in _db.YonetimTablosus.ToList()
                                             select new SelectListItem
                                             {
                                                 Text =s.Id+"",
                                                 Value = s.Id.ToString(),                                               

                                             }).ToList();
            ViewBag.yonetici = yonetici;
            var usermail = User.Identity.Name;
            var dgr = _db.Kullanicis.FirstOrDefault(x => x.Email == usermail);
            List<SelectListItem> sahaa = (from s in _db.Sahas.ToList()
                                          select new SelectListItem
                                          {
                                              Text = s.Baslik,
                                              Value = s.Id.ToString()
                                          }).ToList();
            ViewBag.sahas = sahaa;
            return View(caf);
        }

        [HttpPost]
        public IActionResult Form(Cafe c)
        {
            if (c.Id == 0)
            {
                c.Act = Aktif.Aktif;
                _db.Set<Cafe>().Add(c);
                _db.SaveChanges();
            }
            else
            {
                var x = _db.Cafes.FirstOrDefault(x => x.Id == c.Id);
                x.Act = Aktif.Aktif;
                x.Baslik = c.Baslik;
                x.Fiyat = c.Fiyat;
                x.Tarih = c.Tarih;
                x.Adet = c.Adet;
                x.GuncellemeTarih = c.GuncellemeTarih;
                x.YoneticiId = c.YoneticiId;
                x.Barkod = c.Barkod;
                x.Stoklu = c.Stoklu;
                x.SahaId = c.SahaId;
                _db.Set<Cafe>().Update(x);
                _db.SaveChanges();
            }
            string yon = "/Admin/Cafe";
            return Redirect(yon);
        }
    }
}