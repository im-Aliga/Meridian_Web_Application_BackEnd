namespace Meridian_Web.Areas.Admin.ViewModels.Contact
{
    public class ListContactViewModel
    {
        public ListContactViewModel(string firstName, string email, string subject, string message, int ıd)
        {
            FirstName = firstName;
            Email = email;
            Subject = subject;
            Message = message;
            Id = ıd;
        }

        public int Id { get; set; }
        public string FirstName { get; set; }

        public string Email { get; set; }

        public string Subject { get; set; }

        public string Message { get; set; }
      
    }
}
