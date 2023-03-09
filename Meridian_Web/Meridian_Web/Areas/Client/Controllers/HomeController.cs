
using Meridian_Web.Areas.Client.ViewModels.Home;
using Meridian_Web.Contracts.File;
using Meridian_Web.Database;
using Meridian_Web.Database.Models;
using Meridian_Web.Services.Abstracts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Meridian_Web.Areas.Client.Controllers
{
    [Area("client")]
    [Route("home")]
    public class HomeController : Controller
    {

        private readonly DataContext _dbContext;
        private readonly IFileService _fileService;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public HomeController(DataContext dbContext, IFileService fileService, IHttpContextAccessor httpContextAccessor)
        {
            _dbContext = dbContext;
            _fileService = fileService;
            _httpContextAccessor = httpContextAccessor;
        }

        [HttpGet("~/", Name = "client-home-index")]
        [HttpGet("index")]
        public async Task<IActionResult> IndexAsync()
        {   var  model = new HomeViewModel
            {

                Sliders = await _dbContext.Sliders.OrderBy(s => s.Order).Select(b => new SliderListItemVIewModel(
                       b.Title,
                       b.OfferContext,
                       b.Content,
                       b.StartPrice,
                       b.ButtonName,
                       b.BtnRedirectUrl,
                       b.Order,
                       _fileService.GetFileUrl(b.BgImageNameInFileSystem, UploadDirectory.Slider)
                       ))
                   .ToListAsync(),
            };
          
            return View(model);

        }

    
    }
}
