using CaBackendTest.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

internal sealed class BillingLineConfiguration : IEntityTypeConfiguration<BillingLine>
{
    public void Configure(EntityTypeBuilder<BillingLine> builder)
    {
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Description)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(x => x.Quantity)
            .IsRequired();

        builder.Property(x => x.UnitPrice)
            .IsRequired()
            .HasColumnType("decimal(18,2)");

        builder.Property(x => x.SubTotal)
            .IsRequired()
            .HasColumnType("decimal(18,2)");

        builder.Property(x => x.ProductId)
            .IsRequired();

        builder.Property(x => x.BillingId)
            .IsRequired();

        // Relacionamento BillingLine -> Product
        builder.HasOne(x => x.Product)
            .WithMany() // Se Product não tem coleção de BillingLine, mantenha vazio
            .HasForeignKey(x => x.ProductId)
            .OnDelete(DeleteBehavior.Restrict);

        // Relacionamento BillingLine -> Billing
        builder.HasOne(x => x.Billing)
            .WithMany(b => b.BillingLines)
            .HasForeignKey(x => x.BillingId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
