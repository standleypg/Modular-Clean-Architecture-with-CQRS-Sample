using Microsoft.EntityFrameworkCore;
using RetailPortal.Infrastructure.Data.Context;

namespace RetailPortal.Infrastructure.UnitTests.Data.Repositories;

public class BaseRepositoryTests: IDisposable
{
    protected ApplicationDbContext Context { get; }

    protected BaseRepositoryTests()
    {
        var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase(databaseName: $"RetailPortal-{Guid.NewGuid().ToString()}")
            .Options;
        
        Context = new ApplicationDbContext(options);
        
        Context.Database.EnsureCreated();
        Context.Database.EnsureDeleted();
    }

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    protected virtual void Dispose(bool disposing)
    {
        if (disposing)
        {
            Context.Dispose();
        }
    }

    ~BaseRepositoryTests()
    {
        Dispose(false);
    }
}