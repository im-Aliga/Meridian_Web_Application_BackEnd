using System.Globalization;

namespace Meridian_Web.Infrastructure.Extensions
{
    public static class AppBuilderExtensions
    {
        public static void ConfigureMiddlewarePipeline(this WebApplication app)
        {
            app.UseStaticFiles();

            app.UseAuthentication();
            app.UseAuthorization();
            app.UseSession();
            app.MapControllerRoute(
                name: "default",
                pattern: "{area=exists}/{controller=home}/{action=index}");

            //app.Use(async (context, next) =>
            //{
            //    var cookieValue = context.Request.Cookies["Languages"];
            //    if (!string.IsNullOrEmpty(cookieValue))
            //    {
            //        var culture = new CultureInfo(cookieValue);
            //        Thread.CurrentThread.CurrentCulture = culture;
            //        Thread.CurrentThread.CurrentUICulture = culture;
            //    }

            //    await next();
            //});
        }
    }
}
