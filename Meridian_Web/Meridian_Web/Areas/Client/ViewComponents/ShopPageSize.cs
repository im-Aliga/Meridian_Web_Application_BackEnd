using Meridian_Web.Areas.Client.ViewModels.ShopPage;
using Meridian_Web.Database;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Xml.Linq;

namespace Meridian_Web.Areas.Client.ViewComponents
{
    [ViewComponent(Name = "ShopPageSize")]
    public class ShopPageSize:ViewComponent
    {
        public readonly DataContext _dataContext;

        public ShopPageSize(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {

            var model = await _dataContext.Colors.Select(c => new SizeListItemViewModel(c.Id, c.Name)).ToListAsync();

            return View(model);
        }
    }
}
