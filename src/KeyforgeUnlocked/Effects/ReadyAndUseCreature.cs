using System;
using KeyforgeUnlocked.ActionGroups;
using KeyforgeUnlocked.Creatures;
using KeyforgeUnlocked.Exceptions;
using KeyforgeUnlocked.ResolvedEffects;
using KeyforgeUnlocked.States;
using KeyforgeUnlocked.States.Extensions;
using KeyforgeUnlocked.Types;

namespace KeyforgeUnlocked.Effects
{
  public sealed class ReadyAndUseCreature : EffectWithIdentifiable<ReadyAndUseCreature>
  {
    readonly bool allowOutOfHouseUse;

    public ReadyAndUseCreature(IIdentifiable creature, bool allowOutOfHouseUse) : base(creature)
    {
      this.allowOutOfHouseUse = allowOutOfHouseUse;
    }

    protected override void ResolveImpl(MutableState state)
    {
      var creature = state.FindCreature(Id, out var controllingPlayer, out _);
      if (state.playerTurn != controllingPlayer)
        throw new InvalidTargetException(state, Id);
      if (!creature.IsReady)
      {
        creature.IsReady = true;
        state.UpdateCreature(creature);
        state.ResolvedEffects.Add(new CreatureReadied(creature));
      }

      state.ActionGroups.Add(new UseCreatureGroup(state, creature, allowOutOfHouseUse));
    }

    protected override bool Equals(ReadyAndUseCreature other)
    {
      return base.Equals(other) && allowOutOfHouseUse.Equals(other.allowOutOfHouseUse);
    }

    public override int GetHashCode()
    {
      return HashCode.Combine(base.GetHashCode(), allowOutOfHouseUse);
    }
  }
}