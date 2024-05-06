using Microsoft.EntityFrameworkCore;

namespace OpenSaha.Models
{
    public class SahaContext:DbContext
    {
        public SahaContext(DbContextOptions<SahaContext> options) : base(options) { }

        public virtual DbSet<Saha> Sahas { get; set; }
        public virtual DbSet<SahaResim> SahaResims { get; set; }
        public virtual DbSet<SehirListe> SehirListes  { get; set; }
        public virtual DbSet<Rezervasyon> Rezervasyons { get; set; }
        public virtual DbSet<KullaniciPuanIslemleri> KullaniciPuanIslemleris{ get; set; }
        public virtual DbSet<IlceListe> IlceListes { get; set; }
        public virtual DbSet<Eslesme> Eslesmes  { get; set; }
        public virtual DbSet<Kullanici> Kullanicis { get; set; }
        public virtual DbSet<Ekipmanlar> Ekipmanlars { get; set; }
        public virtual DbSet<EkipmanRezervasyon> EkipmanRezervasyons { get; set; }
        public virtual DbSet<Cafe>  Cafes { get; set; }
        public virtual DbSet<CafeTakip>  CafeTakips { get; set; }
        public virtual DbSet<Takim>  Takims { get; set; }
        public virtual DbSet<TakimTakvim>  TakimTakvims { get; set; }
        public virtual DbSet<Slider>  Sliders { get; set; }
        public virtual DbSet<Odeme> Odemes { get; set; }
        public virtual DbSet<StokTakip> StokTakips { get; set; }
        public virtual DbSet<YonetimTablosu> YonetimTablosus { get; set; }

        internal object GetList()
        {
            throw new NotImplementedException();
        }
    }
}
