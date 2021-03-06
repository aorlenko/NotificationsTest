﻿using System.Threading.Tasks;

namespace NotificationTest.Business
{
    public interface INotificationService
    {
        Task<int> SendMessage(SendMessageCommand message);
    }
}
