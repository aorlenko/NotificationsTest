using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NotificationTest.Business
{
    public interface INotificationService
    {
        Task<int> SendMessage(RegisterUser message);
    }
}
