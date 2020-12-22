using Discord;
using Discord.Addons.Hosting;
using Discord.WebSocket;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BoundyBotNet.Tests
{
    public class BotEmulator
    {
        public BotEmulator()
        {
            InitializeBotAsync().GetAwaiter().GetResult();
        }

        private async Task InitializeBotAsync()
        {
            var builder = new HostBuilder()
                .ConfigureDiscordHost<DiscordSocketClient>((context, config) =>
                {
                    config.SocketConfig = new DiscordSocketConfig
                    {
                        LogLevel = LogSeverity.Verbose,
                        AlwaysDownloadUsers = true,
                        MessageCacheSize = 200
                    };

                    config.Token = context.Configuration["token"];
                })
                .UseConsoleLifetime();
            var host = builder.Build();
            using (host)
            {
                await host.RunAsync();
            }
        }
    }
}
