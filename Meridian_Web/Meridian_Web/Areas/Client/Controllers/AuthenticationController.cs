using Meridian_Web.Areas.Client.ViewModels.Authentication;
using Meridian_Web.Database;
using Meridian_Web.Database.Models;
using Meridian_Web.Services.Abstracts;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace Meridian_Web.Areas.Client.Controllers
{
    [Area("client")]
    [Route("auth")]
    public class AuthenticationController :Controller
    {
        private readonly DataContext _dbContext;
        private readonly IUserService _userService;
        //private readonly SignInManager<User> _signInManager;
        //private readonly UserManager<User> _userManager;

        public AuthenticationController(DataContext dbContext, IUserService userService)
        {
            _dbContext = dbContext;
            _userService = userService;
            //_signInManager = signInManager;
            //_userManager = userManager;
        }

        //[HttpGet("googlelogin", Name = "client-auth-google-login")]
        //public IActionResult GoogleLogin(string ReturnUrl)
        //{
        //    string redirectUrl = Url.Action("ExternalResponse", "Authentication", new { ReturnUrl = ReturnUrl });
        //    AuthenticationProperties properties = _signInManager.ConfigureExternalAuthenticationProperties("Google", redirectUrl);
        //    return new ChallengeResult("Google", properties);
            
        //}
       
        //public async Task<IActionResult> ExternalResponse(string ReturnUrl = "/")
        //{
        //    ExternalLoginInfo loginInfo = await _signInManager.GetExternalLoginInfoAsync();
        //    if (loginInfo == null)
        //        return RedirectToAction("Login");
        //    else
        //    {
        //            dynamic datas = loginInfo.Principal.Identity;
        //            string FirstName = datas.Claims[2].Value.ToString();
        //            string LastName = datas.Claims[3].Value.ToString();
        //            string Email = datas.Claims[4].Value.ToString();
        //        var existingUser = await _dbContext.Users.FirstOrDefaultAsync(p => p.Email == Email);
        //        if (existingUser !=null)
        //        {
        //            // User already exists in database, log them in
        //            Microsoft.AspNetCore.Identity.SignInResult loginResult = await _signInManager.ExternalLoginSignInAsync(loginInfo.LoginProvider, loginInfo.ProviderKey, true);
        //            if (loginResult.Succeeded)
        //                return Redirect(ReturnUrl);
        //            else
        //            {
        //                // Handle login failure
        //            }
        //        }

        //        else
        //        {

        //            User user = new User
        //            {
        //                FirstName = FirstName,
        //                LastName = LastName,
        //                Email = Email,
        //                //Email = loginInfo.Principal.FindFirst(ClaimTypes.Email).Value,
        //                //FirstName = loginInfo.Principal.FindFirst(ClaimTypes.Name).Value

        //            };
        //            await _dbContext.AddAsync(user);
        //            await _dbContext.SaveChangesAsync();
                    
        //            IdentityResult createResult = await _userManager.CreateAsync(user);
                   
        //            if (createResult.Succeeded)
        //            {
                       
        //                IdentityResult addLoginResult = await _userManager.AddLoginAsync(user, loginInfo);
                      
        //                if (addLoginResult.Succeeded)
        //                {
        //                    await _signInManager.SignInAsync(user, true);
                           
        //                    return Redirect(ReturnUrl);
        //                }
        //            }

        //        }
        //    }
        //    return Redirect(ReturnUrl);
        //}


        [HttpGet("login", Name = "client-auth-login")]
        public async Task<IActionResult> LoginAsync()
        {
            if (_userService.IsAuthenticated)
            {
                return RedirectToRoute("client-home-index");
            }

            return View(new LoginViewModel());
        }

        [HttpPost("login", Name = "client-auth-login")]
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

            if (!await _userService.CheckEmailConfirmedAsync(model!.Email))
            {
                ModelState.AddModelError(String.Empty, "Email is not confirmed");
                return View(model);
            }

            await _userService.SignInAsync(model!.Email, model!.Password);

           

            return RedirectToRoute("client-home-index");
        }

        [HttpGet("register", Name = "client-auth-register")]
        public async Task<IActionResult> RegisterAsync()
        {
            return View(new RegisterViewModel());
        }

        [HttpPost("register", Name = "client-auth-register")]
        public async Task<IActionResult> RegisterAsync(RegisterViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            await _userService.CreateAsync(model);

            return RedirectToRoute("client-auth-login");
        }

        [HttpGet("activate/{token}", Name = "client-auth-activate")]
        public async Task<IActionResult> ActivateAsync([FromRoute] string token)
        {
            var userActivation = await _dbContext.UserActivations
                .Include(ua => ua.User)
                .FirstOrDefaultAsync(ua =>
                    !ua!.User!.IsEmailConfirmed &&
                    ua.ActivationToken == token);

            if (userActivation is null)
            {
                return NotFound();
            }

            if (DateTime.Now > userActivation!.ExpireDate)
            {
                return Ok("Token expired unfortunately");
            }

            userActivation!.User!.IsEmailConfirmed = true;

            await _dbContext.SaveChangesAsync();

            return RedirectToRoute("client-auth-login");
        }


        [HttpGet("logout", Name = "client-auth-logout")]
        public async Task<IActionResult> LogoutAsync()
        {
            await _userService.SignOutAsync();

            return RedirectToRoute("client-home-index");
        }
    }
}
