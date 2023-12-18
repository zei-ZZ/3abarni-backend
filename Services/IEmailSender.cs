namespace _3abarni_backend.Services
{
    public interface IEmailSender
    {
        Task SendEmailAsync( string toAddress, string subject, string message);
    }
}
