using System;
using KeyforgeUnlocked.ActionGroups;
using KeyforgeUnlocked.Cards;
using KeyforgeUnlocked.Cards.ActionCards;
using KeyforgeUnlocked.Cards.CreatureCards;
using KeyforgeUnlocked.States;

namespace KeyforgeUnlocked.Effects
{
  public class FirstTurn : IEffect
  {
    public void Resolve(MutableState state)
    {
      foreach (var card in state.Hands[state.PlayerTurn])
        if (card.House == state.ActiveHouse)
          state.ActionGroups.Add(ActionGroup(state, card));

      state.ActionGroups.Add(new NoActionGroup());
    }

    IActionGroup ActionGroup(IState state,
      Card card)
    {
      switch (card)
      {
        case CreatureCard creatureCard:
          return new PlayCreatureCardGroup(state, creatureCard);
        case ActionCard actionCard:
          return new PlayActionCardGroup(actionCard);
        default:
          throw new NotImplementedException();
      }
    }

    protected bool Equals(FirstTurn other)
    {
      return true;
    }

    public override bool Equals(object obj)
    {
      if (ReferenceEquals(null, obj)) return false;
      if (ReferenceEquals(this, obj)) return true;
      if (obj.GetType() != this.GetType()) return false;
      return Equals((FirstTurn) obj);
    }

    public override int GetHashCode()
    {
      return typeof(FirstTurn).GetHashCode();
    }
  }
}