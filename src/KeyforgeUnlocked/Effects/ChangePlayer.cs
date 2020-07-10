using KeyforgeUnlocked.States;
using UnlockedCore.States;

namespace KeyforgeUnlocked.Effects
{
  public sealed class ChangePlayer : IEffect
  {
    public void Resolve(MutableState state)
    {
      state.PlayerTurn = state.PlayerTurn.Other();
      state.TurnNumber++;
    }

    public override bool Equals(object obj)
    {
      if (ReferenceEquals(null, obj)) return false;
      if (ReferenceEquals(this, obj)) return true;
      if (obj.GetType() != this.GetType()) return false;
      return Equals((ChangePlayer) obj);
    }

    bool Equals(ChangePlayer other)
    {
      return true;
    }

    public override int GetHashCode()
    {
      return 1;
    }
  }
}