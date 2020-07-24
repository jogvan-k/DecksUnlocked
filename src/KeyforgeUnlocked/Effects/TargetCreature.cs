using System.Linq;
using KeyforgeUnlocked.ActionGroups;
using KeyforgeUnlocked.States;
using KeyforgeUnlocked.Types;
using UnlockedCore.States;

namespace KeyforgeUnlocked.Effects
{
  public sealed class TargetCreature : IEffect
  {
    EffectOnCreature _effect;
    ValidOn _validOn;

    public TargetCreature(EffectOnCreature effect, ValidOn validOn)
    {
      _effect = effect;
      _validOn = validOn;
    }

    public void Resolve(MutableState state)
    {
      var validTargets = state.Fields[state.PlayerTurn.Other()].Concat(state.Fields[state.PlayerTurn])
        .Where(c => _validOn(state, c)).ToList();

      if (validTargets.Count > 1)
        state.ActionGroups.Add(new TargetCreatureGroup(_effect, validTargets));
      else if (validTargets.Count == 1)
        _effect(state, validTargets.Single());
    }
  }
}