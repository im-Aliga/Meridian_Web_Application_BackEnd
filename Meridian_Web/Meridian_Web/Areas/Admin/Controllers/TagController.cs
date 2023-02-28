using Meridian_Web.Areas.Admin.ViewModels.Tag;
using Meridian_Web.Database;
using Meridian_Web.Database.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Meridian_Web.Areas.Admin.Controllers
{
    [Area("admin")]
    [Route("admin/tag")]
    public class TagController : Controller
    {
        private readonly DataContext _dataContext;
        private readonly ILogger<TagController> _logger;

        public TagController(DataContext dataContext, ILogger<TagController> logger)
        {
            _dataContext = dataContext;
            _logger = logger;
        }

        #region List
        [HttpGet("list", Name = "admin-tag-list")]
        public async Task<IActionResult> ListAsync()
        {
            var model = await _dataContext.Tags
                .Select(c => new ListItemViewModel(c.Id, c.TagName, c.CreatedAt, c.UpdatedAt))
                .ToListAsync();

            return View(model);
        } 
        #endregion

        #region Add
        [HttpGet("add", Name = "admin-tag-add")]
        public async Task<IActionResult> AddAsync()
        {
            return View();
        }
        [HttpPost("add", Name = "admin-tag-add")]
        public async Task<IActionResult> AddAsync(AddViewModel model)
        {
            if (!ModelState.IsValid) return View(model);


            var tag = new Tag
            {

                TagName = model.Tagname,
            };
            await _dataContext.Tags.AddAsync(tag);
            await _dataContext.SaveChangesAsync();
            return RedirectToRoute("admin-tag-list");
        }
        #endregion

        #region Update
        [HttpGet("update/{id}", Name = "admin-tag-update")]
        public async Task<IActionResult> UpdateAsync([FromRoute] int id)
        {
            var tag = await _dataContext.Tags.FirstOrDefaultAsync(n => n.Id == id);


            if (tag is null) return NotFound();

            var model = new UpdateViewModel
            {
                Id = id,
                TagName = tag.TagName,
            };

            return View(model);
        }
        [HttpPost("update/{id}", Name = "admin-tag-update")]
        public async Task<IActionResult> UpdateAsync(UpdateViewModel model)
        {
            var tag = await _dataContext.Tags.FirstOrDefaultAsync(n => n.Id == model.Id);
            if (tag is null) return NotFound();
            if (!ModelState.IsValid) return View(model);
            if (!_dataContext.Tags.Any(n => n.Id == model.Id)) return View(model);

            tag.TagName = model.TagName;

            await _dataContext.SaveChangesAsync();

            return RedirectToRoute("admin-tag-list");

        }
        #endregion

        #region Delete
        [HttpPost("delete/{id}", Name = "admin-tag-delete")]
        public async Task<IActionResult> DeleteAsync(UpdateViewModel model)
        {
            var tag = await _dataContext.Tags.FirstOrDefaultAsync(n => n.Id == model.Id);
            if (tag is null) return NotFound();


            _dataContext.Tags.Remove(tag);
            await _dataContext.SaveChangesAsync();



            return RedirectToRoute("admin-tag-list");

        }

        #endregion

    }
}
