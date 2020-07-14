using KeyforgeUnlocked.Actions;
using KeyforgeUnlocked.Cards;

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

    protected Action DiscardAction()
    {
      return new DiscardCard(Card);
    }
  }
}