using Meridian_Web.Areas.Client.ViewModels.Home;
using Meridian_Web.Contracts.File;
using Meridian_Web.Database;
using Meridian_Web.Services.Abstracts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Xml.Linq;

namespace Meridian_Web.Areas.Client.ViewComponents
{
    [ViewComponent(Name = "NewProductViewComponent")]
    public class NewProductViewComponent :ViewComponent
    {

        private readonly DataContext _datacontext;
        private readonly IFileService _fileService;
        public NewProductViewComponent(DataContext dataContext, IFileService fileService)
        {
            _datacontext = dataContext;
            _fileService = fileService;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var model = new HomeViewModel
            {
                Product = await _datacontext.Products.OrderByDescending(p => p.CreatedAt).Take(8)
                  .Select(b => new ProductListItemViewModel(
                    b.Id,
                    b.Title,
                    b.Price,
                    b.DiscountPrice,
                    b.ProductCatagories.Select(pc => new CategoriesList(pc.Category.Title)).ToList(),
                    b.ProductImages!.Take(1)!.FirstOrDefault() != null
                        ? _fileService.GetFileUrl(b.ProductImages!.Take(1)!.FirstOrDefault()!.ImageNameInFileSystem!, UploadDirectory.Product)
                        : string.Empty

                  ))
                  .ToListAsync()


            };
            return View(model);
        }
    }
}
