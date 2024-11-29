using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RetailPortal.Core.Entities;

namespace RetailPortal.Infrastructure.Data.Configurations;

public class ProductConfigurations: IEntityTypeConfiguration<Product>
{
    public void Configure(EntityTypeBuilder<Product> builder)
    {
        builder.HasKey(e => e.Id);

        builder.Property(p => p.Id)
            .ValueGeneratedOnAdd();

        builder.Property(p => p.Name)
            .IsRequired()
            .HasMaxLength(50);

        builder.Property(p => p.Description)
            .IsRequired()
            .HasMaxLength(200);

        builder.OwnsOne(p=>p.Price, productBuilder =>
        {
            productBuilder.Property(p => p.Value)
                .HasColumnName("Amount")
                .IsRequired();

            productBuilder.Property(p => p.Currency)
                .HasColumnName("Currency")
                .IsRequired()
                .HasMaxLength(3);

            productBuilder.ToTable("Products");
        });

        builder.Property(p => p.ImageUrl)
            .HasDefaultValue(null)
            .IsRequired(false)
            .HasMaxLength(200);

        builder.Property(p => p.CategoryId)
            .IsRequired();

        builder.Property(p => p.SellerId)
            .IsRequired();

        builder.HasOne(p => p.Category)
            .WithMany(c => c.Products)
            .HasForeignKey(p => p.CategoryId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}