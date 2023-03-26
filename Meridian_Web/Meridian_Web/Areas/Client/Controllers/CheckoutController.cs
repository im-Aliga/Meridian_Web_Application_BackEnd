using Meridian_Web.Areas.Client.ViewModels.OrderProducts;
using Meridian_Web.Contracts.File;
using Meridian_Web.Database;
using Meridian_Web.Database.Models;
using Meridian_Web.Database.Models.Enums;
using Meridian_Web.Services.Abstracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DemoApplication.Areas.Client.Controllers
{
    [Area("client")]
    [Route("checkout")]
    [Authorize]
    public class CheckoutController : Controller
    {
        private readonly DataContext _dataContext;
        private readonly IUserService _userService;
        private readonly IFileService _fileService;
        private readonly IOrderService _orderService;


        public CheckoutController
            (DataContext dataContext,
            IUserService userService,
            IFileService fileService,
            IOrderService orderService)

        {
            _dataContext = dataContext;
            _userService = userService;
            _fileService = fileService;
            _orderService = orderService;

        }

        [HttpGet("order-products", Name = "client-checkout-order-products")]
        public async Task<IActionResult> OrderProducts()
        {
            var model = new OrdersProductsViewModel
            {
                Products = await _dataContext.BasketProducts
                    .Where(bp => bp.Basket!.UserId == _userService.CurrentUser.Id)
                    .Select(bp => new OrdersProductsViewModel.ItemViewModel
                    {
                        ImgUrl = _fileService.GetFileUrl(bp.Product.ProductImages.FirstOrDefault().ImageNameInFileSystem, UploadDirectory.Product),
                        Name = bp.Product!.Title,
                        Price = bp.Product.Price,
                        DiscountPrice = bp.Product.DiscountPrice,
                        Quantity = bp.Quantity,
                        Total = (bp.Product.DiscountPrice == null ? bp.Product.Price * bp.Quantity : bp.Product.DiscountPrice * bp.Quantity),
                    }).ToListAsync(),
                Summary = new OrdersProductsViewModel.SummaryViewModel
                {
                    Total = await _dataContext.BasketProducts
                        .Where(bp => bp.Basket!.UserId == _userService.CurrentUser.Id)
                        .SumAsync(bp => (bp.Product.DiscountPrice == null ? bp.Product!.Price * bp.Quantity : bp.Product.DiscountPrice * bp.Quantity))
                }
            };

            return View(model);
        }

        [HttpPost("place-order", Name = "client-checkout-place-order")]
        public async Task<IActionResult> PlaceOrder()
        {
            if (_userService.CurrentUser.UserAddress != null)
            {
              

                var basketProducts = await _dataContext.BasketProducts
                        .Include(bp => bp.Product)
                        .Where(bp => bp.Basket!.UserId == _userService.CurrentUser.Id)
                        .ToListAsync();

                var order = await CreateOrderAsync();

                await CreateAndFulfillOrderProductsAsync(order, basketProducts);

                order.Total = order.OrderProducts!.Sum(op => op.Total);

                await ResetBasketAsync(basketProducts);

                await _dataContext.SaveChangesAsync();
            }
           



            
            return RedirectToRoute("client-home-index");



            async Task ResetBasketAsync(List<BasketProduct> basketProducts)
            {
                await Task.Run(() => _dataContext.RemoveRange(basketProducts));
            }

            async Task CreateAndFulfillOrderProductsAsync(Order order, List<BasketProduct> basketProducts)
            {
                foreach (var basketProduct in basketProducts)
                {
                    var orderProduct = new OrderProduct
                    {
                        OrderId = order.Id,
                        ProductId = basketProduct.ProductId,
                        Price = basketProduct.Product!.Price,
                        Quantity = basketProduct.Quantity,
                        Total = (decimal)(basketProduct.Product!.DiscountPrice == null ? basketProduct.Product.Price * basketProduct.Quantity : basketProduct.Product.DiscountPrice * basketProduct.Quantity),
                    };

                    await _dataContext.AddAsync(orderProduct);
                }
            }

            async Task<Order> CreateOrderAsync()
            {
                var order = new Order
                {
                    Id = await _orderService.GenerateUniqueTrackingCodeAsync(),
                    UserId = _userService.CurrentUser.Id,
                    Status = OrderStatus.Created,
                };

                await _dataContext.Orders.AddAsync(order);

                return order;
            }
        }
    }
}
