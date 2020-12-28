using System.Collections.Immutable;
using KeyforgeUnlocked.Actions;
using KeyforgeUnlocked.Cards;
using KeyforgeUnlocked.States;

namespace KeyforgeUnlocked.ActionGroups
{
  public sealed class PlayArtifactCardGroup : PlayCardGroup<PlayArtifactCardGroup>
  {
    public new IArtifactCard Card => (IArtifactCard) base.Card;
    public PlayArtifactCardGroup(IArtifactCard card) : base(card)
    {
    }

    protected override IImmutableList<IAction> InitiateActions(ImmutableState origin)
    {
      return ImmutableList<IAction>.Empty.AddRange(
        new[]
        {
          (IAction) new PlayArtifactCard(origin, Card), new DiscardCard(origin, Card)
        });
    }
  }
}