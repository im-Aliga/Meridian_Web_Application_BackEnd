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
            if (!ModelState.IsValid)
            {

                return View(model);
            }
            var user = await CreateUser();

            await _dataContext.SaveChangesAsync();

            async Task<Basket> CreateBasketAsync()
            {
                //Create basket process
                var basket = new Basket
                {
                    User = user,
                   
                };
                await _dataContext.Baskets.AddAsync(basket);

                return basket;
            }
            async Task<Wishlist> CreateWishlistAsync()
            {
                //Create basket process
                var wishlist = new Wishlist
                {
                    User = user,
                   
                };
                await _dataContext.Wishlists.AddAsync(wishlist);

                return wishlist;
            }

            async Task<string> RequiredPassword(string password, string comfirmPassword)
            {
                if (password == comfirmPassword)
                {
                    BC.HashPassword(password);
                }
                else
                {
                    return await Task.FromResult("The password and confirm password do not match.");
                }
                return password;
            }

            async Task<User> CreateUser()
            {
                var user = new User
                {
                    Email = model.Email,
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    Password = await RequiredPassword(model.Password,model.ConfirmPassword),
                    CreatedAt = DateTime.Now,
                    UpdatedAt = DateTime.Now,
                    RoleId = model.RoleId,

                };


                await _dataContext.Users.AddAsync(user);
                return user;
            }

            IActionResult GetView()
            {
                var model = _dataContext.Roles.Select(r => new RoleViewModel(r.Id, r.Name)).ToList();
                return View(model);
            }

            return RedirectToRoute("admin-user-list");
        }
        #endregion
    }
}
