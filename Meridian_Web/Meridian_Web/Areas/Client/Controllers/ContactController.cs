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
    [Route("contact")]
    public class ContactController : Controller
    {
        private readonly DataContext _dataContext;
        private readonly IFileService _fileService;

        public ContactController(DataContext dataContext, IFileService fileService)
        {
            _dataContext = dataContext;
            _fileService = fileService;
        }
        [HttpGet("list", Name = "client-contact-list")]
        public async Task<IActionResult> IndexAsync()
        {
            var model = new ContactViewModel
            {
                ContactInformations = await _dataContext.ContactInformations.Select(ci => new ContactInformationListItemViewModel(
                    ci.Title,
                    ci.MainContext,
                    ci.Context,
                    _fileService.GetFileUrl(ci.ImageNameInFileSystem, UploadDirectory.ContactInformation)
                    ))
                .ToListAsync()
            };
            return View(model);
        }

        [HttpPost("list", Name = "client-contact-list")]
        public async Task<IActionResult> IndexAsync([FromForm] ContactViewModel contactViewModel)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            var model = new Contact
            {
                FirstName = contactViewModel.FirstName,
                Email = contactViewModel.Email,
                Subject = contactViewModel.Subject,
                Message = contactViewModel.Message,
                
             
            };

            await _dataContext.AddAsync(model);
            await _dataContext.SaveChangesAsync();

            return RedirectToRoute("client-home-index");
        }
    }
}
