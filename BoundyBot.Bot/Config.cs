using System.IO;

namespace BoundyBot.Bot
{
    public static class Config
    {
        public static string Token => File.ReadAllText("Environment.env").Split('=')[1];
    }
}
