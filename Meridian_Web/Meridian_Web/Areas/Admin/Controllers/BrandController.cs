
using Meridian_Web.Areas.Admin.ViewModels.Brand;
using Meridian_Web.Contracts.File;
using Meridian_Web.Database;
using Meridian_Web.Database.Models;
using Meridian_Web.Services.Abstracts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Meridian_Web.Areas.Admin.Controllers
{
    [Area("admin")]
    [Route("admin/brand")]
    public class BrandController : Controller
    {
        private readonly DataContext _dataContext;
        private readonly IFileService _fileService;

        public BrandController(DataContext dataContext, IFileService fileService)
        {
            _dataContext = dataContext;
            _fileService = fileService;
        }

        #region List
        [HttpGet("list", Name = "admin-brand-list")]
        public async Task<IActionResult> ListAsync()
        {
            var model = await _dataContext.Brands.Select(u => new ListBrandViewModel(
                u.Id,
                u.Name,
                _fileService.GetFileUrl(u.PhoteInFileSystem, UploadDirectory.Brand),
                u.CreatedAt,
                u.UpdatedAt)).ToListAsync();
            return View(model);
        }
        #endregion

        #region Add

        [HttpGet("add", Name = "admin-brand-add")]
        public async Task<IActionResult> AddAsync()
        {
            return View();
        }

        [HttpPost("add", Name = "admin-brand-add")]
        public async Task<IActionResult> AddAsync(AddBrandViewModel model)
        {
            if (!ModelState.IsValid) return View(model);
            var imageNameInSystem = await _fileService.UploadAsync(model!.Image, UploadDirectory.Brand);

            await AddBrand(model.Image!.FileName, imageNameInSystem);


            return RedirectToRoute("admin-brand-list");


            async Task AddBrand(string imageName, string imageNameInSystem)
            {
                var brand = new Brand
                {
                    Name = model.Name,
                    PhoteImageName = imageName,
                    PhoteInFileSystem = imageNameInSystem,
                };

                await _dataContext.Brands.AddAsync(brand);
                await _dataContext.SaveChangesAsync();
            }
        }
        #endregion

        #region Update
        [HttpGet("update/{id}", Name = "admin-brand-update")]
        public async Task<IActionResult> UpdateAsync([FromRoute] int id)
        {
            var brand = await _dataContext.Brands.FirstOrDefaultAsync(b => b.Id == id);
            if (brand is null) return NotFound();




            var model = new AddBrandViewModel
            {
                Id = brand.Id,
                Name = brand.Name,
                ImageUrl = _fileService.GetFileUrl(brand.PhoteInFileSystem, UploadDirectory.Brand)
            };

            return View(model);
        }

        [HttpPost("update/{id}", Name = "admin-brand-update")]
        public async Task<IActionResult> UpdateAsync(AddBrandViewModel model)
        {
            var brand = await _dataContext.Brands.FirstOrDefaultAsync(b => b.Id == model.Id);
            if (brand is null)return NotFound();

            if (!ModelState.IsValid) return View(model);



            if (model.Image != null)
            {
                await _fileService.DeleteAsync(brand.PhoteInFileSystem, UploadDirectory.Brand);
                var imageFileNameInSystem = await _fileService.UploadAsync(model.Image, UploadDirectory.Brand);
                await UpdateBrandAsync(model.Image.FileName, imageFileNameInSystem);

            }
            else
            {
                await UpdateBrandAsync(brand.PhoteImageName, brand.PhoteInFileSystem);
            }


            return RedirectToRoute("admin-brand-list");


            async Task UpdateBrandAsync(string imageName, string imageNameInFileSystem)
            {
                brand.Name = model.Name;
                brand.PhoteImageName = imageName;
                brand.PhoteInFileSystem = imageNameInFileSystem;
                await _dataContext.SaveChangesAsync();
            }
        }
        #endregion
        #region Delete
        [HttpPost("delete/{id}", Name = "admin-brand-delete")]
        public async Task<IActionResult> DeleteAsync([FromRoute] int id)
        {
            var brand = await _dataContext.Brands.FirstOrDefaultAsync(b => b.Id == id);
            if (brand is null)
            {
                return NotFound();
            }

            await _fileService.DeleteAsync(brand.PhoteInFileSystem, UploadDirectory.Brand);

            _dataContext.Brands.Remove(brand);
            await _dataContext.SaveChangesAsync();

            return RedirectToRoute("admin-brand-list");
        }
        #endregion
    }
}
