using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
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

        private async Task<IAudioClient> JoinVoiceChannelAsync(Channel channel)
        {
            try
            {
                return await Client.GetService<AudioService>().Join(channel);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }                
        }

        public async Task PlayAudioAsync(string filePath, Channel channel)
        {
            var _vClient = await JoinVoiceChannelAsync(channel);
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

        public async Task ProcessAudioAsync(string directoryPath, string folderPath, string soundFile, Channel channel)
        {
            DirectoryInfo dir = new DirectoryInfo(directoryPath);
            await PlayAudioAsync($@"{dir.FullName}\{folderPath}\{soundFile}", channel);
        }

        public Dictionary<string, string> BuildSoundIntros()
        {
            return BuildSoundFileDictionary("intros");
        }

        public Dictionary<string, string> BuildSoundFiles()
        {
            return BuildSoundFileDictionary("sounds");
        }

        private Dictionary<string, string> BuildSoundFileDictionary(string folder)
        {
            var dict = new Dictionary<string, string>();
            var directories = Directory.GetDirectories(folder);
            foreach (var directory in directories)
            {
                var files = Directory.GetFiles(directory);
                var file = files.FirstOrDefault();

                var newDirectory = directory.Split('\\');
                var newFile = file.Split('\\');

                dict.Add(newDirectory.Last(), newFile.Last());
            }

            return dict;
        }

        public string BuildCommandList()
        {
            var builder = new StringBuilder();
            foreach (var item in CommandList())
            {
                builder.Append(item).Append(Environment.NewLine);
            }

            return builder.ToString();
        }

        public IEnumerable<string> CommandList()
        {
            var list = new List<string>();
            DirectoryInfo dir = new DirectoryInfo("sounds");
            var directories = Directory.GetDirectories("sounds");
            foreach (var directory in directories)
            {
                var files = Directory.GetFiles(directory);
                var file = files.LastOrDefault();

                var newDirectory = directory.Split('\\');
                var newFile = file.Split('\\');
                var readFile = File.ReadAllText($@"{dir.FullName}\{newDirectory.Last()}\{newFile.Last()}");

                list.Add($"{newDirectory.Last()} - {readFile}");
            }

            return list;
        }
    }
}