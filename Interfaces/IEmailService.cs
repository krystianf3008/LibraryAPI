using MimeKit;

namespace LibraryAPI.Interfaces
{
    public interface IEmailService
    {
        Task SendVerificationEmailAsync(string to,string name, string token);
        Task SendResetPasswordEmailAsync(string to,string name, string token);
    }
}