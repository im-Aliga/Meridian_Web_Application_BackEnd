﻿
using BackEndFinalProject.Areas.Client.ViewModels.Home.Modal;
using Meridian_Web.Areas.Client.ViewModels.Home;
using Meridian_Web.Contracts.File;
using Meridian_Web.Database;
using Meridian_Web.Database.Models;
using Meridian_Web.Services.Abstracts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using static Meridian_Web.Areas.Admin.ViewModels.Product.ProductListViewModel;

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

        [HttpGet("modal/{id}", Name = "product-modal")]
        public async Task<ActionResult> ModalAsync(int id)
        {
            var product = await _dbContext.Products
                .Include(p => p.ProductImages)
                .Include(p=>p.ProductDisconts)
                 .FirstOrDefaultAsync(p => p.Id == id);


            if (product is null)
            {
                return NotFound();
            }
            var ProductDisconts = _dbContext.ProductDisconts
                .Where(pd => pd.ProductId == product.Id)
                .Include(pd => pd.Discont).Select(pd => new ModalViewModel.DiscountList(pd.Discont.Id, pd.Discont.DiscontPers)).ToList();


            var model = new ModalViewModel(
                product.Id,
                product.Title,
                product.Content,
                product.Price,
                product.DiscountPrice != null ? product.DiscountPrice : null,
                product.InStock,
                product.ProductImages!.Take(4).FirstOrDefault() != null
                ? _fileService.GetFileUrl(product.ProductImages.Take(4).FirstOrDefault()!.ImageNameInFileSystem, UploadDirectory.Product)
                : String.Empty,
                 ProductDisconts


                );

            return PartialView("~/Areas/Client/Views/Shared/Partials/_ModalPartial.cshtml", model);
        }


    }
}
