using System.Threading.Tasks;
namespace ApplicationCore.Interfaces
{
    public interface IEmailSender
    {
        Task SendEmailAsync(string destinationEmailAddr, string emailBody, string subject);
    }
}
