using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Domain.ViewModels
{
    public class NotificationModel
    {
        /// <summary>
        /// The unique of the book
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// The title of the book
        /// </summary>
        public required string Title { get; set; }
        /// <summary>
        /// The author of the book
        /// </summary>
        public required string Author { get; set; }
       
        /// <summary>
        /// Customer name
        /// </summary>
        public required string Name { get; set; }

        /// <summary>
        /// Customer Email
        /// </summary>
        public required string Email { get; set; }

        /// <summary>
        /// Notification sent
        /// </summary>
        public bool Sent { get; set; }

        /// <summary>
        /// Notification date sent
        /// </summary>
        public bool DateSent { get; set; }
    }
}
