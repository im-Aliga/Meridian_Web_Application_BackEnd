using Meridian_Web.Database;
using Microsoft.AspNetCore.Mvc;

namespace Meridian_Web.Areas.Admin.Controllers
{
    [Area("admin")]
    [Route("admin/product")]
    public class ProductController : Controller
    {
        private readonly DataContext _dataContext;
        private readonly ILogger<ProductController> _logger;


        public ProductController(DataContext dataContext, ILogger<ProductController> logger)
        {
            _dataContext = dataContext;
            _logger = logger;

        }
        //#region List

        //[HttpGet("list", Name = "admin-product-list")]
        //public async Task<IActionResult> ListAsync()
        //{
        //    var model = await _dataContext.Plants.Select(p => new PlantListViewModel(p.Id, p.Title, p.Content,
        //        p.Price,
        //        p.CreatedAt,
        //        p.PlantCatagories.Select(pc => pc.Category).Select(c => new PlantListViewModel.CategoryViewModeL(c.Title, c.Parent.Title)).ToList(),
        //        p.PlantColors.Select(pc => pc.Color).Select(c => new PlantListViewModel.ColorViewModeL(c.Name)).ToList(),
        //        p.PlantSizes.Select(ps => ps.Size).Select(s => new PlantListViewModel.SizeViewModeL(s.Name)).ToList(),
        //        p.PlantTags.Select(ps => ps.Tag).Select(s => new PlantListViewModel.TagViewModel(s.TagName)).ToList()
        //        )).ToListAsync();


        //    return View(model);
        //}

        //#endregion
    }
}
