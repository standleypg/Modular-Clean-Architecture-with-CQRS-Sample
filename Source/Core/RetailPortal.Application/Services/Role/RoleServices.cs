using Microsoft.EntityFrameworkCore;
using RetailPortal.Application.Common;
using RetailPortal.Domain.Interfaces.Application.Services;
using RetailPortal.Domain.Interfaces.Infrastructure.Data.UnitOfWork;
using RetailPortal.Shared.Constants;

namespace RetailPortal.Application.Services.Role;

public class RoleServices(IUnitOfWork uow) : BaseHandler(uow), IRoleService
{
    public async Task<List<Domain.Entities.Role>> GetAllRolesAsync()
    {
        return await this.Uow.RoleRepository.GetAll().ToListAsync();
    }

    public async Task<Domain.Entities.Role> GetRoleByNameAsync(Roles name)
    {
        var roles = this.Uow.RoleRepository.GetAll();
        var result = await roles.FirstAsync(x => x.Name == name.ToString());

        return result;
    }
}