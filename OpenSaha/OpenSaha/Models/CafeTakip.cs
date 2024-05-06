using System.ComponentModel.DataAnnotations.Schema;

namespace OpenSaha.Models
{
    public class CafeTakip
    {
        public int Id { get; set; }
        [ForeignKey(nameof(Rezervasyon))]
        public int RezervasyonId { get; set; }
        [ForeignKey(nameof(Cafe))]
        public int CafeId { get; set; }
        public int Adet { get; set; }
        public double Ucret { get; set; }
        public long Barkod { get; set; }
        [ForeignKey(nameof(YonetimTablosu))]
        public int YoneticiId { get; set; } 
        public Aktif Act { get; set; }
        public virtual Cafe Cafe { get; set; }
        public virtual Rezervasyon Rezervasyon { get; set; }
        public virtual YonetimTablosu YonetimTablosu { get;  set; }   

    }
}
