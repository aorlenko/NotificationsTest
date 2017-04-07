namespace NotificationTest.Business.Utils
{
    public static class RecipientNameParser
    {
        public static string[] Parse(string names)
        {
            var recipientNames = names.Split(';');
            return recipientNames;
        }
    }
}
