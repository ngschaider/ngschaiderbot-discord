using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
using DSharpPlus.Entities;

namespace ngschaiderBot
{
    public class Commands
    {

        [Command("ping")]
        public async Task Ping(CommandContext ctx)
        {
            await ctx.TriggerTypingAsync();

            var emoji = DiscordEmoji.FromName(ctx.Client, ":ping_pong:");
            await ctx.RespondAsync($"{emoji} Pong! Ping: {ctx.Client.Ping}ms");
        }

        [Command("servus")]
        public async Task Servus(CommandContext ctx)
        {
            await ctx.TriggerTypingAsync();

            await ctx.RespondAsync($"👋 Griaß di {ctx.User.Mention}!");
        }

        [Command("random")]
        public async Task Random(CommandContext ctx, int min, int max)
        {
            await ctx.TriggerTypingAsync();

            Random random = new Random();
            await ctx.RespondAsync($"🎲 Deine Zufallszahl ist: {random.Next(min, max+1)}");
        }

        [Command("roll")]
        public async Task Roll(CommandContext ctx, int num)
        {
            await ctx.TriggerTypingAsync();

            Random random = new Random();

            string output = "";
            for(int i = 0; i < num; i++) {
                output += random.Next(1, 7) + " ";
            }

            await ctx.RespondAsync($"🎲 Du hast folgende Zahlen gewürfelt: {output}");
        }

        [Command("coinflip")]
        public async Task CoinFlip(CommandContext ctx)
        {
            await ctx.TriggerTypingAsync();

            Random random = new Random();
            int random_number = random.Next(0, 2);
            string output = random_number == 0 ? "Kopf" : "Zahl";
            await ctx.RespondAsync($"{ctx.User.Mention} {output}");
        }

        [Command("urban")]
        public async Task Urban(CommandContext ctx, string word)
        {
            await ctx.TriggerTypingAsync();

            UrbanDictionary.SearchResult searchResult = UrbanDictionary.Search(word);
            var emoji = DiscordEmoji.FromName(ctx.Client, ":book:");
            if (searchResult.list.Count == 0)
            {
                await ctx.RespondAsync($"{emoji} Wort '**{word}**' wurde **nicht** gefunden!");
            }
            else
            {
                await ctx.RespondAsync($"{emoji} Definition für '**{searchResult.list[0].word}**': **{searchResult.list[0].definition}**");
            }
        }


        [Command("pepe")]
        [Aliases("feelsbadman")]
        [Description("Feels bad, man.")]
        public async Task Pepe(CommandContext ctx)
        {
            await ctx.TriggerTypingAsync();

            var embed = new DiscordEmbedBuilder
            {
                ImageUrl = "http://i.imgur.com/44SoSqS.jpg"
            };

            await ctx.RespondAsync(embed: embed);
        }

        [Command("randomcolor")]
        [Aliases("randomcolour")]
        [Description("Erzeugt eine zufällige Farbe und gibt sie als RGB, Hex und Vorschau aus.")]
        public async Task RandomColor(CommandContext ctx)
        {
            await ctx.TriggerTypingAsync();

            Random random = new Random();
            float r = random.Next(0, 256);
            float g = random.Next(0, 256);
            float b = random.Next(0, 256);
            string hex = Utils.RGBtoHex((int)r, (int)g, (int)b);
            var embed = new DiscordEmbedBuilder
            {
                Color = new DiscordColor(hex),
                Description = "Hex: "+hex+" | RGB: " + Math.Round(r) + ", " + Math.Round(g) + ", " + Math.Round(b)
            };

            await ctx.RespondAsync(embed: embed);
        }

