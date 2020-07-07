using KeyforgeUnlocked.Cards;
using KeyforgeUnlocked.Exceptions;
using KeyforgeUnlocked.States;

namespace KeyforgeUnlocked.Actions
{
  public class PlayCreature : Action
  {
    CreatureCard Card { get; }
    int Position { get; }

    public PlayCreature(State state,
      CreatureCard card,
      int position) : base(state)
    {
      Card = card;
      Position = position;
    }

    public override State DoAction()
    {
      ValidateNoUnresolvedEffects();
      var mutableState = State.ToMutable();
      mutableState.Effects.Enqueue(
        new Effects.PlayCreature(
          State.PlayerTurn,
          Card,
          Position));
      return mutableState;
    }

    void ValidateNoUnresolvedEffects()
    {
      if (State.Effects.Count != 0)
      {
        throw new UnresolvedEffectsException(State);
      }
    }
  }
}