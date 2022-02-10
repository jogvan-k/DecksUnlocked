using KeyforgeUnlocked.States;

namespace KeyforgeUnlockedConsole.PrintCommands
{
    public interface IPrintCommand
    {
        void Print(IState state);
    }
}