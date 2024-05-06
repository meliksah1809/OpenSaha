using Microsoft.AspNetCore.Mvc;
using OpenSaha.Models;
using System.ComponentModel;

namespace OpenSaha.ViewComponents.Kontrol
{
    public class KontrolPanel: ViewComponent
    {
        private readonly SahaContext _db;
        public KontrolPanel(SahaContext db)
        {
            _db = db;
        }
        public IViewComponentResult Invoke()
        {
            var UserEmail = User.Identity.Name;
            var userid = _db.Kullanicis.FirstOrDefault(x => x.Email == UserEmail);
            var sahalar = _db.Sahas.Where(x => x.KullaniciId == userid.Id).Count();
            ViewBag.sahalar = sahalar;
            //rezervasyon sayılaro
            var onay = _db.Rezervasyons.Where(x=>x.Durum == Durum.Onaylandi && x.YoneticiId == userid.YoneticiId).Count();
            ViewBag.onay = onay;
            var bekleme = _db.Rezervasyons.Where(x=>x.Durum == Durum.Beklemede && x.YoneticiId == userid.YoneticiId).Count();
            ViewBag.bekleme = bekleme;
            var red = _db.Rezervasyons.Where(x=>x.Durum == Durum.Iptal && x.YoneticiId == userid.YoneticiId).Count();
            ViewBag.red = red;
            var ekip = _db.Ekipmanlars.Where(x => x.YoneticiId == userid.YoneticiId).Count();
            ViewBag.ekip=ekip;
            var kafe = _db.Cafes.Where(x => x.YoneticiId == userid.YoneticiId).Count();
            ViewBag.kafe = kafe;
            var user = _db.Kullanicis.FirstOrDefault(x => x.Id == userid.Id);
            string isim = "";
            isim = user.Isim + " " + user.Soyisim;
            ViewBag.isim = isim;
            var odeme = _db.Odemes.Where(x => x.YoneticiId == userid.YoneticiId && x.Act == Aktif.Aktif).Count();
            ViewBag.odeme = odeme;
            return View();

        }
        public int UserIdGetir()
        {
            var UserEmail = User.Identity.Name;
            var user = _db.Kullanicis.FirstOrDefault(x => x.Email == UserEmail);
            return user.Id;

        }
    }
}
