using CaBackendTest.Domain.Entities;

namespace CaBackendTest.Domain.Interfaces.Repositories.Products
{
    public interface IProductRepository
    {
        Task<Product> AddAsync(Product product, CancellationToken cancellationToken);
        Task DeleteAsync(Product product);
        Task<IEnumerable<Product>> GetAllAsync();
        Task<Product> GetById(Guid id);
        Task UpdateAsync(Product product, CancellationToken cancellationToken);
    }
}
