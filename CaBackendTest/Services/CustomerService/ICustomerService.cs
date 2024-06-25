using CaBackendTest.Models;

namespace CaBackendTest.Services.CustomerService
{
    public interface ICustomerService
    {
        Task<ServiceResponse<List<Customer>>> GetCustomers();
        Task<ServiceResponse<List<Customer>>> CreateCustomer(Customer customer);
        Task<ServiceResponse<Customer>> GetCustomerById(Guid id);
        Task<ServiceResponse<List<Customer>>> UpdateCustomer(Customer customer);
        Task<ServiceResponse<List<Customer>>> DeleteCustomer(Guid id);
    }
}
