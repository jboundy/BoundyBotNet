using DSharpPlus.CommandsNext.Attributes;
using DSharpPlus.VoiceNext;
using FFMpegCore;
using FFMpegCore.Enums;
using FFMpegCore.Pipes;
using System;
using System.IO;
using System.Threading.Tasks;

namespace BoundyBotNet.Services
{
    public class AudioService
    {
        public AudioService()
        {

        }

        private async Task<Stream> GenerateSoundStream(Stream inputStream)
        {
            MemoryStream returnStream = null;
            using(Stream outputStream = new MemoryStream())
            {
                await FFMpegArguments
                    .FromPipeInput(new StreamPipeSource(inputStream))
                    .OutputToPipe(new StreamPipeSink(outputStream), options => options
                        .WithAudioCodec(AudioCodec.LibMp3Lame)
                        .ForceFormat("mp3"))
                    .ProcessAsynchronously();

                await outputStream.CopyToAsync(returnStream);
            }
            return returnStream;
        }

        public async Task PlayAudio(VoiceNextConnection vnc, [RemainingText] string file)
        {
            using (var readingStream = File.OpenRead(file))
            {
                var ffout = await GenerateSoundStream(readingStream);

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

        [Obsolete("Code not used and no place to save a file in current implementation", true)]
        public async Task<bool> GenerateSoundFile(string inputPath, string outputPath)
        {
            return await FFMpegArguments
                .FromFileInput(inputPath)
                .OutputToFile(outputPath, true, options => options
                    .WithAudioCodec(AudioCodec.Aac)
                    .WithVariableBitrate(4)
                    .WithFastStart())
                .ProcessAsynchronously();
        }
    }
}
