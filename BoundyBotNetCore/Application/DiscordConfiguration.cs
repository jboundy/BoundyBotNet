using System.Configuration;

namespace BoundyBotNet.Core.Application
{
    public class DiscordConfiguration : ConfigurationSection
    {
        [ConfigurationProperty("AppToken", DefaultValue = "false", IsRequired = true)]
        public string AppToken
        {
            get { return this["AppToken"].ToString(); }
            set { this["AppToken"] = value; }
        }
    }
}
