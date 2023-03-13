using Meridian_Web.Areas.Client.ViewModels.Basket;
using Meridian_Web.Contracts.File;
using Meridian_Web.Database;
using Meridian_Web.Services.Abstracts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;

namespace Meridian_Web.Areas.Client.ViewComponents
{
    [ViewComponent(Name = "MiniBasketComponent")]
    public class MiniBasketComponent : ViewComponent
    {
        private readonly DataContext _dataContext;
        private readonly IUserService _userService;
        private readonly IFileService _fileService;

        public MiniBasketComponent(DataContext dataContext, IUserService userService, IFileService fileService)
        {
            _dataContext = dataContext;
            _userService = userService;
            _fileService = fileService;
        }

        public async Task<IViewComponentResult> InvokeAsync(List<ProductCookieViewModel>? viewModels = null)
        {
            // Case 1 : Qeydiyyat kecilib, o zaman bazadan gotur
            if (_userService.IsAuthenticated)
            {
                var model = await _dataContext.BasketProducts
                    .Where(bp => bp.Basket.UserId == _userService.CurrentUser.Id)
                    .Select(bp =>
                    new ProductCookieViewModel(
                            bp.ProductId,
                            bp.Product!.Title,
                            bp.Product.ProductImages.Take(1).FirstOrDefault() != null
                            ? _fileService.GetFileUrl(bp.Product.ProductImages.Take(1).FirstOrDefault()!.ImageNameInFileSystem, UploadDirectory.Product)
                            : String.Empty,
                            bp.Quantity,
                            bp.Product.Price,
                            bp.Product.Price * bp.Quantity))
                    .ToListAsync();

                return View(model);
            }

            //Case 2: Argument olaraq actiondan gonderilib
            if (viewModels is not null)
            {
                return View(viewModels);
            }

            //Case 3: Argument gonderilmeyib bu zaman cookiden oxu
            var productsCookieValue = HttpContext.Request.Cookies["products"];
            var productsCookieViewModel = new List<ProductCookieViewModel>();
            if (productsCookieValue is not null)
            {
                productsCookieViewModel = JsonSerializer.Deserialize<List<ProductCookieViewModel>>(productsCookieValue);
            }

            return View(productsCookieViewModel);
        }
    }
}
