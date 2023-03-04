    using Meridian_Web.Areas.Admin.ViewModels.Banner;
using Meridian_Web.Areas.Admin.ViewModels.Slider;
using Meridian_Web.Contracts.File;
using Meridian_Web.Database;
using Meridian_Web.Database.Models;
using Meridian_Web.Services.Abstracts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Meridian_Web.Areas.Admin.Controllers
{
    [Area("admin")]
    [Route("admin/banner")]
    public class BannerController : Controller
    {
        private readonly DataContext _dataContext;
        private readonly IFileService _fileService;

        public BannerController(DataContext dataContext, IFileService fileService)
        {
            _dataContext = dataContext;
            _fileService = fileService;
        }



        #region List
        [HttpGet("list", Name = "admin-banner-list")]
        public async Task<IActionResult> ListAsync()
        {
            var model = await _dataContext.Banners.Select(u => new ListBannerViewModel(
                u.Id,
                u.Title,
                u.MainContext,
                u.Context,
                _fileService.GetFileUrl(u.PhoteInFileSystem, UploadDirectory.Banner),
                u.CreatedAt,
                u.UpdatedAt)).ToListAsync();
            return View(model);
        }
        #endregion

        #region Add

        [HttpGet("add", Name = "admin-banner-add")]
        public async Task<IActionResult> AddAsync()
        {

            return View();
        }

        [HttpPost("add", Name = "admin-banner-add")]
        public async Task<IActionResult> AddAsync(AddBannerViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var imageNameInSystem = await _fileService.UploadAsync(model!.Image, UploadDirectory.Banner);

            await AddBanner(model.Image!.FileName, imageNameInSystem);


            return RedirectToRoute("admin-banner-list");


            async Task AddBanner(string imageName, string imageNameInSystem)
            {
                var banner = new Banner
                {
                    Title = model.Title,
                    MainContext = model.MainContext,
                    Context = model.Content,
                    PhoteImageName = imageName,
                    PhoteInFileSystem = imageNameInSystem,
                };

                await _dataContext.Banners.AddAsync(banner);
                await _dataContext.SaveChangesAsync();
            }
        }
        #endregion

        #region Update
        [HttpGet("update/{id}", Name = "admin-banner-update")]
        public async Task<IActionResult> UpdateAsync([FromRoute] int id)
        {
            var banner = await _dataContext.Banners.FirstOrDefaultAsync(b => b.Id == id);
            if (banner is null)
            {
                return NotFound();
            }

            var model = new AddBannerViewModel
            {
                Id = banner.Id,
                Title = banner.Title,
                MainContext = banner.MainContext,
                Content = banner.Context,
                ImageUrl = _fileService.GetFileUrl(banner.PhoteInFileSystem, UploadDirectory.Banner)
            };

            return View(model);
        }

        [HttpPost("update/{id}", Name = "admin-banner-update")]
        public async Task<IActionResult> UpdateAsync(AddBannerViewModel model)
        {
            var banner = await _dataContext.Banners.FirstOrDefaultAsync(b => b.Id == model.Id);
            if (banner is null)
            {
                return NotFound();
            }

            if (!ModelState.IsValid)
            {
                return View(model);
            }
            if (model.Image != null)
            {
                await _fileService.DeleteAsync(banner.PhoteInFileSystem, UploadDirectory.Banner);
                var imageFileNameInSystem = await _fileService.UploadAsync(model.Image, UploadDirectory.Banner);
                await UpdateBannerAsync(model.Image.FileName, imageFileNameInSystem);

            }
            else
            {
                await UpdateBannerAsync(banner.PhoteImageName, banner.PhoteInFileSystem);
            }


            return RedirectToRoute("admin-banner-list");


            async Task UpdateBannerAsync(string imageName, string imageNameInFileSystem)
            {
                banner.Title = model.Title;
                banner.MainContext = model.MainContext;
                banner.Context = model.Content;
                banner.PhoteImageName = imageName;
                banner.PhoteInFileSystem = imageNameInFileSystem;
                await _dataContext.SaveChangesAsync();
            }
        }
        #endregion

        #region Delete
        [HttpPost("delete/{id}", Name = "admin-banner-delete")]
        public async Task<IActionResult> DeleteAsync([FromRoute] int id)
        {
            var banner = await _dataContext.Banners.FirstOrDefaultAsync(b => b.Id == id);
            if (banner is null)
            {
                return NotFound();
            }

            await _fileService.DeleteAsync(banner.PhoteInFileSystem, UploadDirectory.Banner);

            _dataContext.Banners.Remove(banner);
            await _dataContext.SaveChangesAsync();

            return RedirectToRoute("admin-banner-list");
        }
        #endregion
    }
}
