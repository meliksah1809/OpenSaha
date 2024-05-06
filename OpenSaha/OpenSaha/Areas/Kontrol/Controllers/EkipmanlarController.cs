using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using OpenSaha.Models;

namespace OpenSaha.Areas.Kontrol.Controllers
{
    [Area("kontrol")]
    [Authorize(Policy = "UserYonetici")]
    public class EkipmanlarController : Controller
    {
        private readonly SahaContext _db;
        public EkipmanlarController(SahaContext db)
        {
            _db = db;
        }

        public IActionResult Index()
        {
            var usermail = User.Identity.Name;
            var dgr = _db.Kullanicis.FirstOrDefault(x => x.Email == usermail);
            var cafem = _db.Ekipmanlars.Include(x => x.Saha).Where(x => x.YoneticiId == dgr.YoneticiId).OrderByDescending(x=>x.Id);
            return View(cafem.ToList());
        }
        [HttpGet]
        public IActionResult Form(int sahaid, int id = 0)
        {
            var usermail = User.Identity.Name;
            var dgr = _db.Kullanicis.FirstOrDefault(x => x.Email == usermail);
            var kid = dgr.Id;
            var ekip = _db.Ekipmanlars.Include(x => x.Saha).FirstOrDefault(x => x.Id == id);

            if (ekip.Saha.KullaniciId != dgr.Id)
            {
                return RedirectToAction("Index", "Sahalar", new { area = "Kontrol" });
            }

            List<SelectListItem> degerler = (from s in _db.Sahas.Where(x=>x.KullaniciId == kid).ToList()
                                             select new SelectListItem
                                             {
                                                 Text = s.Baslik,
                                                 Value = s.Id.ToString()
                                             }).ToList();
            ViewBag.sha = degerler;
            ViewBag.yid = dgr.YoneticiId;
            return View(ekip);
        }
        [HttpPost]
        public IActionResult Form(Ekipmanlar e)
        {

            if (e.Id == 0)
            {
                e.Act = Aktif.Aktif;
                _db.Set<Ekipmanlar>().Add(e);
                _db.SaveChanges();
            }
            else
            {
                var ek = _db.Ekipmanlars.FirstOrDefault(x => x.Id == e.Id);
                ek.Act = Aktif.Aktif;
                ek.Baslik = e.Baslik;
                ek.SahaId = e.SahaId;
                ek.Aciklama = e.Aciklama;
                ek.Ucret = e.Ucret;
                ek.Adet = e.Adet;
                ek.YoneticiId = e.YoneticiId;

                _db.Set<Ekipmanlar>().Update(ek);
                _db.SaveChanges();

            }
            string yonlendir = "/kontrol/Ekipmanlar";
            return Redirect(yonlendir);
        }

    }
}
