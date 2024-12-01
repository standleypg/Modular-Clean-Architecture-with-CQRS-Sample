using RetailPortal.Domain.Entities;
using RetailPortal.Domain.Interfaces.Repositories;
using RetailPortal.Infrastructure.Data.Context;

namespace RetailPortal.Infrastructure.Data.Repositories;

public class ProductRepository(ApplicationDbContext context) : GenericRepository<Product>(context), IProductRepository;