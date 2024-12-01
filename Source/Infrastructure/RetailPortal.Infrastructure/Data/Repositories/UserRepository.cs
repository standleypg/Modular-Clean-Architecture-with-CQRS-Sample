using RetailPortal.Domain.Entities;
using RetailPortal.Domain.Interfaces.Repositories;
using RetailPortal.Infrastructure.Data.Context;

namespace RetailPortal.Infrastructure.Data.Repositories;

public class UserRepository(ApplicationDbContext context) : GenericRepository<User>(context), IUserRepository;