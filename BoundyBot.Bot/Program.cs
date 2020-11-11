using DSharpPlus;
using System;
using System.Threading.Tasks;

namespace BoundyBot.Bot
{
    class Program
    {
        static DiscordClient _client { get; set; }
        static void Main(string[] args)
        {
            MainAsync(args).ConfigureAwait(false).GetAwaiter().GetResult();
        }
        static async Task MainAsync(string[] args)
        {
            _client = new DiscordClient(new DiscordConfiguration
            {
                Token = "<paste the token here>",
                TokenType = TokenType.Bot
            });

            _client.MessageCreated += async e =>
            {
                
            };

            await _client.ConnectAsync();
            await Task.Delay(-1);
        }
    }
}
