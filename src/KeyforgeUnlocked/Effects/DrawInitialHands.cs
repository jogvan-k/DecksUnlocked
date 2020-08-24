using KeyforgeUnlocked.States;
using UnlockedCore;

namespace KeyforgeUnlocked.Effects
{
  public sealed class DrawInitialHands : IEffect
  {
    public void Resolve(MutableState state)
    {
      for (var i = 0; i < Constants.FirstPlayerStartHand; i++)
        state.Draw(Player.Player1);
      for (var i = 0; i < Constants.SecondPlayerStartHand; i++)
        state.Draw(Player.Player2);
    }

    bool Equals(DrawInitialHands other)
    {
      return true;
    }

    public override bool Equals(object obj)
    {
      return ReferenceEquals(this, obj) || obj is DrawInitialHands other && Equals(other);
    }

    public override int GetHashCode()
    {
      return typeof(DrawInitialHands).GetHashCode();
    }
  }
}