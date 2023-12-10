using Company.DAL.Models;
using System.Net;
using System.Net.Mail;

namespace Company.PL.Helper
{
	public class EmailSetting
	{
		public static void SendEmail (Email email)
		{
			var Client = new SmtpClient("smtp.gmail.com", 587);
			Client.EnableSsl = true;
			Client.Credentials= new NetworkCredential("mostafaghazal248@gmail.com", "xrdlekvttviftfik");
			Client.Send("mostafaghazal248@gmail.com", email.To, email.Subject, email.Body);

		}
	}
}
