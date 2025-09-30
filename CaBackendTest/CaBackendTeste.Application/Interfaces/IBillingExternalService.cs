using CaBackendTest.Application.DTOs;

namespace CaBackendTest.Application.Interfaces
{
    public interface IBillingExternalService
    {
        Task<IEnumerable<InvoiceDto>> GetAllBillingsAsync();
    }
}
