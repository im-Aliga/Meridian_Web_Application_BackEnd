﻿using Meridian_Web.Areas.Admin.ViewModels.Role;
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
                var model = _dataContext.Roles.Select(r => new RoleViewModel(r.Id, r.Name)).ToList();
                return View(model);
            }

            return RedirectToRoute("admin-user-list");
        }
        #endregion
    }
}