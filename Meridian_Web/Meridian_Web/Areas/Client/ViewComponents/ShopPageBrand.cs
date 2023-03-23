using Meridian_Web.Areas.Client.ViewModels.ShopPage;
using Meridian_Web.Database;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Xml.Linq;

namespace Meridian_Web.Areas.Client.ViewComponents
{
    [ViewComponent(Name = "ShopPageBrand")]
    public class ShopPageBrand:ViewComponent
    {
        public readonly DataContext _dataContext;

        public ShopPageBrand(DataContext dataContext)
        {
            _dataContext = dataContext;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {

            var model = await _dataContext.Brands.Select(c => new BrandListItemVIewModel(c.Id, c.Name)).ToListAsync();

            return View(model);
        }


    }
}
