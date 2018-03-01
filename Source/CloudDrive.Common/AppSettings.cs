using CloudDrive.Settings;
using System;

namespace CloudDrive.Common
{
    public class AppSettings : ConfigBase
    {
        #region Singleton Constructor

        private static readonly Lazy<AppSettings> _instance = new Lazy<AppSettings>(() => new AppSettings(), true);
        public static AppSettings Instance { get { return _instance.Value; } }

        private AppSettings() { }

        #endregion

        // Overidden properties

        public override string SettingsFile => "Settings.CloudDrive.config";

        // Public properties

        public string AmazonS3ConnectionString => Get(nameof(AmazonS3ConnectionString));

        public string AzureBlobConnectionString => Get(nameof(AzureBlobConnectionString));

        public bool RunAtStartUp => bool.Parse(Get(nameof(RunAtStartUp)));

        public string RootDirectory => Get(nameof(RootDirectory));

    }
}
