using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OpenSaha.Models
{
	public class YonetimTablosu
	{
        [Key]
        [Range(1000, int.MaxValue)]
        public int Id { get; set; }
        public int SahaSayisi { get; set; }
        public int YoneticiSayisi { get; set; }
        public  Aktif Act { get; set; }

    }
}
