using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using OpenSaha.Models;

namespace OpenSaha.Areas.Admin.Controllers
{
    [Authorize(Policy = "Yonetici")]
    [Area("Admin")]
	public class RezervasyonController : Controller
	{
		private readonly SahaContext _s;
		public RezervasyonController(SahaContext s)
		{
			_s = s;
		}
		public IActionResult Index()
		{
			var saha = _s.Rezervasyons.Include(x => x.Saha).Include(x => x.Kullanici).Where(x => x.Act == Aktif.Aktif).OrderByDescending(x=>x.RandevuBaslangic).OrderByDescending(x=>x.RandevuBaslangic).ToList();
			return View(saha);
		}

		[HttpGet]
		public IActionResult Form(int id)
		{
            List<SelectListItem> degerler = (from s in _s.Kullanicis.ToList()
                                             select new SelectListItem
                                             {
                                                 Text = s.Isim + " " + s.Soyisim,
                                                 Value = s.Id.ToString()
                                             }).ToList();
            ViewBag.dgr = degerler;
            List<SelectListItem> sha = (from s in _s.Sahas.ToList()
                                             select new SelectListItem
                                             {
                                                 Text = s.Baslik,
                                                 Value = s.Id.ToString()
                                             }).ToList();
            ViewBag.sha = sha;
			List<SelectListItem> degerlerr = (from s in _s.YonetimTablosus.ToList()
                                             select new SelectListItem
                                             {
                                                 Text = s.Id+"",
                                                 Value = s.Id.ToString()
                                             }).ToList();
            ViewBag.dgrr = degerlerr;
            Rezervasyon rez = new Rezervasyon();
			if (id != 0)
			{
				rez = _s.Rezervasyons.Include(x => x.Kullanici).Include(x=>x.Saha).FirstOrDefault(x => x.Id == id);
			}
			return View(rez);
		}

		[HttpPost]
		public IActionResult Form(Rezervasyon r)
		{
			if (r.Id == 0)
			{
				Rezervasyon x = new Rezervasyon();
				r.Act = Aktif.Aktif;
				x.SahaId = r.SahaId;
				x.RandevuBaslangic = r.RandevuBaslangic;
				x.RandevuBitis = r.RandevuBitis;
				x.KullaniciId = r.KullaniciId;
				x.EslesmeId = r.EslesmeId;
				x.Durum = r.Durum;
				x.YoneticiId =r.YoneticiId;
				_s.Set<Rezervasyon>().Add(r);
				_s.SaveChanges();
			}
			else
			{
				var x = _s.Rezervasyons.Include(x=>x.Saha).FirstOrDefault(x => x.Id == r.Id);
				x.SahaId = r.SahaId;
				x.RandevuBaslangic = r.RandevuBaslangic;
				x.RandevuBitis = r.RandevuBitis;
				x.KullaniciId = r.KullaniciId;
				x.EslesmeId = r.EslesmeId;
				x.YoneticiId=r.YoneticiId;
				x.Durum = r.Durum;

				_s.Set<Rezervasyon>().Update(x);
				_s.SaveChanges();
			}
            string yon = "/Admin/Rezervasyon";
            return Redirect(yon);
        }

		public int UserIdGetir()
		{
			var UserEmail = User.Identity.Name;
			var user = _s.Kullanicis.FirstOrDefault(x => x.Email == UserEmail);
			return user.Id;
		}
	}
}