using Library.Domain.Models;
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
        private readonly ILogger<BookService> _logger;
        public BookService(ILogger<BookService> logger, IBookRepository bookRepository)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _bookRepository = bookRepository ?? throw new ArgumentNullException(nameof(bookRepository));
        }
        public IEnumerable<Book> GetBookByTitle(string title)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Book> GetBooks()
        {
            var books = _bookRepository.GetAll().ToList();
            return books;
        }
    }
}
