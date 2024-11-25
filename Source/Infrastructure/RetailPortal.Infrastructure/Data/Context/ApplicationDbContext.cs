using Microsoft.EntityFrameworkCore;
using RetailPortal.Core.Entities;

namespace RetailPortal.Infrastructure.Data.Context;

public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : DbContext(options)
{
    public DbSet<Role> Roles { get; init; }
    public DbSet<User> Users { get; init; }
    public DbSet<Address> Addresses { get; init; }
    public DbSet<Product> Products { get; init; }
    public DbSet<Category> Categories { get; init; }
    public DbSet<Seller> Sellers { get; init; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);
    }
}