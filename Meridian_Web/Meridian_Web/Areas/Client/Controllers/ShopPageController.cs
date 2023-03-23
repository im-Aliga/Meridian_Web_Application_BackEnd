using Meridian_Web.Areas.Client.ViewModels.ShopPage;
using Meridian_Web.Contracts.File;
using Meridian_Web.Database;
using Meridian_Web.Services.Abstract;
using Meridian_Web.Services.Abstracts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using static Meridian_Web.Areas.Client.ViewModels.ShopPage.ListItemViewModel;

namespace Meridian_Web.Areas.Client.Controllers
{
    [Area("client")]
    [Route("shoppage")]
    public class ShopPageController : Controller
    {
        private readonly DataContext _dataContext;
        private readonly IBasketService _basketService;
        private readonly IUserService _userService;
        private readonly IFileService _fileService;

        public ShopPageController(DataContext dataContext, IBasketService basketService, IUserService userService, IFileService fileService)
        {
            _dataContext = dataContext;
            _basketService = basketService;
            _userService = userService;
            _fileService = fileService;
        }

        [HttpGet("index", Name = "client-shoppage-index")]
        public async Task<IActionResult> Index(string searchBy, string search, [FromQuery]int?sizeId ,[FromQuery]int?brandId, [FromQuery] int? categoryId, [FromQuery] int? colorId, [FromQuery] int? tagId)
        {
            var productsQuery = _dataContext.Products.AsQueryable();

            if (searchBy == "Name")
            {
                productsQuery = productsQuery.Where(p => p.Title.StartsWith(search) || Convert.ToString(p.Price).StartsWith(search) || search == null);
            }
            else if (categoryId is not null || colorId is not null || tagId is not null || brandId is not null ||sizeId is not null) 
            {
                productsQuery = productsQuery
                    .Include(p => p.ProductCatagories)
                    .Include(p => p.ProductColors)
                    .Include(p => p.ProductTags)
                    .Include(p => p.ProductSizes)
                    .Include(p => p.ProductBrands)
                    .Where(p => categoryId == null || p.ProductCatagories!.Any(pc => pc.CategoryId == categoryId))
                    .Where(p => colorId == null || p.ProductColors!.Any(pc => pc.ColorId == colorId))
                    .Where(p => tagId == null || p.ProductTags!.Any(pt => pt.TagId == tagId))
                    .Where(p => sizeId == null || p.ProductSizes!.Any(ps => ps.SizeId == sizeId))
                    .Where(p => brandId == null || p.ProductBrands!.Any(ps => ps.BrandId == brandId));

            }
            else
            {
                productsQuery = productsQuery.OrderBy(p => p.Price);
            }

            var newProduct = await productsQuery.Select(p => new ListItemViewModel(
                               p.Id,
                               p.Title,
                               p.Content,
                               p.Price,
                               p.DiscountPrice,
                               p.CreatedAt,
                               p.ProductImages.Take(1).FirstOrDefault() != null
                               ? _fileService.GetFileUrl(p.ProductImages.Take(1).FirstOrDefault()!.ImageNameInFileSystem, UploadDirectory.Product)
                               : String.Empty,
                                p.ProductCatagories.Select(p => p.Category).Select(p => new CategoryViewModeL(p.Title)).ToList(),
                                p.ProductColors.Select(p => p.Color).Select(p => new ColorViewModeL(p.Name)).ToList(),
                                p.ProductSizes.Select(p => p.Size).Select(p => new SizeViewModeL(p.Name)).ToList(),
                                p.ProductTags.Select(p => p.Tag).Select(p => new TagViewModel(p.TagName)).ToList(),
                                p.ProductBrands.Select(p=>p.Brand).Select(p=> new BrandViewModel(p.Name)).ToList()
                                )).ToListAsync();

            return View(newProduct);

        }
    }
}
