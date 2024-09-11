using Library.Domain.Models;
using Library.Domain.ViewModels;
using Library.Infrastructure.Repository;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
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

        public async Task AddBook(BookAddModel book)
        {
            var bookItem = new Book { Author = book.Author, Title = book.Title, Total = book.Total, Available = true };
            await _bookRepository.AddAsync(bookItem);
        }

        public async Task<BorrowBook> BorrowBook(BorrowModel borrow)
        {
            var borrowItem = new BorrowBook { BookId = borrow.BookId, Returned = false, CustomerId = borrow.CustomerId, DueDate = borrow.DueDate };
            await _borrowBookRepository.AddAsync(borrowItem);
            //remove the reservation if any
            var reserves = _reserveBookRepository.Find(x => x.BookId == borrow.BookId && x.CustomerId == borrow.CustomerId && x.DueDate > DateTime.Now);
            if (reserves.Any())
            {
                await _reserveBookRepository.RemoveRangeAsync(reserves);
            }
            updateBookAvailability(borrow.BookId);
            return borrowItem;
        }

        async void updateBookAvailability(int bookId)
        {

            var book = await _bookRepository.GetByIdAsync(bookId);
            if (book != null)
            {
                int totalBooksOut = _borrowBookRepository.Find(x => x.BookId == bookId && x.Returned == false).Count() + _reserveBookRepository.Find(x => x.BookId == bookId && x.DueDate > DateTime.Now).Count();
                if (totalBooksOut == book.Total)
                {
                    book.Available = false;
                    await _bookRepository.UpdateAsync(book);
                }
            }
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

        public async Task<ReserveBook> ReserveBook(ReserveModel reserve)
        {
            var reserved = _reserveBookRepository.Find(x => x.BookId == reserve.BookId && x.CustomerId == reserve.CustomerId && x.DueDate > DateTime.Now).FirstOrDefault();
            if (reserved == null)
            {
                var borrowItem = new ReserveBook { BookId = reserve.BookId, CustomerId = reserve.CustomerId, DueDate = DateTime.Now.AddHours(24) };
                await _reserveBookRepository.AddAsync(borrowItem);
                updateBookAvailability(reserve.BookId);
                return borrowItem;
            }
            return reserved;
        }

        public async Task ReturnBook(ReturnModel returnModel)
        {
            var borrowItem = _borrowBookRepository.Find(x => x.BookId == returnModel.BookId && x.CustomerId == returnModel.CustomerId && x.Returned == false).FirstOrDefault();
            if (borrowItem != null)
            {
                borrowItem.Returned = true;
                await _borrowBookRepository.UpdateAsync(borrowItem);
                updateBookAvailability(borrowItem.BookId);
            }
        }


    }
}
