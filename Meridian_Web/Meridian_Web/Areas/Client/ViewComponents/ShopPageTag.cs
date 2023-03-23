using Meridian_Web.Areas.Client.ViewModels.ShopPage;
using Meridian_Web.Database;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Xml.Linq;

namespace Meridian_Web.Areas.Client.ViewComponents
{
    [ViewComponent(Name = "ShopPageTag")]
    public class ShopPageTag:ViewComponent
    {
        public readonly DataContext _dataContext;

        public ShopPageTag(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {

            var model = await _dataContext.Tags.Select(c => new TagListItemViewModel(c.Id, c.TagName)).ToListAsync();

            return View(model);
        }

    }
}
