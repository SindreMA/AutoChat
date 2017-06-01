using Discord;
using Discord.Addons.EmojiTools;
using Discord.Commands;
using Discord.WebSocket;
using Microsoft.Extensions.DependencyInjection;
using Serilog;
using Serilog.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using UtilityBot.Services.Configuration;
using UtilityBot.Services.Logging;
using Newtonsoft.Json;
using System.IO;
using UtilityBot.dto;

namespace UtilityBot
{
    public class CommandHandler
    {
        
        private readonly IServiceProvider _provider;
        private readonly CommandService _commands;
        private readonly DiscordSocketClient _client;
        private readonly Config _config;
        private readonly ILogger _logger;
        public static List<string> VoiceON = new List<string>();
        public static List<PermissionAndGuildDTO> SavedPermission = new List<PermissionAndGuildDTO>();
        private IEnumerable<ulong> Whitelist => _config.ChannelWhitelist;
        public static string FirstPrefix = "";
        public CommandHandler(IServiceProvider provider)
        {
            _provider = provider;
            _client = _provider.GetService<DiscordSocketClient>();
            _client.MessageReceived += ProcessCommandAsync;
            _client.UserVoiceStateUpdated += _client_UserVoiceStateUpdated;
            _commands = _provider.GetService<CommandService>();
            var log = _provider.GetService<LogAdaptor>();
            _commands.Log += log.LogCommand;
            _config = _provider.GetService<Config>();
            _logger = _provider.GetService<Logger>().ForContext<CommandService>();
            string firstPrefix;
            try
            {
                dynamic json = JsonConvert.DeserializeObject(File.ReadAllText("config.json"));
                var prefixes = json.command_activation_strings;
                string stringprefixes = Convert.ToString(prefixes);
                stringprefixes = stringprefixes.Replace("[", "").Replace("]", "").Replace("\"", "");
                var prefixesSplited = stringprefixes.Split(',');
                firstPrefix = prefixesSplited[0];
                FirstPrefix = firstPrefix.Replace(Environment.NewLine, "");
            }
            catch (Exception)
            {
                firstPrefix = "";
                
            }
            try
            {

                string json = File.ReadAllText("VoiceMsgState.json");
                VoiceON = JsonConvert.DeserializeObject<List<string>>(json);
            }
            catch (Exception)
            {
            }
            try
            {
                string json = File.ReadAllText("SavedPermission.json");
                SavedPermission = JsonConvert.DeserializeObject<List<PermissionAndGuildDTO>>(json);
                
            }
            catch (Exception)
            {

                
            }

            //var jsonVoiceON = JsonConvert.DeserializeObject(File.ReadAllText("VoiceMsgState.json"));


            _client.SetGameAsync(firstPrefix + "? for commands");
        }

