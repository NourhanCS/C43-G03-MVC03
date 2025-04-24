using IKEA.DAL.Models;
using System.Net;
using System.Net.Mail;

namespace IKEA.PL.Helpers
{
    public class EmailSettings
    {
        public static void SendEmail(Email email)
        {
            var Client = new SmtpClient("smtp.gmail.com", 587);
            Client.EnableSsl = true;
            Client.Credentials = new NetworkCredential("norhanmohmoud.2004@gmail.com","lbanwqtxuevpexgh"); //APP Password
            Client.Send("norhanmohmoud.2004@gmail.com", email.To, email.Subject, email.Body);

        }
    }
}
