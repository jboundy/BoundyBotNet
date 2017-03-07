using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BoundyBotNet.helpers;
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
            var storageConnectionString = ConfigurationManager.ConnectionStrings["AzureWebJobsStorage"].ToString();
            var config = new JobHostConfiguration(storageConnectionString);

            var host = new JobHost(config);
            host.Call(typeof(Program).GetMethod("Start"));
            host.RunAndBlock();              
        }

        [NoAutomaticTrigger]
        public static void Start()
        {
            var botHelper = new BotHelpers();
            var commandDictionary = botHelper.BuildSoundFiles();
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
                    await botHelper.ProcessAudioAsync(e.Message.Text, soundFile, e.User.VoiceChannel);
                }

                if (e.Message.Text.Equals("!breadme"))
                {
                    await e.Message.Channel.SendMessage(commandList);               
                }
            };

            //_client.UserUpdated += async (s, e) =>
            //{
            //    //this one needs some work...essentially tracking where users are and what channel they are a part of
                  //may consider setting that up as the app starts
            //    var soundFile = GetIntro(e.Server);

            //    if (!string.IsNullOrEmpty(soundFile))
            //    {
            //        await ProcessAudioAsync(soundFile, botHelper, e.Server.CurrentUser.VoiceChannel);
            //    }
            //};

           _client.ExecuteAndWait(async () => {
                await _client.Connect("MjQ5NzkzMjMyNTM4NDM1NTg1.CxLdsg.QzojNKXFJOnPwez_ByUEkKEg4I8", TokenType.Bot);
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

        //private static string FetchIntroSound(string userId)
        //{
        //    switch (userId)
        //    {

        //    }
        //}
    }
}
