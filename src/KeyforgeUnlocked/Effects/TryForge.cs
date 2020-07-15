using KeyforgeUnlocked.ResolvedEffects;
using KeyforgeUnlocked.States;
using static KeyforgeUnlocked.Constants;

namespace KeyforgeUnlocked.Effects
{
  public class TryForge : IEffect
  {
    public void Resolve(MutableState state)
    {
      var playerTurn = state.PlayerTurn;
      if (state.Aember[playerTurn] >= DefaultForgeCost)
      {
        state.Keys[playerTurn]++;
        state.Aember[playerTurn] -= DefaultForgeCost;
        state.ResolvedEffects.Add(new KeyForged(DefaultForgeCost));
        if (state.Keys[playerTurn] >= KeysRequiredToWin)
          state.IsGameOver = true;
      }
    }

    protected bool Equals(TryForge other)
    {
      return true;
    }

    public override bool Equals(object obj)
    {
      if (ReferenceEquals(null, obj)) return false;
      if (ReferenceEquals(this, obj)) return true;
      if (obj.GetType() != this.GetType()) return false;
      return Equals((TryForge) obj);
    }

    public override int GetHashCode()
    {
      return 1;
    }
  }
}