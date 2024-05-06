using System.Security.Cryptography;
using System.Text;

namespace OpenSaha.Helpers
{
	public class Security
	{
		public static string PasswordSifrele(string text)
		{
			string source = text;
			using (SHA1 sha1Hash = SHA1.Create())
			{
				byte[] sourceBytes = Encoding.UTF8.GetBytes(source);
				byte[] hashBytes = sha1Hash.ComputeHash(sourceBytes);
				string hash = BitConverter.ToString(hashBytes).Substring(0, 30).Replace("-", string.Empty);
				return (hash);
			}
		}
	}
}
