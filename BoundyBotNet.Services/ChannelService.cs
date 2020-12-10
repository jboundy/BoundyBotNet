using DSharpPlus;
using DSharpPlus.CommandsNext;
using DSharpPlus.VoiceNext;
using System.Threading.Tasks;

namespace BoundyBotNet.Services
{
    public class ChannelService
    {
        private DiscordClient _client;

        public ChannelService(DiscordClient client)
        {
            _client = client;
        }

        public async Task<VoiceNextConnection> JoinChannel(ulong channelId)
        {
            var channelData = await _client.GetChannelAsync(channelId);
            var vnext = await GetVoiceClient();
            return await vnext.ConnectAsync(channelData);
        }

        public async Task LeaveChannel(ulong channelId)
        {
            var channelData = await _client.GetChannelAsync(channelId);
            var vnext = await GetVoiceClient();
            var vnc = vnext.GetConnection(channelData.Guild);
            vnc.Disconnect();
        }

        private async Task<VoiceNextClient> GetVoiceClient()
        {
            var commandContext = _client.GetCommandsNext();
            return await Task.FromResult(commandContext.Client.GetVoiceNextClient());
        }
    }
}
