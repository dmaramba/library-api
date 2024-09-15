using Library.Domain.Models;
using Library.Domain.ViewModels;
using Library.Infrastructure.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Infrastructure.Repository
{
    public class CustomerRepository : GenericRepository<Customer>, ICustomerRepository
    {
        public CustomerRepository(LibraryContext dbContext) : base(dbContext)
        {

        }

        public CustomerModel GetCustomerData(int customerId)
        {
            CustomerModel customer = new CustomerModel();
            customer.Profile = _context.Customers.FirstOrDefault(x => x.Id == customerId);
            var borrowedBooks = (from book in _context.Books
                                 join borrow in _context.BorrowBooks on book.Id equals borrow.Id
                                 where borrow.CustomerId == customerId && borrow.DueDate > DateTime.Now
                                 select new CustomerBook
                                 {
                                     Title = book.Title,
                                     Author = book.Author,
                                     Id = borrow.Id,
                                     DueDate = borrow.DueDate
                                 }).ToList();

            var reservedBooks = (from book in _context.Books
                                 join reserve in _context.ReserveBooks on book.Id equals reserve.Id
                                 where reserve.CustomerId == customerId && reserve.DueDate > DateTime.Now
                                 select new CustomerBook
                                 {
                                     Title = book.Title,
                                     Author = book.Author,
                                     Id = reserve.Id,
                                     DueDate = reserve.DueDate
                                 }).ToList();

            customer.BorrowedBooks = borrowedBooks;
            customer.ReserveBooks = reservedBooks;
            return customer;
        }
    }
}
