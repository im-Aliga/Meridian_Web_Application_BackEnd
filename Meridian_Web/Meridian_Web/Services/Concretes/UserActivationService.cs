using Meridian_Web.Services.Abstracts;
using Meridian_Web.Areas.Client.ViewModels.Authentication;
using Meridian_Web.Areas.Client.ViewModels.Basket;
using Meridian_Web.Contracts.Email;
using Meridian_Web.Contracts.Identity;
using Meridian_Web.Database;
using Meridian_Web.Database.Models;
using Meridian_Web.Exceptions;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using System.Text.Json;

namespace Meridian_Web.Services.Concretes
{
    public class UserActivationService : IUserActivationService
    {
        private readonly DataContext _dataContext;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IEmailService _emailService;
        private readonly IUrlHelper _urlHelper;
        private readonly DateTime _activationExpireDate;  
        private const string EMAIL_CONFIRMATION_ROUTE_NAME = "client-auth-activate";
        

        public UserActivationService(
            DataContext dataContext,
            IHttpContextAccessor httpContextAccessor,
            IEmailService emailService,
            IUrlHelper urlHelper,
            IConfiguration configuration)
        {
            _dataContext = dataContext;
            _httpContextAccessor = httpContextAccessor;
            _emailService = emailService;
            _urlHelper = urlHelper;

            double activationValidityMonute = 
                Convert.ToDouble(configuration.GetRequiredSection("ActivationValidityMinute").Value);

            _activationExpireDate = DateTime.Now.AddMinutes(activationValidityMonute);
        }



        public async Task SendActivationUrlAsync(User user)
        {
            ArgumentNullException.ThrowIfNull(user);

            var token = GenerateActivationToken();
            var activationUrl = GenerateUrl(token, EMAIL_CONFIRMATION_ROUTE_NAME);
            await CreateUserActivationAsync(user, token, activationUrl, _activationExpireDate);
            var activationMessageDto = PrepareActivationMessage(user.Email!, activationUrl);

            _emailService.Send(activationMessageDto);
        }


        private string GenerateActivationToken()
        {
            return Guid.NewGuid().ToString();
        }

        private string GenerateUrl(string token, string routeName)
        {
            var request = _httpContextAccessor.HttpContext!.Request;
            return _urlHelper.RouteUrl(routeName, new{ token = token }, request.Scheme, request.Host.Value)!;
        }

        private async Task<UserActivation> CreateUserActivationAsync(User user, string token, string activationUrL, DateTime expireDate)
        {
            var userActivation = new UserActivation
            {
                User = user,
                ActivationToken = token,
                ActivationUrl = activationUrL,
                ExpireDate = expireDate,
            };

            await _dataContext.UserActivations.AddAsync(userActivation);

            return userActivation;
        } 

        private MessageDto PrepareActivationMessage(string email, string activationUrl)
        {
            string body = EmailMessages.Body.ACTIVATION_MESSAGE
                .Replace(EmailMessageKeywords.ACTIVATION_URL, activationUrl);

            string subject = EmailMessages.Subject.ACTIVATION_MESSAGE;

            return new MessageDto(email, subject, body);
        }
    }
}
