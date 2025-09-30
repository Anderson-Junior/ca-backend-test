using CaBackendTest.Domain.DTOs;
using CaBackendTest.Domain.Entities;

namespace CaBackendTest.Domain.Interfaces.Services.Customers
{
    public interface ICustomerService
    {
        Task<Customer> AddAsync(UpsertCustomerDto customer, CancellationToken cancellationToken);
        Task<bool> DeleteAsync(Guid id);
        Task<IEnumerable<Customer>> GetAllAsync();
        Task<Customer> GetById(Guid id);
        Task<bool> UpdateAsync(Guid id, UpsertCustomerDto customer, CancellationToken cancellationToken);
    }
}
