using OpenSaha.Models;

namespace OpenSaha.ViewModels.Account
{
	public class ProfilAnasayfa
	{
        public Kullanici Kullanici { get; set; }
        public int puan { get; set; }
        public List<Rezervasyon> Rezervasyon { get; set; }
    }
}
