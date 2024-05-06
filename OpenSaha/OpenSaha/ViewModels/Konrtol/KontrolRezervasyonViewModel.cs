using OpenSaha.Models;
using System.ComponentModel.DataAnnotations.Schema;

namespace OpenSaha.ViewModels.Konrtol
{
    public class KontrolRezervasyonViewModel
    {
        public int Id { get; set; }
        public string Kullanici { get; set; }
        public string Saha { get; set; }
        public DateTime RandevuBaslangic { get; set; }
        public DateTime RandevuBitis { get; set; }
        public string Durum { get; set; }
        public double EkipmanUcret { get; set; }
        public double CafeUcret { get; set; }
        public double SahaUcret { get; set; }
        public double ToplamUcret { get; set; }
        public int YoneticiId { get; set; }
        public int SahaId { get; set; }
    }
}
