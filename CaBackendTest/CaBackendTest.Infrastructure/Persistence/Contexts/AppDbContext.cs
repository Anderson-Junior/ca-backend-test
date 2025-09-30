using CaBackendTest.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace CaBackendTest.Infrastructure.Persistence.Contexts
{
    public sealed class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
    {
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Billing> Billings { get; set; }
        public DbSet<BillingLine> BillingLines { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);
            base.OnModelCreating(modelBuilder);

            // Seed para Customer
            modelBuilder.Entity<Customer>().HasData(new Customer
            {
                Id = Guid.Parse("12081264-5645-407a-ae37-78d5da96fe59"),
                Name = "Cliente Exemplo 1",
                Email = "cliente1@example.com",
                Address = "Rua Exemplo 1, 123"
            });

            // Seed para Product
            modelBuilder.Entity<Product>().HasData(new Product
            {
                Id = Guid.Parse("48c6dc20-a943-4f8f-83ca-1e1cf094a683"),
                Name = "Produto 1"
            });

            modelBuilder.Entity<Product>().HasData(new Product
            {
                Id = Guid.Parse("48c6dc20-a943-4f8f-83ca-1e1cf094a612"),
                Name = "Produto 2"
            });
        }
    }
}
