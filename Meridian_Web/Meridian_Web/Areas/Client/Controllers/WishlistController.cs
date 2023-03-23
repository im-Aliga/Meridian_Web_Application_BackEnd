using Meridian_Web.Areas.Client.ViewComponents;
using Meridian_Web.Areas.Client.ViewModels.Basket;
using Meridian_Web.Areas.Client.ViewModels.Wishlist;
using Meridian_Web.Database;
using Meridian_Web.Services.Abstract;
using Meridian_Web.Services.Abstracts;
using Meridian_Web.Services.Concretes;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;

namespace Meridian_Web.Areas.Client.Controllers
{
    [Area("client")]
    [Route("wishlist")]
    public class WishlistController : Controller
    {
        private readonly DataContext _dataContext;
        private readonly IWishlistService _wishlistService;
        private readonly IUserService _userService;

        public WishlistController(DataContext dataContext, IWishlistService wishlistService, IUserService userService)
        {
            _dataContext = dataContext;
            _wishlistService = wishlistService;
            _userService = userService;
        }

        [HttpGet("list", Name = "client-wishlist-list")]
        public async Task<IActionResult> ListAsync()
        {
            return View();
        }

        [HttpGet("add/{id}", Name = "client-wishlist-add")]
        public async Task<IActionResult> AddProduct([FromRoute] int id)
        {
            var product = await _dataContext.Products.FirstOrDefaultAsync(p => p.Id == id);
            if (product is null)
            {
                return NotFound();
            }

            var productCookiViewModel = await _wishlistService.AddWishlistProductAsync(product);
            if (productCookiViewModel.Any())
            {
                return ViewComponent(nameof(WishlistProductViewComponent), productCookiViewModel);
            }

            return ViewComponent(nameof(WishlistProductViewComponent));
        }

        [HttpGet("delete/{id}", Name = "client-wishlist-delete")]
        public async Task<IActionResult> DeleteProductAsync([FromRoute] int id)
        {
            if (_userService.IsAuthenticated)
            {

                var wishlistProduct = await _dataContext.WishlistProducts
                        .FirstOrDefaultAsync(bp => bp.Wishlist.UserId == _userService.CurrentUser.Id && bp.ProductId == id);

                if (wishlistProduct is null) return NotFound();

                _dataContext.WishlistProducts.Remove(wishlistProduct);
            }
            else
            {

                var product = await _dataContext.Products.FirstOrDefaultAsync(b => b.Id == id);
                if (product is null)
                {
                    return NotFound();
                }

                var productCookieValue = HttpContext.Request.Cookies["wishlistproducts"];
                if (productCookieValue is null)
                {
                    return NotFound();
                }

                var productsCookieViewModel = JsonSerializer.Deserialize<List<WishlistProductCookieVIewModel>>(productCookieValue);
                productsCookieViewModel!.RemoveAll(pcvm => pcvm.Id == id);

                HttpContext.Response.Cookies.Append("wishlistproducts", JsonSerializer.Serialize(productsCookieViewModel));
            }


            await _dataContext.SaveChangesAsync();

            return RedirectToRoute("client-wishlist-list");
        }
    }
}
