using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OpenSaha.Models;

namespace OpenSaha.ViewComponents.Site
{
    public class HizliRez: ViewComponent
    {
        private readonly SahaContext _db;
        public HizliRez(SahaContext db)
        {
            _db = db;
        }
        public IViewComponentResult Invoke(int sahaid)
        {
            var usermail = User.Identity.Name;
            var kull = _db.Kullanicis.FirstOrDefault(x => x.Email == usermail);
            var rez = _db.Rezervasyons.Where(x => x.SahaId == sahaid && x.Durum == Durum.Beklemede).Include(x => x.Saha).Include(x => x.Saha).OrderByDescending(x=>x.RandevuBaslangic).ToList();

            return View(rez);
        }
    }
}
