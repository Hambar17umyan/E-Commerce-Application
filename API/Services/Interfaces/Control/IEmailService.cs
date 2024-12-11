using API.Models.Control.Email;

namespace API.Services.Interfaces.Control
{
    public interface IEmailService
    {
        Task SendEmailAsync(EmailModel email);
    }
}
