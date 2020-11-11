using DSharpPlus;
using System;
using System.Collections.Generic;
using System.Text;
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

    }
}
