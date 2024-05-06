using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;

namespace OpenSaha.Models
{
    public class Saha
    {
        public int Id { get; set; }
        [ForeignKey(nameof(SehirListe))]
        public int SehirId { get; set; }
        [ForeignKey(nameof(IlceListe))]
        public int IlceId { get; set; }
        [ForeignKey(nameof(Kullanici))]
        public int KullaniciId { get; set; }
        public string Baslik { get; set; }
        public string? Aciklama { get; set; }
        public  DateTime AcilisSaat { get; set; }
        public  DateTime KapanisSaat { get; set; }
        public  bool YirmiDortSaat { get; set; }
        public string? Ozellik { get; set; }
        public double Ucret { get; set; }
        [ForeignKey(nameof(YonetimTablosu))]
        public  int YoneticiId { get; set; }    
		public SahaTipi SahaTipi { get; set; }
		public Aktif Act { get; set; }
        public List<SahaResim> SahaResims { get; set; }
        public List<Rezervasyon> Rezervasyons { get; set; }
        public List<Ekipmanlar> Ekipmanlars { get; set; }
        public virtual SehirListe SehirListe{ get; set; }
        public virtual IlceListe IlceListe { get; set; }
        public virtual Kullanici Kullanici { get; set; }
        public virtual YonetimTablosu YonetimTablosu { get; set; }


    }
    [DefaultValue(Aktif.Aktif)]
    public enum Aktif
    {
        Silinmis = 0,
        Aktif = 1
    }
    public enum SahaTipi
    {
		Kapali = 0,
		Acik = 1

    }

}
