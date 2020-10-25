using System.Linq;
using KeyforgeUnlocked.States;
using KeyforgeUnlocked.Types;

namespace KeyforgeUnlocked.Effects
{
  public sealed class TargetAllCreatures : EffectBase<TargetAllCreatures>
  {
    EffectOnCreature effect;
    ValidOn validOn;

    public TargetAllCreatures(EffectOnCreature effect, ValidOn validOn)
    {
      this.effect = effect;
      this.validOn = validOn;
    }

    protected override void ResolveImpl(MutableState state)
    {
      foreach (var creature in state.Fields.SelectMany(f => f.Value).Where(c => validOn(state, c)).ToList())
      {
        effect(state, creature);
      }
    }

    protected override bool Equals(TargetAllCreatures other)
    {
      return effect.Equals(other.effect) && validOn.Equals(other.validOn);
    }
  }
}