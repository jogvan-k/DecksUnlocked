using System.Linq;
using KeyforgeUnlocked.ActionGroups;
using KeyforgeUnlocked.Creatures;
using KeyforgeUnlocked.States;
using KeyforgeUnlocked.Types;

namespace KeyforgeUnlocked.Effects
{
  public sealed class TargetSingleCreature : EffectBase<TargetSingleCreature>
  {
    EffectOnCreature effect;
    ValidOn validOn;

    public TargetSingleCreature(EffectOnCreature effect, ValidOn validOn)
    {
      this.effect = effect;
      this.validOn = validOn;
    }

    protected override void ResolveImpl(MutableState state)
    {
      var validTargets = state.Fields[state.PlayerTurn.Other()].Concat(state.Fields[state.PlayerTurn])
        .Where(c => validOn(state, c)).ToList();

      if (validTargets.Count > 1)
        state.ActionGroups.Add(new TargetCreatureGroup(effect, new LazyList<Creature>(validTargets)));
      else if (validTargets.Count == 1)
        effect(state, validTargets.Single());
    }

    protected override bool Equals(TargetSingleCreature other)
    {
      return effect.Equals(other.effect) && validOn.Equals(other.validOn);
    }

    public override int GetHashCode()
    {
      var hash = base.GetHashCode();
      hash = hash * Constants.PrimeHashBase + effect.GetHashCode();
      hash = hash * Constants.PrimeHashBase + validOn.GetHashCode();

      return hash;
    }
  }
}