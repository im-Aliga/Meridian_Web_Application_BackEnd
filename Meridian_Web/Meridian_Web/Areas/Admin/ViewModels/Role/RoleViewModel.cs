namespace Meridian_Web.Areas.Admin.ViewModels.Role
{
    public class RoleViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public RoleViewModel(int id, string name)
        {
            Id = id;
            Name = name;
        }
    }
}
