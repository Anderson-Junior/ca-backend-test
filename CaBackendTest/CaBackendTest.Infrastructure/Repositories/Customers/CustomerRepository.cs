using CaBackendTest.Domain.Entities;
using CaBackendTest.Domain.Interfaces.Repositories.Cutomers;
using CaBackendTest.Infrastructure.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;

namespace CaBackendTest.Infrastructure.Repositories.Customers
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly AppDbContext _appDbContext;

        public CustomerRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public async Task<Customer> AddAsync(Customer customer, CancellationToken cancellationToken)
        {
            await _appDbContext.AddAsync(customer, cancellationToken);
            await _appDbContext.SaveChangesAsync(cancellationToken);
            return customer;
        }

        public async Task DeleteAsync(Customer customer)
        {
            _appDbContext.Remove(customer);
            await _appDbContext.SaveChangesAsync();
        }

        public async Task<IEnumerable<Customer>> GetAllAsync()
        {
           return await _appDbContext.Customers.ToListAsync();
        }

        public async Task<Customer> GetById(Guid id)
        {
            return await _appDbContext.Customers.FindAsync(id);
        }

        public async Task UpdateAsync(Customer customer, CancellationToken cancellationToken)
        {
            _appDbContext.Update(customer);
            await _appDbContext.SaveChangesAsync(cancellationToken);
        }
    }
}
