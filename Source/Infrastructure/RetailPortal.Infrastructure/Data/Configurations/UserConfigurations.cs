using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RetailPortal.Domain.Entities;

namespace RetailPortal.Infrastructure.Data.Configurations;

public class UserConfigurations : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.HasKey(e => e.Id);

        builder.Property(u => u.Id)
            .ValueGeneratedOnAdd();

        builder.Property(u => u.FirstName)
            .IsRequired()
            .HasMaxLength(50);

        builder.Property(u => u.LastName)
            .IsRequired()
            .HasMaxLength(50);

        builder.Property(u => u.Email)
            .IsRequired()
            .HasMaxLength(50);

        builder.OwnsOne(u=>u.Password, passwordBuilder =>
        {
            passwordBuilder.Property(p => p.PasswordHash)
                .HasColumnName("PasswordHash")
                .IsRequired(false)
                .HasDefaultValue(null);

            passwordBuilder.Property(p => p.PasswordSalt)
                .HasColumnName("PasswordSalt")
                .IsRequired(false)
                .HasDefaultValue(null);

            passwordBuilder.ToTable("Users");
        });

        builder.Property(u => u.TokenProvider)
            .IsRequired()
            .HasConversion<string>();

        builder.HasMany(u=>u.Addresses)
            .WithOne(a=>a.User)
            .HasForeignKey(a=>a.UserId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(u => u.Seller)
            .WithOne(s => s.User)
            .HasForeignKey<Seller>(s => s.UserId)
            .IsRequired(false)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasMany(u => u.Roles)
            .WithMany(r => r.Users)
            .UsingEntity(
                "UserRoles",
                l => l.HasOne(typeof(Role)).WithMany().HasForeignKey("RoleId").HasPrincipalKey(nameof(Role.Id)),
                r => r.HasOne(typeof(User)).WithMany().HasForeignKey("UserId").HasPrincipalKey(nameof(User.Id)),
                j =>
                {
                    j.HasKey("UserId", "RoleId");
                    j.Property<DateTime>("AssignedDate").HasDefaultValueSql("NOW()");
                });
    }
}