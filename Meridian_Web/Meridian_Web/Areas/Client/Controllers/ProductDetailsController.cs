using Meridian_Web.Areas.Client.ViewModels.Home;
using Meridian_Web.Areas.Client.ViewModels.ProductDetails;
using Meridian_Web.Contracts.File;
using Meridian_Web.Database;
using Meridian_Web.Services.Abstracts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Meridian_Web.Areas.Client.Controllers
{
    [Area("client")]
    [Route("productdetails")]
    public class ProductDetailsController : Controller
    {
        private readonly DataContext _dbContext;
        private readonly IFileService _fileService;

        public ProductDetailsController(DataContext dbContext, IFileService fileService)
        {
            _dbContext = dbContext;
            _fileService = fileService;
        }
        [HttpGet("index/{id}", Name = "client-productdetails-index")]
        public async Task<IActionResult> IndexAsync(int id)
        {
            var product = await _dbContext.Products
                .Include(p => p.ProductImages)
                .Include(p => p.ProductSizes)
                .Include(p => p.ProductColors)
                .Include(p=>p.ProductDisconts)
                .FirstOrDefaultAsync(p => p.Id == id);


            if (product is null)
            {
                return NotFound();
            }

            //var catProducts = await _dbContext
            //    .pro.GroupBy(pc => pc.CategoryId).Select(pc => pc.Key).ToListAsync();


            var model = new ProductDetailsViewModel
            {
                Id = product.Id,
                Title = product.Title,
                Description = product.Content,
                Price = product.Price,
                DiscountPrice = product.DiscountPrice,
                InStock = product.InStock,

                Colors = _dbContext.ProductColors.Include(pc => pc.Color).Where(pc => pc.ProductId == product.Id)
                          .Select(pc => new ProductDetailsViewModel.ColorViewModeL(pc.Color.Name, pc.Color.Id)).ToList(),

                Sizes = _dbContext.ProductSizes.Include(ps => ps.Size).Where(ps => ps.ProductId == product.Id)
                       .Select(ps => new ProductDetailsViewModel.SizeViewModeL(ps.Size.Name, ps.Size.Id)).ToList(),
                Discounts = _dbContext.ProductDisconts.Include(ps => ps.Discont).Where(ps => ps.ProductId == product.Id)
                       .Select(ps => new ProductDetailsViewModel.DiscountList(ps.Discont.Id, ps.Discont.DiscontPers)).ToList(),

                Images = _dbContext.ProductImages.Where(p => p.ProductId == product.Id)
                .Select(p => new ProductDetailsViewModel.ImageViewModeL
                (_fileService.GetFileUrl(p.ImageNameInFileSystem, UploadDirectory.Product))).ToList(),


                Products = await _dbContext.ProductCatagories.Include(p => p.Product).Where(pc => pc.ProductId != product.Id)
                .Select(pc => new ProductListItemViewModel(
                    pc.ProductId,
                    pc.Product.Title,
                    pc.Product.Price,
                    pc.Product.DiscountPrice,
                    pc.Product.ProductCatagories.Select(pc=>new CategoriesList(pc.Category.Title)).ToList(),
                    pc.Product.ProductImages.Take(1).FirstOrDefault() != null
                   ? _fileService.GetFileUrl(pc.Product.ProductImages.Take(1).FirstOrDefault().ImageNameInFileSystem, UploadDirectory.Product)
                   : String.Empty
               )).ToListAsync(),

            };

            return View(model);
        }
    }
}
