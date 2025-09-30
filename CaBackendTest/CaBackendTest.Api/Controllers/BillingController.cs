using CaBackendTest.Application.DTOs;
using CaBackendTest.Application.Exceptions;
using CaBackendTest.Application.Interfaces;
using CaBackendTest.Domain.Interfaces.Services.Billings;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace CaBackendTest.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BillingController : ControllerBase
    {
        private readonly IBillingExternalService _billingExternalService;
        private readonly IBillingService _billingService;
        private readonly ILogger<BillingController> _logger;

        public BillingController(
            IBillingExternalService billingExternalService,
            IBillingService billingService,
            ILogger<BillingController> logger)
        {
            _billingExternalService = billingExternalService;
            _billingService = billingService;
            _logger = logger;
        }

        /// <summary>
        /// Retrieves all billings from the external API.
        /// </summary>
        /// <returns>List of invoices.</returns>
        /// <response code="200">Returns the list of invoices successfully retrieved.</response>
        /// <response code="500">Internal server error while fetching billings.</response>
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<InvoiceDto>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(object), (int)HttpStatusCode.InternalServerError)]
        public async Task<ActionResult<IEnumerable<InvoiceDto>>> GetAllBillings()
        {
            try
            {
                var billings = await _billingExternalService.GetAllBillingsAsync();
                return Ok(billings);
            }
            catch (ApplicationException appEx)
            {
                _logger.LogError(appEx, "Error fetching billings from external API.");
                return StatusCode(500, new { error = appEx.Message });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Unexpected error fetching billings from external API.");
                return StatusCode(500, new { error = "Unexpected error fetching billings from external API." });
            }
        }

        /// <summary>
        /// Imports billings from the external API into the local database.
        /// </summary>
        /// <remarks>
        /// This operation fetches billing data from an external API and imports it into the local system.
        /// Validation errors or missing dependencies (customers/products) will result in a bad request response.
        /// </remarks>
        /// <response code="200">Import completed successfully.</response>
        /// <response code="400">Import failed due to validation errors (e.g. missing customers or products).</response>
        /// <response code="500">Internal server error during the import process.</response>
        [HttpPost("ImportsBillingExternalAPI")]
        [ProducesResponseType(typeof(object), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(object), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(object), (int)HttpStatusCode.InternalServerError)]
        public async Task<ActionResult> ImportBillings()
        {
            try
            {
                await _billingService.ImportBillings();
                _logger.LogInformation("Billing import completed successfully.");
                return Ok(new { message = "Import completed successfully." });
            }
            catch (ValidationException ve)
            {
                _logger.LogWarning(ve, "Import failed due to validation.");
                return BadRequest(new { error = ve.Message });
            }
            catch (ApplicationException ae)
            {
                _logger.LogError(ae, "Import failed due to application error.");
                return StatusCode(500, new { error = "Internal error processing billing import." });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Unexpected error during billing import.");
                return StatusCode(500, new { error = "Unexpected error importing billings." });
            }
        }
    }
}
