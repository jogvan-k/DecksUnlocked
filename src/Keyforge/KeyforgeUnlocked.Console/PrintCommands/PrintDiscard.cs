using System;
using KeyforgeUnlocked.States;
using KeyforgeUnlockedConsole.PrintCommands;

namespace KeyforgeUnlockedConsole
{
    public class PrintDiscard : IPrintCommand
    {
        public void Print(IState state)
        {
            Console.WriteLine("Cars in discard:");
            foreach (var card in state.Discards[state.PlayerTurn])
            {
                Console.WriteLine(card.Name);
            }
        }
    }
}