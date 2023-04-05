
using BackEndFinalProject.Areas.Client.ViewModels.Home.Modal;
using Meridian_Web.Areas.Client.ViewModels.Basket;
using Meridian_Web.Contracts.File;
using Meridian_Web.Contracts.Identity;
using Meridian_Web.Database;
using Meridian_Web.Database.Models;
using Meridian_Web.Exceptions;
using Meridian_Web.Services.Abstract;
using Meridian_Web.Services.Abstracts;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using System.Security.Cryptography.X509Certificates;
using System.Text.Json;

namespace Meridian_Web.Services.Concretes
{
    public class BasketService : IBasketService
    {
        private readonly DataContext _dataContext;
        private readonly IUserService _userService;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IFileService _fileService;

        public BasketService(DataContext dataContext, IUserService userService, IHttpContextAccessor httpContextAccessor, IFileService fileService)
        {
            _dataContext = dataContext;
            _userService = userService;
            _httpContextAccessor = httpContextAccessor;
            _fileService = fileService;
        }


        public async Task<List<ProductCookieViewModel>> AddBasketProductAsync(Product product,ModalViewModel model)
        {
            

            if (_userService.IsAuthenticated)
            {
                await AddToDatabaseAsync();

                return new List<ProductCookieViewModel>();
            }


            return AddToCookie();





            
            async Task AddToDatabaseAsync()
            {
                var basketProduct = await _dataContext.BasketProducts.Include(p=>p.Basket)
                    .FirstOrDefaultAsync(bp => bp.Basket.UserId == _userService.CurrentUser.Id && bp.ProductId == product.Id&&bp.SizeId==model.SizeId &&bp.ColorId==model.ColorId);
                if (basketProduct is not null)
                {
                    basketProduct.Quantity++;
                }
                else
                {
                    var basket = await _dataContext.Baskets.FirstAsync(b => b.UserId == _userService.CurrentUser.Id);

                    basketProduct = new BasketProduct
                    {
                        Quantity = 1,
                        BasketId = basket.Id,
                        ProductId = product.Id,
                        SizeId=model.SizeId,
                        ColorId=model.ColorId
                    };

                    await _dataContext.BasketProducts.AddAsync(basketProduct);
                }

                await _dataContext.SaveChangesAsync();
            }


           
            List<ProductCookieViewModel> AddToCookie()
            {
                model = new ModalViewModel
                {
                    SizeId = model.SizeId != null ? model.SizeId : _dataContext.Sizes.FirstOrDefault().Id,
                    ColorId = model.ColorId != null ? model.ColorId : _dataContext.Colors.FirstOrDefault().Id,
                };

                var productCookieValue = _httpContextAccessor.HttpContext.Request.Cookies["products"];
                var productsCookieViewModel = productCookieValue is not null
                    ? JsonSerializer.Deserialize<List<ProductCookieViewModel>>(productCookieValue)
                    : new List<ProductCookieViewModel> { };

                var productCookieViewModel = productsCookieViewModel!.FirstOrDefault(pcvm => pcvm.Id == product.Id&&pcvm.SizeId==model.SizeId&&pcvm.ColorId==model.ColorId);

                if (productCookieViewModel is null || productCookieViewModel.SizeId != model.SizeId||productCookieViewModel.ColorId!=model.ColorId)
                {
                    productsCookieViewModel
                        !.Add(new ProductCookieViewModel(
                        product.Id,
                        product.Title,
                        product.ProductImages!.Take(1)!.FirstOrDefault()! != null
                         ? _fileService.GetFileUrl(product.ProductImages!.Take(1)!.FirstOrDefault()!.ImageNameInFileSystem!, UploadDirectory.Product)
                         : string.Empty,
                        1,
                        product.Price,
                        product.DiscountPrice,
                        product.Price,
                        model.SizeId,
                       _dataContext.ProductSizes
                       .Include(ps => ps.Size)
                       .Where(ps => ps.ProductId == product.Id)
                       .Select(ps => new SizeListItemViewModel(ps.SizeId, ps.Size.Name)).ToList(),
                        model.ColorId,
                        _dataContext.ProductColors
                       .Include(ps => ps.Color)
                       .Where(ps => ps.ProductId == product.Id)
                       .Select(ps => new ColorListItemViewModel(ps.ColorId, ps.Color.Name)).ToList()
                        ));
                }
                else
                {
                    productCookieViewModel.Quantity += 1;
                    productCookieViewModel.Total = productCookieViewModel.Quantity * productCookieViewModel.Price;
                }

                _httpContextAccessor.HttpContext.Response.Cookies.Append("products", JsonSerializer.Serialize(productsCookieViewModel));

                return productsCookieViewModel;
            }
        }
    }
}
