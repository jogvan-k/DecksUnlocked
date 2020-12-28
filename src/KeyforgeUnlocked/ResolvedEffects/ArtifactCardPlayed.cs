using KeyforgeUnlocked.Cards;

namespace KeyforgeUnlocked.ResolvedEffects
{
  public sealed class ArtifactCardPlayed : ResolvedEffectWithCard<ArtifactCardPlayed>
  {
    public ArtifactCardPlayed(ICard card) : base(card)
    {
    }

    public override string ToString()
    {
      return $"{_card.Name} played";
    }
  }
}