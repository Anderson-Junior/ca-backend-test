using CaBackendTest.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CaBackendTest.Infrastructure.Persistence.Configurations
{
    internal sealed class BillingConfiguration : IEntityTypeConfiguration<Billing>
    {
        public void Configure(EntityTypeBuilder<Billing> builder)
        {
            builder.HasKey(x => x.Id);

            // Properties
            builder.Property(x => x.InvoiceNumber)
                .IsRequired()
                .HasMaxLength(40);

            builder.Property(x => x.Date)
                .IsRequired();

            builder.Property(x => x.DueDate)
                .IsRequired();

            builder.Property(x => x.Currency)
                .IsRequired()
                .HasMaxLength(8);

            builder.Property(x => x.TotalAmount)
                .IsRequired()
                .HasColumnType("decimal(18,2)");

            // Relationships

            // Customer 1:N Billing
            builder.HasOne(x => x.Customer)
                .WithMany(c => c.Billings)
                .HasForeignKey(x => x.CustomerId)
                .OnDelete(DeleteBehavior.Restrict);

            // Billing 1:N BillingLines
            builder.HasMany(x => x.BillingLines)
                .WithOne(l => l.Billing)
                .HasForeignKey(l => l.BillingId)
                .OnDelete(DeleteBehavior.Cascade);

            // Inicialização para coleção (opcional, pode ser feito no constructor da entity)
            builder.Navigation(x => x.BillingLines).AutoInclude(false);
        }
    }
}
