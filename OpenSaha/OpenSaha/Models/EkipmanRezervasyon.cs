using System.ComponentModel.DataAnnotations.Schema;

namespace OpenSaha.Models
{
    public class EkipmanRezervasyon
    {
        public int Id { get; set; }
        [ForeignKey(nameof(Rezervasyon))]
        public int RezervasyonId { get; set; }
        [ForeignKey(nameof(Ekipmanlar))]
        public int EkipmanId { get; set; }
        public double Ucret { get; set; }
        public double Adet { get; set; }

        [ForeignKey(nameof(YonetimTablosu))]
        public int YoneticiId { get; set; }
        public Aktif Act { get; set; }
        public virtual Rezervasyon Rezervasyon { get; set; }
        public virtual Ekipmanlar Ekipmanlar { get; set; }
        public virtual YonetimTablosu YonetimTablosu { get; set; }
    }
}
