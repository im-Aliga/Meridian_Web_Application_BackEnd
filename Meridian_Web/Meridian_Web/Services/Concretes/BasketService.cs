
//using Meridian_Web.Areas.Client.ViewModels.Basket;
//using Meridian_Web.Contracts.File;
//using Meridian_Web.Contracts.Identity;
//using Meridian_Web.Database;
//using Meridian_Web.Database.Models;
//using Meridian_Web.Exceptions;
//using Meridian_Web.Services.Abstracts;
//using Microsoft.AspNetCore.Authentication;
//using Microsoft.AspNetCore.Authentication.Cookies;
//using Microsoft.EntityFrameworkCore;
//using System.Security.Claims;
//using System.Security.Cryptography.X509Certificates;
//using System.Text.Json;

//namespace BackEndFinalProject.Services.Concretes
//{
//    public class BasketService : IBasketService
//    {
//        private readonly DataContext _dataContext;
//        private readonly IUserService _userService;
//        private readonly IHttpContextAccessor _httpContextAccessor;
//        private readonly IFileService _fileService;

//        public BasketService(DataContext dataContext, IUserService userService, IHttpContextAccessor httpContextAccessor, IFileService fileService)
//        {
//            _dataContext = dataContext;
//            _userService = userService;
//            _httpContextAccessor = httpContextAccessor;
//            _fileService = fileService;
//        }


//        public async Task<List<ProductCookieViewModel>> AddBasketProductAsync(Plant plant)
//        {
//            if (_userService.IsAuthenticated)
//            {
//                await AddToDatabaseAsync();

//                return new List<ProductCookieViewModel>();
//            }
             
//            return AddToCookie();





//            //Add product to database if user is authenticated
//            async Task AddToDatabaseAsync()
//            {
//                var basketProduct = await _dataContext.BasketProducts
//                    .FirstOrDefaultAsync(bp => bp.Basket.UserId == _userService.CurrentUser.Id && bp.PlantId == plant.Id);
//                if (basketProduct is not null)
//                {
//                    basketProduct.Quantity++;
//                }
//                else
//                {
//                    var basket = await _dataContext.Baskets.FirstAsync(b => b.UserId == _userService.CurrentUser.Id);

//                    basketProduct = new BasketProduct
//                    {
//                        Quantity = 1,
//                        BasketId = basket.Id,
//                        PlantId = plant.Id,
//                    };

//                    await _dataContext.BasketProducts.AddAsync(basketProduct);
//                }

//                await _dataContext.SaveChangesAsync();
//            }


//            //Add product to cookie if user is not authenticated 
//            List<ProductCookieViewModel> AddToCookie()
//            {
                
//                var productCookieValue = _httpContextAccessor.HttpContext.Request.Cookies["products"];
//                var productsCookieViewModel = productCookieValue is not null
//                    ? JsonSerializer.Deserialize<List<ProductCookieViewModel>>(productCookieValue)
//                    : new List<ProductCookieViewModel> { };

//                var productCookieViewModel = productsCookieViewModel!.FirstOrDefault(pcvm => pcvm.Id == plant.Id);

//                if (productCookieViewModel is null)
//                {
//                    productsCookieViewModel
//                        !.Add(new ProductCookieViewModel(
//                        plant.Id,
//                        plant.Title, 
//                        plant.PlantImages!.Take(1)!.FirstOrDefault()! != null
//                         ? _fileService.GetFileUrl(plant.PlantImages!.Take(1)!.FirstOrDefault()!.ImageNameInFileSystem!, UploadDirectory.Plant)
//                         : string.Empty, 
//                        1, 
//                        plant.Price,
//                        plant.Price));
//                }
//                else
//                {
//                    productCookieViewModel.Quantity += 1;
//                    productCookieViewModel.Total = productCookieViewModel.Quantity * productCookieViewModel.Price;
//                }

//                _httpContextAccessor.HttpContext.Response.Cookies.Append("products", JsonSerializer.Serialize(productsCookieViewModel));

//                return productsCookieViewModel; 
//            }
//        }
//    }
//}
