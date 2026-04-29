using App.Interfaces;
using System.Configuration;

namespace TechnicalTest.InterfaceAdapter.Adapters
{
    public class ConfigSettingsAdapter : ISettings
    {
        public string GetConfigValue(string key)
            => ConfigurationManager.AppSettings[key];
    }
}