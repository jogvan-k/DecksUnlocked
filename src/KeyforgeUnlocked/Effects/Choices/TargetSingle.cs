using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using KeyforgeUnlocked.ActionGroups;
using KeyforgeUnlocked.States;
using KeyforgeUnlocked.Types;
using UnlockedCore;

namespace KeyforgeUnlocked.Effects.Choices
{
  public abstract class TargetSingle : EffectBase<TargetSingle>
  {
    readonly EffectOnTarget _effect;
    readonly ValidOn _validOn;
    readonly Targets _targets;

    public TargetSingle(EffectOnTarget effect, Targets targets = Targets.All, ValidOn validOn = null)
    {
      _effect = effect;
      _validOn = validOn ?? Delegates.All;
      _targets = targets;
    }

    protected override void ResolveImpl(MutableState state)
    {
      var validTargets = ValidTargets(state);

      if (validTargets.Count > 1)
        state.ActionGroups.Add(new SingleTargetGroup(_effect, validTargets.ToImmutableList()));
      else if (validTargets.Count == 1)
        _effect(state, validTargets.Single());
    }
    
    protected List<IIdentifiable> ValidTargets(MutableState state)
    {
      return PreFilter(state)
        .Where(c => _validOn(state, c))
        .ToList();
    }

    IEnumerable<IIdentifiable> PreFilter(IState state)
    {
      switch (_targets)
      {
        case Targets.Own:
          return OrderedUnfilteredTargets(state)[state.PlayerTurn];
        case Targets.Opponens:
          return OrderedUnfilteredTargets(state)[state.PlayerTurn.Other()];
        case Targets.All:
          var targets = OrderedUnfilteredTargets(state);
          return targets[state.PlayerTurn.Other()].Concat(targets[state.PlayerTurn]);
        default:
          throw new Exception($"{_targets} not implemented.");
      }
    }

    IReadOnlyDictionary<Player, IOrderedEnumerable<IIdentifiable>> OrderedUnfilteredTargets(IState state)
    {
      return UnfilteredTargets(state).ToReadOnly(kv => kv.Value.OrderBy(i => i.Id));
    }
    protected abstract IReadOnlyDictionary<Player,IEnumerable<IIdentifiable>> UnfilteredTargets(IState state);

    protected override bool Equals(TargetSingle other)
    {
      return _effect.Equals(other._effect) && _validOn.Equals(other._validOn) && _targets.Equals(other._targets);
    }

    public override int GetHashCode()
    {
      return HashCode.Combine(_effect, _validOn, _targets);
    }
  }
}