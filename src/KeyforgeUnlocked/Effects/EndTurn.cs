using KeyforgeUnlocked.ResolvedEffects;
using KeyforgeUnlocked.States;
using KeyforgeUnlocked.Types;

namespace KeyforgeUnlocked.Effects
{
  public sealed class EndTurn : EffectBase<EndTurn>
  {
    protected override void ResolveImpl(MutableState state)
    {
      state.PlayerTurn = state.PlayerTurn.Other();
      state.TurnNumber++;
      state.ActiveHouse = null;
      state.ResolvedEffects.Add(new TurnEnded());
      state.Effects.Enqueue(new CheckGameTurnLimit());
      state.Effects.Enqueue(new TryForge());
      state.Effects.Enqueue(new DeclareHouse());
    }
  }
}