using KeyforgeUnlocked.Cards;

namespace KeyforgeUnlockedTest.Util
{
  public sealed class SampleCreatureCard : CreatureCard
  {
    public SampleCreatureCard(int power = 1,
      int armor = 0,
      House house = House.Undefined) : base(
      "SampleCreatureCard", house, power,
      armor)
    {
    }
  }
}