using System;
using KeyforgeUnlocked.States;
using KeyforgeUnlockedConsole.PrintCommands;

namespace KeyforgeUnlockedConsole
{
    public class PrintArchive : IPrintCommand
    {
        public void Print(IState state)
        {
            Console.WriteLine("Cards in archive:");
            foreach (var card in state.Archives[state.PlayerTurn])
            {
                Console.WriteLine(card.Name);
            }
        }
    }
}