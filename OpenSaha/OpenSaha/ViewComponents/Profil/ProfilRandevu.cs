using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OpenSaha.Models;
using X.PagedList;

namespace OpenSaha.ViewComponents.Profil
{
	public class ProfilRandevu : ViewComponent
	{

		private readonly SahaContext _db;
		public ProfilRandevu(SahaContext db)
		{
			_db = db;
		}
		public IViewComponentResult Invoke(int page = 1)
		{
			var UserMail = User.Identity.Name;
			var dgr = _db.Kullanicis.FirstOrDefault(x => x.Email == UserMail);
			var randevum = _db.Rezervasyons.Include(x => x.Saha).Include(x => x.Kullanici).Where(x => x.KullaniciId == dgr.Id).ToList();
			return View(randevum.ToPagedList(page,5));
		}
	}
}
