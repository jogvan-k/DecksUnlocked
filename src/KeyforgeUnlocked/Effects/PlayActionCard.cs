using KeyforgeUnlocked.Cards;
using KeyforgeUnlocked.ResolvedEffects;
using KeyforgeUnlocked.States;

namespace KeyforgeUnlocked.Effects
{
  public sealed class PlayActionCard : EffectWithCard<PlayActionCard>
  {

    public PlayActionCard(ActionCard card) : base(card)
    {
    }

    protected override void ResolveImpl(MutableState state)
    {
      state.ResolvedEffects.Add(new ActionPlayed(Card));
      Card.CardPlayAbility?.Invoke(state, Card.Id);
      state.Discards[state.playerTurn].Add(Card);
    }
  }
}