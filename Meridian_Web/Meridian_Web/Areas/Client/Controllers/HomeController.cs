
using BackEndFinalProject.Areas.Client.ViewModels.Home.Modal;
using Meridian_Web.Areas.Client.ViewModels.Home;
using Meridian_Web.Contracts.File;
using Meridian_Web.Database;
using Meridian_Web.Database.Models;
using Meridian_Web.Services.Abstracts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;


namespace Meridian_Web.Areas.Client.Controllers
{
    [Area("client")]
    [Route("home")]
    public class HomeController : Controller
    {

        private readonly DataContext _dbContext;
        private readonly IFileService _fileService;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public HomeController(DataContext dbContext, IFileService fileService, IHttpContextAccessor httpContextAccessor)
        {
            _dbContext = dbContext;
            _fileService = fileService;
            _httpContextAccessor = httpContextAccessor;
        }

        [HttpGet("~/", Name = "client-home-index")]
        [HttpGet("index")]
        public async Task<IActionResult> IndexAsync()
        {

            var model = new HomeViewModel
            {

                Sliders = await _dbContext.Sliders.OrderBy(s => s.Order).Select(b => new SliderListItemVIewModel(
                       b.Title,
                       b.OfferContext,
                       b.Content,
                       b.StartPrice,
                       b.ButtonName,
                       b.BtnRedirectUrl,
                       b.Order,
                       _fileService.GetFileUrl(b.BgImageNameInFileSystem, UploadDirectory.Slider)
                       ))
                   .ToListAsync(),

                GlobalOffers = await _dbContext.GlobalOffers.Select(go => new GlobalOfferViewModel(
                       go.Title,
                       go.MainContext,
                       go.Context,
                       go.ButtonContext,
                       go.OfferTime
                         ))
                .ToListAsync(),
                Banners = await _dbContext.Banners.Select(b => new BannerListItemViewModel(
                       b.Title,
                       b.MainContext,
                       b.Context,
                       _fileService.GetFileUrl(b.PhoteInFileSystem, UploadDirectory.Banner)
                       ))
                .ToListAsync(),
                PaymentBenefits = await _dbContext.Payments.Select(b => new PaymentBenefitsViewModel(
                       b.Title,
                       b.Context,
                       _fileService.GetFileUrl(b.ImageNameInFileSystem, UploadDirectory.Payment)
                       ))
                .ToListAsync(),
                Blogs = await _dbContext.Blogs.Include(b => b.BlogTags).Select(b => new BlogListItemViewModel(

                       b.Title,
                       b.Description,
                       b.CreatedAt,
                       b.BlogTags.Select(b => new TagList(b.Tag.TagName)).ToList(),
                       b.BlogFile!.Take(1)!.FirstOrDefault() != null
                        ? _fileService.GetFileUrl(b.BlogFile!.Take(1)!.FirstOrDefault()!.FileNameInFileSystem!, UploadDirectory.Blog)
                        : string.Empty,
                       b.Id
                    ))
                .ToListAsync(),

                FeedBacks = await _dbContext.FeedBacks.Select(f => new FeedBackListItemViewModel(
                         f.FullName,
                         f.Context,
                         f.Role,
                         _fileService.GetFileUrl(f.ProfilePhoteInFileSystem, UploadDirectory.FeedBack)
                    ))
               .ToListAsync(),
                Brands = await _dbContext.Brands.Select(b => new BrandListItemVIewModel(
                    _fileService.GetFileUrl(b.PhoteInFileSystem, UploadDirectory.Brand)
                    ))
              .ToListAsync(),

            };

            return View(model);

        }

        [HttpGet("modal/{id}", Name = "product-modal")]
        public async Task<ActionResult> ModalAsync(int id)
        {
            var product = await _dbContext.Products
                .Include(p => p.ProductImages)
                .Include(p => p.ProductDisconts)
                .Include(p => p.ProductColors)
                .Include(p => p.ProductSizes)
                 .FirstOrDefaultAsync(p => p.Id == id);


            if (product is null)
            {
                return NotFound();
            }
            var productDisconts = await _dbContext.ProductDisconts
                .Where(pd => pd.ProductId == product.Id)
                .Include(pd => pd.Discont)
                .Select(pd => new ModalViewModel.DiscountList(pd.Discont.Id, pd.Discont.DiscontPers)).ToListAsync();

            var productColors = await _dbContext.ProductColors
                .Where(pc => pc.ProductId == product.Id)
                .Include(pc => pc.Color)
                .Select(pc => new ModalViewModel.ColorViewModeL(pc.Color.Name, pc.Color.Id)).ToListAsync();

            var productSize = await _dbContext.ProductSizes
                .Where(ps => ps.ProductId == product.Id)
                .Include(pc => pc.Size)
                .Select(pc => new ModalViewModel.SizeViewModeL(pc.Size.Name, pc.Size.Id)).ToListAsync();


            var model = new ModalViewModel(
                product.Id,
                product.Title,
                product.Content,
                product.Price,
                product.DiscountPrice != null ? product.DiscountPrice : null,
                product.InStock,
                product.ProductImages!.Take(4).FirstOrDefault() != null
                ? _fileService.GetFileUrl(product.ProductImages.Take(4).FirstOrDefault()!.ImageNameInFileSystem, UploadDirectory.Product)
                : String.Empty,
                 productDisconts,
                 productColors,
                 productSize
                );

            return PartialView("~/Areas/Client/Views/Shared/Partials/_ModalPartial.cshtml", model);
        }

        [HttpGet("indexsearch", Name = "client-homesearch-index")]
        public async Task<IActionResult> Search(string searchBy, string search)
        {

            return RedirectToAction("Index", "ShopPage", new { searchBy = searchBy, search = search });

        }


    }
}
