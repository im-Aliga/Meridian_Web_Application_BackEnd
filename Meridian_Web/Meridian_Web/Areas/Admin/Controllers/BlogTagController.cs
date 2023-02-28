using Meridian_Web.Areas.Admin.ViewModels.BlogTag;
using Meridian_Web.Database;
using Meridian_Web.Database.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Meridian_Web.Areas.Admin.Controllers
{
    [Area("admin")]
    [Route("admin/blogtag")]
    public class BlogTagController : Controller
    {
        private readonly DataContext _dataContext;
        private readonly ILogger<BlogCategoryController> _logger;

        public BlogTagController(DataContext dataContext, ILogger<BlogCategoryController> logger)
        {
            _dataContext = dataContext;
            _logger = logger;
        }


        #region List
        [HttpGet("list", Name = "admin-blogtag-list")]
        public async Task<IActionResult> ListAsync()
        {
            var model = await _dataContext.BlogTags
                .Select(c => new ListItemViewModel(c.Id, c.TagName, c.CreatedAt, c.UpdatedAt))
                .ToListAsync();

            return View(model);
        }
        #endregion

        #region Add
        [HttpGet("add", Name = "admin-blogtag-add")]
        public async Task<IActionResult> AddAsync()
        {
            return View();
        }
        [HttpPost("add", Name = "admin-blogtag-add")]
        public async Task<IActionResult> AddAsync(AddViewModel model)
        {
            if (!ModelState.IsValid) return View(model);


            var blogTag = new BlogTag
            {
                TagName = model.Tagname,
            };
            await _dataContext.BlogTags.AddAsync(blogTag);
            await _dataContext.SaveChangesAsync();
            return RedirectToRoute("admin-blogtag-list");
        }
        #endregion

        #region Update
        [HttpGet("update/{id}", Name = "admin-blogtag-update")]
        public async Task<IActionResult> UpdateAsync([FromRoute] int id)
        {
            var blogTag = await _dataContext.BlogTags.FirstOrDefaultAsync(n => n.Id == id);


            if (blogTag is null) return NotFound();

            var model = new UpdateViewModel
            {
                Id = id,
                TagName = blogTag.TagName,
            };

            return View(model);
        }

        [HttpPost("update/{id}", Name = "admin-blogtag-update")]
        public async Task<IActionResult> UpdateAsync(UpdateViewModel model)
        {
            var blogTag = await _dataContext.BlogTags.FirstOrDefaultAsync(n => n.Id == model.Id);
            if (blogTag is null) return NotFound();
            if (!ModelState.IsValid) return View(model);
            if (!_dataContext.BlogTags.Any(n => n.Id == model.Id)) return View(model);

            blogTag.TagName = model.TagName;
            await _dataContext.SaveChangesAsync();

            return RedirectToRoute("admin-blogtag-list");

        }

        #endregion

        #region Delete
        [HttpPost("delete/{id}", Name = "admin-blogtag-delete")]
        public async Task<IActionResult> DeleteAsync(UpdateViewModel model)
        {
            var blogTag = await _dataContext.BlogTags.FirstOrDefaultAsync(n => n.Id == model.Id);
            if (blogTag is null) return NotFound();
            _dataContext.BlogTags.Remove(blogTag);
            await _dataContext.SaveChangesAsync();



            return RedirectToRoute("admin-blogtag-list");

        }
        #endregion
    }
}
