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
      state.ActiveHouse = null;
      state.ResolvedEffects.Add(new ResolvedEffects.TurnEnded());
      state.Effects.Enqueue(new TryForge());
      state.Effects.Enqueue(new DeclareHouse());
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
      return typeof(EndTurn).GetHashCode();
    }
  }
}