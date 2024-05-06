using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OpenSaha.Models;

namespace OpenSaha.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Policy = "Yonetici")]
    public class StoklarController : Controller
    {
        private readonly SahaContext _db;
        public StoklarController(SahaContext db)
        {
            _db = db;
        }

        public IActionResult Index()
        {
            var stok = _db.Cafes.Where(x => x.Act == Aktif.Aktif).OrderByDescending(x=>x.Id).ToList();
            return View(stok);
        }

        [HttpGet]
        public IActionResult Form(int code, int id)
        {
            var stk = _db.Cafes.FirstOrDefault(x => x.YoneticiId == code && x.SahaId == id);
            return View(stk);
        }

        [HttpPost]
        public IActionResult Form(Cafe s)
        {
            if (s.Id == 0)
            {
                s.Act = Aktif.Aktif;
                _db.Set<Cafe>().Add(s);
                _db.SaveChanges();
            }
            else
            {
                var st = _db.Cafes.FirstOrDefault(x => x.Id == s.Id);
                st.Baslik = s.Baslik;
                st.Adet = s.Adet;
                st.Fiyat = s.Fiyat;
                st.Tarih = s.Tarih;
                st.GuncellemeTarih = s.GuncellemeTarih;
                st.YoneticiId = s.YoneticiId;
                st.Barkod = s.Barkod;
                st.SahaId = s.SahaId;
                _db.Set<Cafe>().Update(st);
                _db.SaveChanges();
            }
            return View(s);
        }

        [HttpPost]
        public IActionResult StokEkle()
        {

            return View();
        }
    }
}
