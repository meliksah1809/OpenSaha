using System.ComponentModel.DataAnnotations.Schema;

namespace OpenSaha.Models
{
    public class KullaniciPuanIslemleri
    {
        public int Id { get; set; }
        [ForeignKey(nameof(Kullanici))]
        public int KullaniciId { get; set; }
        public IslemTipi IslemTipi { get; set; }
        public double Ucret { get; set; }
        public double? Puan { get; set; }
        public OdemeTipleri OdemeTipi { get; set; }
        [ForeignKey(nameof(YonetimTablosu))]
        public int? YoneticiId { get; set; } 
        public Aktif Act { get; set; }
        public virtual Kullanici Kullanici { get; set; }
        public virtual YonetimTablosu YonetimTablosu { get; set; }
    }
    public enum IslemTipi
    {
        Ekleme = 0,
        Cikarma = 1,
        EkipBul = 2,

    }
}
