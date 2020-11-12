using DSharpPlus.CommandsNext.Attributes;
using DSharpPlus.VoiceNext;
using FFMpegCore;
using FFMpegCore.Helpers;
using FFMpegCore.Pipes;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoundyBotNet.Services
{
    public class AudioService
    {
        public AudioService()
        {

        }

        public async Task PlayAudio(string filePath, string outputPath)
        {
            FFMpeg.ExtractAudio(filePath, outputPath);
            var analysis = FFProbe.Analyse(outputPath);
 
        }

        public async Task PlayAudio(VoiceNextConnection vnc, [RemainingText] string file)
        {
            var psi = new ProcessStartInfo
            {
                FileName = "ffmpeg",
                Arguments = $@"-i ""{file}"" -ac 2 -f s16le -ar 48000 pipe:1",
                RedirectStandardOutput = true,
                UseShellExecute = false
            };
            var ffmpeg = Process.Start(psi);
            var ffout = ffmpeg.StandardOutput.BaseStream;

            var buff = new byte[3840];
            var br = 0;
            while ((br = ffout.Read(buff, 0, buff.Length)) > 0)
            {
                if (br < buff.Length)
                    for (var i = br; i < buff.Length; i++)
                        buff[i] = 0;

                await vnc.SendAsync(buff, 20);
            }

            await vnc.SendSpeakingAsync(false); 
        }
    }
}
