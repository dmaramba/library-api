using Library.Domain.Models;
using Library.Domain.ViewModels;
using Library.Infrastructure.Repository;
using Microsoft.Extensions.Logging;


namespace Library.Infrastructure.Services
{
    public class NotificationService : INotificationService
    {
        private readonly INotificationRepository _notificationRepository;
        private readonly ILogger<BookService> _logger;
        public NotificationService(ILogger<BookService> logger, INotificationRepository notificationRepository)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _notificationRepository = notificationRepository ?? throw new ArgumentNullException(nameof(notificationRepository));
        }

        public async Task AddNotification(NotificationAddModel notification)
        {
            var customer = new Notification { BookId = notification.BookId, CustomerId = notification.BookId, Sent=false };
            await _notificationRepository.AddAsync(customer);
        }

        public async Task CancelNotification(int id)
        {
            var notification = await _notificationRepository.GetByIdAsync(id);
            if (notification != null)
            {
                await _notificationRepository.RemoveAsync(notification);
            }
        }

        public IEnumerable<NotificationModel> GetNotificationForBook(int bookId)
        {
            return _notificationRepository.GetNotificationForBook(bookId);
        }

        public IEnumerable<NotificationModel> GetNotifications()
        {
            return _notificationRepository.GetNotifications();
        }

        public async Task SendNotificationForBook(int bookId)
        {
            var notifications = GetNotificationForBook(bookId);
            foreach (var notification in notifications)
            {
                //logic to send notification via email

                var notify = await _notificationRepository.GetByIdAsync(notification.Id);
                if (notify != null)
                {
                    notify.Sent = true;
                    notify.DateSent = DateTime.Now;
                    await _notificationRepository.UpdateAsync(notify);
                }
            }
        }
    }
}