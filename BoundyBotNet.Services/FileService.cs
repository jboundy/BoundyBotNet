using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YoutubeExplode;
using YoutubeExplode.Videos;

namespace BoundyBotNet.Services
{
    public class FileService
    {

        public Stream GetHttpData()
        {

        }

        public Stream GetFileData()
        {

        }

        public async Task<Stream> GetYoutubeData(string url)
        {
            var youtube = new YoutubeClient();
            var memoryStream = new MemoryStream();
            var videoId = new VideoId(url);
            var streams = await youtube.Videos.Streams.GetManifestAsync(videoId);
            var streamInfo = streams.GetAudioOnly();
            var mp4Stream = streamInfo.SingleOrDefault(x => x.Container.Name == "mp4");
            return await youtube.Videos.Streams.GetAsync(mp4Stream);
        }
    }
}
