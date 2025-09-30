using CaBackendTest.Domain.DTOs;
using CaBackendTest.Domain.Entities;
using CaBackendTest.Domain.Interfaces.Repositories.Products;
using CaBackendTest.Domain.Interfaces.Services.Products;

namespace CaBackendTest.Application.Services.Products
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;

        public ProductService(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public async Task<Product> AddAsync(UpsertProductDto product, CancellationToken cancellationToken)
        {
            var newProduct = new Product(product.Name);
            return await _productRepository.AddAsync(newProduct, cancellationToken);
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            var productExist = await _productRepository.GetById(id);
            if (productExist == null)
                return false;

            await _productRepository.DeleteAsync(productExist);
            return true;
        }

        public async Task<IEnumerable<Product>> GetAllAsync()
        {
            return await _productRepository.GetAllAsync();
        }

        public async Task<Product> GetById(Guid id)
        {
            return await _productRepository.GetById(id);
        }

        public async Task<bool> UpdateAsync(Guid id, UpsertProductDto updatedProduct, CancellationToken cancellationToken)
        {
            var existingProduct = await _productRepository.GetById(id);
            if (existingProduct == null)
            {
                return false;
            }

            existingProduct.Name = updatedProduct.Name;

            await _productRepository.UpdateAsync(existingProduct, cancellationToken);
            return true;
        }
    }
}
