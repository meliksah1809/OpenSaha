using System.ComponentModel.DataAnnotations.Schema;

namespace OpenSaha.Models
{
    public class SahaPuan
    {
        public int Id { get; set; }
        public int KullaniciId { get; set; }
        public int Bakiye { get; set; }
        [ForeignKey(nameof(YonetimTablosu))]
        public int YoneticiId { get; set; }
        public Aktif Act { get; set; }
        public virtual YonetimTablosu YonetimTablosu { get; set; }
    }
}
