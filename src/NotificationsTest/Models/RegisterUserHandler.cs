using System.Threading;
using System.Threading.Tasks;
using MediatR;
using NotificationsTest.Models;

public class RegisterUserHandler : IAsyncRequestHandler<RegisterUser, bool>
{
    public Task<bool> Handle(RegisterUser message)
    {
        Task.Factory.StartNew(() => SendNotification(message));

        // save to database
        return Task.FromResult(true);
    }

    private async Task<bool> SendNotification(RegisterUser registerUser)
    {
        Thread.Sleep(2000);
        return true;
    }
}