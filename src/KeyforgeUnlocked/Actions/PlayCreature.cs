using KeyforgeUnlocked.Cards;
using KeyforgeUnlocked.Exceptions;
using KeyforgeUnlocked.States;

namespace KeyforgeUnlocked.Actions
{
  public class PlayCreature : Action
  {
    public CreatureCard Card { get; }
    public int Position { get; }

    public PlayCreature(
      CreatureCard card,
      int position)
    {
      Card = card;
      Position = position;
    }

    public override IState DoAction(IState state)
    {
      ValidateNoUnresolvedEffects(state);
      var mutableState = state.ToMutable();
      mutableState.Effects.Enqueue(
        new Effects.PlayCreature(
          state.PlayerTurn,
          Card,
          Position));
      return mutableState.ResolveEffects();
    }

    void ValidateNoUnresolvedEffects(IState state)
    {
      if (state.Effects.Count != 0)
      {
        throw new UnresolvedEffectsException(state);
      }
    }
  }
}