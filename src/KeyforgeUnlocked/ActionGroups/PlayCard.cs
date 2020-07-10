using KeyforgeUnlocked.Actions;
using KeyforgeUnlocked.Cards;

namespace KeyforgeUnlocked.ActionGroups
{
  public abstract class PlayCard : ActionGroupBase
  {
    public Card Card { get; }

    protected PlayCard(
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