using System.Linq;
using KeyforgeUnlocked.States;
using KeyforgeUnlocked.Types;

namespace KeyforgeUnlocked.Effects
{
  public sealed class TargetAllCreatures : EffectBase<TargetAllCreatures>
  {
    EffectOnTarget effect;
    ValidOn _validOn;

    public TargetAllCreatures(EffectOnTarget effect, ValidOn validOn)
    {
      this.effect = effect;
      this._validOn = validOn;
    }

    protected override void ResolveImpl(MutableState state)
    {
      foreach (var creature in state.Fields.SelectMany(f => f.Value).Where(c => _validOn(state, c)).ToList())
      {
        effect(state, creature);
      }
    }

    protected override bool Equals(TargetAllCreatures other)
    {
      return effect.Equals(other.effect) && _validOn.Equals(other._validOn);
    }
  }
}