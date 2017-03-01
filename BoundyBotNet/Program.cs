using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Discord;
using Discord.Audio;
using NAudio.FileFormats.Mp3;
using NAudio.Wave;

namespace BoundyBotNet
{
    class Program
    {
        static void Main(string[] args)
        {
            //var host = new JobHost();
            //// The following code ensures that the WebJob will be running continuously
            //host.Call(typeof(Program).GetMethod("Start"));
            //host.RunAndBlock();

            new Program().Start();
           
        } 

        public DiscordClient _client;

        public void Start()
        {
            _client = new DiscordClient();

            _client.UsingAudio(x => 
            {
                x.Mode = AudioMode.Outgoing;
            });

            _client.MessageReceived += async (s, e) =>
            {
                if (e.Message.Text.Equals("!bhorn"))
                {
                    await PlayAudioAsync(@"D:\Source\Repos\BoundyBotNet\BoundyBotNet\sounds\airhorn_default.wav");
                }

                if (e.Message.Text.Equals("!bfuck"))
                {
                    await PlayAudioAsync(@"D:\Source\Repos\BoundyBotNet\BoundyBotNet\sounds\fuckoff.wav");
                }

                if (e.Message.Text.Equals("!bhugeb"))
                {
                    await PlayAudioAsync(@"D:\Source\Repos\BoundyBotNet\BoundyBotNet\sounds\huge-bitch.wav");
                }

                if (e.Message.Text.Equals("!bff7"))
                {
                    await PlayAudioAsync(@"D:\Source\Repos\BoundyBotNet\BoundyBotNet\sounds\victory_fanfare.wav");
                }

                if (e.Message.Text.Equals("!bcombo"))
                {
                    await PlayAudioAsync(@"D:\Source\Repos\BoundyBotNet\BoundyBotNet\sounds\combobreaker.wav");
                }

                if (e.Message.Text.Equals("!bprincess"))
                {
                    await PlayAudioAsync(@"D:\Source\Repos\BoundyBotNet\BoundyBotNet\sounds\Excuse_Me_Princess.wav");
                }

                if (e.Message.Text.Equals("!bhotstep"))
                {
                    await PlayAudioAsync(@"D:\Source\Repos\BoundyBotNet\BoundyBotNet\sounds\hotstepper.wav");
                }

                if (e.Message.Text.Equals("!bchest"))
                {                   
                   await PlayAudioAsync(@"D:\Source\Repos\BoundyBotNet\BoundyBotNet\sounds\zeldaitem.wav");               
                }

                if (e.Message.Text.Equals("!bburn"))
                {
                    await PlayAudioAsync(@"D:\Source\Repos\BoundyBotNet\BoundyBotNet\sounds\burned.wav");
                }
            };

           _client.ExecuteAndWait(async () => {
                await _client.Connect("MjQ5NzkzMjMyNTM4NDM1NTg1.CxLdsg.QzojNKXFJOnPwez_ByUEkKEg4I8", TokenType.Bot);
            });
        }

        private async Task<IAudioClient> JoinVoiceChannelAsync(ulong channelId)
        {
            var voiceChannel = _client.FindServers("Jon's Cave of Fun").FirstOrDefault()
                .VoiceChannels.FirstOrDefault(x => x.Id == channelId);
            return await _client.GetService<AudioService>().Join(voiceChannel);
        }

        private async Task PlayAudioAsync(string filePath)
        {
            var _vClient = await JoinVoiceChannelAsync(202186271999787009);
            var channelCount = _client.GetService<AudioService>().Config.Channels;
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
    }
}
