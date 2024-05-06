using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using OpenSaha.Models;
using OpenSaha.ViewModels.Genel;
using System.Diagnostics;

namespace OpenSaha.Controllers
{
    public class HomeController : Controller
    {
        
        //private readonly ILogger<HomeController> _logger;


        //public HomeController(ILogger<HomeController> logger)
        //{
        //    _logger = logger;
        //}
        private readonly SahaContext _db;
        public HomeController(SahaContext db)
        {
            _db = db;
        }

		[AllowAnonymous]
        public IActionResult Index()
        {
			bool girisYapmis = false;
			string isim = "";
			var UserMail = User.Identity.Name;
			if (UserMail != null)
			{
				girisYapmis = true;
				var user = _db.Kullanicis.FirstOrDefault(x => x.Email == UserMail);
				isim = user.Isim + " " + user.Soyisim;
			}
			var kullanicii = _db.Kullanicis.Count();
			ViewBag.klnc = kullanicii;		
            var takim = _db.Takims.Count(); 
			ViewBag.tkm = takim; 
            var sahalar = _db.Sahas.Count();
			ViewBag.sha = sahalar;
			ViewBag.girisYapmis = girisYapmis;
			return View();
        }

		public IActionResult Privacy()
        {
            return View();
        }

		[HttpPost]
		public JsonResult SehirAjax(string type, int value)
		{
			SehirIlceViewModel sivm = new SehirIlceViewModel();
			switch (type)
			{
				case "ddlSehirler":
					sivm.Ilce = (from customer in this._db.IlceListes
								 where customer.SehirId == value
								 select new SelectListItem
								 {
									 Value = customer.Id.ToString(),
									 Text = customer.IlceAdi
								 }).ToList();
					break;
			}
			return Json(sivm);
		}

		[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }



    }
}