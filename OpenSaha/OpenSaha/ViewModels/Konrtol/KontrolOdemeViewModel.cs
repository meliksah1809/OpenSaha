using OpenSaha.Models;

namespace OpenSaha.ViewModels.Konrtol
{
    public class KontrolOdemeViewModel
    {
        public int Id { get; set; }
        public string Kullanici { get; set; }
        public Durum Durum { get; set; }
        public double SahaUcret { get; set; }
        public double KafeUcret { get; set; }
        public double EkipmanUcret { get; set; }
        public int RezervasyonId { get; set; }
        public OdemeTipleri OdemeTipleri { get; set; }
        public int SahaId { get; set; }
        public int cafetakipId { get; set; }
        public int ekipmanrezervasyonId { get; set; }
        public int YoneticiId { get; set; }
        public Aktif Act { get; set; }
        public double ToplamUcret { get; set; }
        public DateTime RandevuBaslangic { get; set; }
        public DateTime RandevuBitis { get; set; }


    }
}
