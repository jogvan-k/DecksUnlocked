using KeyforgeUnlocked.States;

namespace KeyforgeUnlockedConsole.ConsoleGames
{
    public class TwoPlayerGame : BaseConsoleGame
    {
        public TwoPlayerGame(IState initialState) : base(initialState)
        {
        }

        protected override void AdvanceState()
        {
            AdvanceStateOnPlayerTurn();
        }
    }
}