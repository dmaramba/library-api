using Library.Domain.Models;
using Library.Domain.ViewModels;
using Library.Infrastructure.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Library.Infrastructure.Repository
{
    public class NotificationRepository : GenericRepository<Notification>, INotificationRepository
    {
        public NotificationRepository(LibraryContext dbContext) : base(dbContext)
        {

        }

        public List<NotificationModel> GetNotificationForBook(int bookId)
        {
            var notifications = (from notification in _context.Notifications
                                 join book in _context.Books on notification.BookId equals book.Id
                                 join customer in _context.Customers on notification.CustomerId equals customer.Id
                                 where notification.Sent == false && notification.BookId == bookId
                                 select new NotificationModel
                                 {
                                     Id = notification.Id,
                                     Sent = notification.Sent,
                                     Title = book.Title,
                                     Author = book.Author,
                                     Name = customer.Name,
                                     Email = customer.Email

                                 }).ToList();
            return notifications;

        }

        public List<NotificationModel> GetNotifications()
        {
            var notifications = (from notification in _context.Notifications
                                 join book in _context.Books on notification.BookId equals book.Id
                                 join customer in _context.Customers on notification.CustomerId equals customer.Id
                                 where notification.Sent == false
                                 select new NotificationModel
                                 {
                                     Id = notification.Id,
                                     Sent = notification.Sent,
                                     Title = book.Title,
                                     Author = book.Author,
                                     Name = customer.Name,
                                     Email = customer.Email

                                 }).ToList();
            return notifications;
        }
    }
}
