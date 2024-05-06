using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using OpenSaha.Models;

namespace OpenSaha.Areas.Kontrol.Controllers
{
	[Area("kontrol")]
	[Authorize(Policy = "UserYonetici")]

	public class OdemeController : Controller
	{
		private readonly SahaContext _db;
		public OdemeController(SahaContext db)
		{
			_db = db;
		}
		public IActionResult Index()
		{
			var usermail = User.Identity.Name;
			var kl = _db.Kullanicis.FirstOrDefault(k => k.Email == usermail);
			var ode = _db.Odemes.Include(x => x.Rezervasyon).Include(x => x.YonetimTablosu).Include(x => x.Rezervasyon.Kullanici).Where(x => x.Act == Aktif.Aktif && x.YoneticiId == kl.YoneticiId).OrderByDescending(x => x.Id);
			//var odta = HttpContext.Session.GetString("odta");
			//ViewBag.odta = odta;
			return View(ode.ToList());
		}
	}
}
