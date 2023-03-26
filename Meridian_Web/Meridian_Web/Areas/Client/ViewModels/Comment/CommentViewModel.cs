namespace Meridian_Web.Areas.Client.ViewModels.Comment
{
    public class CommentViewModel
    {
        public CommentViewModel(int ıd, string name, string context, string email, DateTime createdAt)
        {
            Id = ıd;
            Name = name;
            Context = context;
            Email = email;
            CreatedAt = createdAt;
        }

        public int Id { get; set; } 
        public string Name { get; set; }
        public string Context { get; set; }
        public string Email { get; set; }
        public DateTime CreatedAt { get; set; }

    }
}
