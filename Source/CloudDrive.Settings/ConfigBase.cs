using CloudDrive.Settings.Providers;
using System;
using System.Dynamic;

namespace CloudDrive.Settings
{
    public abstract class ConfigBase : DynamicObject
    {
        #region Members

        private static object _initializationLock = new object();

        #endregion Members

        #region Properties

        private bool IsInitialized => SourceProvider.Values.Count > 0;
        public abstract string SettingsFile { get; set; }
        public virtual ISettingsSourceProvider SourceProvider { get; set; }

        // Dynamic Object
        public static dynamic Setting { get; set; }

        #endregion Properties

        // Ctor

        public ConfigBase()
        {
            SourceProvider = new FileSettingsSourceProvider();

            // Init Dynamic object
            Setting = new ConfigDynamicObject(this);
        }

        public ConfigBase(string settingsFile)
        {
            SourceProvider = new FileSettingsSourceProvider(settingsFile);
            SettingsFile = settingsFile;
        }

        // Public Methods

        public string GetSetting(string key)
        {
            if (!IsInitialized)
            {
                Initialize();
            }
            if (SourceProvider.Values.ContainsKey(key))
            {
                return SourceProvider.Values[key];
            }
            else
            {
                throw new InvalidOperationException($"Setting key '{key}' doesn't exist in the application configuration!");
            }
        }

        public void ResetSetting()
        {
            SourceProvider.Init();
        }

        // Private Methods

        private void Initialize()
        {
            if (!IsInitialized)
            {
                lock (_initializationLock)
                {
                    if (!IsInitialized)
                    {
                        LoadSettings();
                    }
                }
            }
        }

        private void LoadSettings()
        {
            if (SourceProvider == null)
            {
                throw new Exception(string.Format("SourceProvider is null!"));
            }

            if (SourceProvider is FileSettingsSourceProvider)
            {
                if (string.IsNullOrEmpty(SettingsFile))
                {
                    throw new Exception(string.Format("Configuration file isn't specified!"));
                }

                (SourceProvider as FileSettingsSourceProvider).FileName = SettingsFile;
            }

            SourceProvider.Init();
        }

        // Overridden DynamicObject methods

        public override bool TryGetMember(GetMemberBinder binder, out object result)
        {
            result = GetSetting(binder.Name);
            return result == null ? false : true;
        }

        public override bool TryInvokeMember(InvokeMemberBinder binder, object[] args, out object result)
        {
            result = GetSetting(binder.Name);
            return result == null ? false : true;
        }

        // Class DynamicObject

        private class ConfigDynamicObject : DynamicObject
        {
            private ConfigBase _parent;

            public ConfigDynamicObject(ConfigBase parent)
            {
                _parent = parent;
            }

            // Methods

            public override bool TryGetMember(GetMemberBinder binder, out object result)
            {
                result = _parent.GetSetting(binder.Name);
                return result == null ? false : true;
            }

            public override bool TryInvokeMember(InvokeMemberBinder binder, object[] args, out object result)
            {
                result = _parent.GetSetting(binder.Name);
                return result == null ? false : true;
            }
        }
    }
}