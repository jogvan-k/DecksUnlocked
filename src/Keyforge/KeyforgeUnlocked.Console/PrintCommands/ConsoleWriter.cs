using System;
using System.Collections.Generic;
using KeyforgeUnlocked.Cards;
using KeyforgeUnlocked.Types;

namespace KeyforgeUnlockedConsole.PrintCommands
{
    public class ConsoleWriter
    {
        IDictionary<string, ICard> _idToCard;

        public ConsoleWriter(IDictionary<string, ICard> idToCard)
        {
            _idToCard = idToCard;
        }

        public void WriteLine(House house)
        {
            WriteWithFormatting(house.ToString(), house, Console.WriteLine);
        }

        public void Write(ICard card)
        {
            WriteWithFormatting(card.Name, card.House, Console.Write);
        }

        public void WriteLine(ICard card)
        {
            WriteWithFormatting(card.Name, card.House, Console.WriteLine);
        }

        static void WriteWithFormatting(string text, House house, Action<string?> writeFunc)
        {
            Console.ForegroundColor = MapToColor(house);
            writeFunc(text);
            Console.ResetColor();
        }

        public void WriteLine(IIdentifiable id)
        {
            WriteLine(_idToCard[id.Id]);
        }

        static ConsoleColor MapToColor(House house)
        {
            switch (house)
            {
                case House.Brobnar:
                    return ConsoleColor.Red;
                case House.Logos:
                    return ConsoleColor.Blue;
                case House.Sanctum:
                    return ConsoleColor.Yellow;
                case House.Untamed:
                    return ConsoleColor.DarkGreen;
                case House.Shadows:
                    return ConsoleColor.DarkMagenta;
                default:
                    return ConsoleColor.Gray;
            }
        }
    }
}