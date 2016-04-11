using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;

namespace ComicLayouter
{
    /// <summary>
    /// An Quick easy way to configure your app settings(requires Extensions.cs)
    /// </summary>
    public class Config
    {
        private static AppSettingsSection _Settings;
        private static Configuration _config;
        public static Configuration config
        {
            get
            {
                if (_Settings == null)
                {
                    Init();
                }
                return _config;
            }
        }
        public static AppSettingsSection Settings
        {
            get
            {
                if (_Settings == null)
                {
                    Init();
                }
                return _Settings;
            }
        }

        /// <summary>
        /// used as a lookup for get and set.
        /// (hopefully it will speed up get)
        /// </summary>
        private static Dictionary<string, object> _settings;
        /// <summary>
        /// Gets the value string saved under the key in app config, and parses it to the specified type.
        /// </summary>
        /// <typeparam name="T">Type to parse value to</typeparam>
        /// <param name="key">The key to get value from</param>
        /// <param name="Default">A string that can be parsed, to return if value is missing.</param>
        /// <returns>Returns parsed value</returns>
        public static T Get<T>(string key, string Default)
        {
            if (_settings == null)
            {
                _settings = new Dictionary<string, object>();
            }
            if (_settings.ContainsKey(key))
            {
                return (T)_settings[key];
            }
            string s = ReadSetting(key, Default);
            T ret = s.ChangeType<T>();
            _settings[key] = ret;
            return ret;
        }
        /// <summary>
        /// Updates the key in app config to value.ToString()
        /// </summary>
        /// <param name="key">Where to set the value</param>
        /// <param name="value">What data should get written</param>
        public static void Set(string key, object value)
        {
            if (_settings == null)
            {
                _settings = new Dictionary<string, object>();
            }
            _settings[key] = value;
            WriteSetting(key, value.ToString());
        }
        /*
        +--------------------------------------------------------------------------------------------------------------------------------------------------------------+---------+
        |           |   Windows    |   Windows    |   Windows    |Windows NT| Windows | Windows | Windows | Windows | Windows | Windows | Windows | Windows |  Windows | Windows |
        |           |     95       |      98      |     Me       |    4.0   |  2000   |   XP    |  2003   |  Vista  |  2008   |    7    | 2008 R2 |    8    |   8.1    |   10    |
        +--------------------------------------------------------------------------------------------------------------------------------------------------------------+---------+
        |PlatformID | Win32Windows | Win32Windows | Win32Windows | Win32NT  | Win32NT | Win32NT | Win32NT | Win32NT | Win32NT | Win32NT | Win32NT | Win32NT |  ??      |  ??     |
        +--------------------------------------------------------------------------------------------------------------------------------------------------------------+---------+
        |Major      |              |              |              |          |         |         |         |         |         |         |         |         |          |         |
        | version   |      4       |      4       |      4       |    4     |    5    |    5    |    5    |    6    |    6    |    6    |    6    |    6    |     6    |   10    |
        +--------------------------------------------------------------------------------------------------------------------------------------------------------------+---------+
        |Minor      |              |              |              |          |         |         |         |         |         |         |         |         |          |         |
        | version   |      0       |     10       |     90       |    0     |    0    |    1    |    2    |    0    |    0    |    1    |    1    |    2    |     3    |    0    |
        +--------------------------------------------------------------------------------------------------------------------------------------------------------------+---------+
         */
        private static int _major;
        private static int _minor;
        private static void Init()
        {
            if (_Settings == null)
            {
                _major = Environment.OSVersion.Version.Major;
                _minor = Environment.OSVersion.Version.Minor;
                _config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
                _Settings = _config.AppSettings;
            }
        }

        public static string ReadSetting(string key, string Default)
        {
            try
            {
                var settings = config.AppSettings.Settings;
                if (settings[key] == null)
                {
                    return Default;
                }
                return settings[key].Value ?? Default;
            }
            catch (ConfigurationErrorsException)
            {
                Console.WriteLine("Error reading app settings");
            }
            return Default;
        }

        public static bool WriteSetting(string key, string value)
        {
            try
            {
                var settings = config.AppSettings.Settings;
                if (settings[key] == null)
                {
                    settings.Add(key, value);
                }
                else
                {
                    settings[key].Value = value;
                }
                config.Save(ConfigurationSaveMode.Modified);
                ConfigurationManager.RefreshSection(Settings.SectionInformation.Name);
                return true;
            }
            catch (ConfigurationErrorsException)
            {
                Console.WriteLine("Error writing app settings");
            }
            return false;
        }
    }
}
