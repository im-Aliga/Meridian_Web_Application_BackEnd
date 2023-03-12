using Meridian_Web.Areas.Client.ViewModels.Authentication;
using Meridian_Web.Database.Models;

namespace Meridian_Web.Services.Abstracts
{
    public interface IUserActivationService
    {
        Task SendActivationUrlAsync(User user); 

    }
}
