using System;
using System.Net.Mail;



namespace WebShop.Common
{
    public class EmailHandler : IEmailHandler
    {
        public void SendEmail()
        {
            try
            {
                MailMessage mail = new MailMessage();
                SmtpClient SmtpServer = new SmtpClient("smtp.gmail.com");

                mail.From = new MailAddress("tfbank1@gmail.com");
                mail.To.Add("jehona.murati@tfbank.se");
                mail.Subject = "Test Mail";
                mail.Body = "This is for testing SMTP mail from GMAIL";

                SmtpServer.Port = 25;
                SmtpServer.Credentials = new System.Net.NetworkCredential("tfbank1@gmail.com", "TFBank123");
                SmtpServer.EnableSsl = true;

                SmtpServer.Send(mail);
                //MessageBox.Show("mail Send");
            }
            catch (Exception ex)
            {
                //MessageBox.Show(ex.ToString());
            }
        }
    }
}
