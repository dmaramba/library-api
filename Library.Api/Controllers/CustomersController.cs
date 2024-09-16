using Library.Domain.ViewModels;
using Library.Infrastructure.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Swashbuckle.AspNetCore.Annotations;

namespace Library.Api.Controllers
{
    [Route("api/customers/[action]")]
    [ApiController]
    [Authorize(Roles = "Admin")]
    public class CustomersController : ControllerBase
    {
        private readonly ICustomerService customerService;
        public CustomersController(ICustomerService customerService)
        {
            this.customerService = customerService;
        }

        /// <summary>
        /// Add new customer
        /// </summary>
        /// <param name="profileModel">The model for the customer</param>
        [SwaggerOperation(Tags = new[] { "Customers" })]
        [HttpPost(Name = nameof(AddCustomer))]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public IActionResult AddCustomer([FromBody] CustomAddProfileModel profileModel)
        {
            customerService.AddCustomer(profileModel);
            return Ok();
        }

        /// <summary>
        /// Gets the list of all customers
        /// </summary>
        [SwaggerOperation(Tags = new[] { "Customers" })]
        [HttpGet(Name = nameof(GetCustomers))]
        [ProducesResponseType(typeof(IReadOnlyCollection<CustomProfileModel>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public IActionResult GetCustomers()
        {
            return Ok(customerService.GetCustomers());
        }


        /// <summary>
        /// Update customer profile
        /// </summary>
        /// <param name="profileModel">The model for the customer</param>
        [SwaggerOperation(Tags = new[] { "Customers" })]
        [HttpPatch(Name = nameof(UpdateCustomer))]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public IActionResult UpdateCustomer([FromBody] CustomProfileModel profileModel)
        {
            customerService.UpdateCustomer(profileModel);
            return Ok();
        }

        /// <summary>
        /// Get profile of a customer including borrowed and reserved books
        /// </summary>
        [SwaggerOperation(Tags = new[] { "Customers" })]
        [HttpGet(Name = nameof(GetCustomer))]
        [ProducesResponseType(typeof(IReadOnlyCollection<CustomerModel>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public IActionResult GetCustomer(int customerId)
        {

            return Ok(customerService.GetCustomer(customerId));
        }
    }
}
