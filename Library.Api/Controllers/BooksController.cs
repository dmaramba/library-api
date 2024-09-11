using Library.Domain.Models;
using Library.Domain.ViewModels;
using Library.Infrastructure.Services;
using Microsoft.AspNetCore.Cors.Infrastructure;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using static System.Reflection.Metadata.BlobBuilder;

namespace Library.Api.Controllers
{
    [Route("api/books/[action]")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        private readonly IBookService bookService;
        public BooksController(IBookService bookService)
        {
            this.bookService = bookService;
        }


        /// <summary>
        /// Gets the list of all books in the library
        /// </summary>
        [SwaggerOperation(Tags = new[] { "Books" })]
        [HttpGet(Name = nameof(GetBooks))]
        [ProducesResponseType(typeof(IReadOnlyCollection<BookModel>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status401Unauthorized)]
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
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status401Unauthorized)]
        public IActionResult GetBooksByTitle(string title)
        {

            return Ok(bookService.GetBookByTitle(title));
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
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status401Unauthorized)]
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
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> ReserveBook([FromBody] ReserveModel reserveModel)
        {
            var response = await bookService.ReserveBook(reserveModel);
            return Ok(response);
        }


        /// <summary>
        /// Reserve a book
        /// </summary>
        /// <param name="reserveModel">The model with book and customer</param>
        [SwaggerOperation(Tags = new[] { "Books" })]
        [HttpPost(Name = nameof(ReserveBook))]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> ReturnBook([FromBody] ReturnModel returnModel)
        {
            await bookService.ReturnBook(returnModel);
            return Ok();
        }
    }
}
