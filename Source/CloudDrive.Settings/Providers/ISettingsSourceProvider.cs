using System.Collections.Generic;

namespace CloudDrive.Settings.Providers
{
    public interface ISettingsSourceProvider
    {
        // Members + Properties

        Dictionary<string, string> Values { get; }

        // Methods

        void Init();
    }
}
