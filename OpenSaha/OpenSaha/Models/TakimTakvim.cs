using System.ComponentModel.DataAnnotations.Schema;
using System.Security.Cryptography.X509Certificates;

namespace OpenSaha.Models
{
    public class TakimTakvim
    {
        public int Id { get; set; }
        [ForeignKey(nameof(Takim))]
        public int TakimId { get; set; }
        [ForeignKey(nameof(Kullanici))]
        public int KullaniciId { get; set; }
        public int SahaId { get; set; }
        public DateTime TarihBaslangic { get; set; }
        public DateTime TarihBitis { get; set; }
        public Aktif Act { get; set; }
        public virtual Kullanici Kullanici { get; set; }
        public virtual Takim Takim { get; set; }
		public virtual Saha Saha { get; set; }

	}
}
