using API.Models.Control.Email;
using API.Services.Interfaces.Control;
using Microsoft.AspNetCore.Identity.UI.Services;
using System.Net.Mail;
using System.Net;
using Microsoft.AspNetCore.WebUtilities;
using System.Collections;
using System.Text;

namespace API.Services.Concrete.Control
{
    public class EmailService : IEmailService
    {
        private readonly string _smtpServer;
        private readonly int _port;
        private readonly string _senderEmail;
        private readonly string _password;

        public EmailService(IConfiguration configuration)
        {
            var emailSettings = configuration.GetSection("EmailSettings");
            _smtpServer = emailSettings["SmtpServer"];
            _port = int.Parse(emailSettings["Port"]);
            _senderEmail = emailSettings["SenderEmail"];
            _password = emailSettings["SenderPassword"];
        }
        public async Task SendEmailAsync(EmailModel email)
        {
            using var client = new SmtpClient(_smtpServer, _port)
            {
                Credentials = new NetworkCredential(_senderEmail, _password),
                EnableSsl = true
            };

            var mailMessage = new MailMessage
            {
                From = new MailAddress(_senderEmail),
                Subject = email.Subject,
                Body = email.BodyText,
                IsBodyHtml = true
            };
            mailMessage.To.Add(email.Email);

            var attachment = email.Attachments.First();
            using (var memoryStream = new MemoryStream(attachment.Data))
            {
                var att = new Attachment(memoryStream, attachment.Name);
                mailMessage.Attachments.Add(att);

                await client.SendMailAsync(mailMessage);
            }
        }

    }
}
