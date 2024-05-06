using Microsoft.AspNetCore.Mvc;
using OpenSaha.Models;

namespace OpenSaha.Controllers
{
	public class UpdateController : Controller
	{
		private readonly SahaContext _db;
		public UpdateController(SahaContext db)
		{
			_db = db;
		}
		public IActionResult Index(int? id)
		{
			Kullanici obj = new Kullanici();
			if (obj == null)
			{
				return View(obj);
			}
			obj = _db.Kullanicis.FirstOrDefault(x => x.Id == id);
			if (obj == null)
			{
				return NotFound();
			}
			return View(obj);
		}
		[HttpPost]
		public IActionResult Index(Kullanici obj)
		{
			if (ModelState.IsValid)
			{
				if (obj.Id == 0)
				{
					_db.Kullanicis.Add(obj);
				}
				else
				{
					_db.Kullanicis.Update(obj);
				}
				_db.SaveChanges();
				return RedirectToAction("Index");
			}
			return View(obj);
		}
	}
}