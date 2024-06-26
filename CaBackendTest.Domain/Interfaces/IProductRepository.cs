using CaBackendTest.Domain.Entities;

namespace CaBackendTest.Domain.Interfaces
{
    public interface IProductRepository : IBaseRepository<Product>
    {
        Task<Customer> GetByNamel(string name, CancellationToken cancellationToken);
    }
}
