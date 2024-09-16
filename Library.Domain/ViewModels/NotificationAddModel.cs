using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Domain.ViewModels
{
    public class NotificationAddModel
    {
        public int BookId { get; set; }
        public int CustomerId { get; set; }
    }
}
