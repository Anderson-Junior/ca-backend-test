using CaBackendTest.Domain.Entities;

namespace CaBackendTest.Domain.Interfaces.Repositories.Cutomers
{
    public interface ICustomerRepository
    {
        Task<Customer> AddAsync(Customer customer, CancellationToken cancellationToken);
        Task DeleteAsync(Customer customer);
        Task<IEnumerable<Customer>> GetAllAsync();
        Task<Customer> GetById(Guid id);
        Task UpdateAsync(Customer customer, CancellationToken cancellationToken);
    }
}
