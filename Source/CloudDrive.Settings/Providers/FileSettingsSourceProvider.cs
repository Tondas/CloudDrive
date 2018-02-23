using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Xml;

namespace CloudDrive.Settings.Providers
{
    public class FileSettingsSourceProvider : ISettingsSourceProvider
    {
        #region Properties + Members

        public Dictionary<string, string> Values { get; private set; }

        public string ExplicitFilePath { get; set; }

        public string FileName { get; set; }

        #endregion

        // Ctor

        public FileSettingsSourceProvider()
        {
            Values = new Dictionary<string, string>();
        }

        public FileSettingsSourceProvider(string fileName, string explicitFilePath = null)
            : base()
        {
            FileName = fileName;
            ExplicitFilePath = explicitFilePath;
        }

        // Public Methods

        public void Init()
        {
            Values.Clear();
            Initialize();
        }

        // Private Methods

        private void Initialize()
        {
            if (string.IsNullOrEmpty(FileName))
            {
                throw new Exception("Please specify file name for FileSettingsProvider!");
            }

            // Find File 
            var path = FindFilePath();

            // Load XML
            XmlDocument configDoc = new XmlDocument();
            configDoc.Load(path);

            // Parse XML
            foreach (XmlNode node in configDoc.SelectNodes("//appSettings//add"))
            {
                var key = node.Attributes["key"].Value;
                var value = node.Attributes["value"].Value;

                if (!Values.ContainsKey(key))
                {
                    Values.Add(key, value);
                }
                else
                {
                    throw new Exception(string.Format("Key '{0}' already exists in file '{1}'!", key, FileName));
                }
            }

            // Replace values by specific values from AppSettings from web.config or app.config
            //foreach (var item in ConfigurationManager.AppSettings)
            //{
            //    string key = (string)item;
            //    if (Values.ContainsKey((string)key))
            //    {
            //        Values[key] = ConfigurationManager.AppSettings[key];
            //    }
            //}
        }

        private string FindFilePath()
        {
            string settingsFilePath = string.Empty;
            try
            {
                // try to read the file from bin folder (Services, Unit Tests)
                settingsFilePath = new Uri(
                    Path.Combine(
                        Path.GetDirectoryName(
                            Assembly.GetExecutingAssembly().GetName().EscapedCodeBase), FileName)).LocalPath;

                if (File.Exists(settingsFilePath))
                {
                    return settingsFilePath;
                }
            }
            catch { }

            throw new ArgumentException(
                String.Format("Configuration file does not exist in following location\nPath :[{0}]",
                    settingsFilePath));
        }
    }
}