        [Command("scheresteinpapier")]
        [Aliases("ssp")]
        [Description("Generates a random color and displays it as RGB, HEX and as a preview.")]
        public async Task ssp(CommandContext ctx, string input)
        {
            await ctx.TriggerTypingAsync();

            SchereSteinPapier.Choice playerChoice = SchereSteinPapier.GetChoiceFromString(input);
            if(playerChoice == SchereSteinPapier.Choice.Undefined)
            {
                await ctx.RespondAsync(":exclamation: Du kannst nur 'Schere', 'Stein' und 'Papier' auswählen!");
            }
            else
            {
                SchereSteinPapier.Choice botChoice = SchereSteinPapier.GetRandomChoice();
                SchereSteinPapier.WinType winType = SchereSteinPapier.GetWinner(playerChoice, botChoice);

                string choicesString = ctx.User.Mention + " hat " + playerChoice.ToString() + " gewählt | " + Program.discord.CurrentUser.Mention + " hat " + botChoice.ToString() + " gewählt";
                if(winType == SchereSteinPapier.WinType.Player)
                {
                    await ctx.RespondAsync(choicesString + "\n" + ctx.User.Mention + " hat gewonnen!");
                }
                if(winType == SchereSteinPapier.WinType.Bot)
                {
                    await ctx.RespondAsync(choicesString + "\n" + Program.discord.CurrentUser.Mention + " hat gewonnen!");
                }
                if (winType == SchereSteinPapier.WinType.Tie)
                {
                    await ctx.RespondAsync(choicesString + "\n Niemand hat gewonnen!");
                }
            }
            
        }

        [Command("serverinfo")]
        public async Task ServerInfo(CommandContext ctx)
        {
            var embed = new DiscordEmbedBuilder
            {
                Title = ctx.Guild.Name
            };

            int humanMembers = 0;
            int botMembers = 0;
            int onlineMembers = 0;
            int channelCount = 0;
            string roleList = "";

            foreach(DiscordChannel channel in ctx.Guild.Channels) {
                if (!channel.IsCategory) {
                    channelCount++;
                }
            }

            foreach(DiscordMember member in ctx.Guild.Members)
            {
                if (member.IsBot)
                {
                    botMembers++;
                }
                else
                {
                    humanMembers++;
                }

                if(member.Presence.Status.ToString() == "Online") {
                    onlineMembers++;
                }
                
            }

            for(int i = 0; i < ctx.Guild.Roles.Count; i++)
            {
                if(i != 0)
                {
                    roleList += ", ";
                }
                roleList += ctx.Guild.Roles[i].Name;
            }

            embed.AddField("ID", ctx.Guild.Id+"", true);
            embed.AddField("Name", ctx.Guild.Name, true);
            embed.AddField("Owner", ctx.Guild.Owner.Username+"#"+ctx.Guild.Owner.Discriminator, true);
            embed.AddField("Region", ctx.Guild.RegionId, true);
            embed.AddField("Channels", channelCount+"", true);
            embed.AddField("Members", ctx.Guild.Members.Count+"", true);
            embed.AddField("Humans", humanMembers+"", true);
            embed.AddField("Bots", botMembers+"", true);
            embed.AddField("Online", onlineMembers+"", true);
            embed.AddField("Roles", ctx.Guild.Roles.Count+"", true);
            embed.AddField("Role List", roleList);

            await ctx.RespondAsync(embed: embed);
        }

        [Command("test")]
        public async Task Test(CommandContext ctx, int amount)
        {
            await ctx.TriggerTypingAsync();

            IReadOnlyList<DiscordMessage> list = await ctx.Channel.GetMessagesAsync(amount, ctx.Channel.LastMessageId);

            //var message = list[0];
            //var message = await ctx.Channel.GetMessageAsync(ctx.Channel.LastMessageId);

            //IReadOnlyCollection<DiscordEmbedField> fields = message.Embeds[0].Fields;
            foreach (var msg in list)
            {
                await msg.DeleteAsync();
            }
            await ctx.RespondAsync("success");
        }

        [Command("debug")]
        public async Task Debug(CommandContext ctx)
        {
            await ctx.TriggerTypingAsync();

            IReadOnlyList<DiscordMessage> list = await ctx.Channel.GetMessagesAsync(1, ctx.Channel.LastMessageId);

            var message = list[0];

            await ctx.RespondAsync(Utils.RGBtoHex(message.Embeds[0].Color.R, message.Embeds[0].Color.G, message.Embeds[0].Color.B));
            
        }



    }
}
