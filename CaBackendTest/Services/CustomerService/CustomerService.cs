using CaBackendTest.Models;
using CaBackendTest.Repositories.CustomerRepository;

namespace CaBackendTest.Services.CustomerService
{
    public class CustomerService : ICustomerService
    {
        private readonly ICustomerRepository _repository;
        public CustomerService(ICustomerRepository repository)
        {
            _repository = repository;
        }
        public async Task<ServiceResponse<List<Customer>>> GetCustomers()
        {
            ServiceResponse<List<Customer>> serviceResponse = new ServiceResponse<List<Customer>>();
            try
            {
                serviceResponse.Data = await _repository.GetCustomers();

                if (serviceResponse.Data.Count == 0)
                {
                    serviceResponse.Message = "No customers found";
                }
            }
            catch(Exception ex)
            {
                serviceResponse.Message = ex.Message;
                serviceResponse.Success = false;
            }

            return serviceResponse;
        }
        public async Task<ServiceResponse<Customer>> GetCustomerById(Guid id)
        {
            ServiceResponse<Customer> serviceResponse = new ServiceResponse<Customer>();
            try
            {
                Customer customer = await _repository.GetCustomerById(id);
                if (customer == null)
                {
                    serviceResponse.Data = null;
                    serviceResponse.Message = "No customer found";
                    serviceResponse.Success = false;

                    return serviceResponse;
                }
                serviceResponse.Data = customer;
            }
            catch (Exception ex)
            {
                serviceResponse.Message = ex.Message;
                serviceResponse.Success = false;
            }

            return serviceResponse;
        }
        public async Task<ServiceResponse<List<Customer>>> CreateCustomer(Customer customer)
        {
            ServiceResponse<List<Customer>> serviceResponse = new ServiceResponse<List<Customer>>();
            try
            {
                if (customer == null)
                {
                    serviceResponse.Data = null;
                    serviceResponse.Message = "No data sent";
                    serviceResponse.Success = false;

                    return serviceResponse;
                }
                serviceResponse.Data = await _repository.CreateCustomer(customer);
            }
            catch (Exception ex)
            {
                serviceResponse.Message = ex.Message;
                serviceResponse.Success = false;
            }

            return serviceResponse;
        }
        public async Task<ServiceResponse<List<Customer>>> UpdateCustomer(Customer customer)
        {
            ServiceResponse<List<Customer>> serviceResponse = new ServiceResponse<List<Customer>>();
            try
            {
                Customer customerExist = await _repository.GetCustomerById(customer.Id);

                if (customerExist == null)
                {
                    serviceResponse.Data = null;
                    serviceResponse.Message = "No customer found";
                    serviceResponse.Success = false;

                    return serviceResponse;
                }
                serviceResponse.Data = await _repository.UpdateCustomer(customer);
            }
            catch (Exception ex)
            {
                serviceResponse.Message = ex.Message;
                serviceResponse.Success = false;
            }

            return serviceResponse;
        }
        public async Task<ServiceResponse<List<Customer>>> DeleteCustomer(Guid id)
        {
            ServiceResponse<List<Customer>> serviceResponse = new ServiceResponse<List<Customer>>();
            try
            {
                Customer customerExist = await _repository.GetCustomerById(id);

                if (customerExist == null)
                {
                    serviceResponse.Data = null;
                    serviceResponse.Message = "No customer found";
                    serviceResponse.Success = false;

                    return serviceResponse;
                }
                serviceResponse.Data = await _repository.DeleteCustomer(customerExist);
            }
            catch (Exception ex)
            {
                serviceResponse.Message = ex.Message;
                serviceResponse.Success = false;
            }

            return serviceResponse;
        }  
    }
}
