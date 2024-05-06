using System.ComponentModel.DataAnnotations.Schema;

namespace OpenSaha.Models
{
    public class Takim
    {
        public int Id { get; set; }
        [ForeignKey(nameof(Kullanici))]
        public int KullaniciId { get; set; }
        [ForeignKey(nameof(SehirListe))]
        public int SehirId { get; set; }
        [ForeignKey(nameof(IlceListe))]
        public int IlceId { get; set; }
        public int Kadro { get; set; }
        public string Baslik { get; set; }
        public Aktif Act { get; set; }
        public virtual Kullanici Kullanici { get; set; }
        public virtual SehirListe SehirListe { get; set; }
        public virtual IlceListe IlceListe { get; set; }
        public virtual List<TakimTakvim> TakimTakvims { get; set; }


    }
}
