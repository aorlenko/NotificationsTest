using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using NotificationsTest.DataAccess;
using NotificationsTest.DataAccess.Models;

namespace NotificationTest.Business
{
    public class NotificationService : INotificationService
    {
        private readonly IMediator _mediator;
        private readonly NotificationsContext _dbContext;

        public NotificationService(NotificationsContext dbContext, IMediator mediator)
        {
            _mediator = mediator;
            _dbContext = dbContext;
        }

        public async Task<int> SendMessage(RegisterUser message)
        {
            int maxId = 0;

            if (_dbContext.Messages.Any())
                maxId = _dbContext.Messages.Max(e => e.Id);

            _dbContext.Messages.Add(new Message() { Id = ++maxId });
            _dbContext.SaveChanges();

            await _mediator.Send(message);

            return maxId;
        }
    }
}
