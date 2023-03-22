using Meridian_Web.Areas.Client.ViewModels.Basket;
using Meridian_Web.Areas.Client.ViewModels.Wishlist;
using Meridian_Web.Database.Models;

namespace Meridian_Web.Services.Abstract
{
    public interface IWishlistService
    {
        Task<List<WishlistProductCookieVIewModel>> AddWishlistProductAsync(Product product);
    }
}
