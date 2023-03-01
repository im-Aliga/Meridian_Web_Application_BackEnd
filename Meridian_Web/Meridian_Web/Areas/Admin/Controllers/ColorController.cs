using Meridian_Web.Areas.Admin.ViewModels.Color;
using Meridian_Web.Database;
using Meridian_Web.Database.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Meridian_Web.Areas.Admin.Controllers
{
    [Area("admin")]
    [Route("admin/color")]
    public class ColorController : Controller
    {
        private readonly DataContext _dataContext;
        private readonly ILogger<ColorController> _logger;

        public ColorController(DataContext dataContext, ILogger<ColorController> logger)
        {
            _dataContext = dataContext;
            _logger = logger;
        }
        #region List
        [HttpGet("list", Name = "admin-color-list")]
        public async Task<IActionResult> ListAsync()
        {
            var model = await _dataContext.Colors
                .Select(c => new ListItemViewModel(c.Id, c.Name, c.CreatedAt, c.UpdatedAt))
                .ToListAsync();

            return View(model);
        }
        #endregion

        #region Add
        [HttpGet("add", Name = "admin-color-add")]
        public async Task<IActionResult> AddAsync()
        {
            return View();
        }
        [HttpPost("add", Name = "admin-color-add")]
        public async Task<IActionResult> AddAsync(AddViewModel model)
        {
            if (!ModelState.IsValid) return View(model);


            var color = new Color
            {
                Name = model.Name,
            };
            await _dataContext.Colors.AddAsync(color);
            await _dataContext.SaveChangesAsync();
            return RedirectToRoute("admin-color-list");
        }
        #endregion

        #region Update
        [HttpGet("update/{id}", Name = "admin-color-update")]
        public async Task<IActionResult> UpdateAsync([FromRoute] int id)
        {
            var color = await _dataContext.Colors.FirstOrDefaultAsync(n => n.Id == id);
            if (color is null) return NotFound();

            var model = new UpdateViewModel
            {
                Id = id,
                Name = color.Name,

            };

            return View(model);
        }
        [HttpPost("update/{id}", Name = "admin-color-update")]
        public async Task<IActionResult> UpdateAsync(UpdateViewModel model)
        {
            var color = await _dataContext.Colors.FirstOrDefaultAsync(n => n.Id == model.Id);
            if (color is null) return NotFound();
            if (!ModelState.IsValid) return View(model);
            if (!_dataContext.Colors.Any(n => n.Id == model.Id)) return View(model);

            color.Name = model.Name;
            await _dataContext.SaveChangesAsync();

            return RedirectToRoute("admin-color-list");

        }

        #endregion

        #region Delete
        [HttpPost("delete/{id}", Name = "admin-color-delete")]
        public async Task<IActionResult> DeleteAsync(UpdateViewModel model)
        {
            var color = await _dataContext.Colors.FirstOrDefaultAsync(n => n.Id == model.Id);
            if (color is null) return NotFound();


            _dataContext.Colors.Remove(color);
            await _dataContext.SaveChangesAsync();



            return RedirectToRoute("admin-color-list");

        }
        #endregion
    }
}
