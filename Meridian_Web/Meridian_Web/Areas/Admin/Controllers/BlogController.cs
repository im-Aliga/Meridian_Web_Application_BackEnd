using Meridian_Web.Areas.Admin.ViewModels.Blog;
using Meridian_Web.Database;
using Meridian_Web.Database.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Meridian_Web.Areas.Admin.Controllers
{
    [Area("admin")]
    [Route("admin/blog")]
    public class BlogController : Controller
    {
        private readonly DataContext _dataContext;
        private readonly ILogger<BlogController> _logger;


        public BlogController(DataContext dataContext, ILogger<BlogController> logger)
        {
            _dataContext = dataContext;
            _logger = logger;

        }
        #region List
        [HttpGet("list", Name = "admin-blog-list")]
        public async Task<IActionResult> ListAsync()
        {
            var model = await _dataContext.Blogs.Select(b => new BlogListViewModel(
                b.Id,
                b.Title,
                b.Description,
                b.Proverb,
                b.ProverbAuthor,
                b.CreatedAt,
                b.UpdatedAt,
                b.BlogCategory.Select(bc => bc.Category).Select(c => new BlogListViewModel.CategoryViewModeL(c.Title)).ToList(),
                b.BlogTags.Select(bt => bt.Tag).Select(s => new BlogListViewModel.TagViewModel(s.TagName)).ToList()
                )).ToListAsync();


            return View(model);
        }
        #endregion


        #region Add
        [HttpGet("add", Name = "admin-blog-add")]
        public async Task<IActionResult> AddAsync()
        {
            var model = new AddViewModel
            {
                Categories = await _dataContext.BlogCategories
                    .Select(c => new CatagoryListItemViewModel(c.Id, c.Title))
                    .ToListAsync(),
                Tags = await _dataContext.BlogTags.Select(t => new TagListItemViewModel(t.Id, t.TagName)).ToListAsync()
            };

            return View(model);
        }

        [HttpPost("add", Name = "admin-blog-add")]
        public async Task<IActionResult> AddAsync(AddViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return GetView(model);
            }

            foreach (var categoryId in model.CategoryIds)
            {
                if (!await _dataContext.BlogCategories.AnyAsync(c => c.Id == categoryId))
                {
                    ModelState.AddModelError(string.Empty, "Something went wrong");
                    _logger.LogWarning($"Category with id({categoryId}) not found in db ");
                    return GetView(model);
                }

            }

            foreach (var tagId in model.TagIds)
            {
                if (!await _dataContext.BlogTags.AnyAsync(c => c.Id == tagId))
                {
                    ModelState.AddModelError(string.Empty, "Something went wrong");
                    _logger.LogWarning($"Tag with id({tagId}) not found in db ");
                    return GetView(model);
                }

            }
            AddBlog();
            await _dataContext.SaveChangesAsync();
            return RedirectToRoute("admin-blog-list");



            #region GetView
            IActionResult GetView(AddViewModel model)
            {

                model.Categories = _dataContext.BlogCategories
                   .Select(c => new CatagoryListItemViewModel(c.Id, c.Title))
                   .ToList();

                model.Tags = _dataContext.BlogTags
                 .Select(c => new TagListItemViewModel(c.Id, c.TagName))
                 .ToList();


                return View(model);
            }
            #endregion


            #region AddBlog
            async void AddBlog()
            {
                var blog = new Blog
                {
                    Title = model.Title,
                    Description = model.Description,
                    Proverb = model.Proverb,
                    ProverbAuthor = model.ProverbAuthor,
                };

                await _dataContext.Blogs.AddAsync(blog);

                foreach (var catagoryId in model.CategoryIds)
                {
                    var BlogCatagory = new BlogAndBlogCategory
                    {
                        BlogCategoryId = catagoryId,
                        Blog = blog,
                    };

                    await _dataContext.BlogAndBlogCategories.AddAsync(BlogCatagory);
                }



                foreach (var tagId in model.TagIds)
                {
                    var blogTag = new BlogAndBlogTag
                    {
                        BlogTagId = tagId,
                        Blog = blog,
                    };

                    await _dataContext.BlogAndBlogTags.AddAsync(blogTag);
                }


            }
            #endregion
        }
        #endregion

        #region Update
        [HttpGet("update/{id}", Name = "admin-blog-update")]
        public async Task<IActionResult> UpdateAsync([FromRoute] int id)
        {
            var blog = await _dataContext.Blogs
                .Include(c => c.BlogCategory).Include(t => t.BlogTags)
                .FirstOrDefaultAsync(p => p.Id == id);

            if (blog is null)
            {
                return NotFound();
            }

            var model = new UpdateViewModel
            {
                Id = blog.Id,
                Title = blog.Title,
                Description = blog.Description,
                Proverb = blog.Proverb,
                ProverbAuthor = blog.ProverbAuthor,
                Categories = await _dataContext.BlogCategories.Select(c => new CatagoryListItemViewModel(c.Id, c.Title)).ToListAsync(),
                CategoryIds = blog.BlogCategory.Select(pc => pc.BlogCategoryId).ToList(),
                Tags = await _dataContext.BlogTags.Select(c => new TagListItemViewModel(c.Id, c.TagName)).ToListAsync(),
                TagIds = blog.BlogTags.Select(pc => pc.BlogTagId).ToList(),

            };

            return View(model);

        }

        [HttpPost("update/{id}", Name = "admin-blog-update")]
        public async Task<IActionResult> UpdateAsync(UpdateViewModel model)
        {
            var blog = await _dataContext.Blogs
                    .Include(c => c.BlogCategory).Include(t => t.BlogTags)
                    .FirstOrDefaultAsync(p => p.Id == model.Id);

            if (blog is null)
            {
                return NotFound();
            }

            if (!ModelState.IsValid)
            {
                return GetView(model);
            }

            foreach (var categoryId in model.CategoryIds)
            {
                if (!await _dataContext.BlogCategories.AnyAsync(c => c.Id == categoryId))
                {
                    ModelState.AddModelError(string.Empty, "Something went wrong");
                    _logger.LogWarning($"Category with id({categoryId}) not found in db ");
                    return GetView(model);
                }

            }

            foreach (var tagId in model.TagIds)
            {
                if (!await _dataContext.BlogTags.AnyAsync(c => c.Id == tagId))
                {
                    ModelState.AddModelError(string.Empty, "Something went wrong");
                    _logger.LogWarning($"Tag with id({tagId}) not found in db ");
                    return GetView(model);
                }

            }


            UpdateBlogAsync();

            await _dataContext.SaveChangesAsync();

            return RedirectToRoute("admin-blog-list");



            #region GetView
            IActionResult GetView(UpdateViewModel model)
            {
                model.Categories = _dataContext.BlogCategories
                   .Select(c => new CatagoryListItemViewModel(c.Id, c.Title))
                   .ToList();
                model.CategoryIds = blog.BlogCategory.Select(c => c.BlogCategoryId).ToList();

                model.Tags = _dataContext.BlogTags
                 .Select(c => new TagListItemViewModel(c.Id, c.TagName))
                 .ToList();

                model.TagIds = blog.BlogTags.Select(c => c.BlogTagId).ToList();

                return View(model);
            }
            #endregion

            #region UpdateBlog
            async Task UpdateBlogAsync()
            {
                blog.Title = model.Title;
                blog.Description = model.Description;
                blog.Proverb=model.Proverb;
                blog.ProverbAuthor = model.ProverbAuthor;

                #region Catagory
                var categoriesInDb = blog.BlogCategory.Select(bc => bc.BlogCategoryId).ToList();
                var categoriesToRemove = categoriesInDb.Except(model.CategoryIds).ToList();
                var categoriesToAdd = model.CategoryIds.Except(categoriesInDb).ToList();

                blog.BlogCategory.RemoveAll(bc => categoriesToRemove.Contains(bc.BlogCategoryId));

                foreach (var categoryId in categoriesToAdd)
                {
                    var plantCatagory = new BlogAndBlogCategory
                    {
                        BlogCategoryId = categoryId,
                        Blog = blog,
                    };

                    await _dataContext.BlogAndBlogCategories.AddAsync(plantCatagory);
                }
                #endregion

                #region Tag
                var tagInDb = blog.BlogTags.Select(bc => bc.BlogTagId).ToList();
                var tagToRemove = tagInDb.Except(model.TagIds).ToList();
                var tagToAdd = model.TagIds.Except(tagInDb).ToList();

                blog.BlogTags.RemoveAll(bc => tagToRemove.Contains(bc.BlogTagId));


                foreach (var tagId in tagToAdd)
                {
                    var BlogTag = new BlogAndBlogTag
                    {
                        BlogTagId = tagId,
                        Blog = blog,
                    };

                    await _dataContext.BlogAndBlogTags.AddAsync(BlogTag);
                }
                #endregion
            }
            #endregion

        }
        #endregion

        #region Delete
        [HttpPost("delete/{id}", Name = "admin-blog-delete")]
        public async Task<IActionResult> DeleteAsync([FromRoute] int id)
        {
            var blog = await _dataContext.Blogs.FirstOrDefaultAsync(p => p.Id == id);

            if (blog is null)
            {
                return NotFound();
            }

            _dataContext.Blogs.Remove(blog);
            await _dataContext.SaveChangesAsync();
            return RedirectToRoute("admin-blog-list");
        }
        #endregion

    }
}
