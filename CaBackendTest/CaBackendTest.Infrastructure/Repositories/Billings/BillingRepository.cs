using CaBackendTest.Domain.Entities;
using CaBackendTest.Domain.Interfaces.Repositories.Billings;
using CaBackendTest.Infrastructure.Persistence.Contexts;

namespace CaBackendTest.Infrastructure.Repositories.Billings
{
    public class BillingRepository : IBillingRepository
    {
        private readonly AppDbContext _context;

        public BillingRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(Billing billing)
        {
            await _context.Billings.AddAsync(billing);
            await _context.SaveChangesAsync();
        }
    }
}
