using RetailPortal.Domain.Entities;
using RetailPortal.Domain.Interfaces.Infrastructure.Data.Repositories;
using RetailPortal.Infrastructure.Data.Context;

namespace RetailPortal.Infrastructure.Data.Repositories;

public class RoleRepository(ApplicationDbContext context) : GenericRepository<Role>(context), IRoleRepository;