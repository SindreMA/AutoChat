using Discord;
using Discord.Commands;
using Discord.WebSocket;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using UtilityBot.dto;

namespace UtilityBot.Modules.Janitor
{
    public class Main : ModuleBase<SocketCommandContext>
    {
        [Command("help")]
        [Alias("?")]
        public async Task help()
        {
            var channel = Context.User.CreateDMChannelAsync();
            await channel.Result.SendMessageAsync(
                "**"+ CommandHandler.FirstPrefix + "count **(Returns the amount of people in your voice channel)" + Environment.NewLine +
                "**" + CommandHandler.FirstPrefix + "moveallto [X] ** (Moves all people from your voice channel to channel [X] NB! type channel with no spaces [MODS])" + Environment.NewLine +
                "**" + CommandHandler.FirstPrefix + "voicemsg [on/off]** (Turns off or on the join message[MODS])" + Environment.NewLine +
                "**" + CommandHandler.FirstPrefix + "CreateV** (Creates all the a 'v _' text channel per voice channel[ADMINISTRATOR])" + Environment.NewLine +
                "**" + CommandHandler.FirstPrefix + "DeleteV** (Deletes all the text channels that start with 'v _' [ADMINISTRATOR])" + Environment.NewLine +
                "**" + CommandHandler.FirstPrefix + "VRefresh** (Refreshes all the user permission [Usefull if someone joins when the bot is offline][ADMINISTRATOR])" + Environment.NewLine +
                "**" + CommandHandler.FirstPrefix + "ShowConfig** (Shows you the current config for the v_ channels [if you have a config][ADMINISTRATOR])" + Environment.NewLine +
                "**" + CommandHandler.FirstPrefix + "ChangeConfig [Permission] [True/False]** (Lets you change/create a config for the v_ channels [ADMINISTRATOR])" + Environment.NewLine +
                "**" + CommandHandler.FirstPrefix + "RemoveConfig** (Removes your config and goes back to default)" + Environment.NewLine +
                ///CreateRoom/////
                "**" + CommandHandler.FirstPrefix + "help **(shows this message)");


        }
        [Command("removeconfig")]
        public async Task removeconfig()
        {
            try
            {

                foreach (var item in CommandHandler.SavedPermission)
                {
                    if (item.GuildID == Context.Guild.Id)
                    {
                        CommandHandler.SavedPermission.Remove(item);
                        await Context.Channel.SendMessageAsync("Config have been deleted, you are now using default settings!");
                        File.WriteAllText("SavedPermission.json", JsonConvert.SerializeObject(CommandHandler.SavedPermission));
                    }
                }
            }
            catch (Exception)
            {

            }
          

        }
        [Command("showconfig")]
        public async Task showconfig()
        {
            bool didfind = false;
            foreach (var item in CommandHandler.SavedPermission)
            {
                if (Context.Guild.Id == item.GuildID)
                {
                    didfind = true;
                    string perm = JsonConvert.SerializeObject(item.Permissions);
                    await Context.Channel.SendMessageAsync(
                        "Here are the permissions for the v_ channels(what is being set automatically)" + Environment.NewLine +
                        "To change it do the command " + CommandHandler.FirstPrefix.Replace(Environment.NewLine, "") + "ChangeConfig [Permission] [True/False]" +
                        "" + Environment.NewLine +
                        perm.Replace(",\"", Environment.NewLine).Replace("{\"", "").Replace("}", "").Replace("\":", " = ")
                        );
                }
            }
            if (!didfind)
            {
                await Context.Channel.SendMessageAsync(
                    "You are using the default settings!" + Environment.NewLine +
                    "ReadMessages = true        " + Environment.NewLine +
"readMessageHistory = true  " + Environment.NewLine +
"sendMessages = true        " + Environment.NewLine +
"attachFiles = true         " + Environment.NewLine +
"embedLinks = true          " + Environment.NewLine +
"useExternalEmojis = true   " + Environment.NewLine +
"addReactions = true        " + Environment.NewLine +
"mentionEveryone = inherit     " + Environment.NewLine +
"manageMessages = false     " + Environment.NewLine +
"sendTTSMessages = inherit    " + Environment.NewLine +
"managePermissions = false  " + Environment.NewLine +
"createInstantInvite = false" + Environment.NewLine +
"manageChannel = false" + Environment.NewLine +
"manageWebhooks = false"
                    );
            }
        }
        [Command("ChangeConfig")]
        public async Task ChangeConfig(string perm, bool state)
        {


            PermissionDTO newPermissions = new PermissionDTO();
            newPermissions.ReadMessages = true;
            newPermissions.readMessageHistory = true;
            newPermissions.sendMessages = true;
            newPermissions.attachFiles = true;
            newPermissions.embedLinks = true;
            newPermissions.useExternalEmojis = true;
            newPermissions.addReactions = true;
            newPermissions.mentionEveryone = false;
            newPermissions.manageMessages = false;
            newPermissions.sendTTSMessages = false;
            newPermissions.createInstantInvite = false;
            newPermissions.managePermissions = false;
            newPermissions.manageChannel = false;
            newPermissions.manageWebhooks = false;

            foreach (var item in CommandHandler.SavedPermission)
            {
                if (item.GuildID == Context.Guild.Id)
                {
                    if (item.Permissions.ReadMessages)
                    {
                        newPermissions.ReadMessages = true;
                    }
                    else
                    {
                        newPermissions.ReadMessages = false;
                    }
                    if (item.Permissions.readMessageHistory)
                    {
                        newPermissions.readMessageHistory = true;
                    }
                    else
                    {
                        newPermissions.readMessageHistory = false;
                    }
                    if (item.Permissions.sendMessages)
                    {
                        newPermissions.sendMessages = true;
                    }
                    else
                    {
                        newPermissions.sendMessages = false;
                    }
                    if (item.Permissions.attachFiles)
                    {
                        newPermissions.attachFiles = true;
                    }
                    else
                    {
                        newPermissions.attachFiles = false;
                    }
                    if (item.Permissions.embedLinks)
                    {
                        newPermissions.embedLinks = true;
                    }
                    else
                    {
                        newPermissions.embedLinks = false;
                    }
                    if (item.Permissions.useExternalEmojis)
                    {
                        newPermissions.useExternalEmojis = true;
                    }
                    else
                    {
                        newPermissions.useExternalEmojis = false;
                    }
                    if (item.Permissions.addReactions)
                    {
                        newPermissions.addReactions = true;
                    }
                    else
                    {
                        newPermissions.addReactions = false;
                    }
                    if (item.Permissions.mentionEveryone)
                    {
                        newPermissions.mentionEveryone = true;
                    }
                    else
                    {
                        newPermissions.mentionEveryone = false;
                    }
                    if (item.Permissions.manageMessages)
                    {
                        newPermissions.manageMessages = true;
                    }
                    else
                    {
                        newPermissions.manageMessages = false;
                    }
                    if (item.Permissions.sendTTSMessages)
                    {
                        newPermissions.sendTTSMessages = true;
                    }
                    else
                    {
                        newPermissions.sendTTSMessages = false;
                    }
                    if (item.Permissions.createInstantInvite)
                    {
                        newPermissions.createInstantInvite = true;
                    }
                    else
                    {
                        newPermissions.createInstantInvite = false;
                    }
                    if (item.Permissions.managePermissions)
                    {
                        newPermissions.managePermissions = true;
                    }
                    else
                    {
                        newPermissions.managePermissions = false;
                    }
                    if (item.Permissions.manageChannel)
                    {
                        newPermissions.manageChannel = true;
                    }
                    else
                    {
                        newPermissions.manageChannel = false;
                    }
                    if (item.Permissions.manageWebhooks)
                    {
                        newPermissions.manageWebhooks = true;
                    }
                    else
                    {
                        newPermissions.manageWebhooks = false;
                    }
                }
            }



            if (perm.ToLower() == "readMessages".ToLower() && state)
            {
                newPermissions.ReadMessages = true;
            }
            else if (perm.ToLower() == "readMessages".ToLower() && !state)
            {
                newPermissions.ReadMessages = false;
            }
            ////////////////////NEW  SETTING/////////////////////
            else if (perm.ToLower() == "readMessageHistory".ToLower() && state)
            {
                newPermissions.readMessageHistory = true;
            }
            else if (perm.ToLower() == "readMessageHistory".ToLower() && !state)
            {
                newPermissions.readMessageHistory = false;
            }
            ////////////////////NEW  SETTING/////////////////////
            else if (perm.ToLower() == "sendMessages".ToLower() && state)
            {
                newPermissions.sendMessages = true;
            }
            else if (perm.ToLower() == "sendMessages".ToLower() && !state)
            {
                newPermissions.sendMessages = false;
            }
            ////////////////////NEW  SETTING/////////////////////
            else if (perm.ToLower() == "attachFiles".ToLower() && state)
            {
                newPermissions.attachFiles = true;
            }
            else if (perm.ToLower() == "attachFiles".ToLower() && !state)
            {
                newPermissions.attachFiles = false;
            }
            ////////////////////NEW  SETTING/////////////////////
            else if (perm.ToLower() == "embedLinks".ToLower() && state)
            {
                newPermissions.embedLinks = true;
            }
            else if (perm.ToLower() == "embedLinks".ToLower() && !state)
            {
                newPermissions.embedLinks = false;
            }
            ////////////////////NEW  SETTING/////////////////////
            else if (perm.ToLower() == "useExternalEmojis".ToLower() && state)
            {
                newPermissions.useExternalEmojis = true;
            }
            else if (perm.ToLower() == "useExternalEmojis".ToLower() && !state)
            {
                newPermissions.useExternalEmojis = false;
            }
            ////////////////////NEW  SETTING/////////////////////
            else if (perm.ToLower() == "addReactions".ToLower() && state)
            {
                newPermissions.addReactions = true;
            }
            else if (perm.ToLower() == "addReactions".ToLower() && !state)
            {
                newPermissions.addReactions = false;
            }
            ////////////////////NEW  SETTING/////////////////////
            else if (perm.ToLower() == "mentionEveryone".ToLower() && state)
            {
                newPermissions.mentionEveryone = true;
            }
            else if (perm.ToLower() == "mentionEveryone".ToLower() && !state)
            {
                newPermissions.mentionEveryone = false;
            }
            ////////////////////NEW  SETTING/////////////////////
            else if (perm.ToLower() == "manageMessages".ToLower() && state)
            {
                newPermissions.manageMessages = true;
            }
            else if (perm.ToLower() == "manageMessages".ToLower() && !state)
            {
                newPermissions.manageMessages = false;
            }
            ////////////////////NEW  SETTING/////////////////////
            else if (perm.ToLower() == "sendTTSMessages".ToLower() && state)
            {
                newPermissions.sendTTSMessages = true;
            }
            else if (perm.ToLower() == "sendTTSMessages".ToLower() && !state)
            {
                newPermissions.sendTTSMessages = false;
            }
            ////////////////////NEW  SETTING/////////////////////
            else if (perm.ToLower() == "createInstantInvite".ToLower() && state)
            {
                newPermissions.createInstantInvite = true;
            }
            else if (perm.ToLower() == "createInstantInvite".ToLower() && !state)
            {
                newPermissions.createInstantInvite = false;
            }
            ////////////////////NEW  SETTING/////////////////////
            else if (perm.ToLower() == "managePermissions".ToLower() && state)
            {
                newPermissions.managePermissions = true;
            }
            else if (perm.ToLower() == "managePermissions".ToLower() && !state)
            {
                newPermissions.managePermissions = false;
            }
            ////////////////////NEW  SETTING/////////////////////
            else if (perm.ToLower() == "manageChannel".ToLower() && state)
            {
                newPermissions.manageChannel = true;
            }
            else if (perm.ToLower() == "manageChannel".ToLower() && !state)
            {
                newPermissions.manageChannel = false;
            }
            ////////////////////NEW  SETTING/////////////////////
            else if (perm.ToLower() == "manageWebhooks".ToLower() && state)
            {
                newPermissions.manageWebhooks = true;
            }
            else if (perm.ToLower() == "manageWebhooks".ToLower() && !state)
            {
                newPermissions.manageWebhooks = false;
            }
            foreach (var item in CommandHandler.SavedPermission)
            {
                if (item.GuildID == Context.Guild.Id)
                {
                    CommandHandler.SavedPermission.Remove(item);
                }
            }
            PermissionAndGuildDTO permandguild = new PermissionAndGuildDTO();
            permandguild.GuildID = Context.Guild.Id;
            permandguild.Permissions = newPermissions;
            CommandHandler.SavedPermission.Add(permandguild);
            File.WriteAllText("SavedPermission.json", JsonConvert.SerializeObject(CommandHandler.SavedPermission));
            await Context.Channel.SendMessageAsync("Changes have been saved!");



        }
        [Command("DeleteV")]
        [RequireUserPermission(GuildPermission.Administrator)]
        public async Task DeleteV()
        {
            var d = Context.Guild.TextChannels;
            foreach (var TxtChannel in d)
            {
                if (TxtChannel.Name.StartsWith("v_"))
                {
                    await TxtChannel.DeleteAsync();
                    Console.WriteLine("Deleting " + TxtChannel.Name);
                }
                else
                {
                    Console.WriteLine("skipping " + TxtChannel.Name);
                }
            }
        }//rebuild v_
        [Command("CreateV")]
        [RequireUserPermission(GuildPermission.Administrator)]
        public async Task CreateV()
        {

            var d = Context.Guild.VoiceChannels;
            foreach (var VoiceChannel in d)
            {
                var s = await Context.Guild.CreateTextChannelAsync("v_" + VoiceChannel.Name.ToLower().Replace(" ", "_").Replace(":", "").Replace(@"/", "_").Replace(".", "_"));

                await s.AddPermissionOverwriteAsync(Context.Guild.EveryoneRole, Discord.OverwritePermissions.DenyAll(s), Discord.RequestOptions.Default);

            }
        }//rebuild v_
        [Command("VoiceMsg")]
        [RequireUserPermission(GuildPermission.BanMembers)]
        public async Task Voicemsg()
        {
            bool VoiceISON = false;
            if (CommandHandler.VoiceON != null)
            {
                foreach (var item in CommandHandler.VoiceON)
                {
                    if (item == Context.Guild.Id.ToString())
                    {
                        VoiceISON = true;
                    }
                }
                if (VoiceISON)
                {
                    CommandHandler.VoiceON.Remove((Context.Guild.Id.ToString()));
                    await Context.Channel.SendMessageAsync("Join/leave message is now off");
                    File.WriteAllText("VoiceMsgState.json", JsonConvert.SerializeObject(CommandHandler.VoiceON));
                }
                else if (!VoiceISON)
                {
                    CommandHandler.VoiceON.Add((Context.Guild.Id.ToString()));
                    await Context.Channel.SendMessageAsync("Join/leave message is now on");
                    File.WriteAllText("VoiceMsgState.json", JsonConvert.SerializeObject(CommandHandler.VoiceON));
                }
            }
            else
            {
                CommandHandler.VoiceON.Add((Context.Guild.Id.ToString()));
                await Context.Channel.SendMessageAsync("Join/leave message is now on");
                File.WriteAllText("VoiceMsgState.json", JsonConvert.SerializeObject(CommandHandler.VoiceON));
            }

        }
        [Command("vrefresh")]
        public async Task vrefresh()
        {
            await Context.Channel.TriggerTypingAsync();
            var bgw = new BackgroundWorker();
            bgw.DoWork += (_, __) =>
            {
                refresh(Context);
            };
            bgw.RunWorkerCompleted += (_, __) =>
            {

            };
            bgw.RunWorkerAsync();
        }
        [Command("moveallto")]
        [RequireUserPermission(GuildPermission.MoveMembers)]
        public async Task moveallto(string channel)
        {
            var h = Context.Guild.GetUser(Context.User.Id);
            var d = Context.Guild.VoiceChannels;
            var users = await (Context.Guild as IGuild).GetUsersAsync();

            bool exist = false;

            IVoiceChannel j = null;

            foreach (var item in d)
            {
                if (channel.ToLower().Replace(" ", "") == item.Name.ToLower().Replace(" ", ""))
                {
                    exist = true;
                    j = item;
                }
            }
            if (!exist)
            {
                await Context.Channel.SendMessageAsync("Cant find channel!");
            }
            else
            {

                foreach (var item in d)
                {
                    if (item.Id == h.VoiceChannel.Id)
                    {
                        foreach (var user in users)
                        {
                            if (user.VoiceChannel != null)
                            {


                                if (user.VoiceChannel.Id == item.Id)
                                {
                                    await (user as IGuildUser)?.ModifyAsync(x =>
                                    {
                                        x.Channel = Optional.Create(j);
                                    });
                                }
                            }
                        }
                    }

                }




            }
        }
        [Command("count")]
        public async Task Count()
        {
            var h = Context.Guild.GetUser(Context.User.Id);
            var users = (h.VoiceChannel as SocketVoiceChannel).Users;
            int i = users.Count;

            await Context.Channel.SendMessageAsync("There are " + i + " people in " + h.VoiceChannel.Name);
        }
        [Command("info")]
        public async Task Info()
        {
            var application = await Context.Client.GetApplicationInfoAsync();
            await ReplyAsync(
                $"{Format.Bold("Info")}\n" +
                $"- Author: SindreMA#9630\n" +
                $"- Library: Discord.Net ({DiscordConfig.Version})\n" +
                $"- Runtime: {RuntimeInformation.FrameworkDescription} {RuntimeInformation.OSArchitecture}\n" +
                $"- Uptime: {GetUptime()}\n\n" +

                $"{Format.Bold("Stats")}\n" +
                $"- Heap Size: {GetHeapSize()} MB\n" +
                $"- Guilds: {(Context.Client as DiscordSocketClient).Guilds.Count}\n" +
                $"- Channels: {(Context.Client as DiscordSocketClient).Guilds.Sum(g => g.Channels.Count)}\n" +
                $"- Users: {(Context.Client as DiscordSocketClient).Guilds.Sum(g => g.Users.Count)}"
            );
        }
        [Command("setgame")]
        [RequireUserPermission(GuildPermission.Administrator)]
        public async Task SetGame([Remainder]string text)
        {
            if (Context.User.Id == 170605899189190656)
            {
                await Context.Client.SetGameAsync(text);
            }
          
        }
        private static string GetUptime()
           => (DateTime.Now - Process.GetCurrentProcess().StartTime).ToString(@"dd\.hh\:mm\:ss");
        private static string GetHeapSize() => Math.Round(GC.GetTotalMemory(true) / (1024.0 * 1024.0), 2).ToString();
        public async void refresh(SocketCommandContext e)
        {


            var AllowPerm = new OverwritePermissions(
              readMessages: PermValue.Allow,
              readMessageHistory: PermValue.Allow,
              sendMessages: PermValue.Allow,
              attachFiles: PermValue.Allow,
              embedLinks: PermValue.Allow,
              useExternalEmojis: PermValue.Allow,
              addReactions: PermValue.Inherit,
              mentionEveryone: PermValue.Deny,
              manageMessages: PermValue.Deny,
              sendTTSMessages: PermValue.Deny,
              managePermissions: PermValue.Deny,
              createInstantInvite: PermValue.Deny,
              manageChannel: PermValue.Deny,
              manageWebhooks: PermValue.Deny
              );
            var users = await (e.Guild as IGuild).GetUsersAsync();
            var textChannels = await (e.Guild as IGuild).GetTextChannelsAsync();


            foreach (var user in users)
            {
                Console.WriteLine("Starting " + user.Username);
                if (user.VoiceChannel != null)
                {
                    Console.WriteLine("User is in voice");

                    foreach (var channel in textChannels)

                    {

                        if (channel.Name.StartsWith("v_"))
                        {

                            Console.WriteLine("checking " + channel.Name);
                            try
                            {

                                if ("v_" + user.VoiceChannel.Name.ToLower().Replace(" ", "_").Replace(":", "") == channel.Name)
                                {
                                    await channel.AddPermissionOverwriteAsync(user, AllowPerm);
                                }
                                else
                                {
                                    await channel.RemovePermissionOverwriteAsync(user);
                                }

                            }
                            catch (Exception)
                            {
                                Console.WriteLine("fail check");
                            }
                        }
                    }
                }
                else if (user.VoiceChannel == null)
                {
                    Console.WriteLine("User is not in voice");
                    foreach (var channel in textChannels)
                    {
                        if (channel.Name.StartsWith("v_"))
                        {
                            Console.WriteLine("checking " + channel.Name);
                            try
                            {
                                await channel.RemovePermissionOverwriteAsync(user);
                            }
                            catch (Exception)
                            {
                            }

                        }
                    }

                }
            }

            await e.Channel.SendMessageAsync("Permission to V_Channels have been refreshed!");


        }
    }
}
