using KeyforgeUnlocked.Cards.Attributes;
using KeyforgeUnlocked.Effects.Choices;
using KeyforgeUnlocked.States.Extensions;
using KeyforgeUnlocked.Types;

namespace KeyforgeUnlocked.Cards.Logos.Actions
{
    [CardInfo("Twin Bolt Emission", Rarity.Common,
        "Play: Deal 2 damage to a creature and deal 2 damage to a different creature.")]
    [ExpansionSet(Expansion.CotA, 124)]
    [ExpansionSet(Expansion.WC, 142)]
    public sealed class TwinBoltEmission : ActionCard
    {
        static readonly Pip[] Pips = { Pip.Aember };

        static readonly Callback PlayAbility =
            (s, _, _) => s.AddEffect(
                new TargetSingleCreature(
                    (s, t, _) =>
                    {
                        s.DamageCreature(t, 2);
                        s.AddEffect(new TargetSingleCreature((s2, t2, _) => s2.DamageCreature(t2, 2),
                            validOn: Delegates.AllExcept(t)));
                    }, Target.All, Delegates.All));

        public TwinBoltEmission() : this(House.Logos)
        {
        }

        public TwinBoltEmission(House house) : base(house, pips: Pips, PlayAbility)
        {
        }
    }
}