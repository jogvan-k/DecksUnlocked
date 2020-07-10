using System;
using System.Collections.Immutable;
using KeyforgeUnlocked.Actions;
using KeyforgeUnlocked.Cards;
using KeyforgeUnlocked.States;
using Action = KeyforgeUnlocked.Actions.Action;

namespace KeyforgeUnlocked.ActionGroup
{
  public class PlayCreatureCard : PlayCard
  {
    public new CreatureCard Card => (CreatureCard) base.Card;

    public PlayCreatureCard(
      IState state,
      CreatureCard card) : base(state, card)
    {
      if (state == null || card == null)
        throw new ArgumentNullException();
    }

    protected override ImmutableList<Action> InitiateActions(IState state)
    {
      var list = ImmutableList<Action>.Empty;

      var boardLength = state.Fields[state.PlayerTurn].Count;

      var leftPosition = new PlayCreature(Card, 0);
      list = list.Add(leftPosition);

      if (boardLength > 0)
      {
        return list.Add(new PlayCreature(Card, boardLength));
      }

      return list;
    }
  }
}