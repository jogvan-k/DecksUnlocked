using System.Linq;
using KeyforgeUnlocked.ResolvedEffects;
using KeyforgeUnlocked.States;
using UnlockedCore.States;

namespace KeyforgeUnlocked.Effects
{
  public sealed class ReadyCardsAndRestoreArmor : IEffect
  {
    public void Resolve(MutableState state)
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

    bool Equals(ReadyCardsAndRestoreArmor other)
    {
      return true;
    }

    public override bool Equals(object obj)
    {
      return ReferenceEquals(this, obj) || obj is ReadyCardsAndRestoreArmor other && Equals(other);
    }

    public override int GetHashCode()
    {
      return typeof(ReadyCardsAndRestoreArmor).GetHashCode();
    }
  }
}