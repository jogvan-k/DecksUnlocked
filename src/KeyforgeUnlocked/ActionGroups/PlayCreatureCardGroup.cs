using System;
using System.Collections.Immutable;
using KeyforgeUnlocked.Actions;
using KeyforgeUnlocked.Cards.CreatureCards;
using KeyforgeUnlocked.States;
using Action = KeyforgeUnlocked.Actions.Action;

namespace KeyforgeUnlocked.ActionGroups
{
  public sealed class PlayCreatureCardGroup : PlayCardGroup
  {
    public new CreatureCard Card => (CreatureCard) base.Card;

    public int BoardLength { get; }

    public PlayCreatureCardGroup(
      IState state,
      CreatureCard card) : base(card)
    {
      BoardLength = state.Fields[state.PlayerTurn].Count;
      if (state == null || card == null)
        throw new ArgumentNullException();
    }

    protected override IImmutableList<Action> InitiateActions(ImmutableState origin)
    {
      var list = ImmutableList<Action>.Empty;

      var leftPosition = new PlayCreatureCard(origin, Card, 0);
      list = list.Add(leftPosition);

      if (BoardLength > 0)
      {
        list = list.Add(new PlayCreatureCard(origin, Card, BoardLength));
      }

      return list.Add(DiscardAction(origin));
    }
  }
}