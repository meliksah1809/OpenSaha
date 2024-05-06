using System.ComponentModel.DataAnnotations.Schema;

namespace OpenSaha.Models
{
    public class Ekipmanlar
    {
        public int Id { get; set; }
        [ForeignKey(nameof(Saha))]
        public int SahaId { get; set; }
        public string Baslik { get; set; }
        public string? Resim { get; set; }
        public string? Aciklama { get; set; }
        public double Ucret { get; set; }
        public double Adet { get; set; }
        [ForeignKey(nameof(YonetimTablosu))]
        public int YoneticiId { get; set; }
        public Aktif Act { get; set; }
        public List<EkipmanRezervasyon> ekipmanRezervasyons { get; set; }
        public virtual Saha Saha { get; set; }
        public virtual YonetimTablosu YonetimTablosu { get; set; }


    }
}
