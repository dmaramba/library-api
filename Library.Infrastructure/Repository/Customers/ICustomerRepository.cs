using Library.Domain.Models;
using Library.Domain.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Infrastructure.Repository
{
    public interface ICustomerRepository : IGenericRepository<Customer>
    {
        public CustomerModel GetCustomerData(int customerId);
        public List<CustomerBook> GetBorrowedBooks();
        public List<CustomerBook> GetReservedBooks();
    }
}