        private Task _client_UserVoiceStateUpdated(SocketUser arg1, SocketVoiceState arg2, SocketVoiceState arg3)
        {
            bool doesexist = false;
            
            SocketVoiceState State3 = arg3;

            OverwritePermissions AllowPerm = new OverwritePermissions();
        
            
            foreach (var item in SavedPermission)
            {
                ulong GuildID = 0;
                if (arg2.VoiceChannel != null)
                {
                    GuildID = arg2.VoiceChannel.Guild.Id;
                }
                else if (arg3.VoiceChannel != null)
                {
                    GuildID = arg3.VoiceChannel.Guild.Id;
                }

                
                if (item.GuildID == GuildID )
                {
                    doesexist = true;
                    OverwritePermissions Settings = new OverwritePermissions();
                    PermValue readMessages;
                    PermValue readMessageHistory;
                    PermValue sendMessages;
                    PermValue attachFiles;
                    PermValue embedLinks;
                    PermValue useExternalEmojis;
                    PermValue addReactions;
                    PermValue mentionEveryone;
                    PermValue manageMessages;
                    PermValue sendTTSMessages;
                    PermValue createInstantInvite;
                    PermValue managePermissions;
                    PermValue manageChannel;
                    PermValue manageWebhooks;
                    if (item.Permissions.ReadMessages)
                    {
                        readMessages = PermValue.Allow;
                    }
                    else
                    {
                        readMessages = PermValue.Deny;
                    }
                    if (item.Permissions.readMessageHistory)
                    {
                        readMessageHistory = PermValue.Allow;
                    }
                    else
                    {
                        readMessageHistory = PermValue.Deny;
                    }
                    if (item.Permissions.sendMessages)
                    {
                        sendMessages = PermValue.Allow;
                    }
                    else
                    {
                        sendMessages = PermValue.Deny;
                    }
                    if (item.Permissions.attachFiles)
                    {
                        attachFiles = PermValue.Allow;
                    }
                    else
                    {
                        attachFiles = PermValue.Deny;
                    }
                    if (item.Permissions.embedLinks)
                    {
                        embedLinks = PermValue.Allow;
                    }
                    else
                    {
                        embedLinks = PermValue.Deny;
                    }
                    if (item.Permissions.useExternalEmojis)
                    {
                        useExternalEmojis = PermValue.Allow;
                    }
                    else
                    {
                        useExternalEmojis = PermValue.Deny;
                    }
                    if (item.Permissions.addReactions)
                    {
                        addReactions = PermValue.Allow;
                    }
                    else
                    {
                        addReactions = PermValue.Deny;
                    }
                    if (item.Permissions.mentionEveryone)
                    {
                        mentionEveryone = PermValue.Allow;
                    }
                    else
                    {
                        mentionEveryone = PermValue.Deny;
                    }
                    if (item.Permissions.manageMessages)
                    {
                        manageMessages = PermValue.Allow;
                    }
                    else
                    {
                        manageMessages = PermValue.Deny;
                    }
                    if (item.Permissions.sendTTSMessages)
                    {
                        sendTTSMessages = PermValue.Allow;
                    }
                    else
                    {
                        sendTTSMessages = PermValue.Deny;
                    }
                    if (item.Permissions.createInstantInvite)
                    {
                        createInstantInvite = PermValue.Allow;
                    }
                    else
                    {
                        createInstantInvite = PermValue.Deny;
                    }
                    if (item.Permissions.managePermissions)
                    {
                        managePermissions = PermValue.Allow;
                    }
                    else
                    {
                        managePermissions = PermValue.Deny;
                    }
                    if (item.Permissions.manageChannel)
                    {
                        manageChannel = PermValue.Allow;
                    }
                    else
                    {
                        manageChannel = PermValue.Deny;
                    }
                    if (item.Permissions.manageWebhooks)
                    {
                        manageWebhooks = PermValue.Allow;
                    }
                    else
                    {
                        manageWebhooks = PermValue.Deny;
                    }

                    AllowPerm = new OverwritePermissions(
                            readMessages: readMessages,
                            readMessageHistory: readMessageHistory,
                            sendMessages: sendMessages,
                            attachFiles: attachFiles,
                            embedLinks: embedLinks,
                            useExternalEmojis: useExternalEmojis,
                            addReactions: addReactions,
                            mentionEveryone: mentionEveryone,
                            manageMessages: manageMessages,
                            sendTTSMessages: sendTTSMessages,
                            managePermissions: managePermissions,
                            createInstantInvite: createInstantInvite,
                            manageChannel: manageChannel,
                            manageWebhooks: manageWebhooks
                            );
                }
            }
            if (doesexist)
            {

            }
            else
            {
                AllowPerm = new OverwritePermissions(
                             readMessages: PermValue.Allow,
                             readMessageHistory: PermValue.Allow,
                             sendMessages: PermValue.Allow,
                             attachFiles: PermValue.Allow,
                             embedLinks: PermValue.Allow,
                             useExternalEmojis: PermValue.Allow,
                             addReactions: PermValue.Allow,
                             mentionEveryone: PermValue.Inherit,
                             manageMessages: PermValue.Deny,
                             sendTTSMessages: PermValue.Inherit,
                             managePermissions: PermValue.Deny,
                             createInstantInvite: PermValue.Deny,
                             manageChannel: PermValue.Deny,
                             manageWebhooks: PermValue.Deny
                             );
            }
          

            if (arg2.VoiceChannel != arg3.VoiceChannel)
            {
                

                if (arg2.VoiceChannel != null)
                {
                    SocketVoiceState State = arg2;
                    //Remove permission
                    foreach (var Server in _client.Guilds)
                    {
                        if (Server.Users.Contains(arg1))
                        {


                            foreach (var channel in Server.TextChannels)
                            {
                                if (channel.Name == "v_" + State.VoiceChannel.Name.ToLower().Replace(" ", "_").Replace(":", "").Replace(@"/", "_").Replace(".", "_"))
                                {
                                    if (channel.Guild == State.VoiceChannel.Guild)
                                    {
                                        if (VoiceON != null)
                                        {



                                            bool VoiceISON = false;
                                            foreach (var item in VoiceON)
                                            {
                                                if (item == channel.Guild.Id.ToString())
                                                {
                                                    VoiceISON = true;
                                                }
                                            }
                                            if (VoiceISON)
                                            {
                                                channel.SendMessageAsync(arg1.Username + " left the channel!");
                                            }
                                        }

                                    }
                                    channel.RemovePermissionOverwriteAsync(arg1, Discord.RequestOptions.Default);

                                }
                                if (channel.Name == "voice_only" && arg3.VoiceChannel == null)
                                {
                                    if (VoiceON != null)
                                    {



                                        bool VoiceISON = false;
                                        foreach (var item in VoiceON)
                                        {
                                            if (item == channel.Guild.Id.ToString())
                                            {
                                                VoiceISON = true;
                                            }
                                        }
                                        if (VoiceISON)
                                        {
                                            channel.SendMessageAsync(arg1.Username + " left the channel from " + arg2.VoiceChannel.Name + "!");
                                        }
                                    }
                                    channel.RemovePermissionOverwriteAsync(arg1, Discord.RequestOptions.Default);

                                }
                            }
                        }
                    }
                }
                if (arg3.VoiceChannel != null)
                {
                    //Give Permission
                    SocketVoiceState State = arg3;
                    foreach (var Server in _client.Guilds)
                    {
                        if (Server.Users.Contains(arg1))
                        {
                            foreach (var channel in Server.TextChannels)
                            {
                                if (channel.Name == "v_" + State.VoiceChannel.Name.ToLower().Replace(" ", "_").Replace(":", "").Replace(@"/", "_").Replace(".", "_"))
                                {
                                    if (channel.Guild == State.VoiceChannel.Guild)
                                    {
                                        if (VoiceON != null)
                                        {


                                            bool VoiceISON = false;
                                            foreach (var item in VoiceON)
                                            {
                                                if (item == channel.Guild.Id.ToString())
                                                {
                                                    VoiceISON = true;
                                                }
                                            }
                                            if (VoiceISON)
                                            {
                                                channel.SendMessageAsync(arg1.Username + " joined the channel!");

                                            }

                                        }
                                        channel.AddPermissionOverwriteAsync(arg1, AllowPerm, Discord.RequestOptions.Default);

                                    }
                                }
                                if (channel.Name == "voice_only" && arg2.VoiceChannel == null)
                                {
                                    if (VoiceON != null)
                                    {


                                        bool VoiceISON = false;
                                        foreach (var item in VoiceON)
                                        {
                                            if (item == channel.Guild.Id.ToString())
                                            {
                                                VoiceISON = true;
                                            }
                                        }
                                        if (VoiceISON)
                                        {
                                            channel.SendMessageAsync(arg1.Username + " joined the channel from " + arg3.VoiceChannel.Name + "!");

                                        }

                                    }
                                    channel.AddPermissionOverwriteAsync(arg1, AllowPerm, Discord.RequestOptions.Default);

                                }
                            }
                        }
                    }
                }


            }
            return null;
        }

