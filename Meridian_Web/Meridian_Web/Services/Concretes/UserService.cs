﻿
using Meridian_Web.Areas.Client.ViewModels.Authentication;
using Meridian_Web.Areas.Client.ViewModels.Basket;
using Meridian_Web.Areas.Client.ViewModels.Wishlist;
using Meridian_Web.Contracts.Email;
using Meridian_Web.Contracts.Identity;
using Meridian_Web.Database;
using Meridian_Web.Database.Models;
using Meridian_Web.Exceptions;
using Meridian_Web.Services.Abstracts;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using System.Text.Json;
using BC = BCrypt.Net.BCrypt;

namespace Meridian_Web.Services.Concretes
{
    public class UserService : IUserService
    {
        private readonly DataContext _dataContext;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IUserActivationService _userActivationService;
        private User _currentUser;

        public UserService(
            DataContext dataContext,
            IHttpContextAccessor httpContextAccessor,
            IUserActivationService userActivationService)
        {
            _dataContext = dataContext;
            _httpContextAccessor = httpContextAccessor;
            _userActivationService = userActivationService;
        }

        public bool IsAuthenticated
        {
            get => _httpContextAccessor.HttpContext!.User.Identity!.IsAuthenticated;
        }

        public User CurrentUser
        {
            get
            {
                if (_currentUser is not null)
                {
                    return _currentUser;
                }

                var idClaim = _httpContextAccessor.HttpContext!.User.Claims.FirstOrDefault(C => C.Type == CustomClaimNames.ID);
                if (idClaim is null)
                    throw new IdentityCookieException("Identity cookie not found");

                _currentUser = _dataContext.Users.First(u => u.Id == Guid.Parse(idClaim.Value));

                return _currentUser;
            }
        }

        public async Task<bool> CheckEmailConfirmedAsync(string? email)
        {
            return await _dataContext.Users.AnyAsync(u => u.Email == email && u.IsEmailConfirmed);
        }


        public string GetCurrentUserFullName()
        {
            return $"{CurrentUser.FirstName} {CurrentUser.LastName}";
        }

        public async Task<bool> CheckPasswordAsync(string? email, string? password)
        {
            var user = await _dataContext.Users.FirstOrDefaultAsync(u => u.Email == email);

            return user is not null && BC.Verify(password, user.Password);
        }

        public async Task SignInAsync(Guid id, string? role = null)
        {
            var claims = new List<Claim>
            {
                new Claim(CustomClaimNames.ID, id.ToString())
            };

            if (role is not null)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }

            var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            var userPrincipal = new ClaimsPrincipal(identity);

            await _httpContextAccessor.HttpContext!.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, userPrincipal);
        }

        public async Task SignInAsync(string? email, string? password, string? role = null)
        {
            var user = await _dataContext.Users.Include(x=>x.Basket).FirstAsync(u => u.Email == email);
            if (user is not null && BC.Verify(password, user.Password) && user.IsEmailConfirmed == true)
            {

                await SignInAsync(user.Id, role);
                //var productCookieValue = _httpContextAccessor.HttpContext!.Request.Cookies["products"];
                //if (productCookieValue is not null)
                //{
                //    var basket = user.Basket;
                //    var productsCookieViewModel = JsonSerializer.Deserialize<List<ProductCookieViewModel>>(productCookieValue);
                //    foreach (var productCookieViewModel in productsCookieViewModel)
                //    {
                //        var product = await _dataContext.Products.FirstOrDefaultAsync(b => b.Id == productCookieViewModel.Id);
                //        var basketProduct = new BasketProduct
                //        {
                //            Basket = basket,
                //            ProductId = product!.Id,
                //            Quantity = productCookieViewModel.Quantity,
                //        };

                //        await _dataContext.BasketProducts.AddAsync(basketProduct);
                //        await _dataContext.SaveChangesAsync();
                //    }

                //    _httpContextAccessor.HttpContext!.Response.Cookies.Delete("products");
                //}
            }
        }

        public async Task SignOutAsync()
        {
            await _httpContextAccessor.HttpContext!.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
        }

        public async Task CreateAsync(RegisterViewModel model)
        {
            var user = await CreateUserAsync();
            var basket = await CreateBasketAsync();
            var wishlist = await CreatWishlistAsync();
            await CreteBasketProductsAsync();
            await CreteWishlistProductsAsync();

            await _userActivationService.SendActivationUrlAsync(user);

            await _dataContext.SaveChangesAsync();


            async Task<User> CreateUserAsync()
            {
                var user = new User
                {
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    Email = model.Email,
                    Password = BC.HashPassword(model.Password),
               
                };
                await _dataContext.Users.AddAsync(user);

                return user;
            }

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
            async Task<Wishlist> CreatWishlistAsync()
            {
                var wishlist = new Wishlist
                {
                    User = user
                };
                await _dataContext.Wishlists.AddAsync(wishlist);
                return wishlist;

            }

            async Task CreteBasketProductsAsync()
            {
                //Add products to basket if cookie exists
                var productCookieValue = _httpContextAccessor.HttpContext!.Request.Cookies["products"];
                if (productCookieValue is not null)
                {
                    var productsCookieViewModel = JsonSerializer.Deserialize<List<ProductCookieViewModel>>(productCookieValue);
                    foreach (var productCookieViewModel in productsCookieViewModel)
                    {
                        var product = await _dataContext.Products.FirstOrDefaultAsync(b => b.Id == productCookieViewModel.Id);
                        var basketProduct = new BasketProduct
                        {
                            Basket = basket,
                            ProductId = product!.Id,
                            Quantity = productCookieViewModel.Quantity,
                        };

                        await _dataContext.BasketProducts.AddAsync(basketProduct);
                    }

                  
                }
            }
            async Task CreteWishlistProductsAsync()
            {
                //Add products to wishlist if cookie exists
                var productCookieValue = _httpContextAccessor.HttpContext!.Request.Cookies["wishlistproducts"];
                if (productCookieValue is not null)
                {
                    var productsCookieViewModel = JsonSerializer.Deserialize<List<WishlistProductCookieVIewModel>>(productCookieValue);
                    foreach (var productCookieViewModel in productsCookieViewModel)
                    {
                        var product = await _dataContext.Products.FirstOrDefaultAsync(b => b.Id == productCookieViewModel.Id);
                        var wishlistProduct = new WishlistProduct
                        {
                            Wishlist = wishlist,
                            ProductId = product!.Id,
                        };

                        await _dataContext.WishlistProducts.AddAsync(wishlistProduct);
                    }


                }
            }

        }
    }
}
