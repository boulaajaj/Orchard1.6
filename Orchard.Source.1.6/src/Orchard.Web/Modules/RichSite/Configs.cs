using System.Configuration;

namespace RichSite {
    public  static class Configs {
        public static string AppSetting(string key, string defaultValue) {
            return ConfigurationManager.AppSettings[key] ?? defaultValue;
        }

        public static bool AppSettingBool(string key, bool defaultValue) {
            bool val;
            return bool.TryParse(ConfigurationManager.AppSettings[key], out val) ? val : defaultValue;
        }
    }

   
}