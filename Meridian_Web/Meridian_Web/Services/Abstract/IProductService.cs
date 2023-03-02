using Meridian_Web.Areas.Admin.Controllers;

namespace Meridian_Web.Services.Abstract
{
    public interface IProductService
    {
        decimal FindPercent(decimal price, decimal? discountPrice, int disPercent);
      
    }
}
