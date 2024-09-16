using Library.Domain.Models;
using Library.Domain.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Infrastructure.Services
{
    public interface ICustomerService
    {
        IEnumerable<Customer> GetCustomers();
        Task AddCustomer(CustomAddProfileModel customer);

        Task UpdateCustomer(CustomProfileModel customer);

        CustomerModel GetCustomer(int customerId);

        public List<CustomerBook> GetBorrowedBooks();
        public List<CustomerBook> GetReservedBooks();
    }
}
