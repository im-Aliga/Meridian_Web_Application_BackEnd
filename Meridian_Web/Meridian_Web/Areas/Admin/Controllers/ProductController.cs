
using Meridian_Web.Areas.Admin.ViewModels.Product;
using Meridian_Web.Database;
using Meridian_Web.Database.Models;
using Meridian_Web.Services.Abstract;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;

namespace Meridian_Web.Areas.Admin.Controllers
{
    [Area("admin")]
    [Route("admin/product")]
    public class ProductController : Controller
    {
        private readonly DataContext _dataContext;
        private readonly ILogger<ProductController> _logger;
        private readonly IProductService _productService;



        public ProductController(DataContext dataContext, ILogger<ProductController> logger)
        {
            _dataContext = dataContext;
            _logger = logger;

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
                p.ProductBrands.Select(pb => pb.Brand).Select(b => new ProductListViewModel.BrandViewModel(b.Name)).ToList(),
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
                Brands = await _dataContext.Brands.Select(b => new BrandListItemViewModel(b.Id, b.Name)).ToListAsync(),
                Discounts = await _dataContext.Disconts.Select(d => new DiscountListViewModel(d.Id, d.Title)).ToListAsync()
            };

            return View(model);
        }

        [HttpPost("add", Name = "admin-product-add")]
        public async Task<IActionResult> AddAsync(AddViewModel model)
        {
            if (!ModelState.IsValid)
            {
                model = await _productService.GetViewForModel(model);
                return View(model);
            }

            foreach (var categoryId in model.CategoryIds)
            {
                if (!await _dataContext.Categories.AnyAsync(c => c.Id == categoryId))
                {
                    ModelState.AddModelError(string.Empty, "Something went wrong");
                    _logger.LogWarning($"Category with id({categoryId}) not found in db ");
                    model = await _productService.GetViewForModel(model);
                    return View(model);
                }

            }
            foreach (var brandId in model.BrandsIds)
            {
                if (!await _dataContext.Brands.AnyAsync(c => c.Id == brandId))
                {
                    ModelState.AddModelError(string.Empty, "Something went wrong");
                    _logger.LogWarning($"Brand with id({brandId}) not found in db ");
                    model = await _productService.GetViewForModel(model);
                    return View(model);
                }

            }
            if (model.DiscountIds != null)
            {
                foreach (var discountId in model.DiscountIds)
                {
                    if (!await _dataContext.Disconts.AnyAsync(c => c.Id == discountId))
                    {
                        ModelState.AddModelError(string.Empty, "Something went wrong");
                        _logger.LogWarning($"Brand with id({discountId}) not found in db ");
                        model = await _productService.GetViewForModel(model);
                        return View(model);
                    }

                }

            }


            foreach (var sizeId in model.SizeIds)
            {
                if (!await _dataContext.Sizes.AnyAsync(c => c.Id == sizeId))
                {
                    ModelState.AddModelError(string.Empty, "Something went wrong");
                    _logger.LogWarning($"Size with id({sizeId}) not found in db ");
                    model = await _productService.GetViewForModel(model);
                    return View(model);
                }

            }

            foreach (var colorId in model.ColorIds)
            {
                if (!await _dataContext.Colors.AnyAsync(c => c.Id == colorId))
                {
                    ModelState.AddModelError(string.Empty, "Something went wrong");
                    _logger.LogWarning($"Color with id({colorId}) not found in db ");
                    model = await _productService.GetViewForModel(model);
                    return View(model);
                }

            }

            foreach (var tagId in model.TagIds)
            {
                if (!await _dataContext.Tags.AnyAsync(c => c.Id == tagId))
                {
                    ModelState.AddModelError(string.Empty, "Something went wrong");
                    _logger.LogWarning($"Tag with id({tagId}) not found in db ");
                    model = await _productService.GetViewForModel(model);
                    return View(model);
                }

            }


            var product = new Product
            {
                Title = model.Title,
                Content = model.Content,
                Price = model.Price,
                InStock = model.InStock,
            };

            if (model.DiscountIds != null)
            {
                var discounts = await _dataContext.ProductDisconts.Select(d => d.DiscontId).ToListAsync();
                var items = discounts.Intersect(model.DiscountIds).ToList();


                foreach (var item in items)
                {
                    var discasount =  await _dataContext.Disconts.FirstOrDefaultAsync(pd => pd.Id == item);
                    if (discasount != null)
                    {
                        decimal discountPercentage = discasount.DiscontPers;
                        var sum = (product.Price * discasount.DiscontPers) / 100;
                        product.DiscountPrice = product.Price - sum;
                    }
                    else
                    {
                        product.DiscountPrice = null;
                    }

                }

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

            if (model.DiscountIds != null)
            {

                foreach (var discountId in model.DiscountIds)
                {
                    var productDiscount = new ProductDiscont
                    {
                        DiscontId = discountId,
                        Product = product,
                    };

                    await _dataContext.ProductDisconts.AddAsync(productDiscount);
                }

            }



            await _dataContext.Products.AddAsync(product);

            await _dataContext.SaveChangesAsync();

            return RedirectToRoute("admin-product-list");







        }

        #endregion

        #region Update
        [HttpGet("update/{id}", Name = "admin-product-update")]
        public async Task<IActionResult> UpdateAsync([FromRoute] int id)
        {
            var product = await _dataContext.Products
                .Include(c => c.ProductCatagories)
                .Include(c => c.ProductTags)
                .Include(s => s.ProductBrands)
                .Include(t => t.ProductColors)
                .Include(s => s.ProductSizes)
                .Include(d => d.ProductDisconts)
                .FirstOrDefaultAsync(p => p.Id == id);

            if (product is null)
            {
                return NotFound();
            }

            var model = new UpdateViewModel
            {
                Id = product.Id,
                Title = product.Title,
                Content = product.Content,
                Price = product.Price,
                InStock = product.InStock,
                Categories = await _dataContext.Categories.Select(c => new CatagoryListItemViewModel(c.Id, c.Title)).ToListAsync(),
                CategoryIds = product.ProductCatagories.Select(pc => pc.CategoryId).ToList(),
                Sizes = await _dataContext.Sizes.Select(c => new SizeListItemViewModel(c.Id, c.Name)).ToListAsync(),
                SizeIds = product.ProductSizes.Select(pc => pc.SizeId).ToList(),
                Colors = await _dataContext.Colors.Select(c => new ColorListItemViewModel(c.Id, c.Name)).ToListAsync(),
                ColorIds = product.ProductColors.Select(pc => pc.ColorId).ToList(),
                Tags = await _dataContext.Tags.Select(c => new TagListItemViewModel(c.Id, c.TagName)).ToListAsync(),
                TagIds = product.ProductTags.Select(pc => pc.TagId).ToList(),
                Brands = await _dataContext.Brands.Select(c => new BrandListItemViewModel(c.Id, c.Name)).ToListAsync(),
                BrandsIds = product.ProductBrands.Select(pc => pc.BrandId).ToList(),
                Discounts = await _dataContext.Disconts.Select(d => new DiscountListViewModel(d.Id, d.Title)).ToListAsync(),
                DiscountIds = product.ProductDisconts.Select(d => d.DiscontId).ToList()

            };

            return View(model);

        }

        [HttpPost("update/{id}", Name = "admin-product-update")]
        public async Task<IActionResult> UpdateAsync(UpdateViewModel model)
        {
            var product = await _dataContext.Products
                    .Include(c => c.ProductCatagories)
                    .Include(c => c.ProductTags)
                    .Include(s => s.ProductBrands)
                    .Include(t => t.ProductColors)
                    .Include(s => s.ProductSizes)
                    .Include(d => d.ProductDisconts)
                    .FirstOrDefaultAsync(p => p.Id == model.Id);

            if (product is null)
            {
                return NotFound();
            }

            if (!ModelState.IsValid)
            {
                return GetView(model);
            }

            if (model.DiscountIds != null)
            {
                foreach (var discountId in model.DiscountIds)
                {
                    if (!await _dataContext.Disconts.AnyAsync(c => c.Id == discountId))
                    {
                        ModelState.AddModelError(string.Empty, "Something went wrong");
                        _logger.LogWarning($"Brand with id({discountId}) not found in db ");
                        return GetView(model);
                    }

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

            foreach (var categoryId in model.CategoryIds)
            {
                if (!await _dataContext.Categories.AnyAsync(c => c.Id == categoryId))
                {
                    ModelState.AddModelError(string.Empty, "Something went wrong");
                    _logger.LogWarning($"Category with id({categoryId}) not found in db ");
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
            

            product.Title = model.Title;
            product.Content = model.Content;
            product.Price = model.Price;
            product.InStock = model.InStock;

            if (model.DiscountIds != null)
            {
                var discounts = _dataContext.ProductDisconts.Select(d => d.DiscontId).ToList();
                var items = discounts.Intersect(model.DiscountIds).ToList();

                foreach (var item in items)
                {
                    var discasount = _dataContext.Disconts.FirstOrDefault(pd => pd.Id == item);
                    if (discasount != null)
                    {
                        decimal discountPercentage = discasount.DiscontPers;
                        var sum = (product.Price * discasount.DiscontPers) / 100;
                        product.DiscountPrice = product.Price - sum;
                    }
                    else
                    {
                        product.DiscountPrice = null;
                    }
                }
            }
            else
            {

                product.DiscountPrice =null;
                product.ProductDisconts = null;
                


            }



            #region Catagory
            var categoriesInDb = product.ProductCatagories.Select(bc => bc.CategoryId).ToList();
            var categoriesToRemove = categoriesInDb.Except(model.CategoryIds).ToList();
            var categoriesToAdd = model.CategoryIds.Except(categoriesInDb).ToList();

            product.ProductCatagories.RemoveAll(bc => categoriesToRemove.Contains(bc.CategoryId));

            foreach (var categoryId in categoriesToAdd)
            {
                var productCategory = new ProductCatagory
                {
                    CategoryId = categoryId,
                    Product = product,
                };

                await _dataContext.ProductCatagories.AddAsync(productCategory);
            }
            #endregion

            #region Color
            var colorInDb = product.ProductColors.Select(bc => bc.ColorId).ToList();
            var colorToRemove = colorInDb.Except(model.ColorIds).ToList();
            var colorToAdd = model.ColorIds.Except(colorInDb).ToList();

            product.ProductColors.RemoveAll(bc => colorToRemove.Contains(bc.ColorId));


            foreach (var colorId in colorToAdd)
            {
                var productColor = new ProductColor
                {
                    ColorId = colorId,
                    Product = product,
                };

                await _dataContext.ProductColors.AddAsync(productColor);
            }
            #endregion

            #region Size
            var sizeInDb = product.ProductSizes.Select(bc => bc.SizeId).ToList();
            var sizeToRemove = sizeInDb.Except(model.SizeIds).ToList();
            var sizeToAdd = model.SizeIds.Except(sizeInDb).ToList();

            product.ProductSizes.RemoveAll(bc => sizeToRemove.Contains(bc.SizeId));


            foreach (var sizeId in sizeToAdd)
            {
                var productSize = new ProductSize
                {
                    SizeId = sizeId,
                    Product = product,
                };

                await _dataContext.ProductSizes.AddAsync(productSize);
            }

            #endregion

            #region Tag
            var tagInDb = product.ProductTags.Select(bc => bc.TagId).ToList();
            var tagToRemove = tagInDb.Except(model.TagIds).ToList();
            var tagToAdd = model.TagIds.Except(tagInDb).ToList();

            product.ProductTags.RemoveAll(bc => tagToRemove.Contains(bc.TagId));


            foreach (var tagId in tagToAdd)
            {
                var productTag = new ProductTag
                {
                    TagId = tagId,
                    Product = product,
                };

                await _dataContext.ProductTags.AddAsync(productTag);
            }
            #endregion

            #region Brand
            var brandInDb = product.ProductBrands.Select(bc => bc.BrandId).ToList();
            var brandToRemove = brandInDb.Except(model.BrandsIds).ToList();
            var brandToAdd = model.BrandsIds.Except(brandInDb).ToList();

            product.ProductBrands.RemoveAll(bc => brandToRemove.Contains(bc.BrandId));


            foreach (var brandId in brandToAdd)
            {
                var productBrand = new ProductBrand
                {
                    BrandId = brandId,
                    Product = product,
                };

                await _dataContext.ProductBrands.AddAsync(productBrand);
            }
            #endregion

            #region Discount

            if (model.DiscountIds != null)
            {
                var discountInDb = product.ProductDisconts.Select(bc => bc.DiscontId).ToList();
                var discountToRemove = discountInDb.Except(model.DiscountIds).ToList();
                var discountToAdd = model.DiscountIds.Except(discountInDb).ToList();

                product.ProductDisconts.RemoveAll(bc => discountToRemove.Contains(bc.DiscontId));


                foreach (var discountId in discountToAdd)
                {
                    var productDiscount = new ProductDiscont
                    {
                        DiscontId = discountId,
                        Product = product,
                    };

                    await _dataContext.ProductDisconts.AddAsync(productDiscount);
                }
            }

            #endregion
            await _dataContext.SaveChangesAsync();

            return RedirectToRoute("admin-product-list");



            IActionResult GetView(UpdateViewModel model)
            {
                model.Categories = _dataContext.Categories
                   .Select(c => new CatagoryListItemViewModel(c.Id, c.Title))
                   .ToList();
                model.CategoryIds = product.ProductCatagories.Select(c => c.CategoryId).ToList();


                model.Sizes = _dataContext.Sizes
                 .Select(c => new SizeListItemViewModel(c.Id, c.Name))
                 .ToList();
                model.SizeIds = product.ProductSizes.Select(c => c.SizeId).ToList();



                model.Colors = _dataContext.Colors
                 .Select(c => new ColorListItemViewModel(c.Id, c.Name))
                 .ToList();
                model.ColorIds = product.ProductColors.Select(c => c.ColorId).ToList();

                model.Brands = _dataContext.Brands
                    .Select(b => new BrandListItemViewModel(b.Id, b.Name))
                    .ToList();
                model.BrandsIds = product.ProductBrands.Select(b => b.BrandId).ToList();

                model.Discounts = _dataContext.Disconts
                    .Select(d => new DiscountListViewModel(d.Id, d.Title))
                    .ToList();
                model.DiscountIds = product.ProductDisconts.Select(d => d.Id).ToList();

                model.Tags = _dataContext.Tags
                 .Select(c => new TagListItemViewModel(c.Id, c.TagName))
                 .ToList();

                model.TagIds = product.ProductTags.Select(c => c.TagId).ToList();

                return View(model);
            }
            

        }
        #endregion

        #region Delete
        [HttpPost("delete/{id}", Name = "admin-product-delete")]
        public async Task<IActionResult> DeleteAsync([FromRoute] int id)
        {
            var products = await _dataContext.Products.FirstOrDefaultAsync(p => p.Id == id);

            if (products is null)
            {
                return NotFound();
            }

            _dataContext.Products.Remove(products);
            await _dataContext.SaveChangesAsync();
            return RedirectToRoute("admin-product-list");
        }
        #endregion
    }
}
