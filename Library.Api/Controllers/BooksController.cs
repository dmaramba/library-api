using Library.Domain.Models;
using Library.Domain.ViewModels;
using Library.Infrastructure.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Swashbuckle.AspNetCore.Annotations;

namespace Library.Api.Controllers
{
    [Route("api/books/[action]")]
    [ApiController]
    [Authorize(Roles ="Admin")]
    public class BooksController : ControllerBase
    {
        private readonly IBookService bookService;
        private readonly LibrarySetting settings;
        private readonly ICustomerService customerService;
        public BooksController(IBookService bookService, IOptions<LibrarySetting> settings, ICustomerService customerService)
        {
            this.bookService = bookService;
            this.settings = settings?.Value;
            this.customerService = customerService;
        }


        /// <summary>
        /// Gets the list of all books in the library
        /// </summary>
        [SwaggerOperation(Tags = new[] { "Books" })]
        [HttpGet(Name = nameof(GetBooks))]
        [ProducesResponseType(typeof(IReadOnlyCollection<BookModel>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public IActionResult GetBooks()
        {

            return Ok(bookService.GetBooks());
        }


        /// <summary>
        /// Gets the list of all books in the library by title
        /// </summary>
        [SwaggerOperation(Tags = new[] { "Books" })]
        [HttpGet(Name = nameof(GetBooksByTitle))]
        [ProducesResponseType(typeof(IReadOnlyCollection<BookModel>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public IActionResult GetBooksByTitle(string title)
        {

            return Ok(bookService.GetBookByTitle(title));
        }


        /// <summary>
        /// Gets the list of all borrowed books
        /// </summary>
        [SwaggerOperation(Tags = new[] { "Books" })]
        [HttpGet(Name = nameof(GetBorrowedBooks))]
        [ProducesResponseType(typeof(IReadOnlyCollection<CustomerBook>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public IActionResult GetBorrowedBooks()
        {

            return Ok(customerService.GetBorrowedBooks());
        }

        /// <summary>
        /// Gets the list of all reserved books
        /// </summary>
        [SwaggerOperation(Tags = new[] { "Books" })]
        [HttpGet(Name = nameof(GetReservedBooks))]
        [ProducesResponseType(typeof(IReadOnlyCollection<CustomerBook>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public IActionResult GetReservedBooks()
        {

            return Ok(customerService.GetReservedBooks());
        }


        /// <summary>
        /// Add new book to the library
        /// </summary>
        /// <param name="book">The model with book details</param>
        [SwaggerOperation(Tags = new[] { "Books" })]
        [HttpPost(Name = nameof(AddBook))]
        [ProducesResponseType(typeof(IReadOnlyCollection<BookModel>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status401Unauthorized)]
        public IActionResult AddBook([FromBody] BookAddModel book)
        {
            bookService.AddBook(book);
            return Ok();
        }



        /// <summary>
        /// Borrow a book
        /// </summary>
        /// <param name="borrowModel">The model with book and customer</param>
        [SwaggerOperation(Tags = new[] { "Books" })]
        [HttpPost(Name = nameof(BorrowBook))]
        [ProducesResponseType(typeof(BorrowBook), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> BorrowBook([FromBody] BorrowModel borrowModel)
        {
            var response = await bookService.BorrowBook(borrowModel);
            return Ok(response);
        }


        /// <summary>
        /// Reserve a book
        /// </summary>
        /// <param name="reserveModel">The model with book and customer</param>
        [SwaggerOperation(Tags = new[] { "Books" })]
        [HttpPost(Name = nameof(ReserveBook))]
        [ProducesResponseType(typeof(BorrowBook), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> ReserveBook([FromBody] ReserveModel reserveModel)
        {
            var response = await bookService.ReserveBook(reserveModel, settings.BorrowMaxDay);
            return Ok(response);
        }


        /// <summary>
        /// Cancel reservation of a book
        /// </summary>
        /// <param name="id"></param>
        [SwaggerOperation(Tags = new[] { "Books" })]
        [HttpDelete(Name = nameof(CancelReservation))]
        [ProducesResponseType(typeof(BorrowBook), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> CancelReservation(int id)
        {
            await bookService.CancelReservation(id);
            return Ok();
        }

        /// <summary>
        /// Return a book
        /// </summary>
        /// <param name="returnModel">The model with book and customer</param>
        [SwaggerOperation(Tags = new[] { "Books" })]
        [HttpPost(Name = nameof(ReturnBook))]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> ReturnBook([FromBody] ReturnModel returnModel)
        {
            await bookService.ReturnBook(returnModel);
            return Ok();
        }


    }
}
