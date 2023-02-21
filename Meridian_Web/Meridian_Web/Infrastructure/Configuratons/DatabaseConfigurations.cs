
using Meridian_Web.Database;
using Microsoft.EntityFrameworkCore;

namespace Meridian_Web.Infrastructure.Configurations
{
    public static class DatabaseConfigurations
    {
        public static void ConfigureDatabase(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<DataContext>(o =>
            {
                o.UseSqlServer(configuration.GetConnectionString("AliPC"));
            });
        }
    }
}
