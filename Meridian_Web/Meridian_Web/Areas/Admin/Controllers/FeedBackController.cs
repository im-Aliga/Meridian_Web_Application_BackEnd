using Meridian_Web.Areas.Admin.ViewModels.FeedBack;
using Meridian_Web.Contracts.File;
using Meridian_Web.Database;
using Meridian_Web.Database.Models;
using Meridian_Web.Services.Abstracts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Meridian_Web.Areas.Admin.Controllers
{
    [Area("admin")]
    [Route("admin/feedback")]
    public class FeedBackController : Controller
    {
        private readonly DataContext _dataContext;
        private readonly IFileService _fileService;

        public FeedBackController(DataContext dataContext, IFileService fileService)
        {
            _dataContext = dataContext;
            _fileService = fileService;
        }

        #region List
        [HttpGet("list", Name = "admin-feedback-list")]
        public async Task<IActionResult> ListAsync()
        {
            var model = await _dataContext.FeedBacks
                .Select(u => new ListFeedBackViewModel(
                  u.Id, u.FullName, u.Context, u.Role, _fileService.GetFileUrl(u.ProfilePhoteInFileSystem, UploadDirectory.FeedBack), u.CreatedAt, u.UpdatedAt))
                .ToListAsync();

            return View(model);
        }
        #endregion

        #region Add
        [HttpGet("add", Name = "admin-feedback-add")]
        public async Task<IActionResult> AddAsync()
        {

            return View();
        }



        [HttpPost("add", Name = "admin-feedback-add")]
        public async Task<IActionResult> AddAsync(AddFeedbackViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var imageNameInSystem = await _fileService.UploadAsync(model!.Image, UploadDirectory.FeedBack);

            await AddFeedback(model.Image!.FileName, imageNameInSystem);


            return RedirectToRoute("admin-feedback-list");


            async Task AddFeedback(string imageName, string imageNameInSystem)
            {
                var feedBack = new FeedBack
                {
                    FullName = model.FullName,
                    Role = model.Role,
                    Context = model.Context,
                    ProfilePhoteImageName = imageName,
                    ProfilePhoteInFileSystem = imageNameInSystem,
                };

                await _dataContext.FeedBacks.AddAsync(feedBack);
                await _dataContext.SaveChangesAsync();
            }
        }
        #endregion

        #region Update
        [HttpGet("update/{id}", Name = "admin-feedback-update")]
        public async Task<IActionResult> UpdateAsync([FromRoute] int id)
        {
            var feedBack = await _dataContext.FeedBacks.FirstOrDefaultAsync(b => b.Id == id);
            if (feedBack is null)
            {
                return NotFound();
            }

            var model = new AddFeedbackViewModel
            {
                Id = feedBack.Id,
                FullName = feedBack.FullName,
                Context = feedBack.Context,
                Role = feedBack.Role,
                ImageUrl = _fileService.GetFileUrl(feedBack.ProfilePhoteInFileSystem, UploadDirectory.FeedBack)
            };

            return View(model);
        }

        [HttpPost("update/{id}", Name = "admin-feedback-update")]
        public async Task<IActionResult> UpdateAsync(AddFeedbackViewModel model)
        {
            var feedBack = await _dataContext.FeedBacks.FirstOrDefaultAsync(b => b.Id == model.Id);
            if (feedBack is null)
            {
                return NotFound();
            }

            if (!ModelState.IsValid)
            {
                return View(model);
            }
            if (model.Image != null)
            {
                await _fileService.DeleteAsync(feedBack.ProfilePhoteInFileSystem, UploadDirectory.FeedBack);
                var imageFileNameInSystem = await _fileService.UploadAsync(model.Image, UploadDirectory.FeedBack);
                await UpdateFeedBackAsync(model.Image.FileName, imageFileNameInSystem);

            }
            else
            {
                await UpdateFeedBackAsync(feedBack.ProfilePhoteImageName, feedBack.ProfilePhoteInFileSystem);
            }


            return RedirectToRoute("admin-feedback-list");


            async Task UpdateFeedBackAsync(string imageName, string imageNameInFileSystem)
            {
                feedBack.FullName = model.FullName;
                feedBack.Context = model.Context;
                feedBack.Role = model.Role;
                feedBack.ProfilePhoteImageName = imageName;
                feedBack.ProfilePhoteInFileSystem = imageNameInFileSystem;
                await _dataContext.SaveChangesAsync();
            }
        }
        #endregion

        #region Delete
        [HttpPost("delete/{id}", Name = "admin-feedback-delete")]
        public async Task<IActionResult> DeleteAsync([FromRoute] int id)
        {
            var feedBack = await _dataContext.FeedBacks.FirstOrDefaultAsync(b => b.Id == id);
            if (feedBack is null)
            {
                return NotFound();
            }

            await _fileService.DeleteAsync(feedBack.ProfilePhoteInFileSystem, UploadDirectory.FeedBack);

            _dataContext.FeedBacks.Remove(feedBack);
            await _dataContext.SaveChangesAsync();

            return RedirectToRoute("admin-feedback-list");
        }
        #endregion

    }
}
