using RetailPortal.Core.Interfaces.Repositories;
using RetailPortal.Domain.Entities;
using RetailPortal.Infrastructure.Data.Context;

namespace RetailPortal.Infrastructure.Data.Repositories;

public class UserRepository(ApplicationDbContext context) : GenericRepository<User>(context), IUserRepository;