using KeyforgeUnlocked.Cards;

namespace KeyforgeUnlockedTest.Util
{
  public sealed class SampleCreatureCard : CreatureCard
  {
    public SampleCreatureCard() : base(
      "SampleCreatureCard", KeyforgeUnlocked.Cards.House.Undefined, 10,
      0)
    {
    }
  }
}