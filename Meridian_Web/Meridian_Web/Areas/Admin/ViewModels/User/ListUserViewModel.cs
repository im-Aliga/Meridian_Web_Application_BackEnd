using Meridian_Web.Database.Models;

namespace Meridian_Web.Areas.Admin.ViewModels.User
{
    public class ListUserViewModel
    {

        public Guid Id { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Password { get; set; }
        public UserAddress? Address { get; set; }
        public string? Roles { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public ListUserViewModel(Guid ıd, string email, string firstName, string lastName, string password, UserAddress? address, string? roles, DateTime createdAt, DateTime updatedAt)
        {
            Id = ıd;
            Email = email;
            FirstName = firstName;
            LastName = lastName;
            Password = password;
            Address = address;
            Roles = roles;
            CreatedAt = createdAt;
            UpdatedAt = updatedAt;
        }
    }

    
}
