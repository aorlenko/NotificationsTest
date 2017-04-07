namespace NotificationsTest.DataAccess.Models
{
    public class MessageRecipient
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int MessageId { get; set; }
        public Message Message { get; set; }
    }
}
