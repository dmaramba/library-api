using Library.Domain.Models;
using Library.Domain.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Infrastructure.Services
{
    public interface INotificationService
    {

        IEnumerable<NotificationModel> GetNotifications();
        Task AddNotification(NotificationAddModel notification);

        Task CancelNotification(int id);

        Task SendNotificationForBook(int bookId);

        IEnumerable<NotificationModel> GetNotificationForBook(int bookId);
    }
}
