using KeyforgeUnlocked.Cards;
using KeyforgeUnlocked.Exceptions;
using KeyforgeUnlocked.ResolvedEffects;
using KeyforgeUnlocked.States;

namespace KeyforgeUnlocked.Effects
{
  public sealed class DiscardCard : EffectWithCard<DiscardCard>
  {

    public DiscardCard(ICard card) : base(card)
    {
    }

    protected override void ResolveImpl(MutableState state)
    {
      if (!state.Hands[state.PlayerTurn].Remove(Card))
        throw new CardNotPresentException(state, Card.Id);
      state.Discards[state.PlayerTurn].Add(Card);
      state.ResolvedEffects.Add(new CardDiscarded(Card));
    }
  }
}