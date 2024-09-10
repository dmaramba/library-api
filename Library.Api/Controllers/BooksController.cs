using Library.Domain.Models;
using Library.Infrastructure.Services;
using Microsoft.AspNetCore.Cors.Infrastructure;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace Library.Api.Controllers
{
    [Route("api/books")]
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
        [ProducesResponseType(typeof(IReadOnlyCollection<Book>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status401Unauthorized)]
        public IActionResult GetBooks()
        {
            var books = bookService.GetBooks();

            return Ok(books);
        }

    }
}
