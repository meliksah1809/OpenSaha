using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace OpenSaha.ViewModels.Account
{
	public class SifreDegistir
	{
		[Required(ErrorMessage = "Mevcut Şifreniz zorunludur")]
		[Display(Name = "Mevcut Şifreniz")]
		[DataType(DataType.Password)]
		public string OldPassword { get; set; }
		[Required(ErrorMessage = "Yeni Şifreniz zorunludur")]
		[Display(Name = "Yeni Şifreniz")]
		[DataType(DataType.Password)]
		public string PasswordNew { get; set; }
		[Required(ErrorMessage = "Şifrenizi Doğrlamalanız Zorunludur")]
		[Display(Name = "Şifrenizi Doğrulayın")]
		[DataType(DataType.Password)]
		[Compare("PasswordNew",ErrorMessage ="Yeni şifreiz ile doğrulanan şifreniz birbiri ile uyuşmuyor.")]
		public string PasswordConfirm { get; set; }
	}
}
