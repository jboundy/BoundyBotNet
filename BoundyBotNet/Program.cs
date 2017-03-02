using System;
using System.Configuration;
using System.IO;
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
            var builder = new StringBuilder();
            foreach (var item in botHelper.CommandList())
            {
                builder.Append(item).Append(Environment.NewLine);
            }
            var commandList = builder.ToString();
            var _client = botHelper.Client;

            _client.UsingAudio(x => 
            {
                x.Mode = AudioMode.Outgoing;
            });

            _client.MessageReceived += async (s, e) =>
            {
                DirectoryInfo dir = new DirectoryInfo("sounds");

                var soundFile = FetchSoundFile(e.Message.Text);
                if (!string.IsNullOrEmpty(soundFile))
                {
                    await botHelper.PlayAudioAsync($@"{dir.FullName}\{soundFile}");
                }

                if (e.Message.Text.Equals("!breadme"))
                {
                    await e.Message.Channel.SendMessage(commandList);               
                }
            };

           _client.ExecuteAndWait(async () => {
                await _client.Connect("MjQ5NzkzMjMyNTM4NDM1NTg1.CxLdsg.QzojNKXFJOnPwez_ByUEkKEg4I8", TokenType.Bot);
            });

        }

        private static string FetchSoundFile(string commandText)
        {
            switch (commandText)
            {
                case "!bhorn":
                    return "airhorn_default.wav";
                case "!bfuck":
                    return "fuckoff.wav";
                case "!bhugeb":
                    return "huge-bitch.wav";
                case "!bff7":
                    return "victory_fanfare.wav";
                case "!bcombo":
                    return "combobreaker.wav";
                case "!bprincess":
                    return "Excuse_Me_Princess.wav";
                case "!bhotstep":
                    return "hotstepper.wav";
                case "!bchest":
                    return "zeldaitem.wav";
                case "!bburn":
                    return "burned.wav";
                case "!bdrama":
                    return "drama.wav";
                case "!bfatal":
                    return "fatality.wav";
                case "!bfdp":
                    return "fdp.wav";
                case "!bmgs":
                    return "metalgearsolid.wav";
                case "!bsparta":
                    return "thisissparta.wav";
                case "!bwolo":
                    return "wololo.wav";
                case "!bsecret":
                    return "zeldasecret.wav";
                case "!bpardon":
                    return "beg-your-pardon-sir.wav";
                case "!bwhip":
                    return "crack_the_whip.wav";
                case "!bnuts":
                    return "deez-nuts.wav";
                case "!bnope":
                    return "nope.wav";
                case "!bomg":
                    return "omg-who-the-hell-cares_2.wav";
                case "!bobjection":
                    return "phoenix-objection.wav";
                case "!bstfu":
                    return "shut-the-fuck-up.wav";
                case "!bsurprise":
                    return "surprise-motherfucker.wav";
                case "!btada":
                    return "tada.wav";
                case "!bholyfuck":
                    return "yang-holy-fuck.wav";
                case "!billidan":
                    return "illidan-10000.wav";
                case "!bgay":
                    return "hah-gay.wav";
                case "!bwooo":
                    return "woooooooooooo.wav";
                default:;
                    return "";
            }
        }
    }
}
