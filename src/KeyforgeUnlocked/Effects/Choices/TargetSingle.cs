using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using KeyforgeUnlocked.ActionGroups;
using KeyforgeUnlocked.States;
using KeyforgeUnlocked.Types;

namespace KeyforgeUnlocked.Effects.Choices
{
  public abstract class TargetSingle<T> : EffectBase<TargetSingle<T>> where T : IIdentifiable
  {
    protected readonly EffectOnTarget Effect;
    protected readonly ValidOn ValidOn;

    public TargetSingle(EffectOnTarget effect, ValidOn validOn)
    {
      Effect = effect;
      ValidOn = validOn;
    }

    protected override void ResolveImpl(MutableState state)
    {
      var validTargets = ValidTargets(state);

      if (validTargets.Count > 1)
        state.ActionGroups.Add(new SingleTargetGroup(Effect, validTargets.ToImmutableList()));
      else if (validTargets.Count == 1)
        Effect(state, validTargets.Single());
    }
    
    protected List<IIdentifiable> ValidTargets(MutableState state)
    {
      return UnfilteredTargets(state)
        .Where(c => ValidOn(state, c))
        .ToList();
    }
    protected abstract IEnumerable<IIdentifiable> UnfilteredTargets(IState state);

    protected override bool Equals(TargetSingle<T> other)
    {
      return Effect.Equals(other.Effect) && ValidOn.Equals(other.ValidOn);
    }

    public override int GetHashCode()
    {
      return HashCode.Combine(Effect, ValidOn);
    }
  }
}