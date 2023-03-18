using Meridian_Web.Areas.Client.ViewModels.Home;
using Meridian_Web.Contracts.File;
using Meridian_Web.Database;
using Meridian_Web.Services.Abstracts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Xml.Linq;

namespace Meridian_Web.Areas.Client.ViewComponents
{
    [ViewComponent(Name = "SortByOrderProductViewComponent")]
    public class SortByOrderProductViewComponent : ViewComponent
    {

        private readonly DataContext _datacontext;
        private readonly IFileService _fileService;
        public SortByOrderProductViewComponent(DataContext dataContext, IFileService fileService)
        {
            _datacontext = dataContext;
            _fileService = fileService;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var bestSeller= await _datacontext.OrderProducts.GroupBy(x => x.ProductId).OrderByDescending(o=>o.Count()).Take(4).Select(x=>x.Key).ToListAsync();

            var model = new HomeViewModel
            {
                Product = await _datacontext.Products.OrderByDescending(p=>p.Id).Where(p=>bestSeller.Contains(p.Id))
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
