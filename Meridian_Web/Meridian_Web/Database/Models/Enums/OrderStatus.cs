namespace Meridian_Web.Database.Models.Enums
{
    public enum OrderStatus : byte
    {
        None = 0,
        Created = 1,
        Approved = 2,
        Rejected = 4,
        Sent = 8,
        Completed = 16
    }
}
