using CloudDrive.Settings.Providers;
using System;

namespace CloudDrive.Settings
{
    public class Config : ConfigBase
    {
        #region Singleton Constructor

        public override string SettingsFile { get; set; }

        private static readonly Lazy<Config> _instance = new Lazy<Config>(() => new Config(), true);
        public static Config Instance { get { return _instance.Value; } }

        private Config() { }

        #endregion

        // Public Static Methods

        public static string Get(string key)
        {
            return Instance.GetSetting(key);
        }

        public static void Reset()
        {
            Instance.ResetSetting();
        }


        public static void Use(string settingsFile)
        {
            Instance.SettingsFile = settingsFile;
        }

        public static void Use(ISettingsSourceProvider sourceProvider)
        {
            Instance.SourceProvider = sourceProvider;
        }

        public static void Use(string settingsFile, ISettingsSourceProvider sourceProvider)
        {
            Use(settingsFile);
            Use(sourceProvider);
        }
    }
}
