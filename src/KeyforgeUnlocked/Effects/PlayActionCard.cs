using KeyforgeUnlocked.Cards;
using KeyforgeUnlocked.ResolvedEffects;
using KeyforgeUnlocked.States;

namespace KeyforgeUnlocked.Effects
{
  public sealed class PlayActionCard : EffectBase<PlayActionCard>
  {
    public readonly ActionCard Card;

    public PlayActionCard(ActionCard card)
    {
      Card = card;
    }

    protected override void ResolveImpl(MutableState state)
    {
      state.ResolvedEffects.Add(new ActionPlayed(Card));
      Card.PlayAbility?.Invoke(state, Card.Id);
      state.Discards[state.playerTurn].Add(Card);
    }
  }
}