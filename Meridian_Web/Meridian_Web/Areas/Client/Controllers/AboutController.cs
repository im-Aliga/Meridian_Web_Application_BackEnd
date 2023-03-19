using Meridian_Web.Areas.Client.ViewModels.About;
using Meridian_Web.Areas.Client.ViewModels.Home;
using Meridian_Web.Contracts.File;
using Meridian_Web.Database;
using Meridian_Web.Services.Abstracts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Meridian_Web.Areas.Client.Controllers
{
    [Area("client")]
    [Route("about")]
    public class AboutController : Controller
    {
        private readonly DataContext _dataContext;
        private readonly IFileService _fileService;

        public AboutController(DataContext dataContext, IFileService fileService)
        {
            _dataContext = dataContext;
            _fileService = fileService;
        }

        [HttpGet("index", Name = "client-about-index")]
        public async Task<IActionResult> IndexAsync()
        {
            var model = new AboutViewModel
            {

                Abouts = await _dataContext.Abouts.Select(a => new AboutListItemViewModel(
                     a.Title,
                     a.Context
                   ))
              .ToListAsync(),

                OurTeamMembers= await _dataContext.TeamMembers.Select(tm=>new OurTeamMemberLIstItemViewModel(
                    tm.FullName,
                    tm.Position,
                    _fileService.GetFileUrl(tm.BgImageNameInFileSystem,UploadDirectory.TeamMember)
                    ))
                .ToListAsync(),

                FeedBacks=await _dataContext.FeedBacks.Select(fb=>new FeedBackListItemViewModel(
                   fb.FullName,
                   fb.Context,
                   fb.Role,
                    _fileService.GetFileUrl(fb.ProfilePhoteInFileSystem, UploadDirectory.FeedBack)
                    ))
                .ToListAsync(),

            };

            return View(model);
        }
    }
}
