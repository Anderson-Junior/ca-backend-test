using CaBackendTest.Domain.DTOs;
using CaBackendTest.Domain.Entities;
using CaBackendTest.Domain.Interfaces.Services.Customers;
using Microsoft.AspNetCore.Mvc;

namespace CaBackendTest.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CustomerController : ControllerBase
    {
        private readonly ICustomerService _customerService;

        public CustomerController(ICustomerService customerService)
        {
            _customerService = customerService;
        }

        /// <summary>
        /// Get all customers.
        /// </summary>
        /// <returns>A 200 code, with the list of customers, in case of success</returns>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<IEnumerable<Customer>>> GetAll()
        {
            var customers = await _customerService.GetAllAsync();
            return Ok(customers);
        }

        /// <summary>
        /// Get a customer by Id.
        /// </summary>
        /// <param name="id">Customer unique identifier</param>
        /// <returns>A 200 code with customer data, or 404 if not found</returns>
        [HttpGet("{id:guid}")]
        [ProducesResponseType(typeof(Customer), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<Customer>> GetById(Guid id)
        {
            var customer = await _customerService.GetById(id);
            if (customer == null)
                return NotFound();

            return Ok(customer);
        }

        /// <summary>
        /// Create a new customer.
        /// </summary>
        /// <param name="newCustomer">Customer data to create</param>
        /// <param name="cancellationToken">Operation cancellation token</param>
        /// <returns>A 201 code with location header or 400 on validation error</returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> Add([FromBody] UpsertCustomerDto newCustomer, CancellationToken cancellationToken)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var createdCustomer = await _customerService.AddAsync(newCustomer, cancellationToken);

            return CreatedAtAction(nameof(GetById), new { id = createdCustomer.Id }, createdCustomer);
        }


        /// <summary>
        /// Update an existing customer.
        /// </summary>
        /// <param name="id">Customer unique identifier</param>
        /// <param name="updatedCustomer">Customer data to update</param>
        /// <param name="cancellationToken">Operation cancellation token</param>
        /// <returns>204 No Content on success, 400 on validation error, 404 if not found</returns>
        [HttpPut("{id:guid}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> Update(Guid id, [FromBody] UpsertCustomerDto updatedCustomer, CancellationToken cancellationToken)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var updated = await _customerService.UpdateAsync(id, updatedCustomer, cancellationToken);
            if (!updated)
                return NotFound();

            return NoContent();
        }

        /// <summary>
        /// Delete an existing customer.
        /// </summary>
        /// <param name="id">Customer unique identifier</param>
        /// <returns>204 No Content on success, 404 if not found</returns>
        [HttpDelete("{id:guid}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> Delete(Guid id)
        {
            var deleted = await _customerService.DeleteAsync(id);
            if (!deleted)
                return NotFound();

            return NoContent();
        }
    }
}
