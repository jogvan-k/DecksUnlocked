using System.Collections.Immutable;
using KeyforgeUnlocked.Actions;
using KeyforgeUnlocked.Cards.ActionCards;

namespace KeyforgeUnlocked.ActionGroups
{
  public sealed class PlayActionCardGroup : PlayCardGroup
  {
    public new ActionCard Card => (ActionCard) base.Card;

    public PlayActionCardGroup(ActionCard card) : base(card)
    {
    }

    protected override IImmutableList<Action> InitiateActions()
    {
      return ImmutableList<Action>.Empty.AddRange(
        new[]
        {
          (Action) new PlayActionCard(Card), new DiscardCard(Card)
        });
    }
  }
}