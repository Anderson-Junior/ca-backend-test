using CaBackendTest.Infrastructure.Services;
using CaBackendTest.Infrastructure.Tests.Helpers;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Moq;
using RichardSzalay.MockHttp;
using System.Net;
using System.Text.Json;

namespace CaBackendTest.Infrastructure.Tests.Services
{
    public class BillingExternalServiceTests
    {
        private readonly MockHttpMessageHandler _mockHttp;
        private readonly IConfiguration _configuration;
        private readonly ILogger<BillingExternalService> _logger;

        public BillingExternalServiceTests()
        {
            _mockHttp = new MockHttpMessageHandler();

            var configMock = new Mock<IConfiguration>();
            configMock.Setup(c => c["ExtenalBillingsApi"]).Returns("http://fakeapi/billings");
            _configuration = configMock.Object;

            var loggerMock = new Mock<ILogger<BillingExternalService>>();
            _logger = loggerMock.Object;
        }

        [Fact]
        public async Task GetAllBillingsAsync_ReturnsInvoices_WhenResponseIsSuccessful()
        {
            // Arrange
            var expectedInvoices = ExpectedInvoicesMock.GetSampleInvoices();

            var responseJson = JsonSerializer.Serialize(expectedInvoices);

            _mockHttp.When("http://fakeapi/billings")
                .Respond("application/json", responseJson);

            var httpClient = _mockHttp.ToHttpClient();

            var service = new BillingExternalService(httpClient, _logger, _configuration);

            // Act
            var result = await service.GetAllBillingsAsync();

            // Assert
            Assert.NotNull(result);
            Assert.Collection(result,
                item => Assert.Equal(150.00m, item.TotalAmount),
                item => Assert.Equal(200.00m, item.TotalAmount));
        }

        [Fact]
        public async Task GetAllBillingsAsync_ThrowsException_WhenResponseIsUnsuccessful()
        {
            // Arrange
            _mockHttp.When("http://fakeapi/billings")
                .Respond(HttpStatusCode.InternalServerError);

            var httpClient = _mockHttp.ToHttpClient();

            var service = new BillingExternalService(httpClient, _logger, _configuration);

            // Act & Assert
            var ex = await Assert.ThrowsAsync<ApplicationException>(() =>
                service.GetAllBillingsAsync());

            Assert.Contains("status code", ex.Message);
        }
    }
}
