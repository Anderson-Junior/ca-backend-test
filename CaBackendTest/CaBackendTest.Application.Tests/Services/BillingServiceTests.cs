using CaBackendTest.Application.DTOs;
using CaBackendTest.Application.Exceptions;
using CaBackendTest.Application.Interfaces;
using CaBackendTest.Application.Services.Billings;
using CaBackendTest.Application.Tests.Helpers;
using CaBackendTest.Domain.Entities;
using CaBackendTest.Domain.Interfaces.Repositories.Billings;
using CaBackendTest.Domain.Interfaces.Services.Customers;
using CaBackendTest.Domain.Interfaces.Services.Products;
using Microsoft.Extensions.Logging;
using Moq;

namespace CaBackendTest.Application.Tests.Services
{
    public class BillingServiceTests
    {
        Mock<ICustomerService> mockCustomerService = new();
        Mock<IProductService> mockProductService = new();
        Mock<IBillingExternalService> mockExternalService = new();
        Mock<IBillingRepository> mockRepo = new();
        Mock<ILogger<BillingService>> mockLogger = new();

        [Fact]
        public async Task ImportBillings_ShouldImportAll_WhenDataIsValid()
        {
            // Arrange

            var mockCustomer = new Customer { Id = Guid.NewGuid(), Name = "Cliente" };
            var mockProduct = new Product { Id = Guid.NewGuid(), Name = "Produto" };

            var invoice = InvoiceDtoMock.GetInvoiceMock(mockCustomer.Id, mockCustomer.Name, mockProduct.Id);

            mockExternalService.Setup(s => s.GetAllBillingsAsync())
                .ReturnsAsync(new[] { invoice });
            mockCustomerService.Setup(s => s.GetById(mockCustomer.Id))
                .ReturnsAsync(mockCustomer);
            mockProductService.Setup(s => s.GetById(mockProduct.Id))
                .ReturnsAsync(mockProduct);

            var service = new BillingService(
                mockCustomerService.Object,
                mockProductService.Object,
                mockExternalService.Object,
                mockRepo.Object,
                mockLogger.Object
            );

            // Act
            await service.ImportBillings();

            // Assert
            mockRepo.Verify(r => r.AddAsync(It.IsAny<Billing>()), Times.Once);
        }

        [Fact]
        public async Task ImportBillings_ShouldThrowValidationException_IfCustomerDoesNotExist()
        {
            var customerId = Guid.NewGuid();
            var invoice = new InvoiceDto
            {
                InvoiceNumber = "001",
                Customer = new CustomerDto { Id = customerId, Name = "Cliente" },
                Date = DateTime.UtcNow,
                DueDate = DateTime.UtcNow.AddDays(5),
                Currency = "BRL",
                TotalAmount = 100,
                Lines = new List<InvoiceLineDto>()
            };
            mockExternalService.Setup(s => s.GetAllBillingsAsync())
                .ReturnsAsync(new[] { invoice });
            mockCustomerService.Setup(s => s.GetById(customerId))
                .ReturnsAsync((Customer)null);
            var service = new BillingService(
                mockCustomerService.Object,
                mockProductService.Object,
                mockExternalService.Object,
                mockRepo.Object,
                mockLogger.Object
            );

            // Act & Assert
            await Assert.ThrowsAsync<ValidationException>(() => service.ImportBillings());
            mockRepo.Verify(r => r.AddAsync(It.IsAny<Billing>()), Times.Never);
        }

        [Fact]
        public async Task ImportBillings_ShouldThrowValidationException_IfProductNotFound()
        {
            var customer = new Customer { Id = Guid.NewGuid(), Name = "Cliente" };
            var productId = Guid.NewGuid();

            var invoice = InvoiceDtoMock.GetInvoiceMock(customer.Id, customer.Name, productId);

            mockExternalService.Setup(e => e.GetAllBillingsAsync()).ReturnsAsync(new[] { invoice });
            mockCustomerService.Setup(s => s.GetById(customer.Id)).ReturnsAsync(customer);
            mockProductService.Setup(s => s.GetById(productId)).ReturnsAsync((Product)null);

            var service = new BillingService(
                mockCustomerService.Object,
                mockProductService.Object,
                mockExternalService.Object,
                mockRepo.Object,
                mockLogger.Object);

            await Assert.ThrowsAsync<ValidationException>(() => service.ImportBillings());
            mockRepo.Verify(r => r.AddAsync(It.IsAny<Billing>()), Times.Never);
        }

        [Fact]
        public async Task ImportBillings_ShouldThrowApplicationException_IfExternalServiceFails()
        {
            mockExternalService.Setup(e => e.GetAllBillingsAsync()).ThrowsAsync(new ApplicationException("erro"));

            var service = new BillingService(
                mockCustomerService.Object,
                mockProductService.Object,
                mockExternalService.Object,
                mockRepo.Object,
                mockLogger.Object);

            await Assert.ThrowsAsync<ApplicationException>(() => service.ImportBillings());
            mockRepo.Verify(r => r.AddAsync(It.IsAny<Billing>()), Times.Never);
        }
    }
}
