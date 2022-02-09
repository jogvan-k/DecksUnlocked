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
  public class TargetSingle : EffectBase<TargetSingle>
  {
    readonly Callback _effect;
    readonly TargetType _targetType;
    readonly ValidOn _validOn;
    readonly Target _target;

    public TargetSingle(Callback effect, TargetType targetType, Target target = Target.All, ValidOn? validOn = null)
    {
      _effect = effect;
      _validOn = validOn ?? Delegates.All;
      _target = target;
      _targetType = targetType;
    }

    protected override void ResolveImpl(IMutableState state)
    {
      var validTargets = ValidTargets(state);

      if (validTargets.Count > 1)
        state.ActionGroups.Add(new SingleTargetGroup(_effect, validTargets.ToImmutableList()));
      else if (validTargets.Count == 1)
      {
        var t = validTargets.Single();
        _effect(state, t.target, t.owningPlayer);
      }
    }

    protected List<(IIdentifiable target, Player owningPlayer)> ValidTargets(IMutableState state)
    {
      return PreFilter(state)
        .Where(c => _validOn(state, c.target))
        .ToList();
    }

    IEnumerable<(IIdentifiable target, Player owningPlayer)> PreFilter(IState state)
    {
      var player = state.PlayerTurn;

      var targets = Enumerable.Empty<(IIdentifiable, Player)>();

      if ((_target & Target.Opponens) > 0)
        targets = targets.Concat(OrderedTargets(state, player.Other()).Select(t => (t, player.Other())));

      if ((_target & Target.Own) > 0)
        targets = targets.Concat(OrderedTargets(state, player).Select(t => (t, player)));

      return targets;
    }

    IEnumerable<IIdentifiable> OrderedTargets(IState state, Player player)
    {
      var orderedTargets = Enumerable.Empty<IIdentifiable>();

      if ((_targetType & TargetType.Creature) > 0)
        orderedTargets = orderedTargets.Concat(state.Fields[player].Cast<IIdentifiable>());

      if ((_targetType & TargetType.Artifact) > 0)
        orderedTargets = orderedTargets.Concat(state.Artifacts[player].Cast<IIdentifiable>().OrderBy(i => i));

      if ((_targetType & TargetType.CardInHand) > 0)
        orderedTargets = orderedTargets.Concat(state.Hands[player].Cast<IIdentifiable>().OrderBy(i => i));

      if ((_targetType & TargetType.CardInDiscard) > 0)
        orderedTargets = orderedTargets.Concat(state.Discards[player].Cast<IIdentifiable>().OrderBy(i => i));

      return orderedTargets;
    }

    protected override bool Equals(TargetSingle other)
    {
      return _effect.Equals(other._effect)
             && _validOn.Equals(other._validOn)
             && _target.Equals(other._target)
             && _targetType.Equals(other._targetType);
    }

    public override int GetHashCode()
    {
      return HashCode.Combine(_effect, _validOn, _target, _targetType);
    }
  }
}