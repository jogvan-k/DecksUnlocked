using KeyforgeUnlocked.States;
using KeyforgeUnlocked.States.Extensions;
using UnlockedCore;

namespace KeyforgeUnlocked.Effects
{
  public sealed class DrawInitialHands : EffectBase<DrawInitialHands>
  {
    protected override void ResolveImpl(MutableState state)
    {
      for (var i = 0; i < Constants.FirstPlayerStartHand; i++)
        state.Draw(Player.Player1);
      for (var i = 0; i < Constants.SecondPlayerStartHand; i++)
        state.Draw(Player.Player2);
    }
  }
}