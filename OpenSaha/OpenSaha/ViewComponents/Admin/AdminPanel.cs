using Microsoft.AspNetCore.Mvc;
using OpenSaha.Models;

namespace OpenSaha.ViewComponents.Admin
{
    public class AdminPanel : ViewComponent
    {
        private readonly SahaContext _db;
        public AdminPanel(SahaContext db)
        {
            _db = db;
        }
        public IViewComponentResult Invoke()
        {
            var UserEmail = User.Identity.Name;
            var userid = _db.Kullanicis.FirstOrDefault(x => x.Email == UserEmail);
            var user = _db.Kullanicis.FirstOrDefault(x => x.Id == userid.Id);
            string isim = "";
            isim = user.Isim + " " + user.Soyisim;
            ViewBag.isim = isim;
            return View();
        }
    }
}