using Newtonsoft.Json;
using System;
using System.IO;

namespace CloudDrive.Common
{
    public class AppSettings
    {
        private static readonly Lazy<AppSetting> _instance = new Lazy<AppSetting>(() => AppSetting.Init(), true);
        public static AppSetting Instance { get { return _instance.Value; } }
    }

    [JsonObject(MemberSerialization.OptOut)]
    public class AppSetting
    {
        public const string SettingsFile = "Settings.CloudDrive.json";

        public AppSetting()
        {
        }

        // Public properties

        public string AmazonS3ConnectionString { get; set; }

        public string AzureBlobConnectionString { get; set; }

        public bool RunAtStartUp { get; set; }

        public string RootDirectory { get; set; }

        public string RootDirectoryInternal => RootDirectory.EndsWith("\\") ? RootDirectory : RootDirectory + "\\";

        // Public Methods

        public static AppSetting Init()
        {
            return (AppSetting)JsonConvert.DeserializeObject(File.ReadAllText(SettingsFile), typeof(AppSetting));
        }

        public void Save()
        {
            File.WriteAllText(SettingsFile, JsonConvert.SerializeObject(this));
        }
    }
}
