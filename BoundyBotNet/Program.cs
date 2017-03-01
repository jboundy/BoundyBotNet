using System;
using System.IO;
using System.Text;
using BoundyBotNet.helpers;
using Discord;
using Discord.Audio;

namespace BoundyBotNet
{
    class Program
    {
        static void Main(string[] args)
        {
            new Program().Start();        
        } 

        public void Start()
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

                if (e.Message.Text.Equals("!breadme"))
                {
                    await e.Message.Channel.SendMessage(commandList);               
                }

                if (e.Message.Text.Equals("!bhorn"))
                {            
                    await botHelper.PlayAudioAsync($@"{dir.FullName}\airhorn_default.wav");
                }

                if (e.Message.Text.Equals("!bfuck"))
                {
                    await botHelper.PlayAudioAsync($@"{dir.FullName}\fuckoff.wav");
                }

                if (e.Message.Text.Equals("!bhugeb"))
                {
                    await botHelper.PlayAudioAsync($@"{dir.FullName}\huge-bitch.wav");
                }

                if (e.Message.Text.Equals("!bff7"))
                {
                    await botHelper.PlayAudioAsync($@"{dir.FullName}\victory_fanfare.wav");
                }

                if (e.Message.Text.Equals("!bcombo"))
                {
                    await botHelper.PlayAudioAsync($@"{dir.FullName}\combobreaker.wav");
                }

                if (e.Message.Text.Equals("!bprincess"))
                {
                    await botHelper.PlayAudioAsync($@"{dir.FullName}\Excuse_Me_Princess.wav");
                }

                if (e.Message.Text.Equals("!bhotstep"))
                {
                    await botHelper.PlayAudioAsync($@"{dir.FullName}\hotstepper.wav");
                }

                if (e.Message.Text.Equals("!bchest"))
                {                   
                   await botHelper.PlayAudioAsync($@"{dir.FullName}\zeldaitem.wav");               
                }

                if (e.Message.Text.Equals("!bburn"))
                {
                    await botHelper.PlayAudioAsync($@"{dir.FullName}\burned.wav");
                }

                if (e.Message.Text.Equals("!bdrama"))
                {
                    await botHelper.PlayAudioAsync($@"{dir.FullName}\drama.wav");
                }

                if (e.Message.Text.Equals("!bfatal"))
                {
                    await botHelper.PlayAudioAsync($@"{dir.FullName}\fatality.wav");
                }

                if (e.Message.Text.Equals("!bfdp"))
                {
                    await botHelper.PlayAudioAsync($@"{dir.FullName}\fdp.wav");
                }

                if (e.Message.Text.Equals("!bmgs"))
                {
                    await botHelper.PlayAudioAsync($@"{dir.FullName}\metalgearsolid.wav");
                }

                if (e.Message.Text.Equals("!bsparta"))
                {
                    await botHelper.PlayAudioAsync($@"{dir.FullName}\thisissparta.wav");
                }

                if (e.Message.Text.Equals("!bwolo"))
                {
                    await botHelper.PlayAudioAsync($@"{dir.FullName}\wololo.wav");
                }

                if (e.Message.Text.Equals("!bsecret"))
                {
                    await botHelper.PlayAudioAsync($@"{dir.FullName}\zeldasecret.wav");
                }

                if (e.Message.Text.Equals("!bpardon"))
                {
                    await botHelper.PlayAudioAsync($@"{dir.FullName}\beg-your-pardon-sir.wav");
                }

                if (e.Message.Text.Equals("!bwhip"))
                {
                    await botHelper.PlayAudioAsync($@"{dir.FullName}\crack_the_whip.wav");
                }

                if (e.Message.Text.Equals("!bnuts"))
                {
                    await botHelper.PlayAudioAsync($@"{dir.FullName}\deez-nuts.wav");
                }

                if (e.Message.Text.Equals("!bnope"))
                {
                    await botHelper.PlayAudioAsync($@"{dir.FullName}\nope.wav");
                }

                if (e.Message.Text.Equals("!bomg"))
                {
                    await botHelper.PlayAudioAsync($@"{dir.FullName}\omg-who-the-hell-cares_2.wav");
                }

                if (e.Message.Text.Equals("!bobjection"))
                {
                    await botHelper.PlayAudioAsync($@"{dir.FullName}\phoenix-objection.wav");
                }

                if (e.Message.Text.Equals("!bstfu"))
                {
                    await botHelper.PlayAudioAsync($@"{dir.FullName}\shut-the-fuck-up.wav");
                }

                if (e.Message.Text.Equals("!bsurprise"))
                {
                    await botHelper.PlayAudioAsync($@"{dir.FullName}\surprise-motherfucker.wav");
                }

                if (e.Message.Text.Equals("!btada"))
                {
                    await botHelper.PlayAudioAsync($@"{dir.FullName}\tada.wav");
                }

                if (e.Message.Text.Equals("!bholyfuck"))
                {
                    await botHelper.PlayAudioAsync($@"{dir.FullName}\yang-holy-fuck.wav");
                }
            };

           _client.ExecuteAndWait(async () => {
                await _client.Connect("MjQ5NzkzMjMyNTM4NDM1NTg1.CxLdsg.QzojNKXFJOnPwez_ByUEkKEg4I8", TokenType.Bot);
            });
        }
    }
}
