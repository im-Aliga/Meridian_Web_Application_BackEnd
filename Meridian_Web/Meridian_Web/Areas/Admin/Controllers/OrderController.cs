using Meridian_Web.Areas.Admin.ViewModels.Category;
using Meridian_Web.Areas.Admin.ViewModels.FeedBack;
using Meridian_Web.Areas.Admin.ViewModels.Order;
using Meridian_Web.Areas.Client.ViewModels.OrderProducts;
using Meridian_Web.Contracts.Email;
using Meridian_Web.Contracts.File;
using Meridian_Web.Contracts.Order;
using Meridian_Web.Database;
using Meridian_Web.Database.Models;
using Meridian_Web.Services.Abstracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BackEndFinalProject.Areas.Admin.Controllers
{
    [Area("admin")]
    [Route("admin/order")]
    [Authorize(Roles = "admin")]
    public class OrderController : Controller
    {
        private readonly DataContext _dataContext;
        private readonly IFileService _fileService;
        private readonly IEmailService _emailService;

        public OrderController(DataContext dataContext, IFileService fileService, IEmailService emailService)
        {
            _dataContext = dataContext;
            _fileService = fileService;
            _emailService = emailService;
        }
        #region List
        [HttpGet("list", Name = "admin-order-list")]
        public async Task<IActionResult> ListAsync()
        {
            var model = await _dataContext.Orders
                .Select(u => new ListOrderViewModel(
                  u.Id, u.Status, u.Total, u.CreatedAt, u.UpdatedAt))
                .ToListAsync();

            return View(model);
        }
        #endregion

        #region Update
        [HttpGet("update/{id}", Name = "admin-order-update")]
        public async Task<IActionResult> UpdateAsync([FromRoute] string id)
        {
            var order = await _dataContext.Orders.FirstOrDefaultAsync(n => n.Id == id);


            if (order is null) return NotFound();

            var model = new UpdateOrderViewModel
            {
                Id = id,
            };

            return View(model);
        }

        [HttpPost("update/{id}", Name = "admin-order-update")]
        public async Task<IActionResult> UpdateAsync(string id, UpdateOrderViewModel model)
        {
            var order = await _dataContext.Orders.Include(u => u.User).Include(o => o.OrderProducts).FirstOrDefaultAsync(o => o.Id == id);

            if (order is null)
            {
                return NotFound();
            }
            order.Status = model.Status;

            var stausMessageDto = PrepareStausMessage(order.User.Email);
            _emailService.Send(stausMessageDto);


           await _dataContext.SaveChangesAsync();

            return RedirectToRoute("admin-order-list");
            MessageDto PrepareStausMessage(string email)
            {
                string body = "Order Has Been Updated";

                string subject = EmailMessages.Subject.ORDER_ACTIVATION_MESSAGE;

                return new MessageDto(email, subject, body);
            }
        }

        #endregion

        [HttpGet("list/{id}", Name = "admin-orderProduct-list")]
        public async Task<IActionResult> OrderProductList(string id)
        {
            var order = await _dataContext.Orders.FirstOrDefaultAsync(o => o.Id == id);

            if (order is null)
            {
                return NotFound();
            }

            var model = await _dataContext.OrderProducts.Where(o => o.OrderId == order.Id)
                .Select(pc => new OrderProductListItemViewModel(
                        pc.Id,
                        pc.Product.ProductImages.Take(1).FirstOrDefault() != null
                        ? _fileService.GetFileUrl(pc.Product.ProductImages.Take(1).FirstOrDefault().ImageNameInFileSystem, UploadDirectory.Product)
                        : String.Empty,
                        pc.Product.Title,
                        pc.Quantity,
                        (int)pc.Total)).ToListAsync();


            return View(model);
        }
    }
}

