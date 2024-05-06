namespace OpenSaha.ViewModels.Konrtol
{
    public class Test
    {
        public List<CafeTakibi> CafeTakip { get; set; }
        public List<EkipmanTakibi> EkipmanTakip { get; set; }
    }
    public class CafeTakibi
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
    }
    public class EkipmanTakibi
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
    }
}
