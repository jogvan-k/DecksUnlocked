using KeyforgeUnlocked.ResolvedEffects;
using KeyforgeUnlocked.States;
using KeyforgeUnlocked.Types;

namespace KeyforgeUnlocked.Effects
{
  public sealed class ReadyCardsAndRestoreArmor : EffectBase<ReadyCardsAndRestoreArmor>
  {
    protected override void ResolveImpl(MutableState state)
    {
      var field = state.Fields[state.PlayerTurn];
      for (int i = 0; i < field.Count; i++)
      {
        var creature = field[i];
        if (!creature.IsReady)
        {
          creature.IsReady = true;
          creature.BrokenArmor = 0;
          field[i] = creature;
          state.ResolvedEffects.Add(new CreatureReadied(creature));
        }
      }

      field = state.Fields[state.PlayerTurn.Other()];
      for (int i = 0; i < field.Count; i++)
      {
        var creature = field[i];
        creature.BrokenArmor = 0;
        field[i] = creature;
      }
    }
  }
}