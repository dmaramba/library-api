using Library.Domain.Models;
using Library.Domain.ViewModels;
using Library.Infrastructure.Repository;
using Microsoft.Extensions.Logging;

namespace Library.Infrastructure.Services
{
    public class CustomerService : ICustomerService
    {
        private readonly ICustomerRepository _customerRepository;
        private readonly ILogger<BookService> _logger;
        public CustomerService(ILogger<BookService> logger, ICustomerRepository customerRepository)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _customerRepository = customerRepository ?? throw new ArgumentNullException(nameof(customerRepository));
        }
        public async Task AddCustomer(CustomAddProfileModel profile)
        {
            var customer = new Customer { Name = profile.Name, Email = profile.Email };
            await _customerRepository.AddAsync(customer);
        }

        public CustomerModel GetCustomer(int customerId)
        {

            var customer = _customerRepository.GetCustomerData(customerId);
            return customer;
        }

        public IEnumerable<Customer> GetCustomers()
        {
            var customers = _customerRepository.GetAll();
            return customers;
        }

        public async Task UpdateCustomer(CustomProfileModel profile)
        {
            var customer = await _customerRepository.GetByIdAsync(profile.Id);
            if (customer != null)
            {
                customer.Name = profile.Name;
                customer.Email = profile.Email;
                await _customerRepository.UpdateAsync(customer);
            }
        }
    }
}
