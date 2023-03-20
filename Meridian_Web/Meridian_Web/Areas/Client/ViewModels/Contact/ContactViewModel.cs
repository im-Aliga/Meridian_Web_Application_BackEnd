namespace Meridian_Web.Areas.Client.ViewModels.Contact
{
    public class ContactViewModel
    {
        public string FirstName { get; set; }
        public string Email { get; set; }
        public string Subject { get; set; }
        public string Message { get; set; }
        public List<ContactInformationListItemViewModel>? ContactInformations { get; set; }
    }
}
