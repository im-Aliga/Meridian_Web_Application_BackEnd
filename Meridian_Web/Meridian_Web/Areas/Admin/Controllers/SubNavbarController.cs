using Meridian_Web.Areas.Admin.ViewModels.SubNavbar;
using Meridian_Web.Database;
using Meridian_Web.Database.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Meridian_Web.Areas.Admin.Controllers
{
    [Area("admin")]
    [Route("admin/subnavbar")]
    public class SubNavbarController : Controller
    {
        private readonly DataContext _dataContext;
        public SubNavbarController(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        #region List
        [HttpGet("list", Name = "admin-subnavbar-list")]
        public async Task<IActionResult> ListAsync()
        {
            var model = await _dataContext.SubNavbars
                .Select(sn => new ListViewModel(sn.Id, sn.Title, sn.RowNumber,sn.CreatedAt,sn.UpdatedAt, sn.Url, $"{sn.Navbar.Title}"))
                .ToListAsync();

            return View(model);
        }
        #endregion

        #region Add
        [HttpGet("add", Name = "admin-subnavbar-add")]
        public async Task<IActionResult> AddAsync()
        {
            var model = new AddViewModel
            {
                Navbar = await _dataContext.Navbars
                    .Select(a => new NavbarListItemViewModel(a.Id, $" {a.Title}"))
                    .ToListAsync()

            };

            return View(model);
        }

        [HttpPost("add", Name = "admin-subnavbar-add")]
        public async Task<IActionResult> AddAsync(AddViewModel model)
        {
            if (!ModelState.IsValid)
            {
                var subnav = new AddViewModel
                {
                    Navbar = await _dataContext.Navbars
                   .Select(a => new NavbarListItemViewModel(a.Id, $" {a.Title}"))
                   .ToListAsync()

                };
                return View(subnav);
            }

            var subNavbar = new SubNavbar()
            {
                Title = model.Title,
                Url = model.Url,
                RowNumber = model.Order,
                NavbarId = model.NavbarId,
            };
            await _dataContext.SubNavbars.AddAsync(subNavbar);
            await _dataContext.SaveChangesAsync();

            return RedirectToRoute("admin-subnavbar-list");
        }
        #endregion

        #region Update
        [HttpGet("update/{id}", Name = "admin-subnavbar-update")]
        public async Task<IActionResult> UpdateAsync([FromRoute] int id)
        {
            var subnavbar = await _dataContext.SubNavbars.FirstOrDefaultAsync(b => b.Id == id);
            if (subnavbar is null)
            {
                return NotFound();
            }


            var model = new UpdateViewModel
            {

                Title = subnavbar.Title,
                Url = subnavbar.Url,
                Order = subnavbar.RowNumber,
                Navbars = _dataContext.Navbars.Select(n => new NavbarListItemViewModel(n.Id, n.Title)).ToList()

            };

            return View(model);
        }

        [HttpPost("update/{id}", Name = "admin-subnavbar-update")]
        public async Task<IActionResult> UpdateAsync(UpdateViewModel model)
        {
            var subnavbar = await _dataContext.SubNavbars.Include(n => n.Navbar).FirstOrDefaultAsync(n => n.Id == model.Id);
            if (subnavbar is null)
            {
                return NotFound();
            }
            if (!ModelState.IsValid)
            {
                var subnav = new UpdateViewModel
                {

                    Title = subnavbar.Title,
                    Url = subnavbar.Url,
                    Order = subnavbar.RowNumber,
                    Navbars = _dataContext.Navbars.Select(n => new NavbarListItemViewModel(n.Id, n.Title)).ToList()

                };
                return View(subnav);
            }

            subnavbar.Title = model.Title;
            subnavbar.Url = model.Url;
            subnavbar.RowNumber = model.Order;
            subnavbar.NavbarId = model.NavbarId;

            await _dataContext.SaveChangesAsync();
            return RedirectToRoute("admin-subnavbar-list");
        }
        #endregion

        #region Delete
        [HttpPost("delete/{id}", Name = "admin-subnavbar-delete")]
        public async Task<IActionResult> DeleteAsync([FromRoute] int id)
        {
            var model = await _dataContext.SubNavbars.FirstOrDefaultAsync(b => b.Id == id);
            if (model is null)
            {
                return NotFound();
            }

            _dataContext.SubNavbars.Remove(model);
            await _dataContext.SaveChangesAsync();

            return RedirectToRoute("admin-subnavbar-list");
        }
        #endregion
    }
}
