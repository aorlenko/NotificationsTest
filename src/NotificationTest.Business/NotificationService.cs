using System.Threading.Tasks;
using MediatR;
using NotificationsTest.DataAccess;
using NotificationsTest.DataAccess.Models;
using NotificationTest.Business.Utils;
using System;

namespace NotificationTest.Business
{
    public class NotificationService : INotificationService
    {
        private readonly IMediator _bus;
        private readonly NotificationsContext _dbContext;

        public NotificationService(NotificationsContext dbContext, IMediator bus)
        {
            _bus = bus;
            _dbContext = dbContext;
        }

        public async Task<int> SendMessage(SendMessageCommand message)
        {
            if (message == null) throw new ArgumentNullException(nameof(message));
            if (message.Message == null) throw new ArgumentNullException(nameof(message.Message));
            if (message.Recipients == null) throw new ArgumentNullException(nameof(message.Recipients));
            if (message.Subject == null) throw new ArgumentNullException(nameof(message.Subject));

            var newMessage = SaveNewMessage(message);

            // fake send NewMessage via async service bus
            await _bus.Send(message);

            return newMessage.Id;
        }

        private Message SaveNewMessage(SendMessageCommand message)
        {
            var newMessage = new Message
                {
                    Body = message.Message,
                    Subject = message.Subject,
                    IsSent = false
                };

            var recipientNames = RecipientNameParser.Parse(message.Recipients);

            _dbContext.Messages.Add(newMessage);

            foreach (var recipientName in recipientNames)
            {
                var messageRecipient = new MessageRecipient
                {
                    Message = newMessage,
                    Name = recipientName
                };

                _dbContext.MessageRecipients.Add(messageRecipient);
            }

            _dbContext.SaveChanges();

            return newMessage;
        }
    }
}
