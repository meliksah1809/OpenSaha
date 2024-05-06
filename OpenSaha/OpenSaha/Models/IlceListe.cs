using System.ComponentModel.DataAnnotations.Schema;

namespace OpenSaha.Models
{
    public class IlceListe
    {
        public int Id { get; set; }
        public string IlceAdi { get; set; }
        [ForeignKey(nameof(SehirListe))]
        public int SehirId { get; set; }
        public List<Saha>Sahas { get; set; }
        public virtual SehirListe SehirListe { get; set;}
    }
}
