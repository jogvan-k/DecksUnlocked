using System.Collections.Immutable;
using KeyforgeUnlocked.Actions;
using KeyforgeUnlocked.Cards.ActionCards;
using KeyforgeUnlocked.States;

namespace KeyforgeUnlocked.ActionGroups
{
  public sealed class PlayActionCardGroup : PlayCardGroup<PlayActionCardGroup>
  {
    public new ActionCard Card => (ActionCard) base.Card;

    public PlayActionCardGroup(ActionCard card) : base(card)
    {
    }

    protected override IImmutableList<IAction> InitiateActions(ImmutableState origin)
    {
      return ImmutableList<IAction>.Empty.AddRange(
        new[]
        {
          (IAction) new PlayActionCard(origin, Card), new DiscardCard(origin, Card)
        });
    }
  }
}