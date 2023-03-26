using BackEndFinalProject.Areas.Client.ViewModels.Home.Modal;
using Meridian_Web.Areas.Client.ViewComponents;
using Meridian_Web.Areas.Client.ViewModels.Basket;
using Meridian_Web.Database;
using Meridian_Web.Services.Abstract;
using Meridian_Web.Services.Abstracts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;
using System.Xml.Linq;

namespace Meridian_Web.Areas.Client.Controllers
{
    [Area("client")]
    [Route("cart")]
    public class CartController : Controller
    {
        private readonly DataContext _dataContext;
        private readonly IUserService _userService;
        private readonly IBasketService _basketService;


        public CartController(DataContext dataContext, IUserService userService, IBasketService basketService)
        {
            _dataContext = dataContext;
            _userService = userService;
            _basketService = basketService;
        }
        [HttpGet("list", Name = "client-cart-list")]
        public async Task<IActionResult> ListAsync()
        {
            return View();
        }

        [HttpGet("add/{id}", Name = "client-cartpagebasket-add")]
        public async Task<IActionResult> AddProduct([FromRoute] int id,ModalViewModel model)
        {
            var product = await _dataContext.Products.FirstOrDefaultAsync(p => p.Id == id);
            if (product is null)
            {
                return NotFound();
            }

            var productCookiViewModel = await _basketService.AddBasketProductAsync(product,model);
            if (productCookiViewModel.Any())
            {
                return ViewComponent(nameof(Cart), productCookiViewModel);
            }

            return ViewComponent(nameof(Cart),product);
        }


        [HttpGet("update", Name = "client-cartpagebasket-update")]
        public async Task<IActionResult> UpdateProduct()
        {
            return ViewComponent(nameof(MiniBasketComponent));
        }
        [HttpGet("basket-individual-delete/{id}", Name = "client-individual-basket-delete")]
        public async Task<IActionResult> DeleteIndividualProduct([FromRoute] int id)
        {

            var productCookieViewModel = new List<ProductCookieViewModel>();
            if (_userService.IsAuthenticated)
            {

                var basketProduct = await _dataContext.BasketProducts
                    .Include(p => p.Basket).FirstOrDefaultAsync(bp => bp.Basket.UserId == _userService.CurrentUser.Id && bp.ProductId == id);

                if (basketProduct is null)
                {
                    return NotFound();
                }

                if (basketProduct.Quantity > 1)
                {
                    basketProduct.Quantity -= 1;

                }
                else
                {
                    _dataContext.BasketProducts.Remove(basketProduct);
                }
            }
            else
            {
                var product = await _dataContext.Products.FirstOrDefaultAsync(p => p.Id == id);
                if (product is null)
                {
                    return NotFound();
                }
                var productCookieValue = HttpContext.Request.Cookies["products"];
                if (productCookieValue is null)
                {
                    return NotFound();
                }

                productCookieViewModel = JsonSerializer.Deserialize<List<ProductCookieViewModel>>(productCookieValue);

                foreach (var cookieItem in productCookieViewModel)
                {
                    if (cookieItem.Id == id)
                    {

                        if (cookieItem.Quantity > 1)
                        {
                            cookieItem.Quantity -= 1;
                            cookieItem.Total = cookieItem.Quantity * cookieItem.Price;
                        }
                        else
                        {
                            productCookieViewModel.RemoveAll(p => p.Id == cookieItem.Id);
                            break;
                        }
                    }
                }
                HttpContext.Response.Cookies.Append("products", JsonSerializer.Serialize(productCookieViewModel));
            }
            await _dataContext.SaveChangesAsync();
            return ViewComponent(nameof(Cart), productCookieViewModel);
        }



        [HttpGet("delete/{Id}", Name = "client-cart-delete")]
        public async Task<IActionResult> DeleteAsync([FromRoute] int Id)
        {
            if (_userService.IsAuthenticated)
            {

                var basketProduct = await _dataContext.BasketProducts
                        .FirstOrDefaultAsync(bp => bp.Basket.UserId == _userService.CurrentUser.Id && bp.ProductId == Id);

                if (basketProduct is null) return NotFound();

                _dataContext.BasketProducts.Remove(basketProduct);
            }
            else
            {

                var product = await _dataContext.Products.FirstOrDefaultAsync(b => b.Id == Id);
                if (product is null)
                {
                    return NotFound();
                }

                var productCookieValue = HttpContext.Request.Cookies["products"];
                if (productCookieValue is null)
                {
                    return NotFound();
                }

                var productsCookieViewModel = JsonSerializer.Deserialize<List<ProductCookieViewModel>>(productCookieValue);
                productsCookieViewModel!.RemoveAll(pcvm => pcvm.Id == Id);

                HttpContext.Response.Cookies.Append("products", JsonSerializer.Serialize(productsCookieViewModel));
            }


            await _dataContext.SaveChangesAsync();

            return RedirectToRoute("client-cart-list");
        }

        
        


    }
}
