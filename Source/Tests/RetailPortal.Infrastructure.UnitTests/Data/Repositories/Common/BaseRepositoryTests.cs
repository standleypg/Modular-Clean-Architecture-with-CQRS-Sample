using Microsoft.EntityFrameworkCore;
using RetailPortal.Infrastructure.Data.Context;

namespace RetailPortal.Infrastructure.UnitTests.Data.Repositories.Common;

public class BaseRepositoryTests: IDisposable
{
    protected ApplicationDbContext Context { get; }

    protected BaseRepositoryTests()
    {
        var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase(databaseName: $"RetailPortal-{Guid.NewGuid().ToString()}")
            .Options;

        this.Context = new ApplicationDbContext(options);

        this.Context.Database.EnsureCreated();
        this.Context.Database.EnsureDeleted();
    }

    public void Dispose()
    {
        this.Dispose(true);
        GC.SuppressFinalize(this);
    }

    protected virtual void Dispose(bool disposing)
    {
        if (disposing)
        {
            this.Context.Dispose();
        }
    }

    ~BaseRepositoryTests()
    {
        this.Dispose(false);
    }
}