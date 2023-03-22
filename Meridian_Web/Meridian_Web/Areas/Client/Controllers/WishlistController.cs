using Meridian_Web.Areas.Client.ViewComponents;
using Meridian_Web.Database;
using Meridian_Web.Services.Abstract;
using Meridian_Web.Services.Abstracts;
using Meridian_Web.Services.Concretes;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

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
    }
}
