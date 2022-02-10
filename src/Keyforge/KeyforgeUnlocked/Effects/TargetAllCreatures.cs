using System.Linq;
using KeyforgeUnlocked.States;
using KeyforgeUnlocked.Types;

namespace KeyforgeUnlocked.Effects
{
    public sealed class TargetAllCreatures : EffectBase<TargetAllCreatures>
    {
        Callback effect;
        ValidOn _validOn;

        public TargetAllCreatures(Callback effect, ValidOn validOn)
        {
            this.effect = effect;
            this._validOn = validOn;
        }

        protected override void ResolveImpl(IMutableState state)
        {
            foreach (var t in state.Fields.SelectMany(f => f.Value.Select(creature => (creature, f.Key)))
                         .Where(c => _validOn(state, c.creature)).ToList())
            {
                effect(state, t.creature, t.Key);
            }
        }

        protected override bool Equals(TargetAllCreatures other)
        {
            return effect.Equals(other.effect) && _validOn.Equals(other._validOn);
        }
    }
}