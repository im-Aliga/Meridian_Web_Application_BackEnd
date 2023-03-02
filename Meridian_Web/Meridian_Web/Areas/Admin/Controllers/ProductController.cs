using Meridian_Web.Areas.Admin.ViewModels.Plant;
using Meridian_Web.Areas.Admin.ViewModels.Product;
using Meridian_Web.Database;
using Meridian_Web.Database.Models;
using Meridian_Web.Services.Abstract;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Runtime.CompilerServices;

namespace Meridian_Web.Areas.Admin.Controllers
{
    [Area("admin")]
    [Route("admin/product")]
    public class ProductController : Controller
    {
        private readonly DataContext _dataContext;
        private readonly ILogger<ProductController> _logger;
        private readonly IProductService _productService;


        public ProductController(DataContext dataContext, ILogger<ProductController> logger, IProductService productService)
        {
            _dataContext = dataContext;
            _logger = logger;
            _productService = productService;
        }
        #region List

        [HttpGet("list", Name = "admin-product-list")]
        public async Task<IActionResult> ListAsync()
        {
            var model = await _dataContext.Products.Select(p => new ProductListViewModel(
                p.Id, 
                p.Title,
                p.Price, 
                p.DiscountPrice,
                p.InStock,
                p.Content,
                p.CreatedAt,
                p.UpdatedAt,
                p.ProductCatagories.Select(pc => pc.Category).Select(c => new ProductListViewModel.CategoryViewModeL(c.Title)).ToList(),
                p.ProductColors.Select(pc => pc.Color).Select(c => new ProductListViewModel.ColorViewModeL(c.Name)).ToList(),
                p.ProductBrands.Select(pb=>pb.Brand).Select(b=>new ProductListViewModel.BrandViewModel(b.Name)).ToList(),
                p.ProductDisconts.Select(pd => pd.Discont).Select(d => new ProductListViewModel.DiscountViewModel(d.Title, d.DiscontPers, d.DiscountTime)).ToList(),
                p.ProductSizes.Select(ps => ps.Size).Select(s => new ProductListViewModel.SizeViewModeL(s.Name)).ToList(),
                p.ProductTags.Select(ps => ps.Tag).Select(s => new ProductListViewModel.TagViewModel(s.TagName)).ToList()
                )).ToListAsync();


            return View(model);
        }

        #endregion

        #region Add
        [HttpGet("add", Name = "admin-product-add")]
        public async Task<IActionResult> AddAsync()
        {
            var model = new AddViewModel
            {
                Categories = await _dataContext.Categories
                    .Select(c => new CatagoryListItemViewModel(c.Id, c.Title))
                    .ToListAsync(),
                Sizes = await _dataContext.Sizes.Select(s => new SizeListItemViewModel(s.Id, s.Name)).ToListAsync(),
                Colors = await _dataContext.Colors.Select(c => new ColorListItemViewModel(c.Id, c.Name)).ToListAsync(),
                Tags = await _dataContext.Tags.Select(t => new TagListItemViewModel(t.Id, t.TagName)).ToListAsync(),
                Brands=await _dataContext.Brands.Select(b=>new BrandListItemViewModel(b.Id, b.Name)).ToListAsync(),
                Discounts=await _dataContext.Disconts.Select(d=>new DiscountListViewModel(d.Id,d.Title)).ToListAsync()
            };

            return View(model);
        }

        [HttpPost("add", Name = "admin-plant-add")]
        public async Task<IActionResult> AddAsync(AddViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return GetView(model);
            }

            foreach (var categoryId in model.CategoryIds)
            {
                if (!await _dataContext.Categories.AnyAsync(c => c.Id == categoryId))
                {
                    ModelState.AddModelError(string.Empty, "Something went wrong");
                    _logger.LogWarning($"Category with id({categoryId}) not found in db ");
                    return GetView(model);
                }

            }
            foreach (var brandId in model.BrandsIds)
            {
                if (!await _dataContext.Brands.AnyAsync(c => c.Id == brandId))
                {
                    ModelState.AddModelError(string.Empty, "Something went wrong");
                    _logger.LogWarning($"Brand with id({brandId}) not found in db ");
                    return GetView(model);
                }

            }
            foreach (var discountId in model.DiscountIds)
            {
                if (!await _dataContext.Disconts.AnyAsync(c => c.Id == discountId))
                {
                    ModelState.AddModelError(string.Empty, "Something went wrong");
                    _logger.LogWarning($"Brand with id({discountId}) not found in db ");
                    return GetView(model);
                }

            }

