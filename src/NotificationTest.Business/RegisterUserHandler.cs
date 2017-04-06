using System.Threading;
using System.Threading.Tasks;
using MediatR;

namespace NotificationTest.Business
{
    public class RegisterUserHandler : IAsyncRequestHandler<RegisterUser, bool>
    {
        public async Task<bool> Handle(RegisterUser message)
        {
            Task.Factory.StartNew(() => this.SendNotification(message));

            // save to database
            return  await Task.FromResult(true);
        }

        private async Task<bool> SendNotification(RegisterUser registerUser)
        {
            Thread.Sleep(5000); // simulate long-running operation
            return true;
        }
    }
}