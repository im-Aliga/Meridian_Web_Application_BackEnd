﻿using Meridian_Web.Areas.Client.ViewComponents;
using Meridian_Web.Areas.Client.ViewModels.Basket;
using Meridian_Web.Database;
using Meridian_Web.Services.Abstract;
using Meridian_Web.Services.Abstracts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;

namespace Meridian_Web.Areas.Client.Controllers
{
        [Area("client")]
        [Route("basket")]
    public class BasketController : Controller
    {


        private readonly DataContext _dataContext;
        private readonly IBasketService _basketService;
        private readonly IUserService _userService;

        public BasketController(DataContext dataContext, IBasketService basketService, IUserService userService)
        {
            _dataContext = dataContext;
            _basketService = basketService;
            _userService = userService;
        }
        [HttpGet("add/{id}", Name = "client-basket-add")]
        public async Task<IActionResult> AddProductAsync([FromRoute] int id)
        {
            var product = await _dataContext.Products
                .Include(b => b.ProductImages).FirstOrDefaultAsync(b => b.Id == id);
            if (product is null)
            {
                return NotFound();
            }

            var productsCookieViewModel = await _basketService.AddBasketProductAsync(product);
            if (productsCookieViewModel.Any())
            {
                return ViewComponent(nameof(MiniBasketComponent), productsCookieViewModel);
            }

            return ViewComponent(nameof(MiniBasketComponent));
        }

        [HttpGet("delete/{id}", Name = "client-basket-delete")]
        public async Task<IActionResult> DeleteProductAsync([FromRoute] int id)
        {
            var product = await _dataContext.Products.FirstOrDefaultAsync(b => b.Id == id);
            if (product is null)
            {
                return NotFound();
            }

            var productCookieValue = HttpContext.Request.Cookies["products"];
            if (productCookieValue is null)
            {
                return NotFound();
            }

            if (_userService.IsAuthenticated)
            {
                var basketProduct = await _dataContext.BasketProducts.FirstOrDefaultAsync(bp => bp.ProductId == product.Id);
                _dataContext.BasketProducts.Remove(basketProduct);
                await _dataContext.SaveChangesAsync();

               return ViewComponent(nameof(MiniBasketComponent));

            }
            


            var productsCookieViewModel = JsonSerializer.Deserialize<List<ProductCookieViewModel>>(productCookieValue);
            productsCookieViewModel!.RemoveAll(pcvm => pcvm.Id == id);

            HttpContext.Response.Cookies.Append("products", JsonSerializer.Serialize(productsCookieViewModel), new CookieOptions
            {
                MaxAge = TimeSpan.FromDays(1)
            });

            return ViewComponent(nameof(MiniBasketComponent), productsCookieViewModel);
        }
    }
}