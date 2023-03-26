using Meridian_Web.Areas.Client.ViewModels.BlogDetails;
using Meridian_Web.Areas.Client.ViewModels.Comment;
using Meridian_Web.Areas.Client.ViewModels.Contact;
using Meridian_Web.Contracts.File;
using Meridian_Web.Database;
using Meridian_Web.Database.Models;
using Meridian_Web.Services.Abstracts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Meridian_Web.Areas.Client.Controllers
{
    [Area("client")]
    [Route("blogdetails")]
    public class BlogDetailsController : Controller
    {
        private readonly DataContext _dbContext;
        private readonly IFileService _fileService;

        public BlogDetailsController(DataContext dbContext, IFileService fileService)
        {
            _dbContext = dbContext;
            _fileService = fileService;
        }
        [HttpGet("index/{id}", Name = "client-blogdetails-index")]
        public async Task<IActionResult> Index(int id)
        {
            var blog = await _dbContext.Blogs.Include(p => p.BlogFile)
                .Include(p => p.BlogCategory)
                .Include(p => p.BlogTags).FirstOrDefaultAsync(p => p.Id == id);


            if (blog is null)
            {
                return NotFound();
            }

            var model = new BlogDetailsViewModel
            {
                Id = blog.Id,
                Title = blog.Title,
                Description = blog.Description,
                Proverb = blog.Proverb,
                ProverbAuthor = blog.ProverbAuthor,
                CreatedAt = blog.CreatedAt,
                Catagories = _dbContext.BlogAndBlogCategories.Include(ps => ps.Category).Where(ps => ps.BlogId == blog.Id)
                         .Select(ps => new BlogDetailsViewModel.CatagoryViewModeL(ps.Category.Title, ps.Category.Id)).ToList(),
                Tags = _dbContext.BlogAndBlogTags.Include(ps => ps.Tag).Where(ps => ps.BlogId == blog.Id)
                      .Select(ps => new BlogDetailsViewModel.TagViewModeL(ps.Tag.TagName, ps.Tag.Id)).ToList(),
                Files = _dbContext.BlogFiles.Where(p => p.BlogId == blog.Id)
                .Select(p => new BlogDetailsViewModel.FileViewModeL
                (_fileService.GetFileUrl(p.FileNameInFileSystem, UploadDirectory.Blog), p.IsShowVideo, p.IsShowImage,p.IsShowVideo)).ToList()

            };
            return View(model);
        }

        [HttpPost("comment/{blogId}", Name = "client-blogdetails-comment")]
        public async Task<IActionResult> IndexAsync(int blogId,[FromForm] BlogDetailsViewModel commentViewModel)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            var model = new Comment
            {
                BlogId = blogId,
                Name = commentViewModel.Name,
                Email = commentViewModel.Email,
                Context = commentViewModel.Context,
               


            };

            await _dbContext.AddAsync(model);
            await _dbContext.SaveChangesAsync();

            return RedirectToRoute("client-blogdetails-index");
        }
    }
}
