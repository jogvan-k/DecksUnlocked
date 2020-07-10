using KeyforgeUnlocked.Actions;
using KeyforgeUnlocked.Cards;
using KeyforgeUnlocked.States;

namespace KeyforgeUnlocked.ActionGroup
{
  public abstract class PlayCard : ActionGroupBase
  {
    public Card Card { get; }

    protected PlayCard(
      Card card) : base(ActionType.PlayCard)
    {
      Card = card;
    }
  }
}