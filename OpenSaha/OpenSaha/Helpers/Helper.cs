namespace OpenSaha.Helpers
{
	public class Helper
	{
		public static string TelefonNo(string telno)
		{
			string tel = telno;
			tel = tel.Replace("(", "");
			tel = tel.Replace(")", "");
			tel = tel.Replace(".", "");
			tel = tel.Replace("-", "");
			tel = tel.Replace(" ", "");
			if (tel.Length == 11)
				tel = tel.Substring(1, tel.Length - 1);
			if (tel.Length == 10)
				return tel;
			else
				return telno;
		}
	}
}
