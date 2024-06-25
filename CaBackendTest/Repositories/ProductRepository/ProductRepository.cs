using CaBackendTest.DataContext;
using CaBackendTest.Models;
using Microsoft.EntityFrameworkCore;

namespace CaBackendTest.Repositories.ProductRepository
{
    public class ProductRepository : IProductRepository
    {
        private readonly AppDbContext _context;
        public ProductRepository(AppDbContext context)
        {
            _context = context;
        }
        public async Task<List<Product>> GetProducts()
        {
            return await _context.Products.ToListAsync();
        }
        public async Task<Product> GetProductById(Guid id)
        {
            return await _context.Products.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);
        }
        public async Task<List<Product>> CreateProduct(Product Product)
        {
            await _context.Products.AddAsync(Product);
            await _context.SaveChangesAsync();

            return await _context.Products.ToListAsync();
        }
        public async Task<List<Product>> UpdateProduct(Product Product)
        {
            _context.Products.Update(Product);
            await _context.SaveChangesAsync();

            return await _context.Products.ToListAsync();
        }
        public async Task<List<Product>> DeleteProduct(Product Product)
        {
            _context.Products.Remove(Product);
            await _context.SaveChangesAsync();

            return await _context.Products.ToListAsync();
        }
    }
}
