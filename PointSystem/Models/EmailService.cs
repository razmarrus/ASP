using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MimeKit;
using MailKit.Net.Smtp;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using System.Configuration;

namespace PointSystem.Models
{
    public class EmailService
    {
        /*private IOptions<EmailService> settings;

        public EmailService(IOptions<EmailService> settings)
        {
            this.settings = settings;
        }*/
        /*public IConfiguration Configuration { get; }
        public EmailService(IConfiguration configuration)
        {
            Configuration = configuration;
        }*/

        public async Task SendEmailAsync(string email, string subject, string message)
        {


            //int ClientSecret = Configuration.GetConnectionString("ClientSecret");

            var emailMessage = new MimeMessage();

            emailMessage.From.Add(new MailboxAddress("Администрация сайта", "lilianna500@gmail.com"));
            emailMessage.To.Add(new MailboxAddress("", email));
            emailMessage.Subject = subject;
            emailMessage.Body = new TextPart(MimeKit.Text.TextFormat.Html)
            {
                Text = message
            };

            using (var client = new SmtpClient())
            {
                client.ServerCertificateValidationCallback = (s, c, h, e) => true;
                await client.ConnectAsync("smtp.gmail.com", 587, false);
                await client.AuthenticateAsync("lilianna500@gmail.com", "e1kxF5U92LwH4eTy8");
                //ClientSecret = Configuration.GetConnectionString("ClientSecret");
                await client.SendAsync(emailMessage);

                await client.DisconnectAsync(true);
            }
        }
    }
}
