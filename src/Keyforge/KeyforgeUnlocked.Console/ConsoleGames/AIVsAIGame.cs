using KeyforgeUnlocked.States;
using UnlockedCore;

namespace KeyforgeUnlockedConsole.ConsoleGames
{
    public class AIVsAIGame : BaseConsoleGame
    {
        IGameAI _gameAi;

        public AIVsAIGame(IState state, IGameAI gameAi) : base(state)
        {
            _gameAi = gameAi;
        }

        protected override void AdvanceState()
        {
            AdvanceStateOnAITurn(_state.PlayerTurn, _gameAi);
        }
    }
}