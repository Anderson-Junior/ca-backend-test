using CaBackendTest.Models;
using CaBackendTest.Services.CustomerService;
using Microsoft.AspNetCore.Mvc;

namespace CaBackendTest.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : Controller
    {
        private readonly ICustomerService _customerService;
        public CustomerController(ICustomerService customerService)
        {
            _customerService = customerService;
        }

        [HttpGet]
        public async Task<ActionResult<ServiceResponse<List<Customer>>>> GetCustomers()
        {
            return Ok(await _customerService.GetCustomers());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ServiceResponse<Customer>>> GetCustomerById(Guid id)
        {
            return Ok(await _customerService.GetCustomerById(id));
        }

        [HttpPost]
        public async Task<ActionResult<ServiceResponse<List<Customer>>>> CreateCustomer(Customer customer)
        {
            return Ok(await _customerService.CreateCustomer(customer));
        }

        [HttpPut]
        public async Task<ActionResult<ServiceResponse<List<Customer>>>> UpdateCustomer(Customer customer)
        {
            return Ok(await _customerService.UpdateCustomer(customer));
        }

        [HttpDelete]
        public async Task<ActionResult<ServiceResponse<List<Customer>>>> DeleteCustomer(Guid id)
        {
            return Ok(await _customerService.DeleteCustomer(id));
        }
    }
}
