using MimeKit;

namespace Meridian_Web.Contracts.Email
{
    public class MessageDto
    {
        public List<MailboxAddress> To { get; set; }
        public string Subject { get; set; }
        public string Content { get; set; }
        public MessageDto(List<string> to, string subject, string content)
        {
            To = new List<MailboxAddress>();
            To.AddRange(to.Select(x => new MailboxAddress(string.Empty, x)));

            Subject = subject;
            Content = content;
        }
        public MessageDto(string to, string subject, string content)
          : this(new List<string> { to }, subject, content)
        {

        }
    }
}
