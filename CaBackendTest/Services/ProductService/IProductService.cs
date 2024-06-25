using CaBackendTest.Models;

namespace CaBackendTest.Services.ProductService
{
    public interface IProductService
    {
        Task<ServiceResponse<List<Product>>> GetProducts();
        Task<ServiceResponse<List<Product>>> CreateProduct(Product Product);
        Task<ServiceResponse<Product>> GetProductById(Guid id);
        Task<ServiceResponse<List<Product>>> UpdateProduct(Product Product);
        Task<ServiceResponse<List<Product>>> DeleteProduct(Guid id);
    }
}
