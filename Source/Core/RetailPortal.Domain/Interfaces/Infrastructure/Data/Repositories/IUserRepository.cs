using RetailPortal.Domain.Entities;

namespace RetailPortal.Domain.Interfaces.Infrastructure.Data.Repositories;

public interface IUserRepository : IGenericRepository<User>
{
    User? GetUserByEmail(string email);
}