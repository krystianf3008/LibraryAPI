using MimeKit;
using MimeKit.Text;
using MailKit.Net.Smtp;
using MailKit.Security;
using LibraryAPI.Interfaces;
using LibraryAPI.Settings;

namespace LibraryAPI.Services
{
    public class EmailService : IEmailService
    {
        private readonly SmtpSettings _smtpSettings;

        public EmailService(SmtpSettings smtpSettings)
        {
            _smtpSettings = smtpSettings;
        }

        public async Task SendVerificationEmailAsync(string to, string name, string Token)
        {
            var templatePath = "EmailTemplates/verification_email.html";
            var template = File.ReadAllText(templatePath);

            template = template.Replace("{Name}", name);
            template = template.Replace("{Token}", Token);

            var message = new MimeMessage();
            message.From.Add(new MailboxAddress("Your Name", "verification@library.com"));
            message.To.Add(new MailboxAddress("Recipient", to));
            message.Subject = "Account Verification";
            message.Body = new TextPart(TextFormat.Html)
            {
                Text = template
            };

            await SendEmailAsync(message);
        }

        public async Task SendResetPasswordEmailAsync(string to,string name, string Token)
        {
            var templatePath = "EmailTemplates/password_reset_email.html";
            var template = File.ReadAllText(templatePath);

            template = template.Replace("{Name}", name);
            template = template.Replace("{Token}", Token);

            var message = new MimeMessage();
            message.From.Add(new MailboxAddress("Your Name", "password@library.com"));
            message.To.Add(new MailboxAddress("Recipient", to));
            message.Subject = "Reset Password";
            message.Body = new TextPart(TextFormat.Html)
            {
                Text = template
            };

            await SendEmailAsync(message);
        }

        private async Task SendEmailAsync(MimeMessage message)
        {
            using (var client = new SmtpClient())
            {
                client.ServerCertificateValidationCallback = (s, c, h, e) => true;
                await client.ConnectAsync(_smtpSettings.Host, _smtpSettings.Port, SecureSocketOptions.None);
                await client.AuthenticateAsync(_smtpSettings.Username, _smtpSettings.Password);
                await client.SendAsync(message);
                await client.DisconnectAsync(true);
            }
        }
    }
}