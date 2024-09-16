using Library.Domain.Models;
using Library.Domain.ViewModels;
using Library.Infrastructure.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Swashbuckle.AspNetCore.Annotations;

namespace Library.Api.Controllers
{
    [Route("api/customer")]
    [ApiController]
    [Authorize(Roles = "Customer")]
    public class CustomerBooksController : AuthBaseController
    {
        private readonly IBookService bookService;
        private readonly LibrarySetting settings;
        private readonly ICustomerService customerService;
        public CustomerBooksController(IBookService bookService, IOptions<LibrarySetting> settings, ICustomerService customerService)
        {
            this.bookService = bookService;
            this.settings = settings?.Value;
            this.customerService = customerService;
        }


        /// <summary>
        /// Borrow a book by logged in customer
        /// </summary>
        /// <param name="id">The id of the book</param>
        [SwaggerOperation(Tags = new[] { "Customer Books" })]
        [HttpPost(template: "{id}/BorrowBook")]
        [ProducesResponseType(typeof(BorrowBook), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> BorrowBook(int id)
        {
            var borrowModel = new BorrowModel
            {
                CustomerId = UserId,
                BookId = id,
                DueDate = DateTime.Now.AddDays(settings.BorrowMaxDay)
            };
            var response = await bookService.BorrowBook(borrowModel);
            return Ok(response);
        }

        /// <summary>
        /// Reserver a book by logged in customer
        /// </summary>
        /// <param name="id">The id of the book</param>
        [SwaggerOperation(Tags = new[] { "Customer Books" })]
        [HttpPost(template: "{id}/ReserveBook")]
        [ProducesResponseType(typeof(BorrowBook), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> ReserveBook(int id)
        {
            var reserveModel = new ReserveModel
            {
                CustomerId = UserId,
                BookId = id,
            };
            var response = await bookService.ReserveBook(reserveModel, settings.ReserveMaxHour);
            return Ok(response);
        }

        /// <summary>
        /// Get the profile of logged customer including borrowed and reserved books
        /// </summary>
        [SwaggerOperation(Tags = new[] { "Customer Books" })]
        [HttpGet(Name = nameof(GetProfile))]
        [ProducesResponseType(typeof(IReadOnlyCollection<CustomerModel>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public IActionResult GetProfile()
        {

            return Ok(customerService.GetCustomer(UserId));
        }


    }
}
