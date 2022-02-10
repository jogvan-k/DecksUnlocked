using KeyforgeUnlocked.States;
using KeyforgeUnlocked.States.Extensions;
using UnlockedCore;

namespace KeyforgeUnlocked.Effects
{
    public sealed class DrawInitialHands : EffectBase<DrawInitialHands>
    {
        protected override void ResolveImpl(IMutableState state)
        {
            state.Draw(Player.Player1, Constants.FirstPlayerStartHand);
            state.Draw(Player.Player2, Constants.SecondPlayerStartHand);
        }
    }
}