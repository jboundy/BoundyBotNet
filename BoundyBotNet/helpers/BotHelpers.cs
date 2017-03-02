using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Discord;
using Discord.Audio;
using NAudio.Wave;

namespace BoundyBotNet.helpers
{
    public class BotHelpers
    {
        public DiscordClient Client;

        public BotHelpers()
        {
            Client = new DiscordClient();
        }

        private async Task<IAudioClient> JoinVoiceChannelAsync(ulong channelId)
        {
            var voiceChannel = Client.FindServers("Jon's Cave of Fun").FirstOrDefault()
                .VoiceChannels.FirstOrDefault(x => x.Id == channelId);
            return await Client.GetService<AudioService>().Join(voiceChannel);
        }

        public async Task PlayAudioAsync(string filePath)
        {
            var _vClient = await JoinVoiceChannelAsync(202186271999787009);
            var channelCount = Client.GetService<AudioService>().Config.Channels;
            var outFormat = new WaveFormat(48000, 16, 2);
            var length = Convert.ToInt32(outFormat.AverageBytesPerSecond / 60.0 * 1000.0);
            byte[] buffer = new byte[length];

            using (var reader = new WaveFileReader(filePath))
            {
                using (var resampler = new WaveFormatConversionStream(outFormat, reader))
                {
                    int count = 0;
                    while ((count = resampler.Read(buffer, 0, length)) > 0)
                    {
                        _vClient.Send(buffer, 0, count);
                    }
                }
            }

            //todo: maybe someday we can use mp3s
            //using (var reader = new Mp3FileReader(filePath))
            //{
            //    using (var resampler = new DmoMp3FrameDecompressor(outFormat))
            //    {
            //        int count = 0;
            //        while((count = resampler.))
            //    }
            //}
            _vClient.Wait();
            await _vClient.Disconnect();
        }

        public IEnumerable<string> CommandList() =>
            new List<string>
            {
                "!bhorn - Mighty horn",
                "!bfuck - Angry",
                "!bhugeb - Big Huge Bitch",
                "!bff7 -Final Fantasy victory",
                "!bcombo - CCCCombo breaker",
                "!bprincess - Well princess....",
                "!bhotstep - Alex might like this",
                "!bchest - Zelda chest opening",
                "!bburn - Git burnt",
                "!bdrama - Dramatic beaver",
                "!bfatal - MK Fatality",
                "!bfdp - Some funny spanish",
                "!bmgs - Metal Gear Solid alert",
                "!bsparta - This...is...SPARTA!!!",
                "!bwolo - Wololololollo",
                "!bsecret - Zelda Secret",
                "!bpardon - Pardon me...",
                "!bwhip - Its a whip",
                "!bnuts - Deez nuts",
                "!bnope - Nope.avi",
                "!bomg - Family guy reference",
                "!bobjection - Objection!",
                "!bstfu - Just stfu",
                "!bsurprise - Surprise mothafucker",
                "!btada - Tada.....",
                "!bholyfuck - Really makes me giggle",
                "!billidan - Feel the hatred",
                "!bgay - Ha....",
                "!bwooo - RageOrc Wooooo"
            };  
    }
}
