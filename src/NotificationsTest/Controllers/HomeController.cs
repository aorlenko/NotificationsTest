using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using NotificationsTest.DataAccess;
using NotificationsTest.DataAccess.Models;
using NotificationsTest.Models;
using NotificationTest.Business;

namespace NotificationsTest.Controllers
{
    public class HomeController : Controller
    {
        private static readonly IList<CommentModel> _comments;

        private readonly IMediator _mediator;
        private readonly INotificationService _notificationService;
        private readonly NotificationsContext _dbContext;

        public HomeController(NotificationsContext dbContext, IMediator mediator,
            INotificationService notificationService)
        {
            _mediator = mediator;
            _notificationService = notificationService;
            _dbContext = dbContext;
        }

        static HomeController()
        {
            _comments = new List<CommentModel>
            {
                new CommentModel
                {
                    Id = 1,
                    Author = "Daniel Lo Nigro",
                    Text = "Hello ReactJS.NET World!"
                },
                new CommentModel
                {
                    Id = 2,
                    Author = "Pete Hunt",
                    Text = "This is one comment"
                },
                new CommentModel
                {
                    Id = 3,
                    Author = "Jordan Walke",
                    Text = "This is *another* comment"
                },
            };
        }

        public IActionResult Index()
        {
            return View();
        }

        [Route("comments")]
        [ResponseCache(Location = ResponseCacheLocation.None, NoStore = true)]
        public ActionResult Comments()
        {
            return Json(_comments);
        }

        [Route("comments/new")]
        [HttpPost]
        public ActionResult AddComment(CommentModel comment)
        {
            // Create a fake ID for this comment
            comment.Id = _comments.Count + 1;
            _comments.Add(comment);
            return Content("Success :)");
        }

        [HttpPost]
        [Route("comments/register")]
        public async Task<IActionResult> Register(NotificationTest.Business.RegisterUser registerUser)
        {
            //int maxId = 0;

            //if (_dbContext.Messages.Any())
            //    maxId = _dbContext.Messages.Max(e => e.Id);

            //_dbContext.Messages.Add(new Message() { Id = ++maxId });
            //_dbContext.SaveChanges();

            //await _mediator.Send(registerUser);

            int generatedId = await _notificationService.SendMessage(registerUser);
            return Json(generatedId);
        }
    }
}
