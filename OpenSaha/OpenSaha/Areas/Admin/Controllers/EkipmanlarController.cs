using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using OpenSaha.Models;
using X.PagedList;

namespace OpenSaha.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Policy = "Yonetici")]
    public class EkipmanlarController : Controller
    {
        private readonly SahaContext _db;
        public EkipmanlarController(SahaContext s)
        {
            _db = s;
        }
        public IActionResult Index(int page = 1)
        {
            var Ekipman = _db.Ekipmanlars.Include(x => x.Saha).Where(x => (x.Act == Aktif.Aktif)).OrderByDescending(x=>x.Id).ToList();
            return View(Ekipman);
        }

        [HttpGet]
        public IActionResult Form(int sahaid, int id = 0)
        {
            var ekip = _db.Ekipmanlars.Include(x => x.Saha).FirstOrDefault(x => x.Id == id);
            List<SelectListItem> degerler = (from s in _db.Sahas.ToList()
                                             select new SelectListItem
                                             {
                                                 Text = s.Baslik,
                                                 Value = s.Id.ToString()
                                             }).ToList();
            ViewBag.sha = degerler;
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
            string yon = "/Admin/Ekipmanlar";
            return Redirect(yon);
        }
    }
}