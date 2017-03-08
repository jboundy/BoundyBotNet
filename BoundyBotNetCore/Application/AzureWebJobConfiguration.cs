using System.Configuration;

namespace BoundyBotNet.Core.Application
{
    public class AzureWebJobConfiguration : ConfigurationSection
    {
        [ConfigurationProperty("ConnectionString", DefaultValue = "false", IsRequired = true)]
        public string ConnectionString
        {
            get { return this["ConnectionString"].ToString(); }
            set { this["ConnectionString"] = value; }
        }
    }
}
