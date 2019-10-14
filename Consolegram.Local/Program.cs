using System;
using System.IO;
using Newtonsoft.Json;

namespace Consolegram.Local
{
    public static class Program
    {
        private static string configPath = "consolegram.settings.json";
        
        public static void Main()
        {
            var config = GetConfigurationFromFile();
            ConsolegramClientFactory
                .New(config.ApiId, config.ApiHash)
                .Run();
        }

        private static ConsolegramConfiguration GetConfigurationFromFile()
        {
            if (!File.Exists(configPath))
                throw new Exception();
            
            var rowConfig = File.ReadAllText(configPath);
            return JsonConvert.DeserializeObject<ConsolegramConfiguration>(rowConfig);
        }
    }
}