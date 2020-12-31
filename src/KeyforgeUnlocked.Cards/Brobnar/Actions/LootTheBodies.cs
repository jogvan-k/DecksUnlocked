using KeyforgeUnlocked.States.Extensions;
using KeyforgeUnlocked.Types;
using KeyforgeUnlocked.Types.Events;

namespace KeyforgeUnlocked.Cards.Brobnar.Actions
{
  [CardName("Loot the Bodies")]
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