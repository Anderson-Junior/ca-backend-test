using CaBackendTest.Domain.DTOs;
using CaBackendTest.Domain.Entities;
using CaBackendTest.Domain.Interfaces.Repositories.Cutomers;
using CaBackendTest.Domain.Interfaces.Services.Customers;

namespace CaBackendTest.Application.Services.Customers
{
    public class CustomerService : ICustomerService
    {
        private readonly ICustomerRepository _customerRepository;

        public CustomerService(ICustomerRepository customerRepository)
        {
            _customerRepository = customerRepository;
        }

        public async Task<Customer> AddAsync(UpsertCustomerDto customer, CancellationToken cancellationToken)
        {
            var newCustomer = new Customer
            (
                customer.Name,
                customer.Email,
                customer.Address
            );

            return await _customerRepository.AddAsync(newCustomer, cancellationToken);
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            var customerExist = await _customerRepository.GetById(id);
            if (customerExist == null)
                return false;

            await _customerRepository.DeleteAsync(customerExist);
            return true;
        }

        public async Task<IEnumerable<Customer>> GetAllAsync()
        {
            return await _customerRepository.GetAllAsync();
        }

        public async Task<Customer> GetById(Guid id)
        {
            return await _customerRepository.GetById(id);
        }

        public async Task<bool> UpdateAsync(Guid id, UpsertCustomerDto updatedCustomer, CancellationToken cancellationToken)
        {
            var existingCustomer = await _customerRepository.GetById(id);
            if (existingCustomer == null)
            {
                return false;
            }

            existingCustomer.Name = updatedCustomer.Name;
            existingCustomer.Email = updatedCustomer.Email;
            existingCustomer.Address = updatedCustomer.Address;

            await _customerRepository.UpdateAsync(existingCustomer, cancellationToken);
            return true;
        }
    }
}
