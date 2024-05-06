using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using OpenSaha.Models;

namespace OpenSaha.ViewComponents.Site
{
	public class PartialFiltre : ViewComponent
	{

		private readonly SahaContext _db;
		public PartialFiltre(SahaContext db)
		{
			_db = db;
		}
		public IViewComponentResult Invoke()
		{

			List<SelectListItem> degerler = (from s in _db.SehirListes.ToList()
											 select new SelectListItem
											 {
												 Text = s.SehirAdi,
												 Value = s.Id.ToString()
											 }).ToList();
			ViewBag.shr = degerler;


			List<SelectListItem> ilce = (from i in _db.IlceListes.ToList()
										 select new SelectListItem
										 {
											 Text = i.IlceAdi,
											 Value = i.Id.ToString(),
										 }).ToList();
			ViewBag.ilce = ilce;
			int userid = UserIdGetir();

			ViewBag.ilc = userid;

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
