using Microsoft.AspNetCore.Mvc.Rendering;

namespace OpenSaha.ViewModels.Genel
{
	public class SehirIlceViewModel
	{
		public SehirIlceViewModel()
		{
			this.Sehir = new List<SelectListItem>();
			this.Ilce = new List<SelectListItem>();
		}
		public List<SelectListItem> Sehir { get; set; }
		public List<SelectListItem> Ilce { get; set; }

		public int SehirId { get; set; }
		public int IlceId { get; set; }
	}
}
