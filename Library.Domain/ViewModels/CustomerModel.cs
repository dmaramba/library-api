using Library.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Domain.ViewModels
{
    public class CustomerModel
    {
        public  Customer? Profile { get; set; }
        public List<CustomerBook> BorrowedBooks { get; set; }

        public List<CustomerBook> ReserveBooks { get; set; }

    }
}
