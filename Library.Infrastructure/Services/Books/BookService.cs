using Library.Domain.Models;
using Library.Domain.ViewModels;
using Library.Infrastructure.Repository;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Infrastructure.Services
{
    public class BookService : IBookService
    {
        private readonly IBookRepository _bookRepository;
        private readonly IBorrowBookRepository _borrowBookRepository;
        private readonly IReserveBookRepository _reserveBookRepository;
        private readonly ILogger<BookService> _logger;
        public BookService(ILogger<BookService> logger, IBookRepository bookRepository, IBorrowBookRepository borrowBookRepository, IReserveBookRepository reserveBookRepository)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _bookRepository = bookRepository ?? throw new ArgumentNullException(nameof(bookRepository));
            _borrowBookRepository = borrowBookRepository ?? throw new ArgumentNullException(nameof(borrowBookRepository));
            _reserveBookRepository = reserveBookRepository ?? throw new ArgumentNullException(nameof(reserveBookRepository));
        }

        public void AddBook(BookAddModel book)
        {
            var bookItem = new Book { Author = book.Author, Title = book.Title, Total = book.Total, Available = true };
            _bookRepository.Add(bookItem);
        }

        public IEnumerable<BookModel> GetBookByTitle(string title)
        {
            var bookList = new List<BookModel>();
            var books = _bookRepository.Find(x => x.Title.Contains(title));
            foreach (var item in books)
            {
                DateTime? availableDate = null;
                if (!item.Available)
                {
                    var nextReturn = _borrowBookRepository.Find(x => x.BookId == item.Id && x.Returned == false && x.DueDate > DateTime.Now).OrderBy(x => x.DueDate).FirstOrDefault();
                    if (nextReturn != null)
                        availableDate = nextReturn.DueDate;
                }

                bookList.Add(new BookModel
                {
                    Id = item.Id,
                    Title = item.Title,
                    Author = item.Author,
                    Available = item.Available,
                    BorrowedCopies = _borrowBookRepository.Find(x => x.BookId == item.Id && x.Returned == false).Count(),
                    ReservedCopies = _reserveBookRepository.Find(x => x.BookId == item.Id && x.DueDate > DateTime.Now).Count(),
                    NextAvailableDate = availableDate
                });
            }
            return bookList;
        }


        public IEnumerable<BookModel> GetBooks()
        {
            var bookList = new List<BookModel>();
            var books = _bookRepository.GetAll().ToList();
            foreach (var item in books)
            {
                DateTime? availableDate = null;
                if (!item.Available)
                {
                    var nextReturn = _borrowBookRepository.Find(x => x.BookId == item.Id && x.Returned == false && x.DueDate > DateTime.Now).OrderBy(x => x.DueDate).FirstOrDefault();
                    if (nextReturn != null)
                        availableDate = nextReturn.DueDate;
                }

                bookList.Add(new BookModel
                {
                    Id = item.Id,
                    Title = item.Title,
                    Author = item.Author,
                    Available = item.Available,
                    BorrowedCopies = _borrowBookRepository.Find(x => x.BookId == item.Id && x.Returned == false).Count(),
                    ReservedCopies = _reserveBookRepository.Find(x => x.BookId == item.Id && x.DueDate > DateTime.Now).Count(),
                    NextAvailableDate = availableDate
                });
            }
            return bookList;
        }
    }
}
