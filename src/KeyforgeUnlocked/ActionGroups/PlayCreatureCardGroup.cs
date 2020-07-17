using System;
using System.Collections.Immutable;
using KeyforgeUnlocked.Actions;
using KeyforgeUnlocked.Cards;
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

    protected override IImmutableSet<Action> InitiateActions()
    {
      var list = ImmutableHashSet<Action>.Empty;

      var leftPosition = new PlayCreature(Card, 0);
      list = list.Add(leftPosition);

      if (BoardLength > 0)
      {
        list = list.Add(new PlayCreature(Card, BoardLength));
      }

      return list.Add(DiscardAction());
    }
  }
}