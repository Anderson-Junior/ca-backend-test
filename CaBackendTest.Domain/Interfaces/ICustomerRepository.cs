using CaBackendTest.Domain.Entities;

namespace CaBackendTest.Domain.Interfaces
{
    public interface ICustomerRepository : IBaseRepository<Customer>
    {
        Task<Customer> GetByEmail(string email, CancellationToken cancellationToken);
    }
}
