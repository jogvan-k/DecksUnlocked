using KeyforgeUnlocked.Actions;
using KeyforgeUnlocked.Cards;
using KeyforgeUnlocked.States;

namespace KeyforgeUnlocked.ActionGroups
{
  public abstract class PlayCardGroup : ActionGroupBase
  {
    public Card Card { get; }

    protected PlayCardGroup(
      Card card) : base(ActionType.PlayCard)
    {
      Card = card;
    }

    protected Action DiscardAction(ImmutableState origin)
    {
      return new DiscardCard(origin, Card);
    }
  }
}