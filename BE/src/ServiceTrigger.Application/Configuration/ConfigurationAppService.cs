using System.Threading.Tasks;
using Abp.Authorization;
using Abp.Runtime.Session;
using ServiceTrigger.Configuration.Dto;

namespace ServiceTrigger.Configuration
{
    [AbpAuthorize]
    public class ConfigurationAppService : ServiceTriggerAppServiceBase, IConfigurationAppService
    {
        public async Task ChangeUiTheme(ChangeUiThemeInput input)
        {
            await SettingManager.ChangeSettingForUserAsync(AbpSession.ToUserIdentifier(), AppSettingNames.UiTheme, input.Theme);
        }
    }
}
