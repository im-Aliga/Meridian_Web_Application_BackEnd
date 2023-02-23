using Meridian_Web.Contracts.Email;

namespace Meridian_Web.Services.Abstracts
{
    public interface IEmailService
    {
        public void Send(MessageDto messageDto);
    }
}
