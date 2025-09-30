using CaBackendTest.Application.DTOs;
using CaBackendTest.Application.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System.Net.Http.Json;
using System.Text.Json;

namespace CaBackendTest.Infrastructure.Services
{
    public class BillingExternalService : IBillingExternalService
    {
        private readonly HttpClient _httpClient;
        private readonly ILogger<BillingExternalService> _logger;
        private readonly IConfiguration _config;
        private readonly string _urlBillings;

        public BillingExternalService(HttpClient httpClient, ILogger<BillingExternalService> logger, IConfiguration config)
        {
            _httpClient = httpClient;
            _logger = logger;
            _config = config;
            _urlBillings = config["ExtenalBillingsApi"];
        }

        public async Task<IEnumerable<InvoiceDto>> GetAllBillingsAsync()
        {
            try
            {
                var response = await _httpClient.GetAsync(_urlBillings);

                if (!response.IsSuccessStatusCode)
                {
                    _logger.LogError("Request to external API failed. StatusCode: {StatusCode}", response.StatusCode);
                    throw new ApplicationException($"Error accessing external API: status code {response.StatusCode}");
                }

                var billings = await response.Content.ReadFromJsonAsync<List<InvoiceDto>>();

                if (billings == null)
                {
                    _logger.LogWarning("Empty or invalid response from external API.");
                    return new List<InvoiceDto>();
                }

                _logger.LogInformation("Billing data retrieved successfully from external API. Total: {Count}", billings.Count);
                return billings;
            }
            catch (HttpRequestException httpEx)
            {
                _logger.LogError(httpEx, "Error performing HTTP request to external billing API.");
                throw new ApplicationException("Failed to communicate with the external billing service.", httpEx);
            }
            catch (NotSupportedException nsEx)
            {
                _logger.LogError(nsEx, "Unsupported content format in response.");
                throw new ApplicationException("Response format from external service is not supported.", nsEx);
            }
            catch (JsonException jsonEx)
            {
                _logger.LogError(jsonEx, "Error deserializing response from external API.");
                throw new ApplicationException("Invalid or malformed response from external API.", jsonEx);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Unexpected error while obtaining data from external billing API.");
                throw new ApplicationException("Unexpected error while obtaining data from external billing API.", ex);
            }
        }
    }
}