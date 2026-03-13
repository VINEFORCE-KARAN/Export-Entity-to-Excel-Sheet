using TestApp.Configuration.Dto;
using System.Threading.Tasks;

namespace TestApp.Configuration;

public interface IConfigurationAppService
{
    Task ChangeUiTheme(ChangeUiThemeInput input);
}
