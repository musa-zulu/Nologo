using System.Threading.Tasks;
using Nologo.Domain.Settings;

namespace Nologo.Service.Contracts
{
    public interface IEmailService
    {
        Task SendEmailAsync(MailRequest mailRequest);
    }
}
