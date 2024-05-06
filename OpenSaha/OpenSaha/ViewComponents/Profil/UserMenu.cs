using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OpenSaha.Models;

namespace OpenSaha.ViewComponents.Profil
{
	public class UserMenu : ViewComponent
	{
		private readonly SahaContext _db;
		public UserMenu(SahaContext db)
		{
			_db = db;
		}
		public IViewComponentResult Invoke()
		{
			int userid = UserIdGetir();
			var dgr = _db.Rezervasyons.Where(x=>x.KullaniciId==userid && x.Durum == Durum.Beklemede && x.Act == Aktif.Aktif).Count();
			ViewBag.TotalCount = dgr;
			var tkm = _db.Takims.Where(x=>x.KullaniciId==userid).Count(); 
			ViewBag.TotalCountTkm = tkm; 
			var musait = _db.TakimTakvims.Where(x => x.KullaniciId == userid && x.Act == Aktif.Aktif).Count();
			ViewBag.mst= musait;
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