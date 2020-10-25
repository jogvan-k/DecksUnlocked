using KeyforgeUnlocked.ResolvedEffects;
using KeyforgeUnlocked.States;
using static KeyforgeUnlocked.Constants;

namespace KeyforgeUnlocked.Effects
{
  public class TryForge : EffectBase<TryForge>
  {
    protected override void ResolveImpl(MutableState state)
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
  }
}