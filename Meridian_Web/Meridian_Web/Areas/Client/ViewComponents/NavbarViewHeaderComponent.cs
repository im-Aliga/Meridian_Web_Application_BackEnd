
using Meridian_Web.Database;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;


namespace Meridian_Web.Areas.Admin.ViewComponents
{
    [ViewComponent(Name = "NavbarViewHeaderComponent")]
    public class NavbarViewHeaderComponent : ViewComponent
    {
        private readonly DataContext _datacontext;
        public NavbarViewHeaderComponent(DataContext dataContext)
        {
            _datacontext = dataContext;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var model =await _datacontext.Navbars.Include(n => n.SubNavbars.OrderBy(sn => sn.RowNumber)).Where(n => n.IsShowHeader).OrderBy(n => n.RowNumber).ToListAsync();

            return View( model);
        }
    }
}
