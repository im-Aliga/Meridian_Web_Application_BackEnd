

using Meridian_Web.Areas.Admin.ViewModels.Banner;
using Meridian_Web.Areas.Admin.ViewModels.ContactInformation;
using Meridian_Web.Contracts.File;
using Meridian_Web.Database;
using Meridian_Web.Database.Models;
using Meridian_Web.Services.Abstracts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Meridian_Web.Areas.Admin.Controllers
{
    [Area("admin")]
    [Route("admin/contactinformation")]
    public class ContactInformationController : Controller
    {
        private readonly DataContext _dataContext;
        private readonly IFileService _fileService;

        public ContactInformationController(DataContext dataContext, IFileService fileService)
        {
            _dataContext = dataContext;
            _fileService = fileService;
        }

        #region List
        [HttpGet("list", Name = "admin-contactinformation-list")]
        public async Task<IActionResult> ListAsync()
        {
            var model = await _dataContext.ContactInformations.Select(u => new ListContactInformationViewModel(
                u.Id,
                u.Title,
                u.MainContext,
                u.Context,
                _fileService.GetFileUrl(u.ImageNameInFileSystem, UploadDirectory.ContactInformation),
                u.CreatedAt,
                u.UpdatedAt)).ToListAsync();
            return View(model);
        }
        #endregion

        #region Add

        [HttpGet("add", Name = "admin-contactinformation-add")]
        public async Task<IActionResult> AddAsync()
        {

            return View();
        }

        [HttpPost("add", Name = "admin-contactinformation-add")]
        public async Task<IActionResult> AddAsync(AddContactInformationViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var imageNameInSystem = await _fileService.UploadAsync(model!.Image, UploadDirectory.ContactInformation);

            await AddContactInformation(model.Image!.FileName, imageNameInSystem);


            return RedirectToRoute("admin-contactinformation-list");


            async Task AddContactInformation(string imageName, string imageNameInSystem)
            {
                var contactInformation = new ContactInformation
                {
                    Title = model.Title,
                    MainContext = model.MainContext,
                    Context = model.Content,
                    ImageName = imageName,
                    ImageNameInFileSystem = imageNameInSystem,
                };

                await _dataContext.ContactInformations.AddAsync(contactInformation);
                await _dataContext.SaveChangesAsync();
            }
        }
        #endregion

        #region Update
        [HttpGet("update/{id}", Name = "admin-contactinformation-update")]
        public async Task<IActionResult> UpdateAsync([FromRoute] int id)
        {
            var contactInformation = await _dataContext.ContactInformations.FirstOrDefaultAsync(b => b.Id == id);
            if (contactInformation is null)
            {
                return NotFound();
            }

            var model = new AddContactInformationViewModel
            {
                Id = contactInformation.Id,
                Title = contactInformation.Title,
                MainContext = contactInformation.MainContext,
                Content = contactInformation.Context,
                ImageUrl = _fileService.GetFileUrl(contactInformation.ImageNameInFileSystem, UploadDirectory.ContactInformation)
            };

            return View(model);
        }

        [HttpPost("update/{id}", Name = "admin-contactinformation-update")]
        public async Task<IActionResult> UpdateAsync(AddContactInformationViewModel model)
        {
            var contactInformation = await _dataContext.ContactInformations.FirstOrDefaultAsync(b => b.Id == model.Id);
            if (contactInformation is null)
            {
                return NotFound();
            }

            if (!ModelState.IsValid)
            {
                return View(model);
            }
            if (model.Image != null)
            {
                await _fileService.DeleteAsync(contactInformation.ImageNameInFileSystem, UploadDirectory.ContactInformation);
                var imageFileNameInSystem = await _fileService.UploadAsync(model.Image, UploadDirectory.ContactInformation);
                await UpdateContactInformationAsync(model.Image.FileName, imageFileNameInSystem);

            }
            else
            {
                await UpdateContactInformationAsync(contactInformation.ImageName, contactInformation.ImageNameInFileSystem);
            }


            return RedirectToRoute("admin-contactinformation-list");


            async Task UpdateContactInformationAsync(string imageName, string imageNameInFileSystem)
            {
                contactInformation.Title = model.Title;
                contactInformation.MainContext = model.MainContext;
                contactInformation.Context = model.Content;
                contactInformation.ImageNameInFileSystem = imageName;
                contactInformation.ImageNameInFileSystem = imageNameInFileSystem;
                await _dataContext.SaveChangesAsync();
            }
        }
        #endregion

        #region Delete
        [HttpPost("delete/{id}", Name = "admin-contactinformation-delete")]
        public async Task<IActionResult> DeleteAsync([FromRoute] int id)
        {
            var contactInformation = await _dataContext.ContactInformations.FirstOrDefaultAsync(b => b.Id == id);
            if (contactInformation is null)
            {
                return NotFound();
            }

            await _fileService.DeleteAsync(contactInformation.ImageNameInFileSystem, UploadDirectory.ContactInformation);

            _dataContext.ContactInformations.Remove(contactInformation);
            await _dataContext.SaveChangesAsync();

            return RedirectToRoute("admin-contactinformation-list");
        }
        #endregion

    }
}
