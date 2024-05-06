using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace OpenSaha.Models
{
    public class Cafe
    {
        public int Id { get; set; }
        public string Baslik { get; set; }
        public double Fiyat { get; set; }
        public int Adet { get ; set; }  
        public DateTime Tarih { get; set; } 
        public DateTime GuncellemeTarih { get; set; }
        public bool Stoklu { get; set; }
        [ForeignKey(nameof(Saha))]
        public int SahaId { get; set; }
        public long Barkod { get; set; }
        [ForeignKey(nameof(YonetimTablosu))]
        public int YoneticiId { get; set; }
        public Aktif Act { get; set; }
        public virtual List<CafeTakip> CafeTakips { get; set; }
        public virtual Saha Saha { get; set; }
        public virtual YonetimTablosu YonetimTablosu { get; set; }

    }
}
