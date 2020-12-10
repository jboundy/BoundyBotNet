using BoundyBotNet.Services;
using DSharpPlus;
using DSharpPlus.VoiceNext;
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
                Token = Config.Token,
                TokenType = TokenType.Bot
            });

            var commandList = new CommandService().BuildCommandList();
            var audioService = new AudioService();
            var channelService = new ChannelService(_client);

            _client.UseVoiceNext();
            
            _client.MessageCreated += async e =>
            {
                if (commandList.ContainsKey(e.Message.Content))
                {
                    var vnc = await channelService.JoinChannel(e.Channel.Id);
                    await audioService.PlayAudio(vnc, commandList[e.Message.Content]);
                    await channelService.LeaveChannel(e.Channel.Id);
                }
            };

            await _client.ConnectAsync();
            await Task.Delay(-1);
        }
    }
}
