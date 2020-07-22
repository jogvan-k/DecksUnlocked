using System;
using KeyforgeUnlocked.Cards;
using KeyforgeUnlocked.Types;

namespace KeyforgeUnlockedTest.Util
{
  public sealed class SampleCreatureCard : CreatureCard
  {
    public SampleCreatureCard(int power = 1,
      int armor = 0,
      House house = House.Undefined,
      Keyword[] keywords = null,
      Delegates.Callback fightAbility = null)
      : base(
        "SampleCreatureCard",
        house,
        power,
        armor,
        keywords ?? Array.Empty<Keyword>(),
        fightAbility)
    {
    }
  }
}