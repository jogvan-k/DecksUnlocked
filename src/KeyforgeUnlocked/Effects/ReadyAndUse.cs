using System;
using KeyforgeUnlocked.ActionGroups;
using KeyforgeUnlocked.Creatures;
using KeyforgeUnlocked.Exceptions;
using KeyforgeUnlocked.ResolvedEffects;
using KeyforgeUnlocked.States;
using KeyforgeUnlocked.States.Extensions;

namespace KeyforgeUnlocked.Effects
{
  public sealed class ReadyAndUse : EffectWithCreature<ReadyAndUse>
  {
    readonly bool allowOutOfHouseUse;

    public ReadyAndUse(Creature creature, bool allowOutOfHouseUse) : base(creature)
    {
      this.allowOutOfHouseUse = allowOutOfHouseUse;
    }

    protected override void ResolveImpl(MutableState state)
    {
      var target = state.FindCreature(Creature.Id, out var controllingPlayer, out _);
      if (state.playerTurn != controllingPlayer)
        throw new InvalidTargetException(state, Creature.Id);
      if (!target.IsReady)
      {
        target.IsReady = true;
        state.UpdateCreature(target);
        state.ResolvedEffects.Add(new CreatureReadied(target));
      }

      state.ActionGroups.Add(new UseCreatureGroup(state, target, allowOutOfHouseUse));
    }

    protected override bool Equals(ReadyAndUse other)
    {
      return base.Equals(other) && allowOutOfHouseUse.Equals(other.allowOutOfHouseUse);
    }

    public override int GetHashCode()
    {
      return HashCode.Combine(base.GetHashCode(), allowOutOfHouseUse);
    }
  }
}