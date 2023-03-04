using Meridian_Web.Areas.Admin.ViewModels.Product;
using Meridian_Web.Database;
using Meridian_Web.Database.Models;
using Meridian_Web.Services.Abstract;
using Microsoft.EntityFrameworkCore;

namespace Meridian_Web.Services.Concretes
{
    public class ProductService : IProductService
    {
        private readonly DataContext _dataContext;

        public ProductService(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public void FindPers(AddViewModel model,Product product)
        {
           
        }

        public async Task<AddViewModel> GetViewForModel(AddViewModel model)
        {

            model.Brands =await _dataContext.Brands
                .Select(b => new BrandListItemViewModel(b.Id, b.Name))
                .ToListAsync();
            model.Discounts = await _dataContext.Disconts
                .Select(b => new DiscountListViewModel(b.Id, b.Title))
                .ToListAsync();
            model.Categories = await _dataContext.Categories
               .Select(c => new CatagoryListItemViewModel(c.Id, c.Title))
            .ToListAsync();

            model.Sizes = await _dataContext.Sizes
             .Select(c => new SizeListItemViewModel(c.Id, c.Name))
            .ToListAsync();

            model.Colors = await _dataContext.Colors
             .Select(c => new ColorListItemViewModel(c.Id, c.Name))
            .ToListAsync();

            model.Tags = await _dataContext.Tags
             .Select(c => new TagListItemViewModel(c.Id, c.TagName))
             .ToListAsync();


            return model;
        }

       
    }
}
