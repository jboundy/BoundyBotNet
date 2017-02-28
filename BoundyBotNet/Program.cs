using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Discord;
using Discord.Audio;
using Discord.Commands.Permissions.Visibility;
using NAudio.Wave;

namespace BoundyBotNet
{
    class Program
    {
        static void Main(string[] args) => new Program().Start();

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
                if (e.Message.Text.Equals("ping"))
                {
                    await PlayAudio(@"D:\Source\Repos\BoundyBotNet\BoundyBotNet\sounds\airhorn.mp3");
                }
            };

            //_client.MessageReceived += async (s, e) =>
            //{
            //    if (e.Message.Text.Equals("ping"))
            //    {
            //        var voiceChannel = _client.FindServers("Jon's Cave of Fun")
            //                .FirstOrDefault()
            //                .VoiceChannels.FirstOrDefault(x => x.Id == 282252528392077312);

            //        var _vClient = await _client.GetService<AudioService>().Join(voiceChannel); // Join the Voice Channel, and return the IAudioClient.

            //        var filePath = @"D:\Source\Repos\BoundyBotNet\BoundyBotNet\sounds\airhorn.mp3";

            //        var process = Process.Start(new ProcessStartInfo
            //        { // FFmpeg requires us to spawn a process and hook into its stdout, so we will create a Process
            //            FileName = "ffmpeg",
            //            Arguments = $"-i {filePath} " + // Here we provide a list of arguments to feed into FFmpeg. -i means the location of the file/URL it will read from
            //            "-f s16le -ar 48000 -ac 2 pipe:1", // Next, we tell it to output 16-bit 48000Hz PCM, over 2 channels, to stdout.
            //            UseShellExecute = false,
            //            RedirectStandardOutput = true // Capture the stdout of the process
            //        });
            //        Thread.Sleep(2000); // Sleep for a few seconds to FFmpeg can start processing data.

            //        int blockSize = 1910; // The size of bytes to read per frame; 1920 for mono
            //        byte[] buffer = new byte[blockSize];
            //        int byteCount;

            //        while (true) // Loop forever, so data will always be read
            //        {
            //            byteCount = process.StandardOutput.BaseStream // Access the underlying MemoryStream from the stdout of FFmpeg
            //                    .Read(buffer, 0, blockSize); // Read stdout into the buffer

            //            if (byteCount == 0) // FFmpeg did not output anything
            //                break; // Break out of the while(true) loop, since there was nothing to read.

            //            _vClient.Send(buffer, 0, byteCount); // Send our data to Discord
            //        }
            //        _vClient.Wait(); // Wait for the Voice Client to finish sending data, as ffMPEG may have already finished buffering out a song, and it is unsafe to return now.
            //    }            
            //};


            _client.ExecuteAndWait(async () => {
                await _client.Connect("MjQ5NzkzMjMyNTM4NDM1NTg1.CxLdsg.QzojNKXFJOnPwez_ByUEkKEg4I8", TokenType.Bot);
            });
        }

        private async Task PlayAudio(string filePath)
        {
            var voiceChannel = _client.FindServers("Jon's Cave of Fun")
            .FirstOrDefault()
            .VoiceChannels.FirstOrDefault(x => x.Id == 282252528392077312);

            var _vClient = await _client.GetService<AudioService>().Join(voiceChannel); // Join the Voice Channel, and return the IAudioClient.

            var channelCount = _client.GetService<AudioService>().Config.Channels;
            var outFormat = new WaveFormat(48000, 16, 2);

            using (var mp3Reader = new Mp3FileReader(filePath))
            {
                using (var resampler = new MediaFoundationResampler(mp3Reader, outFormat))
                {
                    resampler.ResamplerQuality = 60; // Set the quality of the resampler to 60, the highest quality
                    int blockSize = outFormat.AverageBytesPerSecond / 50; // Establish the size of our AudioBuffer
                    byte[] buffer = new byte[blockSize];
                    int byteCount;

                    while ((byteCount = resampler.Read(buffer, 0, blockSize)) > 0)
                    {
                        if (byteCount < blockSize)
                        {
                            for (int i = byteCount; i < blockSize; i++)
                            {
                                buffer[i] = 0;
                            }

                            _vClient.Send(buffer, 0, blockSize);
                        }
                    }
                }
            }
        }
    }
}
