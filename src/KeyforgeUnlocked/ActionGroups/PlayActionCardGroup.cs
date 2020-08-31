using System.Collections.Immutable;
using KeyforgeUnlocked.Actions;
using KeyforgeUnlocked.Cards.ActionCards;
using KeyforgeUnlocked.States;

namespace KeyforgeUnlocked.ActionGroups
{
  public sealed class PlayActionCardGroup : PlayCardGroup
  {
    public new ActionCard Card => (ActionCard) base.Card;

    public PlayActionCardGroup(ActionCard card) : base(card)
    {
    }

    protected override IImmutableList<Action> InitiateActions(ImmutableState origin)
    {
      return ImmutableList<Action>.Empty.AddRange(
        new[]
        {
          (Action) new PlayActionCard(origin, Card), new DiscardCard(origin, Card)
        });
    }
  }
}