using KeyforgeUnlocked.Cards.Attributes;
using KeyforgeUnlocked.States.Extensions;
using KeyforgeUnlocked.Types;
using KeyforgeUnlocked.Types.Events;

namespace KeyforgeUnlocked.Cards.Logos.Actions
{
  [CardInfo("Library Access", Rarity.Common,
    "Play: for the remainder of the turn, each time you play another card, draw a card.\nPurge Library Access.")]
  [ExpansionSet(Expansion.CotA, 115)]
  public sealed class LibraryAccess : ActionCard
  {
    static readonly Callback PlayAbility =
      (s, i, p) =>
      {
        s.Events.SubscribeUntilEndOfTurn(
          i,
          EventType.CardPlayed,
          (s, _, p) => { s.Draw(p); });
        s.PurgeCard(p, (ICard) i);
      };

    public LibraryAccess() : this(House.Logos)
    {
    }

    public LibraryAccess(House house) : base(house, playAbility: PlayAbility)
    {
    }
  }
}