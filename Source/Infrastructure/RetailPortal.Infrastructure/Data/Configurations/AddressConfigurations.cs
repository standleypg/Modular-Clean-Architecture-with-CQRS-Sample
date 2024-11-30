using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RetailPortal.Domain.Entities;

namespace RetailPortal.Infrastructure.Data.Configurations;

public class AddressConfigurations: IEntityTypeConfiguration<Address>
{
    public void Configure(EntityTypeBuilder<Address> builder)
    {
        builder.HasKey(e => e.Id);

        builder.Property(a => a.Id)
            .ValueGeneratedOnAdd();

        builder.Property(a => a.Street)
            .IsRequired()
            .HasMaxLength(50);

        builder.Property(a => a.City)
            .IsRequired()
            .HasMaxLength(50);

        builder.Property(a => a.State)
            .IsRequired()
            .HasMaxLength(50);

        builder.Property(a => a.PostalCode)
            .IsRequired()
            .HasMaxLength(50);

        builder.Property(a => a.Country)
            .IsRequired()
            .HasMaxLength(50);

        builder.HasOne(a => a.User)
            .WithMany(u => u.Addresses)
            .HasForeignKey(a => a.UserId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}