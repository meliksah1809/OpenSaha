using OpenSaha.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace OpenSaha.ViewModels.Account
{
	public class LoginViewModel
	{
		[Required(ErrorMessage = "Email adresi veya telefon gereklidir")]
		[Display(Name = "Telefon veya E-Mail  Adresiniz")]
		public string EmailPhone { get; set; }
		[Required(ErrorMessage = "Şifre zorunludur")]
		[Display(Name = "Şifreniz")]
		[DataType(DataType.Password)]
		public string Password { get; set; }
		public UserType UserType { get; set; }
    }
}
