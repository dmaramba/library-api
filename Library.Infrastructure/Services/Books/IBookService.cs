using Library.Domain.Models;
using Library.Domain.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Infrastructure.Services
{
    public interface IBookService
    {
        IEnumerable<BookModel> GetBooks();

        IEnumerable<BookModel> GetBookByTitle(string title);

        Task AddBook(BookAddModel book);

        Task<BorrowBook> BorrowBook(BorrowModel borrow);

        Task<ReserveBook> ReserveBook(ReserveModel reserve, int period);

        Task ReturnBook(ReturnModel returnModel);

        Task CancelReservation(int id);
    }
}
