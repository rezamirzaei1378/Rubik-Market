using Microsoft.Extensions.Logging;
using Rubik_Market.Application.Services.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace Rubik_Market.Application.Services.Implementation
{
    public class EmailSender : IEmailSender
    {
        private readonly ILogger<EmailSender> _logger;

        public EmailSender(ILogger<EmailSender> logger)
        {
            _logger = logger;
        }
        public bool SendEmail(string to, string subject, string body)
        {
            try
            {

                MailMessage mail = new MailMessage();
                SmtpClient SmtpServer = new SmtpClient("smtp.gmail.com");

                mail.From = new MailAddress("alirezamz.78.12@gmail.com", "کد تایید حساب کاربری");
                mail.To.Add(to);
                mail.Subject = subject;
                mail.Body = body;
                mail.IsBodyHtml = true;

                SmtpServer.Port = 587;
                SmtpServer.EnableSsl = true;

                SmtpServer.Credentials = new System.Net.NetworkCredential("alirezamz.78.12@gmail.com", "mqln tvbz toxa ptcc");
                SmtpServer.Send(mail);

                return true;
            }
            catch (Exception exception)
            {
                _logger.LogError($"Email Error\n\tErrorMessage:: {exception.Message}");
                return false;
            }
        }
    }
}
