using CaBackendTest.Domain.Entities;

namespace CaBackendTest.Domain.Interfaces.Repositories.Billings
{
    public interface IBillingRepository
    {
        Task AddAsync(Billing billing);
    }
}
