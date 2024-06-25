using CaBackendTest.Models;

namespace CaBackendTest.Repositories.CustomerRepository
{
    public interface ICustomerRepository
    {
        Task<List<Customer>> GetCustomers();
        Task<List<Customer>> CreateCustomer(Customer customer);
        Task<Customer> GetCustomerById(Guid id);
        Task<List<Customer>> UpdateCustomer(Customer customer);
        Task<List<Customer>> DeleteCustomer(Customer customer);
    }
}
