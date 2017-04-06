using System.Threading;
using System.Threading.Tasks;
using MediatR;

namespace NotificationTest.Business
{
    public class SendMessageCommandHandler : IAsyncRequestHandler<SendMessageCommand, bool>
    {
        public async Task<bool> Handle(SendMessageCommand message)
        {
            // use new thread to simulate async operation
            Task.Factory.StartNew(() => SendNotification(message));
            return await Task.FromResult(true);
        }

        /// <summary>
        /// External send message service
        /// </summary>
        /// <param name="sendMessageCommand"></param>
        /// <returns></returns>
        private async Task<bool> SendNotification(SendMessageCommand sendMessageCommand)
        {
            Thread.Sleep(5000); // simulate long-running operation
            return true;
        }
    }
}