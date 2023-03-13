using Meridian_Web.Areas.Client.ViewModels.Basket;
using Meridian_Web.Database.Models;

namespace Meridian_Web.Services.Abstract
{
    public interface IBasketService
    {
        Task<List<ProductCookieViewModel>> AddBasketProductAsync(Product product);
    }
}
