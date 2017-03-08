using System.Configuration;
using BoundyBotNet.Core.Application;

namespace BoundyBotNetCore.Application
{
    public static class Settings
    {
        public static DiscordConfiguration DiscordConfig { get; internal set; }
        public static AzureWebJobConfiguration AzureWebJobConfig { get; internal set; }
        public static void Initialize()
        {
            DiscordConfig = ConfigurationManager.GetSection("DiscordConfiguration") as DiscordConfiguration;
            AzureWebJobConfig = ConfigurationManager.GetSection("AzureWebJobConfiguration") as AzureWebJobConfiguration;
        }
    }
}
