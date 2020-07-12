using KeyforgeUnlocked.States;
using UnlockedCore.States;

namespace KeyforgeUnlocked.Effects
{
  public sealed class EndTurn : IEffect
  {
    public void Resolve(MutableState state)
    {
      state.PlayerTurn = state.PlayerTurn.Other();
      state.TurnNumber++;
      state.ResolvedEffects.Add(new ResolvedEffects.TurnEnded());
    }

    public override bool Equals(object obj)
    {
      if (ReferenceEquals(null, obj)) return false;
      if (ReferenceEquals(this, obj)) return true;
      if (obj.GetType() != this.GetType()) return false;
      return Equals((EndTurn) obj);
    }

    bool Equals(EndTurn other)
    {
      return true;
    }

    public override int GetHashCode()
    {
      return 1;
    }
  }
}