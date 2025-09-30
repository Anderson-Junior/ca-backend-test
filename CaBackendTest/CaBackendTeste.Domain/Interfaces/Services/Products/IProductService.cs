using CaBackendTest.Domain.DTOs;
using CaBackendTest.Domain.Entities;

namespace CaBackendTest.Domain.Interfaces.Services.Products
{
    public interface IProductService 
    {
        Task<Product> AddAsync(UpsertProductDto Product, CancellationToken cancellationToken);
        Task<bool> DeleteAsync(Guid id);
        Task<IEnumerable<Product>> GetAllAsync();
        Task<Product> GetById(Guid id);
        Task<bool> UpdateAsync(Guid id, UpsertProductDto updatedProduct, CancellationToken cancellationToken);
    }
}
