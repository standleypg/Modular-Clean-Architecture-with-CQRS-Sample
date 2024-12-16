using RetailPortal.Domain.Entities;
using RetailPortal.Shared.Constants;

namespace RetailPortal.Domain.Interfaces.Application.Services;

public interface IRoleService
{
    Task<List<Role>> GetAllRolesAsync();
    Task<Role> GetRoleByNameAsync(Roles name);
}