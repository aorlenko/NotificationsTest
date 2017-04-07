using MediatR;

namespace NotificationTest.Business
{
    public class SendMessageCommand : IRequest<bool>
    {
        public string Subject { get; set; }
        public string Message { get; set; }
        public string Recipients { get; set; }
    }
}
