using CaBackendTest.Domain.DTOs;
using CaBackendTest.Domain.Entities;
using CaBackendTest.Domain.Interfaces.Services.Products;
using Microsoft.AspNetCore.Mvc;

namespace CaBackendTest.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;

        public ProductController(IProductService productService)
        {
            _productService = productService;
        }

        /// <summary>
        /// Get all products.
        /// </summary>
        /// <returns>A 200 code, with the list of products, in case of success</returns>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<IEnumerable<Product>>> GetAll()
        {
            var products = await _productService.GetAllAsync();
            return Ok(products);
        }

        /// <summary>
        /// Get a product by Id.
        /// </summary>
        /// <param name="id">Product unique identifier</param>
        /// <returns>A 200 code with product data, or 404 if not found</returns>
        [HttpGet("{id:guid}")]
        [ProducesResponseType(typeof(Product), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<Product>> GetById(Guid id)
        {
            var product = await _productService.GetById(id);
            if (product == null)
                return NotFound();

            return Ok(product);
        }

        /// <summary>
        /// Create a new product.
        /// </summary>
        /// <param name="newProduct">Product data to create</param>
        /// <param name="cancellationToken">Operation cancellation token</param>
        /// <returns>A 201 code with location header or 400 on validation error</returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> Add([FromBody] UpsertProductDto newProduct, CancellationToken cancellationToken)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var createdProduct = await _productService.AddAsync(newProduct, cancellationToken);
            return CreatedAtAction(nameof(GetById), new { id = createdProduct.Id }, createdProduct);
        }

        /// <summary>
        /// Update an existing product.
        /// </summary>
        /// <param name="id">Product unique identifier</param>
        /// <param name="updatedProduct">Product data to update</param>
        /// <param name="cancellationToken">Operation cancellation token</param>
        /// <returns>204 No Content on success, 400 on validation error, 404 if not found</returns>
        [HttpPut("{id:guid}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> Update(Guid id, [FromBody] UpsertProductDto updatedProduct, CancellationToken cancellationToken)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var updated = await _productService.UpdateAsync(id, updatedProduct, cancellationToken);
            if (!updated)
                return NotFound();

            return NoContent();
        }

        /// <summary>
        /// Delete an existing product.
        /// </summary>
        /// <param name="id">Product unique identifier</param>
        /// <returns>204 No Content on success, 404 if not found</returns>
        [HttpDelete("{id:guid}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> Delete(Guid id)
        {
            var deleted = await _productService.DeleteAsync(id);
            if (!deleted)
                return NotFound();

            return NoContent();
        }
    }
}