        public async Task ConfigureAsync()
        {
            await _commands.AddModulesAsync(Assembly.GetEntryAssembly());
        }

        private async Task ProcessCommandAsync(SocketMessage pMsg)
        {
            var message = pMsg as SocketUserMessage;
            if (message == null) return;
            if (message.Content.StartsWith("##")) return;

            int argPos = 0;
            if (!ParseTriggers(message, ref argPos)) return;

            var context = new SocketCommandContext(_client, message);
            var result = await _commands.ExecuteAsync(context, argPos, _provider);
            /*
            if (result is SearchResult search && !search.IsSuccess)
            {
                await message.AddReactionAsync(EmojiExtensions.FromText(":mag_right:"));
            }
            else if (result is PreconditionResult precondition && !precondition.IsSuccess)
                await message.AddReactionAsync(EmojiExtensions.FromText(":no_entry:"));
            else if (result is ParseResult parse && !parse.IsSuccess)
                await message.Channel.SendMessageAsync($"**Parse Error:** {parse.ErrorReason}");
            else if (result is TypeReaderResult reader && !reader.IsSuccess)
                await message.Channel.SendMessageAsync($"**Read Error:** {reader.ErrorReason}");
            else if (!result.IsSuccess)

                await message.AddReactionAsync(EmojiExtensions.FromText(":rage:"));
                */
            _logger.Debug("Invoked {Command} in {Context} with {Result}", message, context.Channel, result);
        }

        private bool ParseTriggers(SocketUserMessage message, ref int argPos)
        {
            bool flag = false;
            if (message.HasMentionPrefix(_client.CurrentUser, ref argPos)) flag = true;
            else
            {
                foreach (var prefix in _config.CommandStrings)
                {
                    if (message.HasStringPrefix(prefix, ref argPos))
                    {
                        flag = true;
                        break;
                    }
                }
            }
            return flag;
        }
    }
}
