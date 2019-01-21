using System.Threading.Tasks;
using ServiceTrigger.Configuration.Dto;

namespace ServiceTrigger.Configuration
{
    public interface IConfigurationAppService
    {
        Task ChangeUiTheme(ChangeUiThemeInput input);
    }
}
