using Meridian_Web.Areas.Client.ViewModels.Price;
using Meridian_Web.Database;
using Microsoft.AspNetCore.Mvc;
using System.Xml.Linq;

namespace Meridian_Web.Areas.Client.ViewComponents
{
    [ViewComponent(Name = "ShopPagePrice")]
    public class ShopPagePrice : ViewComponent
    {
        public readonly DataContext _dataContext;

        public ShopPagePrice(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public async Task<IViewComponentResult> InvokeAsync(PriceViewModel model)
        {
            return View(model);
        }

    }
}
