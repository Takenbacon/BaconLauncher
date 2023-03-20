using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace BaconLauncher.Config
{
    public class ConfigManager
    {
        public static ConfigManager Instance { get; protected set; } = new ConfigManager();
        public Config Config = new Config();

        private static string ConfigFile = "data.json";

        public void LoadConfig()
        {
            try
            {
                string jsonString = File.ReadAllText(ConfigFile);
                Config = JsonSerializer.Deserialize<Config>(jsonString)!;
            }
            catch (Exception ex)
            {

            }
        }

        public void SaveConfig()
        {
            try
            {
                var options = new JsonSerializerOptions { WriteIndented = true };
                string jsonString = JsonSerializer.Serialize(Config, options);
                File.WriteAllText(ConfigFile, jsonString);
            }
            catch (Exception ex)
            {

            }
        }
    }
}
