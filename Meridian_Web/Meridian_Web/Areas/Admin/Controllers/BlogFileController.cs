using Meridian_Web.Areas.Admin.ViewModels.BlogFile;
using Meridian_Web.Contracts.File;
using Meridian_Web.Database;
using Meridian_Web.Database.Models;
using Meridian_Web.Services.Abstracts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Meridian_Web.Areas.Admin.Controllers
{
    [Area("admin")]
    [Route("admin/blogfile")]
    public class BlogFileController : Controller
    {
        private readonly DataContext _dataContext;
        private readonly IFileService _fileService;

        public BlogFileController(DataContext dataContext, IFileService fileService)
        {
            _dataContext = dataContext;
            _fileService = fileService;
        }
        #region List
        [HttpGet("{blogId}/image/list", Name = "admin-blogfile-list")]
        public async Task<IActionResult> ListAsync([FromRoute] int blogId)
        {
            var blog = await _dataContext.Blogs.Include(b => b.BlogFile).FirstOrDefaultAsync(b => b.Id == blogId);
            if (blog == null) return NotFound();

            var model = new BlogFileViewModel { Blogİd = blog.Id };

            model.Files = blog.BlogFile.Select(b => new BlogFileViewModel.ListItem
            {
                Id = b.Id,
                FileUrl = _fileService.GetFileUrl(b.FileNameInFileSystem, Contracts.File.UploadDirectory.Blog),
                CreatedAt = b.CreatedAt,
                IsShowAudio = b.IsShowAudio,
                IsShowImage = b.IsShowImage,
                IsShowVideo = b.IsShowVideo,

            }).ToList();

            return View(model);
        }
        #endregion

        #region Add
        [HttpGet("{blogId}/image/add", Name = "admin-blogfile-add")]
        public async Task<IActionResult> AddAsync()
        {
            return View(new BlogFileAddViewModel());
        }

        [HttpPost("{blogId}/image/add", Name = "admin-blogfile-add")]
        public async Task<IActionResult> AddAsync([FromRoute] int blogId, BlogFileAddViewModel model)
        {
            if (!ModelState.IsValid) return View(model);

            var blog = await _dataContext.Blogs.FirstOrDefaultAsync(b => b.Id == blogId);

            if (blog is null)
            {
                return NotFound();
            }

            var fileNameInSystem = await _fileService.UploadAsync(model.File, UploadDirectory.Blog);

            var blogFile = new BlogFile
            {
                Blog = blog,
                FileName = model.File.FileName,
                FileNameInFileSystem = fileNameInSystem,
                IsShowImage = model.IsShowImage,
                IsShowVideo = model.IsShowVideo,
                IsShowAudio = model.IsShowAudio,

            };

            await _dataContext.BlogFiles.AddAsync(blogFile);

            await _dataContext.SaveChangesAsync();

            return RedirectToRoute("admin-blogfile-list", new { BlogId = blogId });

        }
        #endregion
        #region Delete
        [HttpPost("{blogId}/image/{blogFileId}/delete", Name = "admin-blogfile-delete")]
        public async Task<IActionResult> Delete([FromRoute] int blogId, [FromRoute] int blogFileId)
        {

            var blogFile = await _dataContext.BlogFiles.FirstOrDefaultAsync(p => p.BlogId == blogId && p.Id == blogFileId);

            if (blogFile is null)
            {
                return NotFound();
            }

            await _fileService.DeleteAsync(blogFile.FileNameInFileSystem, UploadDirectory.Blog);

            _dataContext.BlogFiles.Remove(blogFile);

            await _dataContext.SaveChangesAsync();

            return RedirectToRoute("admin-blogfile-list", new { BlogId = blogId });

        }
        #endregion
    }
}
