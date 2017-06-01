using Discord;
using Discord.Addons.EmojiTools;
using Discord.Addons.InteractiveCommands;
using Discord.Commands;
using System;
using System.Linq;
using System.Threading.Tasks;
using UtilityBot.Services.Tags;

namespace UtilityBot.Modules.Tags
{
    public class TagModule : ModuleBase<SocketCommandContext>
    {
        private readonly TagService _service;
        private readonly InteractiveService _interactive;

        public TagModule(TagService service, InteractiveService interactive)
        {
            _service = service;
            _interactive = interactive;
        }

       

    }
}
