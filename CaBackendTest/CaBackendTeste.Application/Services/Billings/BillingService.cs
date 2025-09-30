using CaBackendTest.Application.DTOs;
using CaBackendTest.Application.Exceptions;
using CaBackendTest.Application.Interfaces;
using CaBackendTest.Domain.Entities;
using CaBackendTest.Domain.Interfaces.Repositories.Billings;
using CaBackendTest.Domain.Interfaces.Services.Billings;
using CaBackendTest.Domain.Interfaces.Services.Customers;
using CaBackendTest.Domain.Interfaces.Services.Products;
using Microsoft.Extensions.Logging;

namespace CaBackendTest.Application.Services.Billings
{
    public class BillingService : IBillingService
    {
        private readonly ICustomerService _customerService;
        private readonly IProductService _productService;
        private readonly IBillingExternalService _billingExternalService;
        private readonly IBillingRepository _billingRepository;
        private readonly ILogger<BillingService> _logger;

        public BillingService(
            ICustomerService customerService,
            IProductService productService,
            IBillingExternalService billingExternalService,
            IBillingRepository billingRepository,
            ILogger<BillingService> logger)
        {
            _customerService = customerService;
            _productService = productService;
            _billingExternalService = billingExternalService;
            _billingRepository = billingRepository;
            _logger = logger;
        }

        public async Task ImportBillings()
        {
            IEnumerable<InvoiceDto> billings;

            try
            {
                billings = await _billingExternalService.GetAllBillingsAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error when calling external API to get billings");
                throw new ApplicationException("Failed to access the external billing service.", ex);
            }

            foreach (var billing in billings)
            {
                try
                {
                    var customer = await _customerService.GetById(billing.Customer.Id);
                    if (customer == null)
                    {
                        _logger.LogError("Customer not found: {CustomerId} for invoice {InvoiceNumber}", billing.Customer.Id, billing.InvoiceNumber);
                        throw new ValidationException($"Could not create invoice {billing.InvoiceNumber} because the customer {billing.Customer.Id} does not exist in the system.");
                    }

                    var billingLines = new List<BillingLine>();
                    foreach (var line in billing.Lines)
                    {
                        var product = await _productService.GetById(line.ProductId);
                        if (product == null)
                        {
                            _logger.LogError("Product not found: {ProductId} for invoice {InvoiceNumber}", line.ProductId, billing.InvoiceNumber);
                            throw new ValidationException($"Could not create invoice {billing.InvoiceNumber} because the product {line.ProductId} does not exist in the system.");
                        }

                        var billingLine = new BillingLine(
                            line.ProductId,
                            line.Description,
                            line.Quantity,
                            line.UnitPrice,
                            line.Subtotal
                        );

                        billingLines.Add(billingLine);
                    }

                    Billing newBilling = new(
                        billing.InvoiceNumber,
                        billing.Date,
                        billing.DueDate,
                        billing.Currency,
                        billing.TotalAmount,
                        billing.Customer.Id,
                        customer,
                        billingLines
                    );

                    await _billingRepository.AddAsync(newBilling);
                    _logger.LogInformation("Invoice {InvoiceNumber} imported successfully.", billing.InvoiceNumber);
                }
                catch (ValidationException)
                {
                    throw;
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Unexpected error importing invoice {InvoiceNumber}", billing.InvoiceNumber);
                    throw new ApplicationException($"Unexpected error importing invoice {billing.InvoiceNumber}.", ex);
                }
            }
        }
    }
}