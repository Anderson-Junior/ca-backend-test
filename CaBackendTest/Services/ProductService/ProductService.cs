using CaBackendTest.Models;
using CaBackendTest.Repositories.ProductRepository;

namespace CaBackendTest.Services.ProductService
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _repository;
        public ProductService(IProductRepository repository)
        {
            _repository = repository;
        }
        public async Task<ServiceResponse<List<Product>>> GetProducts()
        {
            ServiceResponse<List<Product>> serviceResponse = new ServiceResponse<List<Product>>();
            try
            {
                serviceResponse.Data = await _repository.GetProducts();

                if (serviceResponse.Data.Count == 0)
                {
                    serviceResponse.Message = "No Products found";
                }
            }
            catch (Exception ex)
            {
                serviceResponse.Message = ex.Message;
                serviceResponse.Success = false;
            }

            return serviceResponse;
        }
        public async Task<ServiceResponse<Product>> GetProductById(Guid id)
        {
            ServiceResponse<Product> serviceResponse = new ServiceResponse<Product>();
            try
            {
                Product Product = await _repository.GetProductById(id);
                if (Product == null)
                {
                    serviceResponse.Data = null;
                    serviceResponse.Message = "No Product found";
                    serviceResponse.Success = false;

                    return serviceResponse;
                }
                serviceResponse.Data = Product;
            }
            catch (Exception ex)
            {
                serviceResponse.Message = ex.Message;
                serviceResponse.Success = false;
            }

            return serviceResponse;
        }
        public async Task<ServiceResponse<List<Product>>> CreateProduct(Product Product)
        {
            ServiceResponse<List<Product>> serviceResponse = new ServiceResponse<List<Product>>();
            try
            {
                if (Product == null)
                {
                    serviceResponse.Data = null;
                    serviceResponse.Message = "No data sent";
                    serviceResponse.Success = false;

                    return serviceResponse;
                }
                serviceResponse.Data = await _repository.CreateProduct(Product);
            }
            catch (Exception ex)
            {
                serviceResponse.Message = ex.Message;
                serviceResponse.Success = false;
            }

            return serviceResponse;
        }
        public async Task<ServiceResponse<List<Product>>> UpdateProduct(Product Product)
        {
            ServiceResponse<List<Product>> serviceResponse = new ServiceResponse<List<Product>>();
            try
            {
                Product ProductExist = await _repository.GetProductById(Product.Id);

                if (ProductExist == null)
                {
                    serviceResponse.Data = null;
                    serviceResponse.Message = "No Product found";
                    serviceResponse.Success = false;

                    return serviceResponse;
                }
                serviceResponse.Data = await _repository.UpdateProduct(Product);
            }
            catch (Exception ex)
            {
                serviceResponse.Message = ex.Message;
                serviceResponse.Success = false;
            }

            return serviceResponse;
        }
        public async Task<ServiceResponse<List<Product>>> DeleteProduct(Guid id)
        {
            ServiceResponse<List<Product>> serviceResponse = new ServiceResponse<List<Product>>();
            try
            {
                Product ProductExist = await _repository.GetProductById(id);

                if (ProductExist == null)
                {
                    serviceResponse.Data = null;
                    serviceResponse.Message = "No Product found";
                    serviceResponse.Success = false;

                    return serviceResponse;
                }
                serviceResponse.Data = await _repository.DeleteProduct(ProductExist);
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
