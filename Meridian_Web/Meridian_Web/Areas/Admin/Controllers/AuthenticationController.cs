using Meridian_Web.Areas.Admin.ViewModels.Authentication;
using Meridian_Web.Contracts.Identity;
using Meridian_Web.Database;
using Meridian_Web.Database.Models;
using Meridian_Web.Services.Abstracts;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Data;
using System.Security.Claims;

namespace Meridian_Web.Areas.Admin.Controllers
{
    [Area("admin")]
    [Route("admin/auth")]
    
    public class AuthenticationController : Controller
    {
        private readonly DataContext _dataContext;
        private readonly IUserService _userService;

        public AuthenticationController(DataContext dataContext, IUserService userService)
        {
            _dataContext = dataContext;
            _userService = userService;
        }

        #region Login and Logout

        [HttpGet("login", Name = "admin-auth-login")]
        public async Task<IActionResult> LoginAsync()
        {
            return View(new LoginViewModel());
        }

        [HttpPost("login", Name = "admin-auth-login")]
        public async Task<IActionResult> LoginAsync(LoginViewModel? model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            if (!await _userService.CheckPasswordAsync(model!.Email, model!.Password))
            {
                ModelState.AddModelError(String.Empty, "Email or password is not correct");
                return View(model);
            }

            var user = await _dataContext.Users
                .Include(u => u.Role)
                .SingleAsync(u => u.Email == model!.Email);
            if (user.RoleId == 1)
            {
              await _userService.SignInAsync(model.Email, model.Password, user.Role.Name);
            }
            else
            {
                ModelState.AddModelError(String.Empty, "You are not admin");
                return View(model);
            }

            return RedirectToRoute("admin-dashboard-index");
        }

        [HttpGet("logout", Name = "admin-auth-logout")]
        public async Task<IActionResult> LogoutAsync()
        {
            await _userService.SignOutAsync();

            return RedirectToRoute("admin-auth-login");
        }

        #endregion
    }
}
