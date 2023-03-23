using Meridian_Web.Areas.Client.ViewModels.ShopPage;
using Meridian_Web.Database;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
namespace Meridian_Web.Areas.Client.ViewComponents
{
    [ViewComponent(Name = "ShopPageColor")]
    public class ShopPageColor:ViewComponent
    {
        public readonly DataContext _dataContext;

        public ShopPageColor(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {

            var model = await _dataContext.Colors.Select(c => new ColorListItemViewModel(c.Id, c.Name)).ToListAsync();

            return View(model);
        }
    }
}
