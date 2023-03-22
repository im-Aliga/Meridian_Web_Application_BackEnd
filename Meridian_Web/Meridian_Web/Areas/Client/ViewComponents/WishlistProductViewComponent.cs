using Meridian_Web.Areas.Client.ViewModels.Basket;
using Meridian_Web.Areas.Client.ViewModels.Wishlist;
using Meridian_Web.Contracts.File;
using Meridian_Web.Database;
using Meridian_Web.Services.Abstracts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;
using System.Xml.Linq;

namespace Meridian_Web.Areas.Client.ViewComponents
{
    [ViewComponent(Name = "WishlistProductViewComponent")]
    public class WishlistProductViewComponent:ViewComponent
    {
        private readonly DataContext _dataContext;
        private readonly IUserService _userService;
        private readonly IFileService _fileService;

        public WishlistProductViewComponent(DataContext dataContext, IUserService userService, IFileService fileService)
        {
            _dataContext = dataContext;
            _userService = userService;
            _fileService = fileService;
        }

        public async Task<IViewComponentResult> InvokeAsync(List<WishlistProductCookieVIewModel> viewModel = null)
        {
            if (_userService.IsAuthenticated)
            {



                var model = await _dataContext.WishlistProducts

                    .Where(bp => bp.Wishlist.UserId == _userService.CurrentUser.Id)
                    .Select(bp =>
                        new WishlistProductCookieVIewModel(
                            bp.ProductId,
                            bp.Product!.Title,
                            bp.Product.ProductImages.Take(1).FirstOrDefault()! != null
                            ? _fileService.GetFileUrl(bp.Product.ProductImages!.Take(1)!.FirstOrDefault()!.ImageNameInFileSystem!, UploadDirectory.Product)
                            : String.Empty,
                            bp.Product.Price,
                            bp.Product.DiscountPrice,
                            bp.Quantity
                          



                            ))
                    .ToListAsync();

                return View(model);
            }



            //Case 3: Argument gonderilmeyib bu zaman cookiden oxu
            var productsCookieValue = HttpContext.Request.Cookies["wishlistproducts"];
            var productsCookieViewModel = new List<WishlistProductCookieVIewModel>();
            if (productsCookieValue is not null)
            {
                productsCookieViewModel = JsonSerializer.Deserialize<List<WishlistProductCookieVIewModel>>(productsCookieValue);
            }

            if (viewModel != null)
            {
                return View(viewModel);
            }


            return View(productsCookieViewModel);


        }
    }
}
