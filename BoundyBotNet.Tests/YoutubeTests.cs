using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;
using System.Threading.Tasks;
using YoutubeExplode;
using YoutubeExplode.Videos;

namespace BoundyBotNet.Tests
{
    [TestClass]
    public class YoutubeTests
    {
        [TestMethod]
        public async Task GetAudioStreamDataAsync()
        {
            var youtube = new YoutubeClient();
            var url = "https://www.youtube.com/watch?v=vyuHCcI4Q-g";
            // Read the video ID
            var videoId = new VideoId(url);

            // Get media streams & choose the best muxed stream
            var streams = await youtube.Videos.Streams.GetManifestAsync(videoId);
            var streamInfo = streams.GetAudioOnly();
            var mp4Stream = streamInfo.SingleOrDefault(x => x.Container.Name == "mp4");
            var stream = await youtube.Videos.Streams.GetAsync(mp4Stream);
            Assert.IsNotNull(stream);
        }
    }
}
