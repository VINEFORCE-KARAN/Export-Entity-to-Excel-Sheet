using Abp.Application.Services;
using TestApp.Sessions.Dto;
using System.Threading.Tasks;

namespace TestApp.Sessions;

public interface ISessionAppService : IApplicationService
{
    Task<GetCurrentLoginInformationsOutput> GetCurrentLoginInformations();
}