            foreach (var sizeId in model.SizeIds)
            {
                if (!await _dataContext.Sizes.AnyAsync(c => c.Id == sizeId))
                {
                    ModelState.AddModelError(string.Empty, "Something went wrong");
                    _logger.LogWarning($"Size with id({sizeId}) not found in db ");
                    return GetView(model);
                }

            }

            foreach (var colorId in model.ColorIds)
            {
                if (!await _dataContext.Colors.AnyAsync(c => c.Id == colorId))
                {
                    ModelState.AddModelError(string.Empty, "Something went wrong");
                    _logger.LogWarning($"Color with id({colorId}) not found in db ");
                    return GetView(model);
                }

            }

            foreach (var tagId in model.TagIds)
            {
                if (!await _dataContext.Tags.AnyAsync(c => c.Id == tagId))
                {
                    ModelState.AddModelError(string.Empty, "Something went wrong");
                    _logger.LogWarning($"Tag with id({tagId}) not found in db ");
                    return GetView(model);
                }

            }
            var discounts = await _dataContext.Disconts.ToListAsync();

            var product = new Product
            {
                Title = model.Title,
                Content = model.Content,
                Price = model.Price,
                InStock = model.InStock


            };
            var discount = discounts.FirstOrDefault(d => d.ProductDisconts.Any(pd => pd.ProductId == product.Id));
            if (discount != null)
            {
                decimal discountPercentage = discount.DiscontPers;
                product.DiscountPrice = product.Price * (1 - discountPercentage / 100);
            }
            else
            {
                product.DiscountPrice = null; 
            }



            await _dataContext.Products.AddAsync(product);

            foreach (var catagoryId in model.CategoryIds)
            {
                var productCatagory = new ProductCatagory
                {
                    CategoryId = catagoryId,
                    Product = product,
                };

                await _dataContext.ProductCatagories.AddAsync(productCatagory);
            }

            foreach (var colorId in model.ColorIds)
            {
                var productColor = new ProductColor
                {
                    ColorId = colorId,
                    Product = product,
                };

                await _dataContext.ProductColors.AddAsync(productColor);
            }

            foreach (var sizeId in model.SizeIds)
            {
                var productSize = new ProductSize
                {
                    SizeId = sizeId,
                    Product = product,
                };

                await _dataContext.ProductSizes.AddAsync(productSize);
            }

            foreach (var tagId in model.TagIds)
            {
                var productTag = new ProductTag
                {
                    TagId = tagId,
                    Product = product,
                };

                await _dataContext.ProductTags.AddAsync(productTag);
            }
            foreach (var brandId in model.BrandsIds)
            {
                var productBrand = new ProductBrand
                {
                    BrandId = brandId,
                    Product = product,
                };

                await _dataContext.ProductBrands.AddAsync(productBrand);
            }
            foreach (var discountId in model.DiscountIds)
            {
                var productDiscount = new ProductDiscont
                {
                    DiscontId = discountId,
                    Product = product,
                };

                await _dataContext.ProductDisconts.AddAsync(productDiscount);
            }

            await _dataContext.SaveChangesAsync();

            return RedirectToRoute("admin-product-list");



            IActionResult GetView(AddViewModel model)
            {
                model.Brands = _dataContext.Brands
                    .Select(b => new BrandListItemViewModel(b.Id, b.Name))
                    .ToList();
                model.Discounts = _dataContext.Disconts
                    .Select(b => new DiscountListViewModel(b.Id, b.Title))
                    .ToList();
                model.Categories = _dataContext.Categories
                   .Select(c => new CatagoryListItemViewModel(c.Id, c.Title))
                   .ToList();

                model.Sizes = _dataContext.Sizes
                 .Select(c => new SizeListItemViewModel(c.Id, c.Name))
                 .ToList();

                model.Colors = _dataContext.Colors
                 .Select(c => new ColorListItemViewModel(c.Id, c.Name))
                 .ToList();

                model.Tags = _dataContext.Tags
                 .Select(c => new TagListItemViewModel(c.Id, c.TagName))
                 .ToList();


                return View(model);
            }


          
        }

        #endregion
    }
}
