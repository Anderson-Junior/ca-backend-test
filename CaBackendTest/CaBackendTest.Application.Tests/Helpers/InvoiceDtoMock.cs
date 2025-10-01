using CaBackendTest.Application.DTOs;

namespace CaBackendTest.Application.Tests.Helpers
{
    public static class InvoiceDtoMock
    {
        public static InvoiceDto GetInvoiceMock(Guid customerId, string customerName, Guid productId)
        {
            return new InvoiceDto
            {
                InvoiceNumber = "001",
                Customer = new CustomerDto
                {
                    Id = customerId,
                    Name = customerName,
                    Email = "mockcustomer@example.com",
                    Address = "123 Mock St"
                },
                Date = DateTime.UtcNow,
                DueDate = DateTime.UtcNow.AddDays(5),
                Currency = "BRL",
                TotalAmount = 100,
                Lines = new List<InvoiceLineDto>
                {
                    new InvoiceLineDto
                    {
                        ProductId = productId,
                        Description = "Produto",
                        Quantity = 1,
                        UnitPrice = 100,
                        Subtotal = 100
                    }
                }
            };
        }
    }
}
