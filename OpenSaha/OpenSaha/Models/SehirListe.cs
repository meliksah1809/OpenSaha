namespace OpenSaha.Models
{
    public class SehirListe
    {
        public int Id { get; set; }
        public string SehirAdi { get; set; }
        public virtual List<Saha>Sahas { get; set; }  
        public virtual List<IlceListe> IlceListes { get; set; }  


    }
}
