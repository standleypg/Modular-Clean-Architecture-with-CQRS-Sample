using RetailPortal.Domain.Entities;
using RetailPortal.Domain.Interfaces.Repositories;
using RetailPortal.Infrastructure.Data.Context;

namespace RetailPortal.Infrastructure.Data.Repositories;

public class CategoryRepository(ApplicationDbContext context): GenericRepository<Category>(context), ICategoryRepository;