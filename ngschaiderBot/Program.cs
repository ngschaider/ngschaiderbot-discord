using System;
using System.Threading.Tasks;
using DSharpPlus;
using DSharpPlus.CommandsNext;

namespace ngschaiderBot
{
    class Program
    {

        public static DiscordClient discord;
        public static CommandsNextModule commands;

        static void Main(string[] args)
        {
            MainAsync(args).ConfigureAwait(false).GetAwaiter().GetResult();
        }

        static async Task MainAsync(string[] args)
        {
            discord = new DiscordClient(new DiscordConfiguration
            {
                Token = "NDE0NzkyOTk0ODc3NzM0OTEy.Xteifg.hyWFPVK8_QjYI3kwVK2ZT4flqcs",
                TokenType = TokenType.Bot,
                UseInternalLogHandler = true,
                LogLevel = LogLevel.Debug
            });
            commands = discord.UseCommandsNext(new CommandsNextConfiguration
            {
                StringPrefix = "!"
            });
            commands.RegisterCommands<Commands>();
            

            discord.MessageCreated += async e =>
            {
                /*if (e.Message.Content.ToLower().StartsWith("ping"))
                {
                    await e.Message.RespondAsync("Pong!");
                }*/
            };

            await discord.ConnectAsync();
            await Task.Delay(-1);
        }

    }
}
