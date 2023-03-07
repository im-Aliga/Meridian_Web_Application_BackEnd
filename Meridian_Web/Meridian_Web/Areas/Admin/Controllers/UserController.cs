using FluentValidation;
using Meridian_Web.Areas.Admin.Validators.Admin.User;
using Meridian_Web.Areas.Admin.ViewModels.Role;
using Meridian_Web.Areas.Admin.ViewModels.User;
using Meridian_Web.Database;
using Meridian_Web.Database.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BC = BCrypt.Net.BCrypt;

namespace Meridian_Web.Areas.Admin.Controllers
{
    [Area("admin")]
    [Route("admin/user")]
    public class UserController : Controller
    {
        private readonly DataContext _dataContext;

        public UserController(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        #region List
        [HttpGet("list", Name = "admin-user-list")]

        public async Task<IActionResult> ListAsync()
        {
            var model = await _dataContext.Users
                .OrderByDescending(a => a.CreatedAt)
                .Select(u => new ListUserViewModel(
                    u.Id, u.Email, u.FirstName, u.LastName, u.Password, u.UserAddress, u.Role != null ? u.Role.Name : null, u.CreatedAt, u.UpdatedAt))
                .ToListAsync();

            return View(model);
        }
        #endregion

        #region Add
        [HttpGet("add", Name = "admin-user-add")]
        public async Task<IActionResult> AddAsync()
        {
            var model = new AddUserViewModel
            {
                Roles = await _dataContext.Roles
                    .Select(r => new RoleViewModel(r.Id, r.Name))
                    .ToListAsync()

            };

            return View(model);
        }


        [HttpPost("add", Name = "admin-user-add")]
        public async Task<IActionResult> AddAsync(AddUserViewModel model)
        {
            var validator = new UserAddViewModelValidator(_dataContext);
            var validationResult = await validator.ValidateAsync(model);

            if (!validationResult.IsValid)
            {
                foreach (var error in validationResult.Errors)
                {
                    ModelState.AddModelError(error.PropertyName, error.ErrorMessage);
                }

                return GetView();
            }
            var address = await CreateUserAddress();
            var user = await CreateUser();

            await _dataContext.SaveChangesAsync();

            async Task<Basket> CreateBasketAsync()
            {
               
                var basket = new Basket
                {
                    User = user,
                   
                };
                await _dataContext.Baskets.AddAsync(basket);

                return basket;
            }
            async Task<Wishlist> CreateWishlistAsync()
            {
              
                var wishlist = new Wishlist
                {
                    User = user,
                   
                };
                await _dataContext.Wishlists.AddAsync(wishlist);

                return wishlist;
            }

            async Task<User> CreateUser()
            {
                var user = new User
                {
                    Email = model.Email,
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    UserAddress=address,
                    Password = BC.HashPassword(model.Password),
                    RoleId = model.RoleId,

                };


                await _dataContext.Users.AddAsync(user);
                return user;
            }

            async Task<UserAddress> CreateUserAddress()
            {
                var address = new UserAddress
                {
                    City = model.Address.City,
                    Address = model.Address.Address,

                };


                await _dataContext.UserAddresses.AddAsync(address);
                return address;
            }

            IActionResult GetView()
            {
                var roles = _dataContext.Roles.Select(r => new RoleViewModel(r.Id, r.Name)).ToList();
                var model = new AddUserViewModel { Roles = roles };
                return View(model);


            }

            return RedirectToRoute("admin-user-list");
        }
        #endregion

        #region Update
        [HttpGet("update/{id}", Name = "admin-user-update")]
        public async Task<IActionResult> UpdateAsync([FromRoute] Guid id)
        {
            var user = await _dataContext.Users.Include(u=>u.UserAddress).FirstOrDefaultAsync(u => u.Id == id);
            if (user is null)
            {
                return NotFound();
            }

            var address = new UserAdressViewModel
            {
              
                City = user.UserAddress.City,
                Address = user.UserAddress.Address,

            };
            var model = new UpdateUserViewModel
            {
                Id = user.Id,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                Address = address,
                RoleId = user.RoleId,
                Roles = await _dataContext.Roles.Select(r => new RoleViewModel(r.Id, r.Name)).ToListAsync()
            };


            return View(model);
        }

        [HttpPost("update/{id}", Name = "admin-user-update")]
        public async Task<IActionResult> UpdateAsync([FromRoute] Guid id, [FromForm] UpdateUserViewModel model)
        {

            var validator = new UpdateUserViewModelValidator(_dataContext);
            var validationResult = await validator.ValidateAsync(model);

            if (!validationResult.IsValid)
            {
                foreach (var error in validationResult.Errors)
                {
                    ModelState.AddModelError(error.PropertyName, error.ErrorMessage);
                }

                return GetView();
            }
            var user = await _dataContext.Users.Include(u=>u.UserAddress).FirstOrDefaultAsync(a => a.Id == id);
            if (user is null) return NotFound();

            user.FirstName = model.FirstName;
            user.LastName = model.LastName;
            user.Email = model.Email;
            user.UserAddress.City = model.Address.City;
            user.UserAddress.Address = model.Address.Address;   
            user.RoleId = model.RoleId;


            IActionResult GetView()
            {
                var roles = _dataContext.Roles.Select(r => new RoleViewModel(r.Id, r.Name)).ToList();
                var model = new UpdateUserViewModel { Roles = roles };
                return View(model);


            }

            await _dataContext.SaveChangesAsync();


            return RedirectToRoute("admin-user-list");
        }
        #endregion

        #region Delete
        [HttpPost("delete/{id}", Name = "admin-user-delete")]
        public async Task<IActionResult> DeleteAsync(Guid id)
        {
            var user = await _dataContext.Users.FirstOrDefaultAsync(a => a.Id == id);
            if (user is null)
            {
                return NotFound();
            }

            _dataContext.Users.Remove(user);
            await _dataContext.SaveChangesAsync();

            return RedirectToRoute("admin-user-list");
        }
        #endregion
    }
}
