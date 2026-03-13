using Abp.Application.Services;
using TestApp.Authorization.Accounts.Dto;
using System.Threading.Tasks;

namespace TestApp.Authorization.Accounts;

public interface IAccountAppService : IApplicationService
{
    Task<IsTenantAvailableOutput> IsTenantAvailable(IsTenantAvailableInput input);

    Task<RegisterOutput> Register(RegisterInput input);
}
