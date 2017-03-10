using System.Collections.Generic;
using System.Configuration;
using BoundyBotNet.helpers;
using BoundyBotNetCore.Application;
using Discord;
using Discord.Audio;
using Microsoft.Azure.WebJobs;

namespace BoundyBotNet
{
    public class Program
    {
        static void Main(string[] args)
        {
            //Configure JobHost
            Settings.Initialize();
            var config = new JobHostConfiguration(Settings.AzureWebJobConfig.ConnectionString);

            var host = new JobHost(config);
            host.Call(typeof(Program).GetMethod("Start"));
            host.RunAndBlock();              
        }

        [NoAutomaticTrigger]
        public static void Start()
        {
            var botHelper = new BotHelpers();
            var commandDictionary = botHelper.BuildSoundFiles();
            var introDictionary = botHelper.BuildSoundIntros();
            var commandList = botHelper.BuildCommandList();      

            var _client = botHelper.Client;

            _client.UsingAudio(x => 
            {
                x.Mode = AudioMode.Outgoing;
            });

            _client.MessageReceived += async (s, e) =>
            {
                var soundFile = FetchSoundFile(e.Message.Text, commandDictionary);

                if (!string.IsNullOrEmpty(soundFile))
                {
                    await botHelper.ProcessAudioAsync("sounds", e.Message.Text, soundFile, e.User.VoiceChannel);
                }

                if (e.Message.Text.Equals("!breadme"))
                {
                    await e.Message.Channel.SendMessage(commandList);               
                }
            };

            //_client.UserUpdated += async (s, e) =>
            //{
            //    var soundFile = FetchSoundFile(e.After.Name, introDictionary);

            //    if (!string.IsNullOrEmpty(soundFile))
            //    {
            //        await botHelper.ProcessAudioAsync("intros", e.After.Name, soundFile, e.After.VoiceChannel);
            //    }
            //};

            _client.ExecuteAndWait(async () => {
                await _client.Connect(Settings.DiscordConfig.AppToken, TokenType.Bot);
            });
        }

        private static string FetchSoundFile(string commandText, Dictionary<string, string> soundDict)
        {
            if (soundDict.ContainsKey(commandText))
            {
                return soundDict[commandText];
            }
            return "";
        }
    }
}
