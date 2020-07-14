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

    public PlayCreatureCardGroup(
      IState state,
      CreatureCard card) : base(card)
    {
      if (state == null || card == null)
        throw new ArgumentNullException();
      Actions = InitiateActions(state);
    }

    ImmutableList<Action> InitiateActions(IState state)
    {
      var list = ImmutableList<Action>.Empty;

      var boardLength = state.Fields[state.PlayerTurn].Count;

      var leftPosition = new PlayCreature(Card, 0);
      list = list.Add(leftPosition);

      if (boardLength > 0)
      {
        list = list.Add(new PlayCreature(Card, boardLength));
      }

      return list.Add(DiscardAction());
    }
  }
}