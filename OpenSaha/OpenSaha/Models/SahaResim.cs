using System.ComponentModel.DataAnnotations.Schema;

namespace OpenSaha.Models
{
    public class SahaResim
    {
        public int Id { get; set; }
        [ForeignKey(nameof(Saha))]
        public int SahaId { get; set; }
        public string? Resim { get; set; }
        public string Baslik { get; set; }
        [ForeignKey(nameof(YonetimTablosu))]
        public int YoneticiId { get; set; }
        public Aktif Act { get; set; }
        public virtual Saha Saha { get; set; }
        public virtual YonetimTablosu YonetimTablosu { get; set; }
    }
}
