
using Meridian_Web.Areas.Client.ViewModels.Account;
using Meridian_Web.Areas.Client.ViewModels.OrderProducts;
using Meridian_Web.Contracts.File;
using Meridian_Web.Database;
using Meridian_Web.Database.Models;
using Meridian_Web.Services.Abstracts;
using Meridian_Web.Services.Concretes;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Principal;
using BC = BCrypt.Net.BCrypt;

namespace Meridian_Web.Areas.Client.Controllers
{
    [Area("client")]
    [Route("account")]
    [Authorize]
    public class AccountController : Controller
    {
        private readonly DataContext _dataContext;
        private readonly IUserService _userService;
        private readonly IFileService _fileService;

        public AccountController(DataContext dataContext, IUserService userService, IFileService fileService)
        {
            _dataContext = dataContext;
            _userService = userService;
            _fileService = fileService;
        }
        [HttpGet("dashboard", Name = "client-account-dashboard")]
        public async Task<IActionResult> DashboardAsync()
        {
            var user = await _dataContext.Users.FirstOrDefaultAsync(u => u.Id == _userService.CurrentUser.Id);
            var model = new MyProfileViewModel
            {
                Name = user.FirstName,
                Surname = user.LastName,
                Email = user.Email

            };

            return View(model);
        }

        [HttpGet("edit", Name = "client-account-edit")]

        public async Task<IActionResult> EditProfileAsync()
        {

           var user =await _dataContext.Users.FirstOrDefaultAsync(u=>u.Id == _userService.CurrentUser.Id);
            var model = new EditMyProfileViewModel
            {
                Name = user.FirstName,
                Surname = user.LastName,
                Email = user.Email,
                Password = null,
                ConfirmPassword = null,
                CurrentPassword = null

            };

            return View(model);
        }
        [HttpPost("edit", Name = "client-account-edit")]

        public async Task<IActionResult> EditProfileAsync(EditMyProfileViewModel newUser)
        {

            if (!ModelState.IsValid)
            {
                return View(newUser);
            }

            var user = await _dataContext.Users.FirstOrDefaultAsync(u => u.Id == _userService.CurrentUser.Id);

            if (user is null)
            {
                return NotFound();
            }

            if (newUser.CurrentPassword == user.Password)
            {
                return View(newUser);
            }
            user.FirstName = newUser.Name;
            user.LastName = newUser.Surname;
            user.Email = newUser.Email;
            user.Password = BC.HashPassword(newUser.Password);
            await _dataContext.SaveChangesAsync();

            return RedirectToRoute("client-account-dashboard");
        }
        [HttpGet("address", Name = "client-account-address")]
        public async Task<IActionResult> AddressAsync()
        {
            var user = _userService.CurrentUser;

            var address = await _dataContext.UserAddresses.FirstOrDefaultAsync(a => a.UserId == user.Id);

            if (address is null)
            {
                return RedirectToRoute("client-account-edit-address", new EditAdressViewModel());
            }

            var model = new AdressListItemViewModel
            {
                User = $"{address.User.FirstName} {address.User.LastName}",
                City = address.City,
                Adress=address.Address
            };
            return View(model);
        }

        [HttpGet("editAddress", Name = "client-account-edit-address")]
        public async Task<IActionResult> EditAddress()
        {
            var user = _userService.CurrentUser;

            var address = await _dataContext.UserAddresses.FirstOrDefaultAsync(a => a.UserId == user.Id);

            if (address is null)
            {
                return View(new EditAdressViewModel());
            }


            var model = new EditAdressViewModel
            {
                Adress = address.Address,
                City = address.City,

            };
            return View(model);
        }

        [HttpPost("editAddress", Name = "client-account-edit-address")]
        public async Task<IActionResult> EditAddress(EditAdressViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var user = _userService.CurrentUser;

            var address = await _dataContext.UserAddresses.FirstOrDefaultAsync(a => a.UserId == user.Id);



            if (address is not null)
            {
                address.Address = model.Adress;
                address.City = model.City;
                

            }
            else
            {
                var newAddress = new UserAddress
                {
                    UserId = user.Id,
                    Address = model.Adress,
                    City = model.City,
                };
                await _dataContext.UserAddresses.AddAsync(newAddress);
            }

            await _dataContext.SaveChangesAsync();

            return RedirectToRoute("client-account-address");
        }

        [HttpGet("order", Name = "client-account-order")]
        public async Task<IActionResult> Order()
        {
            var model = await _dataContext.Orders.Where(o => o.UserId == _userService.CurrentUser.Id)
                  .Select(b => new OrderViewModel(b.Id, b.CreatedAt, b.Status, b.Total))
                  .ToListAsync();


            return View(model);
        }

        [HttpGet("list/{id}", Name = "client-orderProduct-list")]
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
