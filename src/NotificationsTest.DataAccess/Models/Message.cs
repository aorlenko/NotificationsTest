using System.Collections.Generic;

namespace NotificationsTest.DataAccess.Models
{
    public class Message
    {
        public int Id { get; set; }
        public HashSet<MessageRecipient> MessageRecipients { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }
        public bool IsSent { get; set; }
    }
}
