using System;
using System.IO;
using System.Text.Json;

namespace GameLibrary.Configuration
{
    class ConfigurationManager
    {
        private static readonly Lazy<ConfigurationManager> _instance =
            new(() => new ConfigurationManager());

        public static ConfigurationManager Instance => _instance.Value;

        private static string _configFilePath = Path.Combine("Data", "settings.json");

        public AppConfig Config { get; private set; }

        private ConfigurationManager()
        {
            if (File.Exists(_configFilePath))
            {
                string json = File.ReadAllText(_configFilePath);
                Config = JsonSerializer.Deserialize<AppConfig>(json) ?? new AppConfig { DownloadPath = "" };
            }
            else
            {
                Config = new AppConfig
                {
                    DownloadPath = "/Users/Shared/Downloads",
                    MaxDownloadSpeed = 0,
                    EnableAutoUpdates = false,
                    CurrentTheme = AppConfig.Theme.System
                };
                Save();
            }
        }

        public static void SetConfigPath(string path) => _configFilePath = path;

        public void Save()
        {
            var options = new JsonSerializerOptions { WriteIndented = true };
            string json = JsonSerializer.Serialize(Config, options);
            File.WriteAllText(_configFilePath, json);
        }

        public void UpdateSetting(Action<AppConfig> update)
        {
            update(Config);
            Save();
        }
    }
}