using KeyforgeUnlocked.Actions;
using KeyforgeUnlocked.Cards;
using KeyforgeUnlocked.States;

namespace KeyforgeUnlocked.ActionGroup
{
  public abstract class PlayCard : ActionGroupBase
  {
    public Card Card { get; }

    protected PlayCard(
      IState state,
      Card card) : base(ActionType.PlayCard, state)
    {
      Card = card;
    }
  }
}