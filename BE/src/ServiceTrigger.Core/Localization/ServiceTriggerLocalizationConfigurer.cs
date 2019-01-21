using Abp.Configuration.Startup;
using Abp.Localization.Dictionaries;
using Abp.Localization.Dictionaries.Xml;
using Abp.Reflection.Extensions;

namespace ServiceTrigger.Localization
{
    public static class ServiceTriggerLocalizationConfigurer
    {
        public static void Configure(ILocalizationConfiguration localizationConfiguration)
        {
            localizationConfiguration.Sources.Add(
                new DictionaryBasedLocalizationSource(ServiceTriggerConsts.LocalizationSourceName,
                    new XmlEmbeddedFileLocalizationDictionaryProvider(
                        typeof(ServiceTriggerLocalizationConfigurer).GetAssembly(),
                        "ServiceTrigger.Localization.SourceFiles"
                    )
                )
            );
        }
    }
}
