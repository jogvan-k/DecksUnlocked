using KeyforgeUnlocked.Cards;
using KeyforgeUnlocked.ResolvedEffects;
using KeyforgeUnlocked.States;

namespace KeyforgeUnlocked.Effects
{
  public sealed class PlayActionCard : PlayCard<PlayActionCard>
  {

    public PlayActionCard(IActionCard card) : base(card)
    {
    }

    protected override void ResolveImpl(MutableState state)
    {
      state.ResolvedEffects.Add(new ActionPlayed(Card));
      ResolvePlayEffects(state);
      state.Discards[state.playerTurn].Add(Card);
    }
  }
}