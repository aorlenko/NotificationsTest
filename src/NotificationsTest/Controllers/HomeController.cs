using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using NotificationTest.Business;

namespace NotificationsTest.Controllers
{
    public class HomeController : Controller
    {
        private readonly INotificationService _notificationService;

        public HomeController(INotificationService notificationService)
        {
            _notificationService = notificationService;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [Route("messages/send")]
        public async Task<IActionResult> SendMessage(SendMessageCommand sendMessageCommand)
        {
            int generatedId = await _notificationService.SendMessage(sendMessageCommand);
            return Json(generatedId);
        }
    }
}
