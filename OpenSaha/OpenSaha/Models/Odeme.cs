using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;

namespace OpenSaha.Models
{
	public class Odeme
	{
		public int Id { get; set; }
		public double? SahaUcret { get; set; }
		public double? KafeUcret { get; set; }
		public double? EkipmanUcret { get; set; }
		[ForeignKey(nameof(Rezervasyon))]
		public int RezervasyonId { get; set; }
		public OdemeTipleri OdemeTipleri { get; set; }
		[ForeignKey(nameof(Saha))]
		public int  SahaId { get; set; }
		public virtual Saha Saha { get; set; }
		[ForeignKey(nameof(YonetimTablosu))]
		public int? YoneticiId { get; set; }
		public Aktif Act { get; set; }
		public Rezervasyon Rezervasyon { get; set; }
		public virtual YonetimTablosu YonetimTablosu { get; set; }

    }
	[DefaultValue(OdemeTipleri.Nakit)]
	public enum OdemeTipleri
	{
		Nakit = 0,
		KrediKarti = 1,
		SanalPos = 2
	}
}
