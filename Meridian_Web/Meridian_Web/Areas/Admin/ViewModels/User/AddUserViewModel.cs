﻿using Meridian_Web.Areas.Admin.ViewModels.Role;
using Meridian_Web.Database.Models;
using System.ComponentModel.DataAnnotations;

namespace Meridian_Web.Areas.Admin.ViewModels.User
{
    public class AddUserViewModel
    {
       
        public string Email { get; set; }
       
        public string FirstName { get; set; }
      
        public string LastName { get; set; }
     
        public string Password { get; set; }
   
        public string ConfirmPassword { get; set; }
        public int? RoleId { get; set; }
        public List<RoleViewModel>? Roles { get; set; }
        public UserAddress? Address { get; set; }

       
    }
}
