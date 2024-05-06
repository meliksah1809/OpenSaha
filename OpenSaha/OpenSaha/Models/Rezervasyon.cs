using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel;

namespace OpenSaha.Models
{
    public class Rezervasyon
    {
        public int Id { get; set; }
        [ForeignKey(nameof(Saha))]
        public int SahaId { get; set; }
        public DateTime RandevuBaslangic { get; set; }
        public DateTime RandevuBitis { get; set; }
        [ForeignKey(nameof(Kullanici))]
        public int KullaniciId { get; set; }
        [ForeignKey(nameof(Eslesme))]
        public int? EslesmeId { get; set; }
        public Durum Durum { get; set; }
        [ForeignKey(nameof(YonetimTablosu))]
        public int YoneticiId { get; set; }
        public Aktif Act { get; set; }
        public virtual Saha Saha { get; set; }
        public virtual Eslesme? Eslesme { get; set; }
        public virtual Kullanici Kullanici { get; set; }
        public virtual List<EkipmanRezervasyon> EkipmanRezervasyons { get; set; }
        public virtual List<CafeTakip> CafeTakips { get; set; }
        public virtual YonetimTablosu YonetimTablosu { get; set; }
    }
    public enum Durum
    {
        Iptal = 0,
        Beklemede = 1,
        Onaylandi = 2
    }

    public enum SahaTipleri
    {
        Kapali=0,
        Acik=1
    }

}
