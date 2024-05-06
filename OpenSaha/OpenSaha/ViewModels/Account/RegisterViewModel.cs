using OpenSaha.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace OpenSaha.ViewModels.Account
{
	public class RegisterViewModel
	{
		[Required(ErrorMessage = "Email adresi gereklidir")]
		[Display(Name = "E-Mail Adresiniz")]
		[EmailAddress(ErrorMessage = "Email adresi doğru formatta değil")]
		public string Email { get; set; }

		//************************************************
		[Required(ErrorMessage = "Şifre zorunludur")]
		[Display(Name = "Şifreniz")]
		[DataType(DataType.Password)]
		public string Password { get; set; }

		//************************************************
		[Required(ErrorMessage = "İsim zorunludur")]
		[Display(Name = "Adınız")]
		public string Isim { get; set; }

		//************************************************
		[Required(ErrorMessage = "Soyisim zorunludur")]
		[Display(Name = "Soyadınız")]
		public string Soyisim { get; set; }

		//************************************************
		[Required(ErrorMessage = "Telefon numarası zorunludur")]
		[Display(Name = "Telefon Numaranız")]
		[DataType(DataType.PhoneNumber, ErrorMessage = "Geçerli bir telefon numarası giriniz")]
		//[RegularExpression(@"^([0-9]{10})$", ErrorMessage = "Geçerli bir telefon numarası giriniz")]
		public string Telefon { get; set; }
        public int SahaSayisi { get; set; }
        public int YoneticiSayisi { get; set; }
		public UserType UserType { get; set; }

		internal object Id(int arg)
		{
			throw new NotImplementedException();
		}
	}
}
