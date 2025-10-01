using CaBackendTest.Application.DTOs;

namespace CaBackendTest.Infrastructure.Tests.Helpers
{
    public static class ExpectedInvoicesMock
    {
        public static InvoiceDto[] GetSampleInvoices()
        {
            return
            [
                new InvoiceDto
                {
                    InvoiceNumber = "INV-001",
                    Customer = new CustomerDto
                    {
                        Id = Guid.NewGuid(),
                        Name = "Customer A",
                        Email = "customerA@example.com",
                        Address = "123 Main St"
                    },
                    Date = DateTime.UtcNow.Date,
                    DueDate = DateTime.UtcNow.Date.AddDays(30),
                    TotalAmount = 150.00m,
                    Currency = "USD",
                    Lines = new List<InvoiceLineDto>
                    {
                        new InvoiceLineDto
                        {
                            ProductId = Guid.NewGuid(),
                            Description = "Product X",
                            Quantity = 2,
                            UnitPrice = 50.00m,
                            Subtotal = 100.00m
                        },
                        new InvoiceLineDto
                        {
                            ProductId = Guid.NewGuid(),
                            Description = "Product Y",
                            Quantity = 1,
                            UnitPrice = 50.00m,
                            Subtotal = 50.00m
                        }
                    }
                },
                new InvoiceDto
                {
                    InvoiceNumber = "INV-002",
                    Customer = new CustomerDto
                    {
                        Id = Guid.NewGuid(),
                        Name = "Customer B",
                        Email = "customerB@example.com",
                        Address = "456 Another St"
                    },
                    Date = DateTime.UtcNow.Date.AddDays(-10),
                    DueDate = DateTime.UtcNow.Date.AddDays(20),
                    TotalAmount = 200.00m,
                    Currency = "USD",
                    Lines = new List<InvoiceLineDto>
                    {
                        new InvoiceLineDto
                        {
                            ProductId = Guid.NewGuid(),
                            Description = "Product Z",
                            Quantity = 4,
                            UnitPrice = 50.00m,
                            Subtotal = 200.00m
                        }
                    }
                }
            ];
        }
    }
}
