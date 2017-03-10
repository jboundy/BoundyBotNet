## BoundyBot Documentation ##

### Install Bot in your Discord ###

If you would like to install the bot in your discord server, you can use the link below

https://discordapp.com/oauth2/authorize?client_id=249793232538435585&scope=bot&permissions=0

### How to use the Bot ###

A list of sounds have been pre-loaded. You can find them all by entering the !breadme command.

### Deploy your own Bot ###

The bot is designed as a webjob in Microsoft Azure. To have the app run in Azure, change provide your connection strings to the designated areas in the App.config file in the console app. You can also run the solution on your local machine as well

At this time, the bot can only support wav files. MP3s will be coming someday

### Current issues ###
- MP3s are not supported
- There is lag from Azure and does throw exceptions
- Needs to implement command method rather than thru messages