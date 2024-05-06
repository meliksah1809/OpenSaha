using System.ComponentModel.DataAnnotations.Schema;

namespace OpenSaha.Models
{
	public class StokTakip
	{
		public int Id { get; set; }
		[ForeignKey(nameof(Cafe))]
		public int CafeId { get; set; }
		public Islem Islem { get; set; }
		public int Adet { get; set; }
		public DateTime Tarih { get; set; }
		public double BirimFiyat { get; set; }
		public long Barkod { get; set; }
		[ForeignKey(nameof(YonetimTablosu))]
		public int YoneticiId { get; set; }
		public Aktif Act { get; set; }
		public virtual Cafe Cafe { get; set; }
		public virtual YonetimTablosu YonetimTablosu { get; set; }


	}
	public enum Islem
	{
		Girdi = 0,
		Cikti = 1
	}
}
