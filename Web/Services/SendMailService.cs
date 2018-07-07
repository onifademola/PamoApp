using System;
using System.Net;
using System.Net.Mail;
using System.Net.Mime;
using System.Threading.Tasks;
using System.Web;

namespace Web.Services
{
    public class SendMailService
    {
        public bool TryToSendMail(MailMessage msg)
        {
            try
            {
                //using (var smtp = new SmtpClient())
                //{
                //    //var credential = new NetworkCredential
                //    //{
                //    //    //UserName = "results@eaglesbyte.org",
                //    //    //Password = "st@rter#99"
                //    //    UserName = "eaglesbyteinnovations@gmail.com",
                //    //    Password = "AmaraShola.1"
                //    //    //UserName = System.Configuration.ConfigurationManager.AppSettings.Get("UserName"),  // replace with valid value
                //    //    //Password = System.Configuration.ConfigurationManager.AppSettings.Get("Password")  // replace with valid value
                //    //};
                //    //smtp.Credentials = credential;
                //    smtp.Host = System.Configuration.ConfigurationManager.AppSettings.Get("Host");
                //    smtp.Port = Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings.Get("SMTPPort"));
                //    smtp.EnableSsl = Convert.ToBoolean(System.Configuration.ConfigurationManager.AppSettings.Get("EnableSSL"));
                //    //smtp.EnableSsl = true;

                //    //smtp.Host = "mail.eaglesbyte.org";
                //    //smtp.Port = 25;
                //    //smtp.EnableSsl = false;
                //    smtp.UseDefaultCredentials = true;
                //    smtp.Send(msg);
                //}
                //SmtpClient smtpClient = new SmtpClient("smtp.sendgrid.net", Convert.ToInt32(587));
                SmtpClient smtpClient = new SmtpClient();
                //smtpClient.Host = "smtp.gmail.com";
                smtpClient.Host = "mail.eaglesbyte.org";
                smtpClient.EnableSsl = false;
                //System.Net.NetworkCredential credentials = new System.Net.NetworkCredential("eaglesbyteinnovations@gmail.com", "AmaraShola.1");
                System.Net.NetworkCredential credentials = new System.Net.NetworkCredential("results@eaglesbyte.org", "st@rter#99");
                smtpClient.UseDefaultCredentials = true;
                smtpClient.Credentials = credentials;
                smtpClient.Port = 8889;
                smtpClient.Send(msg);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool SendEmail(EmailMessage message)
        {
            MailMessage msg = new MailMessage();
            msg.From = new MailAddress(System.Configuration.ConfigurationManager.AppSettings.Get("UserName"),
                System.Configuration.ConfigurationManager.AppSettings.Get("DisplayName"));
            msg.To.Add(new MailAddress(message.Recipient));
            msg.Subject = message.Subject;
            msg.Body = message.Body;

            var result = TryToSendMail(msg);
            if (result == true)
                return result;
            else
                return false;
        }

        public async Task<bool> SendPasswordAsync(string userId, string subject, string body, string recipient)
        {
            MailMessage msg = new MailMessage();
            //msg.From = new MailAddress(System.Configuration.ConfigurationManager.AppSettings.Get("UserName"),
            //    System.Configuration.ConfigurationManager.AppSettings.Get("DisplayName"));
            msg.From = new MailAddress("results@eaglesbyte.org");
            msg.Sender = new MailAddress("results@eaglesbyte.org");
            msg.To.Add(new MailAddress(recipient));
            msg.Subject = subject;
            msg.Body = body;
            string text = "text body";
            string html = @"<p>html body</p>";
            msg.AlternateViews.Add(AlternateView.CreateAlternateViewFromString(text, null, MediaTypeNames.Text.Plain));
            msg.AlternateViews.Add(AlternateView.CreateAlternateViewFromString(html, null, MediaTypeNames.Text.Html));
            msg.IsBodyHtml = true;

            // Send the e-mail message via the specified SMTP server.
            bool result = await Task.Run(() => TryToSendMail(msg));
            if (result == true)
                return result;
            else
                return false;
        }

        public void SendReportEmail(EmailMessage message, string _text, string _html)
        {
            string text = string.Format("Please click on this link to {0}: {1}", message.Subject, message.Body);
            string html = "Please confirm your account by clicking this link: <a href=\"" + message.Body + "\">link</a><br/>";

            html += HttpUtility.HtmlEncode(@"Or click on the copy the following link on the browser:" + message.Body);

            MailMessage msg = new MailMessage();
            msg.From = new MailAddress(System.Configuration.ConfigurationManager.AppSettings.Get("UserName"),
                System.Configuration.ConfigurationManager.AppSettings.Get("DisplayName"));
            msg.To.Add(new MailAddress(message.Recipient));
            msg.Subject = message.Subject;
            msg.AlternateViews.Add(AlternateView.CreateAlternateViewFromString(text, null, MediaTypeNames.Text.Plain));
            msg.AlternateViews.Add(AlternateView.CreateAlternateViewFromString(html, null, MediaTypeNames.Text.Html));


            // Send the e-mail message via the specified SMTP server.
            using (var smtp = new SmtpClient())
            {
                var credential = new NetworkCredential
                {
                    UserName = System.Configuration.ConfigurationManager.AppSettings.Get("UserName"),  // replace with valid value
                    Password = System.Configuration.ConfigurationManager.AppSettings.Get("Password")  // replace with valid value
                };
                smtp.Credentials = credential;
                smtp.Host = System.Configuration.ConfigurationManager.AppSettings.Get("Host");
                smtp.Port = Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings.Get("SMTPPort"));
                smtp.EnableSsl = Convert.ToBoolean(System.Configuration.ConfigurationManager.AppSettings.Get("EnableSSL"));
                smtp.Send(msg);                
            }
        }
    }
}