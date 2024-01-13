
using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.IdentityModel.Tokens;
using MimeKit;
using MimeKit.Text;


namespace _3abarni_backend.Services
{
    public class EmailSender : IEmailSender
    {
        private readonly IConfiguration _config;

        public EmailSender(IConfiguration config)
        {
            _config = config;
        }

        public async Task SendEmailAsync( string toAddress, string subject, string message)
        {
            var email = new MimeMessage();
            email.From.Add( MailboxAddress.Parse(_config["SMTP:Username"]));
            email.To.Add(MailboxAddress.Parse(toAddress));
            email.Subject=subject;
            email.Body= new TextPart(TextFormat.Html) { Text = message };

            using (var client= new SmtpClient())
            {
                await client.ConnectAsync(_config["SMTP:Host"], int.Parse(_config["SMTP:Port"]), SecureSocketOptions.StartTls);
                await client.AuthenticateAsync(_config["SMTP:Username"], _config["SMTP:Password"]);
                await client.SendAsync(email);
                await client.DisconnectAsync(true);


            }
        }
    }
}
