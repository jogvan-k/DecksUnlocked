using KeyforgeUnlocked.Cards;

namespace KeyforgeUnlocked.ResolvedEffects
{
  public sealed class ActionPlayed : ResolvedEffectWithCard<ActionPlayed>
  {
    public ActionPlayed(ICard card) : base(card)
    {
    }

    public override string ToString()
    {
      return $"{_card.Name} played";
    }
  }
}