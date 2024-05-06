using System.ComponentModel;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;

namespace OpenSaha.Models
{
    public class Kullanici
    {
        public int Id { get; set; }
        [Display(Name ="Isim")]
        public string Isim { get; set; }
        public string Soyisim { get; set; }
        public string Telefon { get; set; }
        public string Email { get; set; }
        [Display(Name = "Password")]
        public string Password { get; set; }
        public DateTime? DogumTarihi { get; set; }
        public double? Puan { get; set;  } = 0;
        public UserType UserType { get; set; }
        [ForeignKey(nameof(YonetimTablosu))]
        public int? YoneticiId { get; set; }
        public Aktif Act { get; set; } 
        public virtual List<Rezervasyon> Rezervasyons { get; set; }
		public virtual List<Takim> Takims { get; set; }
		public virtual List<TakimTakvim> TakimTakvims { get; set; }
        public virtual List<Saha> Sahas { get; set; }
        public virtual YonetimTablosu YonetimTablosu { get; set; }


	}
    [DefaultValue(UserType.Kullanici)]
    public enum UserType
    {
        Kullanici = 0,
        Yonetici = 1,
        SahaYonetici = 2,
    }
}
