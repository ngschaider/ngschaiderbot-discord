using System;
using System.Collections.Generic;
using System.Text;

namespace ngschaiderBot
{
    public class SchereSteinPapier
    {

        public enum Choice
        {
            Schere,
            Stein,
            Papier,
            Undefined
        }

        public enum WinType
        {
            Player,
            Bot,
            Tie,
            Undefined
        }

        public static Choice GetRandomChoice()
        {
            Random random = new Random();
            int rand = random.Next(1, 4);
            if(rand == 1)
            {
                return Choice.Schere;
            }
            else if (rand == 2)
            {
                return Choice.Stein;
            }
            else if(rand == 3)
            {
                return Choice.Papier;
            }
            else
            {
                return Choice.Undefined;
            }
        }

        public static Choice GetChoiceFromString(string input)
        {
            input = input.ToLower();
            if (input == "schere") return Choice.Schere;
            if (input == "stein") return Choice.Stein;
            if (input == "papier") return Choice.Papier;
            return Choice.Undefined;
        }

        public static WinType GetWinner(Choice player, Choice bot)
        {
            if(player == bot)
            {
                return WinType.Tie;
            }

            if(player == Choice.Schere)
            {
                if(bot == Choice.Stein)
                {
                    return WinType.Bot;
                }
                else if(bot == Choice.Papier)
                {
                    return WinType.Player;
                }
            }
            else if(player == Choice.Stein)
            {
                if(bot == Choice.Schere)
                {
                    return WinType.Player;
                }
                else if (bot == Choice.Papier)
                {
                    return WinType.Bot;
                }
            }
            else if(player == Choice.Papier)
            {
                if(bot == Choice.Schere)
                {
                    return WinType.Bot;
                }
                else if(bot == Choice.Stein)
                {
                    return WinType.Player;
                }
            }

            return WinType.Undefined;
        }

    }
}
