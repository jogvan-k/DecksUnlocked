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
      return ImmutableList<IAction>.Empty.AddRange(
        new[]
        {
          (IAction) new PlayActionCard(origin, Card), new DiscardCard(origin, Card)
        });
    }
  }
}