
using Meridian_Web.Services.Abstracts;
using Meridian_Web.Services.Concretes;

namespace Meridian_Web.Infrastructure.Configurations
{
    public static class RegisterCustomServicesConfigurations
    {
        public static void RegisterCustomServices(this IServiceCollection services, IConfiguration configuration)
        {
            //services.AddScoped<IEmailService, SMTPService>();
            services.AddScoped<IFileService, FileService>();
            //services.AddScoped<IUserService, UserService>();
            //services.AddScoped<IUserActivationService, UserActivationService>();
            //services.AddScoped<IBasketService, BasketService>();
            //services.AddScoped<IOrderService, OrderService>();
        }
    }
}
