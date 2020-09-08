using System.Linq;
using KeyforgeUnlocked.ActionGroups;
using KeyforgeUnlocked.Creatures;
using KeyforgeUnlocked.States;
using KeyforgeUnlocked.Types;

namespace KeyforgeUnlocked.Effects
{
  public sealed class TargetSingleCreature : IEffect
  {
    EffectOnCreature _effect;
    ValidOn _validOn;

    public TargetSingleCreature(EffectOnCreature effect, ValidOn validOn)
    {
      _effect = effect;
      _validOn = validOn;
    }

    public void Resolve(MutableState state)
    {
      var validTargets = state.Fields[state.PlayerTurn.Other()].Concat(state.Fields[state.PlayerTurn])
        .Where(c => _validOn(state, c)).ToList();

      if (validTargets.Count > 1)
        state.ActionGroups.Add(new TargetCreatureGroup(_effect, new LazyList<Creature>(validTargets)));
      else if (validTargets.Count == 1)
        _effect(state, validTargets.Single());
    }
  }
}