using KeyforgeUnlocked.Cards.Attributes;
using KeyforgeUnlocked.States.Extensions;
using KeyforgeUnlocked.Types;
using KeyforgeUnlocked.Types.Events;

namespace KeyforgeUnlocked.Cards.Brobnar.Actions
{
    [CardInfo("Loot the Bodies", Rarity.Common,
        "Play: For the remainder of the turn, gain 1 æmber each time an enemy creature is destroyed.",
        "\"Loot the Bodies! Hit the Floor! Loot the Bodies! Hit the Floor!\" -Brobnar War Chant")]
    [ExpansionSet(Expansion.CotA, 10)]
    public sealed class LootTheBodies : ActionCard
    {
        static readonly Callback PlayAbility =
            (s, t, _) => s.Events.SubscribeUntilEndOfTurn(t, EventType.CreatureDestroyed,
                (s, _, p) =>
                {
                    if (p.Equals(s.PlayerTurn.Other()))
                        s.GainAember();
                });

        public LootTheBodies() : this(House.Brobnar)
        {
        }

        public LootTheBodies(House house) : base(house, playAbility: PlayAbility)
        {
        }
    }
}