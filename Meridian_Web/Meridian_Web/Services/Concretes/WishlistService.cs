using Meridian_Web.Areas.Client.ViewModels.Basket;
using Meridian_Web.Areas.Client.ViewModels.Wishlist;
using Meridian_Web.Contracts.File;
using Meridian_Web.Database;
using Meridian_Web.Database.Models;
using Meridian_Web.Services.Abstract;
using Meridian_Web.Services.Abstracts;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;

namespace Meridian_Web.Services.Concretes
{
    public class WishlistService :IWishlistService
    {
        private readonly DataContext _dataContext;
        private readonly IUserService _userService;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IFileService _fileService;

        public WishlistService(DataContext dataContext, IUserService userService, IHttpContextAccessor httpContextAccessor, IFileService fileService)
        {
            _dataContext = dataContext;
            _userService = userService;
            _httpContextAccessor = httpContextAccessor;
            _fileService = fileService;
        }

        public async Task<List<WishlistProductCookieVIewModel>> AddWishlistProductAsync(Product product)
        {
            if (_userService.IsAuthenticated)
            {
                await AddToDatabaseAsync();

                return new List<WishlistProductCookieVIewModel>();
            }


            return AddToCookie();






            async Task AddToDatabaseAsync()
            {
                var wishlistProduct = await _dataContext.WishlistProducts
                    .FirstOrDefaultAsync(bp => bp.Wishlist.UserId == _userService.CurrentUser.Id && bp.ProductId == product.Id);
                if (wishlistProduct is not null)
                {
                    wishlistProduct.Quantity++;
                }
                else
                {
                    var wishlist = await _dataContext.Wishlists.FirstAsync(b => b.UserId == _userService.CurrentUser.Id);

                    wishlistProduct = new WishlistProduct
                    {
                        Quantity = 1,
                        WishlistId = wishlist.Id,
                        ProductId = product.Id,
                    };

                    await _dataContext.WishlistProducts.AddAsync(wishlistProduct);
                }

                await _dataContext.SaveChangesAsync();
            }



            List<WishlistProductCookieVIewModel> AddToCookie()
            {

                var productCookieValue = _httpContextAccessor.HttpContext.Request.Cookies["wishlistproducts"];

                var productsCookieViewModel = productCookieValue is not null
                    ? JsonSerializer.Deserialize<List<WishlistProductCookieVIewModel>>(productCookieValue)
                    : new List<WishlistProductCookieVIewModel> { };

                var productCookieViewModel = productsCookieViewModel!.FirstOrDefault(pcvm => pcvm.Id == product.Id);

                if (productCookieViewModel is null)
                {
                    productsCookieViewModel
                        !.Add(new WishlistProductCookieVIewModel(
                        product.Id,
                        product.Title,
                        product.ProductImages!.Take(1)!.FirstOrDefault()! != null
                         ? _fileService.GetFileUrl(product.ProductImages!.Take(1)!.FirstOrDefault()!.ImageNameInFileSystem!, UploadDirectory.Product)
                         : string.Empty,
                        product.Price,
                        product.DiscountPrice,
                        1
                       
                        ));
                }
                else
                {
                    productCookieViewModel.Quantity += 1;
                  
                }

                _httpContextAccessor.HttpContext.Response.Cookies.Append("wishlistproducts", JsonSerializer.Serialize(productsCookieViewModel));

                return productsCookieViewModel;
            }
        }
    }
}
