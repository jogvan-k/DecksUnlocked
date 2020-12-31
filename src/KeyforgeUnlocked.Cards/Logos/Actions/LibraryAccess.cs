using KeyforgeUnlocked.States.Extensions;
using KeyforgeUnlocked.Types;
using KeyforgeUnlocked.Types.Events;

namespace KeyforgeUnlocked.Cards.Logos.Actions
{
  public sealed class LibraryAccess : ActionCard
  {
    static readonly Callback PlayAbility =
      (s, i, p) =>
      {
        s.Events.SubscribeUntilLeavesPlay(
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