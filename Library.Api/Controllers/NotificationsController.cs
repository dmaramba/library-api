using Library.Domain.Models;
using Library.Domain.ViewModels;
using Library.Infrastructure.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Swashbuckle.AspNetCore.Annotations;

namespace Library.Api.Controllers
{
    [Route("api/notifications/[action]")]
    [ApiController]
    public class NotificationsController : ControllerBase
    {

        private readonly INotificationService notificationService;
        public NotificationsController(INotificationService notificationService)
        {
            this.notificationService = notificationService;
        }


        /// <summary>
        /// Gets the list of all pending notifications
        /// </summary>
        [SwaggerOperation(Tags = new[] { "Notifications" })]
        [HttpGet(Name = nameof(GetNotifications))]
        [ProducesResponseType(typeof(IReadOnlyCollection<NotificationModel>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public IActionResult GetNotifications()
        {

            return Ok(notificationService.GetNotifications());
        }


        /// <summary>
        /// Borrow a book by admin
        /// </summary>
        /// <param name="notificationAddModel">The model with book and customer</param>
        [SwaggerOperation(Tags = new[] { "Notifications" })]
        [HttpPost(Name = nameof(AddNotification))]
        [ProducesResponseType(typeof(BorrowBook), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> AddNotification([FromBody] NotificationAddModel notificationAddModel)
        {
            await notificationService.AddNotification(notificationAddModel);
            return Ok();
        }

        /// <summary>
        /// Borrow a book by admin
        /// </summary>
        /// <param name="id">notification id</param>
        [SwaggerOperation(Tags = new[] { "Notifications" })]
        [HttpDelete(Name = nameof(CancelNotification))]
        [ProducesResponseType(typeof(BorrowBook), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> CancelNotification(int id)
        {
            await notificationService.CancelNotification(id);
            return Ok();
        }

    }
}
