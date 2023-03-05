
using Meridian_Web.Areas.Admin.ViewModels.Payment;
using Meridian_Web.Contracts.File;
using Meridian_Web.Database;
using Meridian_Web.Database.Models;
using Meridian_Web.Services.Abstracts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Meridian_Web.Areas.Admin.Controllers
{
    [Area("admin")]
    [Route("admin/payment")]
    public class PaymentController : Controller
    {
        private readonly DataContext _dataContext;
        private readonly IFileService _fileService;

        public PaymentController(DataContext dataContext, IFileService fileService)
        {
            _dataContext = dataContext;
            _fileService = fileService;
        }

        #region List
        [HttpGet("list", Name = "admin-payment-list")]
        public async Task<IActionResult> ListAsync()
        {
            var model = await _dataContext.Payments.Select(u => new ListPaymentViewModel(
                u.Id,
                u.Title,
                u.Context,
                _fileService.GetFileUrl(u.ImageNameInFileSystem, UploadDirectory.Payment),
                u.CreatedAt,
                u.UpdatedAt)).ToListAsync();
            return View(model);
        }
        #endregion

        #region Add

        [HttpGet("add", Name = "admin-payment-add")]
        public async Task<IActionResult> AddAsync()
        {

            return View();
        }

        [HttpPost("add", Name = "admin-payment-add")]
        public async Task<IActionResult> AddAsync(AddPaymentViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var imageNameInSystem = await _fileService.UploadAsync(model!.Image, UploadDirectory.Payment);

            await AddPayment(model.Image!.FileName, imageNameInSystem);


            return RedirectToRoute("admin-payment-list");


            async Task AddPayment(string imageName, string imageNameInSystem)
            {
                var payment = new Payment
                {
                    Title = model.Title,
                    Context = model.Content,
                    ImageName = imageName,
                    ImageNameInFileSystem = imageNameInSystem,
                };

                await _dataContext.Payments.AddAsync(payment);
                await _dataContext.SaveChangesAsync();
            }
        }
        #endregion

        #region Update
        [HttpGet("update/{id}", Name = "admin-payment-update")]
        public async Task<IActionResult> UpdateAsync([FromRoute] int id)
        {
            var payment = await _dataContext.Payments.FirstOrDefaultAsync(b => b.Id == id);
            if (payment is null) return NotFound();


            var model = new AddPaymentViewModel
            {
                Id = payment.Id,
                Title = payment.Title,
                Content = payment.Context,
                ImageUrl = _fileService.GetFileUrl(payment.ImageNameInFileSystem, UploadDirectory.Payment)
            };

            return View(model);
        }

        [HttpPost("update/{id}", Name = "admin-payment-update")]
        public async Task<IActionResult> UpdateAsync(AddPaymentViewModel model)
        {
            var payment = await _dataContext.Payments.FirstOrDefaultAsync(b => b.Id == model.Id);

            if (payment is null)return NotFound();
            

            if (!ModelState.IsValid) return View(model);



            if (model.Image != null)
            {
                await _fileService.DeleteAsync(payment.ImageNameInFileSystem, UploadDirectory.Payment);
                var imageFileNameInSystem = await _fileService.UploadAsync(model.Image, UploadDirectory.Payment);
                await UpdatePaymentAsync(model.Image.FileName, imageFileNameInSystem);

            }
            else
            {
                await UpdatePaymentAsync(payment.ImageName, payment.ImageNameInFileSystem);
            }


            return RedirectToRoute("admin-payment-list");


            async Task UpdatePaymentAsync(string imageName, string imageNameInFileSystem)
            {
                payment.Title = model.Title;
                payment.Context = model.Content;
                payment.ImageNameInFileSystem = imageName;
                payment.ImageNameInFileSystem = imageNameInFileSystem;
                await _dataContext.SaveChangesAsync();
            }
        }
        #endregion

        #region Delete
        [HttpPost("delete/{id}", Name = "admin-payment-delete")]
        public async Task<IActionResult> DeleteAsync([FromRoute] int id)
        {
            var payment = await _dataContext.Payments.FirstOrDefaultAsync(b => b.Id == id);
            if (payment is null) return NotFound();
            await _fileService.DeleteAsync(payment.ImageNameInFileSystem, UploadDirectory.Payment);

            _dataContext.Payments.Remove(payment);
            await _dataContext.SaveChangesAsync();

            return RedirectToRoute("admin-payment-list");
        }
        #endregion

    }
}
