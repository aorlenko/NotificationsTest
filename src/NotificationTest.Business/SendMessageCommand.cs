using MediatR;

namespace NotificationTest.Business
{
    public class SendMessageCommand : IRequest<bool>
    {
        public string Subject { get; set; }
        public string Body { get; set; }
        public string Recipients { get; set; }
    }

}
