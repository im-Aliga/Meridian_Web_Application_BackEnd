using Meridian_Web.Areas.Admin.ViewModels.Navbar;
using Meridian_Web.Database;
using Meridian_Web.Database.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Meridian_Web.Areas.Admin.Controllers
{
    [Area("admin")]
    [Route("admin/navbar")]
    public class NavbarController : Controller
    {
        private readonly DataContext _dataContext;

        public NavbarController(DataContext dataContext)
        {
            _dataContext = dataContext;
        }
        #region List
        [HttpGet("list", Name = "admin-navbar-list")]
        public async Task<IActionResult> ListAsync()
        {
            var model = await _dataContext.Navbars
                .Select(n => new ListViewModel(n.Id, n.Title, n.RowNumber,n.Url,n.CreatedAt,n.UpdatedAt, n.IsShowHeader, n.IsShowFooter))
                .ToListAsync();

            return View(model);
        }
        #endregion

        #region Add
        [HttpGet("add", Name = "admin-navbar-add")]
        public async Task<IActionResult> AddAsync()
        {
            return View();
        }


        [HttpPost("add", Name = "admin-navbar-add")]
        public async Task<IActionResult> AddAsync(AddViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var navbar = new Navbar
            {
                Title = model.Title,
                Url = model.Url,
                RowNumber = model.Order,
                IsShowHeader = model.IsShowHeader,
                IsShowFooter = model.IsShowFooter

            };
            await _dataContext.Navbars.AddAsync(navbar);
            await _dataContext.SaveChangesAsync();
            return RedirectToRoute("admin-navbar-list");
        }
        #endregion

        #region Update
        [HttpGet("update/{id}", Name = "admin-navbar-update")]
        public async Task<IActionResult> UpdateAsync([FromRoute] int id)
        {
            var navbar = await _dataContext.Navbars.FirstOrDefaultAsync(n => n.Id == id);
            if (navbar is null)
            {
                return NotFound();
            }

            var model = new UpdateViewModel
            {
                Id = navbar.Id,
                Title = navbar.Title,
                Order = navbar.RowNumber,
                Url = navbar.Url,
                IsShowFooter = navbar.IsShowFooter,
                IsShowHeader = navbar.IsShowHeader,

            };

            return View(model);
        }

        [HttpPost("update/{id}", Name = "admin-navbar-update")]
        public async Task<IActionResult> UpdateAsync(UpdateViewModel model)
        {
            var navbar = await _dataContext.Navbars.FirstOrDefaultAsync(n => n.Id == model.Id);
            if (navbar is null)
            {
                return NotFound();
            }

            if (!ModelState.IsValid)
            {
                return View(model);
            }

            navbar.Title = model.Title;
            navbar.RowNumber = model.Order;
            navbar.Url = model.Url;
            navbar.IsShowHeader = model.IsShowHeader;
            navbar.IsShowFooter = model.IsShowFooter;

            await _dataContext.SaveChangesAsync();




            return RedirectToRoute("admin-navbar-list");





        }
        #endregion

        #region Delete
        [HttpPost("delete/{id}", Name = "admin-navbar-delete")]
        public async Task<IActionResult> DeleteAsync([FromRoute] int id)
        {
            var navbar = await _dataContext.Navbars.FirstOrDefaultAsync(b => b.Id == id);
            if (navbar is null)
            {
                return NotFound();
            }

            _dataContext.Navbars.Remove(navbar);
            await _dataContext.SaveChangesAsync();

            return RedirectToRoute("admin-navbar-list");
        }
        #endregion
    }
}
