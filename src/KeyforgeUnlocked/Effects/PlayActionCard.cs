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

    protected override void ResolveImpl(IMutableState state)
    {
      state.ResolvedEffects.Add(new ActionCardPlayed(Card));
      ResolvePlayEffects(state);
      state.Discards[state.PlayerTurn].Add(Card);
    }
  }
}