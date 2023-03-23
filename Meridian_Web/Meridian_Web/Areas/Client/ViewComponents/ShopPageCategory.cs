using Meridian_Web.Areas.Client.ViewModels.ShopPage;
using Meridian_Web.Database;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Xml.Linq;

namespace Meridian_Web.Areas.Client.ViewComponents
{
    [ViewComponent(Name = "ShopPageCategory")]
    public class ShopPageCategory:ViewComponent
    {
        public readonly DataContext _dataContext;

        public ShopPageCategory(DataContext dataContext)
        {
            _dataContext = dataContext;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {

            var model = await _dataContext.Categories.Select(c => new CategoryListItemViewModel(c.Id, c.Title)).ToListAsync();

            return View(model);
        }
    }
}
