using RetailPortal.Core.Entities;
using RetailPortal.Core.Interfaces.Repositories;
using RetailPortal.Infrastructure.Data.Context;

namespace RetailPortal.Infrastructure.Data.Repositories;

public class ProductRepository(ApplicationDbContext context) : GenericRepository<Product>(context), IProductRepository;