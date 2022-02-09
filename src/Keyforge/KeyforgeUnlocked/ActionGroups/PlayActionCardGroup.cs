using System.Collections.Immutable;
using KeyforgeUnlocked.Actions;
using KeyforgeUnlocked.Cards;
using KeyforgeUnlocked.States;

namespace KeyforgeUnlocked.ActionGroups
{
  public sealed class PlayActionCardGroup : PlayCardGroup<PlayActionCardGroup>
  {
    public new IActionCard Card => (IActionCard) base.Card;

    public PlayActionCardGroup(IActionCard card) : base(card)
    {
    }

    protected override IImmutableList<IAction> InitiateActions(ImmutableState origin)
    {
      var playAction = new PlayActionCard(origin, Card);
      var discardAction = new DiscardCard(origin, Card);
      if (Card.CardPlayAllowed(origin, playAction))
      {
        return ImmutableList.Create<IAction>(playAction, discardAction);
      }

      return ImmutableList.Create<IAction>(discardAction);
    }

    public override string ToString()
    {
      return $"Play action card";
    }
  }
}