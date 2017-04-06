using MediatR;

namespace NotificationTest.Business
{
    public class RegisterUser : IRequest<bool>, INotification
    {
        public string EmailAddress { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }

}
