using RetailPortal.Domain.Entities;
using RetailPortal.Domain.Interfaces.Infrastructure.Data.Repositories;
using RetailPortal.Infrastructure.Data.Context;

namespace RetailPortal.Infrastructure.Data.Repositories;

public class UserRepository(ApplicationDbContext context) : GenericRepository<User>(context), IUserRepository
{
    public User? GetUserByEmail(string email)
    {
        return context.Users.FirstOrDefault(u => u.Email == email);
    }
}