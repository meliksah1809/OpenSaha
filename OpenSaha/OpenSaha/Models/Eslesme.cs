using System.ComponentModel.DataAnnotations.Schema;

namespace OpenSaha.Models
{
    public class Eslesme
    {
        public int Id { get; set; }
        public int EvSahibi { get; set; }
        public int? Deplasman { get; set; }
        public string Sahalar { get; set; }
        public DateTime MacTarihi { get; set; }
        public DateTime OlusturmaTarihi { get; set; }
        public DateTime AktifSure { get; set; }
        public OnayDurum OnayDurum { get; set; }
        [ForeignKey(nameof(YonetimTablosu))]
        public int YoneticiId { get; set; }
        public Aktif Act { get; set; }
        public virtual YonetimTablosu YonetimTablosu { get; set; }   
    }
    public enum OnayDurum
    {
        Silindi = 0,
        Istek = 1,
        Onay = 2,
        Red = 3,
    }
}
