using System.Linq;
using KeyforgeUnlocked.States;
using KeyforgeUnlocked.Types;

namespace KeyforgeUnlocked.Effects
{
  public sealed class TargetAllCreatures : IEffect
  {
    EffectOnCreature _effect;
    ValidOn _validOn;

    public TargetAllCreatures(EffectOnCreature effect, ValidOn validOn)
    {
      _effect = effect;
      _validOn = validOn;
    }

    public void Resolve(MutableState state)
    {
      foreach (var creature in state.Fields.SelectMany(f => f.Value).Where(c => _validOn(state, c)).ToList())
      {
        _effect(state, creature);
      }
    }
  }
}