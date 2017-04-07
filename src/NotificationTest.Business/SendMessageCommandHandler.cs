using System.Threading;
using System.Threading.Tasks;
using MediatR;
using NotificationTest.Business.Utils;

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
            var recipientNames = RecipientNameParser.Parse(sendMessageCommand.Recipients);

            foreach (var recipientName in recipientNames)
            {
                SendMessage(recipientName, sendMessageCommand.Message);
            }

            return true;
        }

        private void SendMessage(string recipientName, string body)
        {
            Thread.Sleep(1000); // simulate long-running operation
        }
    }
}