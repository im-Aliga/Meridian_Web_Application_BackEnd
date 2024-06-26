﻿using AspNetCore.IServiceCollection.AddIUrlHelper;
using Meridian_Web.Database;
using Meridian_Web.Infrastructure.Configurations;
using Meridian_Web.Options;
//using Meridian_Web.Services.Abstracts;
//using Meridian_Web.Services.Concretes;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Components.Server.ProtectedBrowserStorage;
using Microsoft.AspNetCore.Identity;

using Meridian_Web.Database.Models;


namespace Meridian_Web.Infrastructure.Extensions
{
    public static class ServiceCollectionExtensions
    {


        public static void ConfigureServices(this IServiceCollection services, IConfiguration configuration)
        {

            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                .AddCookie(o =>
                {
                    o.Cookie.Name = "Identity";
                    o.ExpireTimeSpan = TimeSpan.FromMinutes(20);
                    o.LoginPath = "/auth/login";
                    o.AccessDeniedPath = "/admin/auth/login";
                });

                //.AddGoogle(x =>
                //{
                //    x.ClientId = configuration["GoogleClientId"];
                //    x.ClientSecret = configuration["GoogleClientSecret"];
                //});

            //services.AddIdentity<User, IdentityRole<Guid>>()
            //      .AddEntityFrameworkStores<DataContext>()
            //       .AddDefaultTokenProviders();



            services.ConfigureDatabase(configuration);
            services.AddSession();

            services.AddHttpContextAccessor();

            services.ConfigureMvc();

            services.AddUrlHelper();


            services.ConfigureOptions(configuration);

            services.ConfigureFluentValidatios(configuration);

            services.RegisterCustomServices(configuration);


        }
    }
}
