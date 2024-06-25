using CaBackendTest.Models;
using CaBackendTest.Services.ProductService;
using Microsoft.AspNetCore.Mvc;

namespace CaBackendTest.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _ProductService;
        public ProductController(IProductService ProductService)
        {
            _ProductService = ProductService;
        }

        [HttpGet]
        public async Task<ActionResult<ServiceResponse<List<Product>>>> GetProducts()
        {
            return Ok(await _ProductService.GetProducts());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ServiceResponse<Product>>> GetProductById(Guid id)
        {
            return Ok(await _ProductService.GetProductById(id));
        }

        [HttpPost]
        public async Task<ActionResult<ServiceResponse<List<Product>>>> CreateProduct(Product Product)
        {
            return Ok(await _ProductService.CreateProduct(Product));
        }

        [HttpPut]
        public async Task<ActionResult<ServiceResponse<List<Product>>>> UpdateProduct(Product Product)
        {
            return Ok(await _ProductService.UpdateProduct(Product));
        }

        [HttpDelete]
        public async Task<ActionResult<ServiceResponse<List<Product>>>> DeleteProduct(Guid id)
        {
            return Ok(await _ProductService.DeleteProduct(id));
        }
    }
}
