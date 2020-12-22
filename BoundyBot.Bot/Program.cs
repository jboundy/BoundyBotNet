using BoundyBotNet.Services;
using DSharpPlus;
using DSharpPlus.VoiceNext;
using System;
using System.Linq;
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
            _client.UseVoiceNext();

            var commandList = new CommandService().BuildCommandList();
            var audioService = new AudioService();
            var channelService = new ChannelService(_client);

            _client.MessageCreated += async e =>
            {
                if (commandList.ContainsKey(e.Message.Content))
                { 
                    var vnc = await channelService.JoinChannel(247175209767927810);
                    await audioService.PlayAudio(vnc, commandList[e.Message.Content]);
                    await channelService.LeaveChannel(e.Channel.Id);
                }

                if (e.Message.Content.Contains("!btubeaudio"))
                {
                    var url = e.Message.Content.Split(' ')
                            .ToList()
                            .SingleOrDefault(x => x.Contains("http"));
                    var vnc = await channelService.JoinChannel(247175209767927810);
                    await audioService.PlayAudio(vnc, url);
                    await channelService.LeaveChannel(e.Channel.Id);
                }
            };

            await _client.ConnectAsync();
            await Task.Delay(-1);
        }
    }
}
