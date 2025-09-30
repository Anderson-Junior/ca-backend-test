using CaBackendTest.Domain.Entities;
using CaBackendTest.Domain.Interfaces.Repositories.Products;
using CaBackendTest.Infrastructure.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;

namespace CaBackendTest.Infrastructure.Repositories.Products
{
    public class ProductRepository : IProductRepository
    {
        private readonly AppDbContext _appDbContext;

        public ProductRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public async Task<Product> AddAsync(Product product, CancellationToken cancellationToken)
        {
            await _appDbContext.AddAsync(product, cancellationToken);
            await _appDbContext.SaveChangesAsync(cancellationToken);
            return product;
        }

        public async Task DeleteAsync(Product Product)
        {
            _appDbContext.Remove(Product);
            await _appDbContext.SaveChangesAsync();
        }

        public async Task<IEnumerable<Product>> GetAllAsync()
        {
            return await _appDbContext.Products.ToListAsync();
        }

        public async Task<Product> GetById(Guid id)
        {
            return await _appDbContext.Products.FindAsync(id);
        }

        public async Task UpdateAsync(Product Product, CancellationToken cancellationToken)
        {
            _appDbContext.Update(Product);
            await _appDbContext.SaveChangesAsync(cancellationToken);
        }
    }
}
