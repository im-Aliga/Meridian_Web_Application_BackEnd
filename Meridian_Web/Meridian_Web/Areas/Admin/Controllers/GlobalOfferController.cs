using Meridian_Web.Areas.Admin.ViewModels.GlobalOffer;
using Meridian_Web.Database;
using Meridian_Web.Database.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Meridian_Web.Areas.Admin.Controllers
{
    [Area("admin")]
    [Route("admin/globaloffer")]
    public class GlobalOfferController : Controller
    {
        private readonly DataContext _dataContext;
        private readonly ILogger<GlobalOfferController> _logger;

        public GlobalOfferController(DataContext dataContext, ILogger<GlobalOfferController> logger)
        {
            _dataContext = dataContext;
            _logger = logger;
        }

        #region List
        [HttpGet("list", Name = "admin-globaloffer-list")]
        public async Task<IActionResult> ListAsync()
        {
            var model = await _dataContext.GlobalOffers
                .Select(c => new ListItemViewModel(c.Id, c.Title, c.MainContext, c.Context,c.ButtonContext,c.OfferTime, c.CreatedAt, c.UpdatedAt))
                .ToListAsync();

            return View(model);
        }
        #endregion

        #region Add
        [HttpGet("add", Name = "admin-globaloffer-add")]
        public async Task<IActionResult> AddAsync()
        {
            return View();
        }
        [HttpPost("add", Name = "admin-globaloffer-add")]
        public async Task<IActionResult> AddAsync(AddViewModel model)
        {
            if (!ModelState.IsValid) return View(model);

            var globalOffer = new GlobalOffer
            {
                Title = model.Title,
                MainContext = model.MainContext,
                Context = model.Context,
                OfferTime = model.OfferTime,
                ButtonContext = model.ButtonContext,
                
            };
            await _dataContext.GlobalOffers.AddAsync(globalOffer);
            await _dataContext.SaveChangesAsync();
            return RedirectToRoute("admin-globaloffer-list");
        }
        #endregion

        #region Update
        [HttpGet("update/{id}", Name = "admin-globaloffer-update")]
        public async Task<IActionResult> UpdateAsync([FromRoute] int id)
        {
            var globalOffer = await _dataContext.GlobalOffers.FirstOrDefaultAsync(n => n.Id == id);
            if (globalOffer is null) return NotFound();
            var model = new UpdateViewModel
            {
                Id = id,
                Title = globalOffer.Title,
                Context = globalOffer.Context,
                MainContext= globalOffer.MainContext,
                ButtonContext= globalOffer.ButtonContext,
                OfferTime= globalOffer.OfferTime,
            };

            return View(model);
        }

        [HttpPost("update/{id}", Name = "admin-globaloffer-update")]
        public async Task<IActionResult> UpdateAsync(UpdateViewModel model)
        {
            var globalOffer = await _dataContext.GlobalOffers.FirstOrDefaultAsync(n => n.Id == model.Id);
            if (globalOffer is null) return NotFound();
            if (!ModelState.IsValid) return View(model);
            if (!_dataContext.GlobalOffers.Any(n => n.Id == model.Id)) return View(model);

            globalOffer.Title = model.Title;
            globalOffer.MainContext = model.MainContext;
            globalOffer.Context=model.Context;
            globalOffer.ButtonContext= model.ButtonContext;
            globalOffer.OfferTime = model.OfferTime;
            
            await _dataContext.SaveChangesAsync();

            return RedirectToRoute("admin-globaloffer-list");

        }

        #endregion

        #region Delete
        [HttpPost("delete/{id}", Name = "admin-globaloffer-delete")]
        public async Task<IActionResult> DeleteAsync(UpdateViewModel model)
        {
            var globalOffer = await _dataContext.GlobalOffers.FirstOrDefaultAsync(n => n.Id == model.Id);
            if (globalOffer is null) return NotFound();
            _dataContext.GlobalOffers.Remove(globalOffer);
            await _dataContext.SaveChangesAsync();



            return RedirectToRoute("admin-globaloffer-list");

        }
        #endregion
    }
}
