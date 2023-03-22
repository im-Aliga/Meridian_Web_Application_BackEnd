using Meridian_Web.Contracts.Order;

namespace Meridian_Web.Contracts.Email
{
    public static class EmailMessages
    {
        public static class Subject
        {
            public const string ACTIVATION_MESSAGE = $"Account Activation";
            public const string ORDER_ACTIVATION_MESSAGE = $"Order has been aproverd";
        }

        public static class Body
        {
            public const string ACTIVATION_MESSAGE = $"Thanks for signing up with Meridian!\r\nYou must follow this link to activate your account : {EmailMessageKeywords.ACTIVATION_URL}";
        }
    }
}
