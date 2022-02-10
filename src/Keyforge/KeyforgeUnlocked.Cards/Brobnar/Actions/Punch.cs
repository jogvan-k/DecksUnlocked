using KeyforgeUnlocked.Cards.Attributes;
using KeyforgeUnlocked.Effects.Choices;
using KeyforgeUnlocked.States.Extensions;
using KeyforgeUnlocked.Types;

namespace KeyforgeUnlocked.Cards.Brobnar.Actions
{
    [CardInfo("Punch", Rarity.Common,
        "Play: Deal 3 damage to a creature.",
        "Three for flinching.")]
    [ExpansionSet(Expansion.CotA, 12)]
    public sealed class Punch : ActionCard
    {
        static readonly Pip[] Pips = { Pip.Aember };

        static readonly Callback PlayAbility =
            (s, _, _) => s.AddEffect(new TargetSingleCreature((s, t, _) => s.DamageCreature(t, 3)));

        public Punch() : this(House.Brobnar)
        {
        }

        public Punch(House house) : base(house, pips: Pips, PlayAbility)
        {
        }
    }
}