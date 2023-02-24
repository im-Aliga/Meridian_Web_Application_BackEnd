
using Meridian_Web.Areas.Admin.ViewModels.BlogCategory;
using Meridian_Web.Database;
using Meridian_Web.Database.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Meridian_Web.Areas.Admin.Controllers
{
    [Area("admin")]
    [Route("admin/blogcategory")]
    public class BlogCategoryController : Controller
    {
        private readonly DataContext _dataContext;
        private readonly ILogger<BlogCategoryController> _logger;

        public BlogCategoryController(DataContext dataContext, ILogger<BlogCategoryController> logger)
        {
            _dataContext = dataContext;
            _logger = logger;
        }

        #region List
        [HttpGet("list", Name = "admin-blogcategory-list")]
        public async Task<IActionResult> ListAsync()
        {
            var model = await _dataContext.BlogCategories
                .Select(c => new ListItemViewModel(c.Id, c.Title,c.CreatedAt,c.UpdatedAt))
                .ToListAsync();

            return View(model);
        }
        #endregion

        #region Add
        [HttpGet("add", Name = "admin-blogcategory-add")]
        public async Task<IActionResult> AddAsync()
        {
            return View();
        }
        [HttpPost("add", Name = "admin-blogcategory-add")]
        public async Task<IActionResult> AddAsync(AddViewModel model)
        {
            if (!ModelState.IsValid) return View(model);


            var blogcategory = new BlogCategory
            {
                Title = model.Title,
            };
            await _dataContext.BlogCategories.AddAsync(blogcategory);
            await _dataContext.SaveChangesAsync();
            return RedirectToRoute("admin-blogcategory-list");
        }
        #endregion

        #region Update
        [HttpGet("update/{id}", Name = "admin-blogcategory-update")]
        public async Task<IActionResult> UpdateAsync([FromRoute] int id)
        {
            var blogCategory = await _dataContext.BlogCategories.FirstOrDefaultAsync(n => n.Id == id);


            if (blogCategory is null) return NotFound();

            var model = new UpdateViewModel
            {
                Id = id,
                Title = blogCategory.Title,
            };

            return View(model);
        }

        [HttpPost("update/{id}", Name = "admin-blogcategory-update")]
        public async Task<IActionResult> UpdateAsync(UpdateViewModel model)
        {
            var blogCategory = await _dataContext.BlogCategories.FirstOrDefaultAsync(n => n.Id == model.Id);
            if (blogCategory is null) return NotFound();
            if (!ModelState.IsValid) return View(model);
            if (!_dataContext.BlogCategories.Any(n => n.Id == model.Id)) return View(model);

            blogCategory.Title = model.Title;
            await _dataContext.SaveChangesAsync();

            return RedirectToRoute("admin-blogcategory-list");

        }

        #endregion

        #region Delete
        [HttpPost("delete/{id}", Name = "admin-blogcategory-delete")]
        public async Task<IActionResult> DeleteAsync(UpdateViewModel model)
        {
            var blogCategory = await _dataContext.BlogCategories.FirstOrDefaultAsync(n => n.Id == model.Id);
            if (blogCategory is null) return NotFound();
            _dataContext.BlogCategories.Remove(blogCategory);
            await _dataContext.SaveChangesAsync();



            return RedirectToRoute("admin-blogcategory-list");

        }
        #endregion
    }
}
