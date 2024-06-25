using CaBackendTest.Models;

namespace CaBackendTest.Repositories.ProductRepository
{
    public interface IProductRepository
    {
        Task<List<Product>> GetProducts();
        Task<List<Product>> CreateProduct(Product Product);
        Task<Product> GetProductById(Guid id);
        Task<List<Product>> UpdateProduct(Product Product);
        Task<List<Product>> DeleteProduct(Product Product);
    }
}
