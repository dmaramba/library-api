using Library.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Infrastructure.Services
{
    public interface IBookService
    {
        IEnumerable<Book> GetBooks();
        IEnumerable<Book> GetBookByTitle(string title);
    }
}
