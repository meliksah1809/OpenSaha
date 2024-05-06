using System.ComponentModel.DataAnnotations.Schema;

namespace OpenSaha.Models
{
    public class Slider
    {
        public int Id { get; set; }
        public string? Resim1 { get; set; }
        public string? Resim2 { get; set; }
        public string? Yazi { get; set; }
        public string? ButonYazi { get; set; }
        public string? ButonLink { get; set; }
        [ForeignKey(nameof(YonetimTablosu))]
        public int YoneticiId { get; set; }
        public Aktif Act { get; set; }
        public virtual YonetimTablosu YonetimTablosu { get; set; }
    }
}
