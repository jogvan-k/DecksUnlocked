using System;
using KeyforgeUnlocked.Actions;
using KeyforgeUnlocked.Cards;
using KeyforgeUnlocked.States;
using Action = KeyforgeUnlocked.Actions.Action;

namespace KeyforgeUnlocked.ActionGroups
{
  public abstract class PlayCardGroup : ActionGroupBase
  {
    public Card Card { get; }

    protected PlayCardGroup(Card card)
    {
      Card = card;
    }

    protected Action DiscardAction(ImmutableState origin)
    {
      return new DiscardCard(origin, Card);
    }

    public override int GetHashCode()
    {
      return HashCode.Combine(base.GetHashCode(), Card);
    }
  }
}