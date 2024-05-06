using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OpenSaha.Models;

namespace OpenSaha.ViewComponents.Kontrol
{
    public class RezKontrol : ViewComponent
    {
        private readonly SahaContext _db;
        public RezKontrol(SahaContext db)
        {
            _db = db;
        }
        public IViewComponentResult Invoke(int id)
        {
            var usermail = User.Identity.Name;
            var kull = _db.Kullanicis.FirstOrDefault(x => x.Email == usermail);
            var sahaid = _db.Sahas.FirstOrDefault(x => x.Id == id);
            var rez = _db.Rezervasyons.Where(x => x.YoneticiId == kull.YoneticiId && x.Durum == Durum.Beklemede).Include(x => x.Saha).Include(x => x.Kullanici).ToList();

            return View(rez);
        }
    }
}
