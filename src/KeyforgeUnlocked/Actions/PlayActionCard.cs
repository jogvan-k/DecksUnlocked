using KeyforgeUnlocked.Cards;
using KeyforgeUnlocked.Exceptions;
using KeyforgeUnlocked.States;

namespace KeyforgeUnlocked.Actions
{
  public sealed class PlayActionCard : BasicActionWithCard<PlayActionCard>
  {

    public PlayActionCard(ImmutableState origin, IActionCard card) : base(origin, card)
    {
    }

    protected override void DoSpecificActionNoResolve(IMutableState state)
    {
      if (!state.Hands[state.PlayerTurn].Remove(Card))
        throw new CardNotPresentException(state, Card);

      state.Effects.Push(new Effects.PlayActionCard((IActionCard) Card));
    }
  }
}