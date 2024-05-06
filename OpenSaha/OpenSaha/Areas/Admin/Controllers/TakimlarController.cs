using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OpenSaha.Models;

namespace OpenSaha.Areas.Admin.Controllers
{
	[Area("Admin")]
    [Authorize(Policy = "Yonetici")]
    public class TakimlarController : Controller
	{
		private readonly SahaContext _db;

		public TakimlarController(SahaContext db)
		{
			_db = db;
		}

		public IActionResult Index()
		{
			var takimlar = _db.Takims.Include(x=>x.Kullanici).Include(x=>x.SehirListe).Include(x=>x.IlceListe).OrderByDescending(x=>x.Id).ToList();
			return View(takimlar);
		}
	}
}