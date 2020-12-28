using System;
using KeyforgeUnlocked.ActionGroups;
using KeyforgeUnlocked.Cards;
using KeyforgeUnlocked.States;

namespace KeyforgeUnlocked.Effects
{
  public class FirstTurn : EffectBase<FirstTurn>
  {
    protected override void ResolveImpl(MutableState state)
    {
      foreach (var card in state.Hands[state.PlayerTurn])
        if (card.House == state.ActiveHouse)
          state.ActionGroups.Add(ActionGroup(state, card));

      state.ActionGroups.Add(new NoActionGroup());
    }

    IActionGroup ActionGroup(IState state,
      ICard card)
    {
      switch (card)
      {
        case ICreatureCard creatureCard:
          return new PlayCreatureCardGroup(state, creatureCard);
        case IActionCard actionCard:
          return new PlayActionCardGroup(actionCard);
        case IArtifactCard artifactCard:
          return new PlayArtifactCardGroup(artifactCard);
        default:
          throw new NotImplementedException();
      }
    }
  }
}