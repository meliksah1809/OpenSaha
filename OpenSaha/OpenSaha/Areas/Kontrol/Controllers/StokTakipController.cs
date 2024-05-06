using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using OpenSaha.Models;

namespace OpenSaha.Areas.Kontrol.Controllers
{
	[Area("kontrol")]
	[Authorize(Policy = "UserYonetici")]
	public class StokTakipController : Controller
	{
		private readonly SahaContext _db;
		public StokTakipController(SahaContext db)
		{
			_db = db;
		}
		public int UserIdGetir()
		{
			var UserEmail = User.Identity.Name;
			var userid = _db.Kullanicis.FirstOrDefault(x => x.Email == UserEmail);
			return userid.Id;
		}
		public IActionResult Index(int id)
		{
			var UserEmail = User.Identity.Name;
			var userid = _db.Kullanicis.FirstOrDefault(x => x.Email == UserEmail);
			var stktkp = _db.StokTakips.Include(x => x.Cafe).Include(x => x.Cafe.Saha).Where(x => x.YoneticiId == userid.YoneticiId).OrderByDescending(x => x.Tarih).ToList();
			return View(stktkp);
		}
	}
}