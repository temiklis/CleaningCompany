using System.Threading.Tasks;

namespace CleaningCompany.Application.Interfaces
{
    public interface IEmailService
    {
        Task SendEmailAsync(string email, string subject, string message);
    }
}
