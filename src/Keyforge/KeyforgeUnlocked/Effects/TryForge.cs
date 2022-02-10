using KeyforgeUnlocked.ResolvedEffects;
using KeyforgeUnlocked.States;
using KeyforgeUnlocked.States.Extensions;
using KeyforgeUnlocked.Types.Events;
using static KeyforgeUnlocked.Constants;

namespace KeyforgeUnlocked.Effects
{
    public class TryForge : EffectBase<TryForge>
    {
        protected override void ResolveImpl(IMutableState state)
        {
            var playerTurn = state.PlayerTurn;
            if (state.Aember[playerTurn] >= DefaultForgeCost)
            {
                state.Keys[playerTurn]++;
                state.Aember[playerTurn] -= DefaultForgeCost;
                state.ResolvedEffects.Add(new KeyForged(playerTurn, DefaultForgeCost));
                state.RaiseEvent(EventType.KeyForged, owningPlayer: playerTurn);
                if (state.Keys[playerTurn] >= KeysRequiredToWin)
                    state.IsGameOver = true;
            }
        }
    }
}