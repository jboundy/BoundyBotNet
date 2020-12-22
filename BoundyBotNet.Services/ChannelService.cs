using DSharpPlus;
using DSharpPlus.CommandsNext;
using DSharpPlus.Entities;
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
            var vnext = _client.GetVoiceNextClient();
            var vnc = vnext.GetConnection(channelData.Guild);
            vnc = await vnext.ConnectAsync(channelData);
            return vnc;
        }

        public async Task LeaveChannel(ulong channelId)
        {
            var channelData = await _client.GetChannelAsync(channelId);
            var vnext = _client.GetVoiceNextClient();
            var vnc = vnext.GetConnection(channelData.Guild);
            vnc.Disconnect();
        }
    }
}
