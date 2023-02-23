using BackEndFinalProject.Areas.Admin.ViewModels.About;
using Meridian_Web.Database;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Meridian_Web.Areas.Admin.Controllers
{
    [Area("admin")]
    [Route("admin/about")]
    public class AboutController : Controller
    {
        private readonly DataContext _dataContext;

        public AboutController(DataContext dataContext)
        {
            _dataContext = dataContext;
        }


        #region List
        [HttpGet("list", Name = "admin-about-list")]
        public async Task<IActionResult> ListAsync()
        {
            var model = await _dataContext.Abouts
                .Select(a => new ListItemViewModel(a.Id,a.Title, a.Context,a.UpdatedAt))
                .ToListAsync();

            return View(model);
        }
        #endregion

        #region Update
        [HttpGet("update/{id}", Name = "admin-about-update")]
        public async Task<IActionResult> UpdateAsync([FromRoute] int id)
        {
            var about = await _dataContext.Abouts.FirstOrDefaultAsync(n => n.Id == id);


            if (about is null) return NotFound();

            var model = new UpdateViewModel
            {
                Id = id,
                Title = about.Title,
                Content = about.Context,


            };

            return View(model);
        }
        [HttpPost("update/{id}", Name = "admin-about-update")]
        public async Task<IActionResult> UpdateAsync(UpdateViewModel model)
        {
            var about = await _dataContext.Abouts.FirstOrDefaultAsync(n => n.Id == model.Id);
            if (about is null) return NotFound();
            if (!ModelState.IsValid) return View(model);
            if (!_dataContext.Abouts.Any(n => n.Id == model.Id)) return View(model);





            about.Context = model.Content;
            about.Title = model.Title;

            await _dataContext.SaveChangesAsync();

            return RedirectToRoute("admin-about-list");

        }
        #endregion
    }
}
