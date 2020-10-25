using KeyforgeUnlocked.ActionGroups;
using KeyforgeUnlocked.Creatures;
using KeyforgeUnlocked.Exceptions;
using KeyforgeUnlocked.ResolvedEffects;
using KeyforgeUnlocked.States;

namespace KeyforgeUnlocked.Effects
{
  public sealed class ReadyAndUse : EffectBase<ReadyAndUse>
  {
    readonly Creature target;
    readonly bool allowOutOfHouseUse;

    public ReadyAndUse(Creature target, bool allowOutOfHouseUse)
    {
      this.target = target;
      this.allowOutOfHouseUse = allowOutOfHouseUse;
    }

    protected override void ResolveImpl(MutableState state)
    {
      var target = state.FindCreature(this.target.Id, out var controllingPlayer);
      if (state.playerTurn != controllingPlayer)
        throw new InvalidTargetException(state, this.target.Id);
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
      return target.Equals(other.target) && allowOutOfHouseUse.Equals(other.allowOutOfHouseUse);
    }

    public override int GetHashCode()
    {
      var hash = base.GetHashCode();
      hash = hash * Constants.PrimeHashBase + target.GetHashCode();
      hash = hash * Constants.PrimeHashBase + allowOutOfHouseUse.GetHashCode();

      return hash;
    }
  }
}