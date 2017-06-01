# Autochat
A simple bot fot having voice textchannels

### Compiling
This bot requires:
- .NET Core SDK (CoreCLR) 1.1
- .NET Core Tooling 1.0.0

### Contributing
To develop, you need:
- Visual Studio 2017

----------------------------------------------------------------------------------------------------
To setup the bot you must have atleast the "Manage Server" permission on the server.

The first thing you need to do is to add the bot by going on this link:
https://discordapp.com/oauth2/authorize?&client_id=319475718431965184&scope=bot+&permissions=8

On the website you need to select the server you want the bot to join and click "Authorize".

You should now see that the bot have joined your server!
To get the v_ channels in you need to use the command .CreateV

You should now see as an administrator all the channels showing up on your right.
That should be all needed to do.

I do recommend checking with a none administrator user so you can see how its looking for the enduser.

You can do .help if you want to see all the commands for the bot.

(PS! If you just want one text channel for all voice channels, then just create a text channel named "voiceonly" and deny every read permission)

As a administrator you might wana put mute on the v Channels

----------------------------------------------------------------------------------------------------

Here are the commands:

  .count (Returns the amount of people in your voice channel)
  
  .moveallto [X]  (Moves all people from your voice channel to channel [X] NB! type channel with no spaces [MODS])
  
  .voicemsg [on/off] (Turns off or on the join message[MODS])
  
  .CreateV (Creates all the a 'v ' text channel per voice channel[ADMINISTRATOR])
  
  .DeleteV (Deletes all the text channels that start with 'v ' [ADMINISTRATOR])
  
  .VRefresh (Refreshes all the user permission [Usefull if someone joins when the bot is offline][ADMINISTRATOR])
  
  .ShowConfig (Shows you the current config for the v channels [if you have a config][ADMINISTRATOR])
  
  .ChangeConfig [Permission] [True/False] (Lets you change/create a config for the v channels [ADMINISTRATOR])
  
  .RemoveConfig (Removes your config and goes back to default)
  
  .help (shows this message)



# If you need help with the bot, feel free to contact me on discord https://discord.gg/2WFdNyq
