using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Chess_Tournament_Tracker.IL.EmailInfrastructures
{
    public class EmailSender
    {
        public void SendPassword(string pseudo, string password ,string mailAdress,string? content = null)
        {
            SmtpClient smtpClient = new SmtpClient("SSL0.ovh.net", 587);
            smtpClient.Credentials = new NetworkCredential("net2022@Khunly.be", "test1234=");
            smtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;
            smtpClient.EnableSsl = true;

            MailMessage mail = new MailMessage("net2022@Khunly.be",mailAdress);
            mail.Subject = "Envois de mot de passe";

            if(content is null)
            {
                mail.Body = $"Merci pour votre inscription {pseudo}. \n\n" +
                            $"Voici votre mot de passe : {password} \n\n" +
                             "Cordialement \n" +
                             "Chess Tournament Tracker";
            }
            else
            {
                mail.Body = content;
            }
            smtpClient.Send(mail);
        }
    }
}
