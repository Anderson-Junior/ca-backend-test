using CaBackendTest.DataContext;
using CaBackendTest.Models;
using Microsoft.EntityFrameworkCore;

namespace CaBackendTest.Repositories.CustomerRepository
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly AppDbContext _context;
        public CustomerRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<Customer>> GetCustomers()
        {
            return await _context.Customers.ToListAsync();
        }
        public async Task<Customer> GetCustomerById(Guid id)
        {
            return await _context.Customers.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);
        }
        public async Task<List<Customer>> CreateCustomer(Customer customer)
        {
            await _context.Customers.AddAsync(customer);
            await _context.SaveChangesAsync();

            return await _context.Customers.ToListAsync();
        }
        public async Task<List<Customer>> UpdateCustomer(Customer customer)
        {
            _context.Customers.Update(customer);
            await _context.SaveChangesAsync();

            return await _context.Customers.ToListAsync();
        }
        public async Task<List<Customer>> DeleteCustomer(Customer customer)
        {
            _context.Customers.Remove(customer);
            await _context.SaveChangesAsync();

            return await _context.Customers.ToListAsync();
        }

    }
}
