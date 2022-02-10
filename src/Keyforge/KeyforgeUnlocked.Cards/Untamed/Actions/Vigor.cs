using KeyforgeUnlocked.Cards.Attributes;
using KeyforgeUnlocked.Effects.Choices;
using KeyforgeUnlocked.States.Extensions;
using KeyforgeUnlocked.Types;

namespace KeyforgeUnlocked.Cards.Untamed.Actions
{
    [CardInfo("Vigor", Rarity.Common,
        "Play: Heal up to 3 damage from a creature. If you healed 3 damage, gain 1 æmber.")]
    [ExpansionSet(Expansion.CotA, 338)]
    public sealed class Vigor : ActionCard
    {
        static readonly Pip[] Pips = { Pip.Aember };

        static readonly Callback PlayAbility =
            (s, _, _) => s.AddEffect(new TargetSingleCreature(
                (s, t, _) =>
                {
                    var healedAmount = s.HealCreature(t, 3);
                    if (healedAmount == 3) s.GainAember();
                }));

        public Vigor() : this(House.Untamed)
        {
        }

        public Vigor(House house) : base(house, pips: Pips, PlayAbility)
        {
        }
    }
}