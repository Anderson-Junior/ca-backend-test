using CaBackendTest.Application.Services.Products;
using CaBackendTest.Domain.DTOs;
using CaBackendTest.Domain.Entities;
using CaBackendTest.Domain.Interfaces.Repositories.Products;
using Moq;

namespace CaBackendTest.Application.Tests.Services
{
    public class ProductServiceTests
    {
        private readonly Mock<IProductRepository> mockProductRepo = new();

        [Fact]
        public async Task AddAsync_ShouldAddProductAndReturnCreatedProduct()
        {
            //Arrange
            var inputDto = new UpsertProductDto { Name = "New Product" };
            var expectedProduct = new Product(inputDto.Name);

            mockProductRepo
                .Setup(repo => repo.AddAsync(It.IsAny<Product>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(expectedProduct);

            var service = new ProductService(mockProductRepo.Object);

            // Act
            var result = await service.AddAsync(inputDto, CancellationToken.None);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(expectedProduct.Name, result.Name);
            mockProductRepo.Verify(repo => repo.AddAsync(It.Is<Product>(p => p.Name == inputDto.Name), It.IsAny<CancellationToken>()), Times.Once);
        }

        [Fact]
        public async Task GetAllAsync_ShouldReturnAllProducts()
        {
            //Arrange
            var products = new List<Product>
            {
                new Product { Id = Guid.NewGuid(), Name = "Produto A" },
                new Product { Id = Guid.NewGuid(), Name = "Produto B" }
            };

            mockProductRepo.Setup(repo => repo.GetAllAsync())
                .ReturnsAsync(products);

            var service = new ProductService(mockProductRepo.Object);

            // Act
            var result = await service.GetAllAsync();

            // Assert
            Assert.NotNull(result);
            Assert.Equal(2, result.Count());
            Assert.Contains(result, p => p.Name == "Produto A");
            Assert.Contains(result, p => p.Name == "Produto B");
            mockProductRepo.Verify(repo => repo.GetAllAsync(), Times.Once);
        }

        [Fact]
        public async Task GetById_ShouldReturnProduct_WhenProductExists()
        {
            // Arrange
            var product = new Product("Produto A");

            mockProductRepo.Setup(repo => repo.GetById(product.Id))
                .ReturnsAsync(product);

            var service = new ProductService(mockProductRepo.Object);

            // Act
            var result = await service.GetById(product.Id);

            Assert.NotNull(result);
            Assert.Equal(product.Id, result.Id);
            Assert.Equal(product.Name, result.Name);
        }

        [Fact]
        public async Task DeleteAsync_ShouldReturnFalse_WhenProductDoesNotExist()
        {
            // Arrange
            var nonExistentId = Guid.NewGuid();

            mockProductRepo.Setup(repo => repo.GetById(nonExistentId))
                .ReturnsAsync((Product)null);

            var service = new ProductService(mockProductRepo.Object);

            // Act
            var resultGetById = await service.GetById(nonExistentId);
            var result = await service.DeleteAsync(nonExistentId);

            Assert.Null(resultGetById);
            Assert.False(result);

            mockProductRepo.Verify(repo => repo.DeleteAsync(It.IsAny<Product>()), Times.Never);
        }

        [Fact]
        public async Task DeleteAsync_ShouldReturnTrueAndDeleteProduct_WhenProductExists()
        {
            var product = new Product("Produto A");

            mockProductRepo.Setup(repo => repo.GetById(product.Id))
                .ReturnsAsync(product);

            var service = new ProductService(mockProductRepo.Object);

            // Act
            var resultGetById = await service.GetById(product.Id);
            var result = await service.DeleteAsync(product.Id);

            Assert.NotNull(resultGetById);
            Assert.True(result);

            mockProductRepo.Verify(repo => repo.DeleteAsync(It.IsAny<Product>()), Times.Once);
        }

        [Fact]
        public async Task UpdateAsync_ShouldReturnFalse_WhenProductDoesNotExist()
        {
            // Arrange
            var nonExistentId = Guid.NewGuid();
            var updatedProductDto = new UpsertProductDto { Name = "Produto Atualizado" };

            mockProductRepo.Setup(repo => repo.GetById(nonExistentId))
                .ReturnsAsync((Product)null);

            var service = new ProductService(mockProductRepo.Object);

            // Act
            var result = await service.UpdateAsync(nonExistentId, updatedProductDto, CancellationToken.None);

            // Assert
            Assert.False(result);

            mockProductRepo.Verify(repo => repo.UpdateAsync(It.IsAny<Product>(), It.IsAny<CancellationToken>()), Times.Never);
        }

        [Fact]
        public async Task UpdateAsync_ShouldReturnTrue_WhenProductIsSuccessfullyUpdated()
        {
            // Arrange
            var existingProduct = new Product("Produto Original");
            var updatedProductDto = new UpsertProductDto { Name = "Produto Atualizado" };

            mockProductRepo.Setup(repo => repo.GetById(existingProduct.Id))
                .ReturnsAsync(existingProduct);

            mockProductRepo.Setup(repo => repo.UpdateAsync(existingProduct, It.IsAny<CancellationToken>()))
                .Returns(Task.CompletedTask);

            var service = new ProductService(mockProductRepo.Object);

            // Act
            var result = await service.UpdateAsync(existingProduct.Id, updatedProductDto, CancellationToken.None);

            // Assert
            Assert.True(result);
            Assert.Equal(updatedProductDto.Name, existingProduct.Name);

            mockProductRepo.Verify(repo => repo.GetById(existingProduct.Id), Times.Once);
            mockProductRepo.Verify(repo => repo.UpdateAsync(existingProduct, It.IsAny<CancellationToken>()), Times.Once);
        }
    }
}