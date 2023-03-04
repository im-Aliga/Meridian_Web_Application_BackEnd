
using Meridian_Web.Areas.Admin.ViewModels.ContactInformation;
using Meridian_Web.Contracts.File;
using Meridian_Web.Database;
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
            var model = await _dataContext.Banners.Select(u => new ListContactInformationViewModel(
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



    }
}
