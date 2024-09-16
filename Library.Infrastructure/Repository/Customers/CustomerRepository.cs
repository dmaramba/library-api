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
    public class CustomerRepository : GenericRepository<Customer>, ICustomerRepository
    {
        public CustomerRepository(LibraryContext dbContext) : base(dbContext)
        {

        }

        public List<CustomerBook> GetBorrowedBooks()
        {
            var borrowedBooks = (from borrow in _context.BorrowBooks
                                 join book in _context.Books on borrow.BookId equals book.Id
                                 join customer in _context.Customers on borrow.CustomerId equals customer.Id
                                 where borrow.Returned == false
                                 select new CustomerBook
                                 {
                                     Title = book.Title,
                                     Author = book.Author,
                                     Id = borrow.Id,
                                     DueDate = borrow.DueDate,
                                     Email = customer.Email,
                                     Name = customer.Name
                                 }).ToList();

            return borrowedBooks.OrderBy(x => x.DueDate).ToList();
        }

        public CustomerModel GetCustomerData(int customerId)
        {
            CustomerModel customer = new CustomerModel();
            customer.Profile = _context.Customers.FirstOrDefault(x => x.Id == customerId);
            var borrowedBooks = (from borrow in _context.BorrowBooks
                                 join book in _context.Books on borrow.BookId equals book.Id
                                 where borrow.CustomerId == customerId && borrow.Returned == false
                                 select new CustomerBook
                                 {
                                     Title = book.Title,
                                     Author = book.Author,
                                     Id = borrow.Id,
                                     DueDate = borrow.DueDate
                                 }).ToList();

            var reservedBooks = (from reserve in _context.ReserveBooks
                                 join book in _context.Books on reserve.BookId equals book.Id
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

        public List<CustomerBook> GetReservedBooks()
        {
            var reservedBooks = (from reserve in _context.ReserveBooks
                                 join book in _context.Books on reserve.BookId equals book.Id
                                 join customer in _context.Customers on reserve.CustomerId equals customer.Id
                                 where reserve.DueDate > DateTime.Now
                                 select new CustomerBook
                                 {
                                     Title = book.Title,
                                     Author = book.Author,
                                     Id = reserve.Id,
                                     DueDate = reserve.DueDate,
                                     Email = customer.Email,
                                     Name = customer.Name
                                 }).ToList();

            return reservedBooks.OrderBy(x => x.DueDate).ToList();
        }
    }
}
