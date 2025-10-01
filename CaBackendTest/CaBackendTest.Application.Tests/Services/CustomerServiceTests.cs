using CaBackendTest.Application.Services.Customers;
using CaBackendTest.Domain.DTOs;
using CaBackendTest.Domain.Entities;
using CaBackendTest.Domain.Interfaces.Repositories.Cutomers;
using Moq;

namespace CaBackendTest.Application.Tests.Services
{
    public class CustomerServiceTests
    {
        private readonly Mock<ICustomerRepository> mockCustomerRepo = new();

        Customer customer = new("João", "joaosilva@gmail.com", "Maria Sobania, 477");
        UpsertCustomerDto updatedCustomerDto = new() { Name = "João Silva", Email = "silvajoao@hotmail.com", Address = "Rua Joaquina Zidane, 854" };

        [Fact]
        public async Task AddAsync_ShouldAddCustomerAndReturnCreatedCustomer()
        {
            //Arrange
            var inputDto = new UpsertCustomerDto 
            { 
                Name = "João da Silva",
                Email = "joao@gmail.com",
                Address = "Rua Alvin Pedro, 85"
            };
            var expectedCustomer = new Customer(inputDto.Name, inputDto.Email, inputDto.Address);

            mockCustomerRepo
                .Setup(repo => repo.AddAsync(It.IsAny<Customer>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(expectedCustomer);

            var service = new CustomerService(mockCustomerRepo.Object);

            // Act
            var result = await service.AddAsync(inputDto, CancellationToken.None);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(expectedCustomer.Name, result.Name);
            Assert.Equal(expectedCustomer.Email, result.Email);
            Assert.Equal(expectedCustomer.Address, result.Address);

            mockCustomerRepo.Verify(repo => repo.AddAsync(
                It.Is<Customer>(p =>
                    p.Name == inputDto.Name &&
                    p.Email == inputDto.Email &&
                    p.Address == inputDto.Address),
                It.IsAny<CancellationToken>()), Times.Once);
        }

        [Fact]
        public async Task GetAllAsync_ShouldReturnAllCustomers()
        {
            // Arrange
            var customers = new List<Customer>
            {
                new Customer{ Id = Guid.NewGuid(), Name = "João Silva", Email = "joaosilva@gmail.com", Address = "Rua Maria Joaquina, 458"},
            };

            mockCustomerRepo.Setup(repo => repo.GetAllAsync())
                .ReturnsAsync(customers);

            var service = new CustomerService(mockCustomerRepo.Object);

            // Act
            var result = await service.GetAllAsync();

            // Assert
            Assert.NotNull(result);
            Assert.Single(result);
            Assert.Contains(result, p => p.Name == "João Silva");
            Assert.Contains(result, p => p.Email == "joaosilva@gmail.com");
            Assert.Contains(result, p => p.Address == "Rua Maria Joaquina, 458");

            mockCustomerRepo.Verify(repo => repo.GetAllAsync(), Times.Once);
        }

        [Fact]
        public async Task GetById_ShouldReturnCustomer_WhenCustomerExists()
        {
            // Arrange
            mockCustomerRepo.Setup(repo => repo.GetById(customer.Id))
                .ReturnsAsync(customer);

            var service = new CustomerService(mockCustomerRepo.Object);

            // Act
            var result = await service.GetById(customer.Id);

            Assert.NotNull(result);
            Assert.Equal(customer.Id, result.Id);
            Assert.Equal(customer.Name, result.Name);
        }

        [Fact]
        public async Task DeleteAsync_ShouldReturnFalse_WhenCustomerDoesNotExist()
        {
            // Arrange
            var nonExistentId = Guid.NewGuid();

            mockCustomerRepo.Setup(repo => repo.GetById(nonExistentId))
                .ReturnsAsync((Customer)null);

            var service = new CustomerService(mockCustomerRepo.Object);

            // Act
            var resultGetById = await service.GetById(nonExistentId);
            var result = await service.DeleteAsync(nonExistentId);

            Assert.Null(resultGetById);
            Assert.False(result);

            mockCustomerRepo.Verify(repo => repo.DeleteAsync(It.IsAny<Customer>()), Times.Never);
        }

        [Fact]
        public async Task DeleteAsync_ShouldReturnTrueAndDeleteCustomer_WhenCustomerExists()
        {
            mockCustomerRepo.Setup(repo => repo.GetById(customer.Id))
                .ReturnsAsync(customer);

            var service = new CustomerService(mockCustomerRepo.Object);

            // Act
            var resultGetById = await service.GetById(customer.Id);
            var result = await service.DeleteAsync(customer.Id);

            Assert.NotNull(resultGetById);
            Assert.True(result);

            mockCustomerRepo.Verify(repo => repo.DeleteAsync(It.IsAny<Customer>()), Times.Once);
        }

        [Fact]
        public async Task UpdateAsync_ShouldReturnFalse_WhenCustomerDoesNotExist()
        {
            // Arrange
            var nonExistentId = Guid.NewGuid();
            var updatedCustomerDto = new UpsertCustomerDto { Name = "João Silva", Email ="joaosilva@gmail.com", Address = "Rua Joaquina Zidane, 854" };

            mockCustomerRepo.Setup(repo => repo.GetById(nonExistentId))
                .ReturnsAsync((Customer)null);

            var service = new CustomerService(mockCustomerRepo.Object);

            // Act
            var result = await service.UpdateAsync(nonExistentId, updatedCustomerDto, CancellationToken.None);

            // Assert
            Assert.False(result);

            mockCustomerRepo.Verify(repo => repo.UpdateAsync(It.IsAny<Customer>(), It.IsAny<CancellationToken>()), Times.Never);
        }

        [Fact]
        public async Task UpdateAsync_ShouldReturnTrue_WhenCustomerIsSuccessfullyUpdated()
        {
            // Arrange
            mockCustomerRepo.Setup(repo => repo.GetById(customer.Id))
                .ReturnsAsync(customer);

            mockCustomerRepo.Setup(repo => repo.UpdateAsync(customer, It.IsAny<CancellationToken>()))
                .Returns(Task.CompletedTask);

            var service = new CustomerService(mockCustomerRepo.Object);

            // Act
            var result = await service.UpdateAsync(customer.Id, updatedCustomerDto, CancellationToken.None);

            // Assert
            Assert.True(result);

            // simulates editing the email, but without changing the other fields (name and address), PUT REST
            Assert.Equal(updatedCustomerDto.Email, customer.Email);

            mockCustomerRepo.Verify(repo => repo.GetById(customer.Id), Times.Once);
            mockCustomerRepo.Verify(repo => repo.UpdateAsync(customer, It.IsAny<CancellationToken>()), Times.Once);
        }
    }
}
